export interface ImageUploadResponse {
  success: boolean;
  url?: string;
  key?: string;
  error?: string;
}

export interface PresignedPostResponse {
  success: boolean;
  uploadUrl?: string;
  fields?: Record<string, string>;
  finalUrl?: string;
  error?: string;
}

export interface DeleteImageResponse {
  success: boolean;
  error?: string;
}

export interface MultipleDeleteResponse {
  success: boolean;
  results: Array<{
    url: string;
    success: boolean;
    error?: string;
  }>;
}

export type ImageFolder = 'products' | 'categories' | 'users' | 'temp';
