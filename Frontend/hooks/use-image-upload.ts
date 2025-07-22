'use client';

import { useState } from 'react';
import type { IMAGE_CONFIG } from '@/lib/aws/s3-config';

interface UseImageUploadOptions {
  folder: keyof typeof IMAGE_CONFIG.folders;
  onSuccess?: (url: string) => void;
  onError?: (error: string) => void;
}

interface UploadState {
  uploading: boolean;
  progress: number;
  error: string | null;
  url: string | null;
}

export function useImageUpload({
  folder,
  onSuccess,
  onError,
}: UseImageUploadOptions) {
  const [state, setState] = useState<UploadState>({
    uploading: false,
    progress: 0,
    error: null,
    url: null,
  });

  const uploadImage = async (file: File): Promise<string | null> => {
    setState((prev) => ({
      ...prev,
      uploading: true,
      error: null,
      progress: 0,
    }));

    try {
      // Method 1: Direct upload via API route
      const formData = new FormData();
      formData.append('file', file);
      formData.append('folder', folder);

      const response = await fetch('/api/upload/direct', {
        method: 'POST',
        body: formData,
      });

      const result = await response.json();

      if (!result.success) {
        throw new Error(result.error || 'Upload failed');
      }

      setState((prev) => ({
        ...prev,
        uploading: false,
        url: result.url,
        progress: 100,
      }));
      onSuccess?.(result.url);
      return result.url;
    } catch (error: any) {
      const errorMessage = error.message || 'Upload failed';
      setState((prev) => ({
        ...prev,
        uploading: false,
        error: errorMessage,
        progress: 0,
      }));
      onError?.(errorMessage);
      return null;
    }
  };

  const uploadWithPresignedUrl = async (file: File): Promise<string | null> => {
    setState((prev) => ({
      ...prev,
      uploading: true,
      error: null,
      progress: 0,
    }));

    try {
      // Step 1: Get presigned URL
      const presignedResponse = await fetch('/api/upload', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          fileName: file.name,
          contentType: file.type,
          folder,
        }),
      });

      const presignedData = await presignedResponse.json();

      if (presignedData.error) {
        throw new Error(presignedData.error);
      }

      setState((prev) => ({ ...prev, progress: 25 }));

      // Step 2: Upload to S3 using presigned URL
      const formData = new FormData();
      Object.entries(presignedData.fields).forEach(([key, value]) => {
        formData.append(key, value as string);
      });
      formData.append('file', file);

      const uploadResponse = await fetch(presignedData.uploadUrl, {
        method: 'POST',
        body: formData,
      });

      if (!uploadResponse.ok) {
        throw new Error('Failed to upload to S3');
      }

      setState((prev) => ({
        ...prev,
        uploading: false,
        url: presignedData.finalUrl,
        progress: 100,
      }));

      onSuccess?.(presignedData.finalUrl);
      return presignedData.finalUrl;
    } catch (error: any) {
      const errorMessage = error.message || 'Upload failed';
      setState((prev) => ({
        ...prev,
        uploading: false,
        error: errorMessage,
        progress: 0,
      }));
      onError?.(errorMessage);
      return null;
    }
  };

  const deleteImage = async (imageUrl: string): Promise<boolean> => {
    try {
      const response = await fetch('/api/upload/delete', {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ imageUrl }),
      });

      const result = await response.json();
      return result.success;
    } catch (error) {
      console.error('Error deleting image:', error);
      return false;
    }
  };

  const reset = () => {
    setState({
      uploading: false,
      progress: 0,
      error: null,
      url: null,
    });
  };

  return {
    ...state,
    uploadImage,
    uploadWithPresignedUrl,
    deleteImage,
    reset,
  };
}
