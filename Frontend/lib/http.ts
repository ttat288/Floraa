'use server';

import type { RequestInit } from 'next/dist/server/web/spec-extension/request';
import { redirect } from 'next/navigation';

import { env } from '@/env.mjs';
import { getCookie, setCookie } from '@/helper/utils/tokenUtils';

interface TokenResponse {
  data: {
    accessToken: { value: string };
    refreshToken: { value: string };
  };
}

export async function fetchWithAuth<T>(
  url: string,
  options: RequestInit = {},
  baseUrl: string = env.NEXT_PUBLIC_API_ENDPOINT
): Promise<T> {
  const token = await getCookie('accessToken');
  const headers = {
    'Content-Type': 'application/json',
    ...(token ? { Authorization: `Bearer ${token}` } : {}),
  };

  const controller = new AbortController();
  const timeout = setTimeout(() => controller.abort(), 80000);

  try {
    const res = await fetch(`${baseUrl}${url}`, {
      ...options,
      headers: { ...headers, ...options.headers },
      credentials: 'include',
      signal: controller.signal,
    });

    clearTimeout(timeout);

    if (res.status === 401) {
      return await handleTokenRefresh<T>(url, options);
    }
    if (res.status === 204 || res.headers.get('content-length') === '0') {
      return null as T;
    }

    const responseData = await res.json();
    if (!res.ok) {
      throw responseData;
    }
    return responseData;
  } catch (error) {
    console.log(error);
    clearTimeout(timeout);
    throw error;
  }
}

async function handleTokenRefresh<T>(
  url: string,
  options: RequestInit
): Promise<T> {
  try {
    const refreshToken = await getCookie('refreshToken');
    if (!refreshToken) {
      throw new Error('No refresh token found in cookies.');
    }
    const headers = {
      'Content-Type': 'application/json',
      ...(refreshToken ? { Cookie: `refreshToken=${refreshToken}` } : {}),
    };
    const refreshTokenResponse = await fetch(
      `${env.NEXT_PUBLIC_API_ENDPOINT}/api/auth/refresh-token`,
      {
        ...options,
        method: 'POST',
        headers: { ...headers, ...options.headers },
        credentials: 'include',
      }
    );

    const tokenData: TokenResponse = await refreshTokenResponse.json();
    if (!refreshTokenResponse.ok) {
      throw new Error('Failed to refresh token');
    }

    const newAccessToken = tokenData.data.accessToken.value;
    const newRefreshToken = tokenData.data.refreshToken.value;

    setCookie('accessToken', newAccessToken, {
      httpOnly: false,
      secure: false,
      path: '/',
      sameSite: 'strict',
      expires: new Date(Date.now() + 15 * 60 * 1000), // 15 minutes.
    });

    setCookie('refreshToken', newRefreshToken, {
      httpOnly: false,
      secure: false,
      path: '/',
      sameSite: 'strict',
      expires: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000), // 7 days.
    });

    // Re-call the protected API now that tokens are updated.
    const finalData = await fetchWithAuth<T>(url, {
      ...options,
      credentials: 'include',
    });
    return finalData;
  } catch (error) {
    const refreshError = error as Error;
    if (
      refreshError.message === 'Failed to refresh token' ||
      refreshError.message === 'No refresh token found in cookies.'
    ) {
      redirect('/auth/login');
    }
    throw error;
  }
}
