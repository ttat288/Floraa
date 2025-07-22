import { HeadObjectCommand } from '@aws-sdk/client-s3';
import { s3Client, S3_BUCKET_NAME, S3_REGION, IMAGE_CONFIG } from './s3-config';
import { v4 as uuidv4 } from 'uuid';

export interface UploadResult {
  success: boolean;
  url?: string;
  key?: string;
  error?: string;
}

export interface DeleteResult {
  success: boolean;
  error?: string;
}

/**
 * Generate a unique file key for S3
 */
export function generateFileKey(
  folder: keyof typeof IMAGE_CONFIG.folders,
  originalName: string
): string {
  const timestamp = Date.now();
  const uuid = uuidv4().substring(0, 8);
  const extension = originalName.substring(originalName.lastIndexOf('.'));
  const sanitizedName = originalName
    .replace(/[^a-zA-Z0-9.-]/g, '_')
    .substring(0, 50);

  return `${IMAGE_CONFIG.folders[folder]}/${timestamp}-${uuid}-${sanitizedName}${extension}`;
}

/**
 * Get public URL from S3 key
 */
export function getPublicUrl(key: string): string {
  return `https://${S3_BUCKET_NAME}.s3.${S3_REGION}.amazonaws.com/${key}`;
}

/**
 * Extract S3 key from URL
 */
export function extractKeyFromUrl(url: string): string | null {
  try {
    const urlPattern = new RegExp(
      `https://${S3_BUCKET_NAME}\\.s3\\.${S3_REGION}\\.amazonaws\\.com/(.+)`
    );
    const match = url.match(urlPattern);
    return match ? match[1] : null;
  } catch (error) {
    console.error('Error extracting key from URL:', error);
    return null;
  }
}

/**
 * Validate image file
 */
export function validateImageFile(file: File): {
  valid: boolean;
  error?: string;
} {
  // Check file size
  if (file.size > IMAGE_CONFIG.maxSize) {
    return {
      valid: false,
      error: `File size exceeds ${
        IMAGE_CONFIG.maxSize / (1024 * 1024)
      }MB limit`,
    };
  }

  // Check file type with type assertion
  if (
    !IMAGE_CONFIG.allowedTypes.includes(
      file.type as (typeof IMAGE_CONFIG.allowedTypes)[number]
    )
  ) {
    return {
      valid: false,
      error: `File type ${
        file.type
      } is not allowed. Allowed types: ${IMAGE_CONFIG.allowedTypes.join(', ')}`,
    };
  }

  // Check file extension with type assertion
  const extension = file.name
    .toLowerCase()
    .substring(file.name.lastIndexOf('.'));
  if (
    !IMAGE_CONFIG.allowedExtensions.includes(
      extension as (typeof IMAGE_CONFIG.allowedExtensions)[number]
    )
  ) {
    return {
      valid: false,
      error: `File extension ${extension} is not allowed. Allowed extensions: ${IMAGE_CONFIG.allowedExtensions.join(
        ', '
      )}`,
    };
  }

  return { valid: true };
}

/**
 * Check if object exists in S3
 */
export async function checkObjectExists(key: string): Promise<boolean> {
  try {
    const command = new HeadObjectCommand({
      Bucket: S3_BUCKET_NAME,
      Key: key,
    });

    await s3Client.send(command);
    return true;
  } catch (error) {
    const s3Error = error as { name?: string };
    if (s3Error.name === 'NotFound') {
      return false;
    }
    throw error;
  }
}
