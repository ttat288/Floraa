import { NextResponse } from 'next/server';
import { env } from '@/env.mjs';
import { sign } from '@/lib/jwt';

export interface LoginResponse {
  success: boolean;
  message: string;
  data: {
    token: string;
    refreshToken: string;
    expiresAt: string;
    user: {
      id: string;
      email: string;
      name: string;
      phoneNumber: string;
      role: number;
    };
  };
  errors: string[];
  statusCode: number;
  timestamp: string;
}

export async function POST(request: Request): Promise<Response> {
  try {
    const reqData = await request.json();

    const netResponse = await fetch(
      `${env.NEXT_PUBLIC_API_ENDPOINT}/api/Auth/login`,
      {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(reqData),
        credentials: 'include',
      }
    );

    if (!netResponse.ok) {
      return NextResponse.json(
        {
          success: false,
          message: 'Tên đăng nhập hoặc mật khẩu không đúng',
          errors: ['Invalid credentials'],
          statusCode: netResponse.status,
          timestamp: new Date().toISOString(),
        },
        { status: netResponse.status }
      );
    }

    if (
      netResponse.status === 204 ||
      netResponse.headers.get('content-length') === '0'
    ) {
      return NextResponse.json(
        {
          success: false,
          message: 'Tên đăng nhập hoặc mật khẩu không đúng',
          errors: ['Empty response from server'],
          statusCode: 400,
          timestamp: new Date().toISOString(),
        },
        { status: 400 }
      );
    }

    let data: LoginResponse;
    try {
      data = await netResponse.json();
    } catch (error) {
      return NextResponse.json(
        {
          success: false,
          message: 'Lỗi xử lý phản hồi từ server',
          errors: ['Invalid JSON response'],
          statusCode: 500,
          timestamp: new Date().toISOString(),
        },
        { status: 500 }
      );
    }

    if (!data.success) {
      return NextResponse.json(
        {
          success: false,
          message: data.message || 'Đăng nhập thất bại',
          errors: data.errors || [],
          statusCode: data.statusCode || 400,
          timestamp: data.timestamp || new Date().toISOString(),
        },
        { status: data.statusCode || 400 }
      );
    }

    // Create JWT token for role management
    let roleToken: string | undefined;
    try {
      roleToken = await sign(
        { role: data.data.user.role.toString() },
        env.NEXT_PUBLIC_JWT_SECRET
      );
    } catch (error) {
      return NextResponse.json(
        {
          success: false,
          message: 'Lỗi tạo token xác thực',
          errors: ['Token creation failed'],
          statusCode: 500,
          timestamp: new Date().toISOString(),
        },
        { status: 500 }
      );
    }

    const response = NextResponse.json(data, { status: 200 });

    // Set cookies
    if (roleToken) {
      response.cookies.set('roleToken', roleToken, {
        httpOnly: true,
        secure: process.env.NODE_ENV === 'production',
        sameSite: 'strict',
        path: '/',
        maxAge: 3600, // 1 hour
      });
    }

    // Set access token
    response.cookies.set('accessToken', data.data.token, {
      httpOnly: false,
      secure: process.env.NODE_ENV === 'production',
      sameSite: 'strict',
      path: '/',
      maxAge: 3600, // 1 hour
    });

    // Set refresh token
    response.cookies.set('refreshToken', data.data.refreshToken, {
      httpOnly: false,
      secure: process.env.NODE_ENV === 'production',
      sameSite: 'strict',
      path: '/',
      maxAge: 7 * 24 * 60 * 60, // 7 days
    });

    return response;
  } catch (error) {
    return NextResponse.json(
      {
        success: false,
        message: 'Có lỗi xảy ra, vui lòng thử lại',
        errors: ['Internal server error'],
        statusCode: 500,
        timestamp: new Date().toISOString(),
      },
      { status: 500 }
    );
  }
}
