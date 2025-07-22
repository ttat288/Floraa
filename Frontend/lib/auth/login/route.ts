import { NextResponse } from 'next/server';

import { env } from '@/env.mjs';

import { sign } from '@/lib/jwt';

interface LoginResponse {
  isSuccess: boolean;
  data?: {
    role: string;
  };
  title?: string;
}

export async function POST(request: Request): Promise<Response> {
  try {
    const reqData = await request.json();
    const netResponse = await fetch(
      `${env.NEXT_PUBLIC_API_ENDPOINT}/api/auth/login`,
      {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(reqData),
        credentials: 'include',
      }
    );

    if (!netResponse.ok) {
      try {
        await netResponse.json();
      } catch {
        await netResponse.text();
      }

      throw new Error('Username or password is incorrect');
    }

    const setCookieHeader = netResponse.headers.get('set-cookie');

    if (
      netResponse.status === 204 ||
      netResponse.headers.get('content-length') === '0'
    ) {
      return NextResponse.json(
        {
          isSuccess: false,
          title: 'Username or password is incorrect',
          errors: [],
        },
        { status: 400 }
      );
    }

    let data: LoginResponse = { isSuccess: false };
    try {
      data = await netResponse.json();
    } catch {
      await netResponse.text();
      return NextResponse.json(
        {
          isSuccess: false,
          title: 'Username or password is incorrect',
          errors: [],
        },
        { status: 400 }
      );
    }

    let token: string | undefined;
    try {
      token = await sign(
        { role: data.data?.role || 'user' },
        env.NEXT_PUBLIC_JWT_SECRET
      );
    } catch {
      return NextResponse.json(
        {
          isSuccess: false,
          title: 'Authentication Error',
          errors: [],
        },
        { status: 500 }
      );
    }

    const response = NextResponse.json(data, { status: netResponse.status });

    if (token) {
      response.cookies.set('roleToken', token, {
        httpOnly: true,
        secure: process.env.NODE_ENV === 'production',
        sameSite: 'strict',
        path: '/',
        maxAge: 3600, // 1 hour
      });
    }

    if (setCookieHeader) {
      response.headers.append('Set-Cookie', setCookieHeader);
    }

    return response;
  } catch {
    return NextResponse.json(
      {
        isSuccess: false,
        title: 'Username or password is incorrect',
        errors: [],
      },
      { status: 500 }
    );
  }
}
