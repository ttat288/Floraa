import { RequestInit } from 'next/dist/server/web/spec-extension/request';

import { env } from '@/env.mjs';

import { fetchWithAuth } from '../http';

const DEFAULT_BASE_URL = env.NEXT_PUBLIC_API_ENDPOINT || '';

export const http = {
  get: <T>(
    url: string,
    options?: RequestInit,
    baseUrl: string = DEFAULT_BASE_URL
  ) => fetchWithAuth<T>(url, { method: 'GET', ...options }, baseUrl),

  post: <T>(
    url: string,
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    body?: any,
    options?: RequestInit,
    baseUrl: string = DEFAULT_BASE_URL
  ) =>
    fetchWithAuth<T>(
      url,
      { method: 'POST', body: JSON.stringify(body), ...options },
      baseUrl
    ),

  put: <T>(
    url: string,
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    body?: any,
    options?: RequestInit,
    baseUrl: string = DEFAULT_BASE_URL
  ) =>
    fetchWithAuth<T>(
      url,
      { method: 'PUT', body: JSON.stringify(body), ...options },
      baseUrl
    ),

  delete: <T>(
    url: string,
    options?: RequestInit,
    baseUrl: string = DEFAULT_BASE_URL
  ) => fetchWithAuth<T>(url, { method: 'DELETE', ...options }, baseUrl),
};

export enum HttpVerb {
  /**
   * The GET HTTP verb.
   */
  GET,
  /**
   * The HEAD HTTP verb.
   */
  HEAD,
  /**
   * The PUT HTTP verb.
   */
  PUT,
  /**
   * The DELETE HTTP verb.
   */
  DELETE,
}
