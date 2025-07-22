'use server';

import { PutObjectCommand, DeleteObjectCommand } from '@aws-sdk/client-s3';
import { createPresignedPost } from '@aws-sdk/s3-presigned-post';
import { s3Client, S3_BUCKET_NAME, IMAGE_CONFIG } from './s3-config';
import {
  generateFileKey,
  getPublicUrl,
  extractKeyFromUrl,
  validateImageFile,
  type UploadResult,
  type DeleteResult,
} from './s3-utils';

/**
 * Upload image buffer to S3 (Server-side upload)
 */
export async function uploadImageToS3(
  buffer: Buffer,
  fileName: string,
  folder: keyof typeof IMAGE_CONFIG.folders,
  contentType: string
): Promise<UploadResult> {
  try {
    // Validate content type with type assertion
    if (
      !IMAGE_CONFIG.allowedTypes.includes(
        contentType as (typeof IMAGE_CONFIG.allowedTypes)[number]
      )
    ) {
      return {
        success: false,
        error: `Content type ${contentType} is not allowed`,
      };
    }

    const key = generateFileKey(folder, fileName);

    const command = new PutObjectCommand({
      Bucket: S3_BUCKET_NAME,
      Key: key,
      Body: buffer,
      ContentType: contentType,
      ACL: 'public-read',
      CacheControl: 'max-age=31536000', // 1 year
    });

    await s3Client.send(command);

    const url = getPublicUrl(key);

    return {
      success: true,
      url,
      key,
    };
  } catch (error) {
    const errorMessage =
      error instanceof Error ? error.message : 'Failed to upload image';
    console.error('Error uploading to S3:', error);
    return {
      success: false,
      error: errorMessage,
    };
  }
}

/**
 * Upload image from File object (for form data)
 */
export async function uploadImageFile(
  file: File,
  folder: keyof typeof IMAGE_CONFIG.folders
): Promise<UploadResult> {
  try {
    // Validate file
    const validation = validateImageFile(file);
    if (!validation.valid) {
      return {
        success: false,
        error: validation.error,
      };
    }

    // Convert file to buffer
    const arrayBuffer = await file.arrayBuffer();
    const buffer = Buffer.from(arrayBuffer);

    return await uploadImageToS3(buffer, file.name, folder, file.type);
  } catch (error) {
    const errorMessage =
      error instanceof Error ? error.message : 'Failed to process file upload';
    console.error('Error processing file upload:', error);
    return {
      success: false,
      error: errorMessage,
    };
  }
}

/**
 * Delete image from S3 by URL
 */
export async function deleteImageFromS3(
  imageUrl: string
): Promise<DeleteResult> {
  try {
    const key = extractKeyFromUrl(imageUrl);
    if (!key) {
      return {
        success: false,
        error: 'Invalid S3 URL format',
      };
    }

    return await deleteImageByKey(key);
  } catch (error) {
    const errorMessage =
      error instanceof Error ? error.message : 'Failed to delete image';
    console.error('Error deleting image from S3:', error);
    return {
      success: false,
      error: errorMessage,
    };
  }
}

/**
 * Delete image from S3 by key
 */
export async function deleteImageByKey(key: string): Promise<DeleteResult> {
  try {
    const command = new DeleteObjectCommand({
      Bucket: S3_BUCKET_NAME,
      Key: key,
    });

    await s3Client.send(command);

    return { success: true };
  } catch (error) {
    const errorMessage =
      error instanceof Error ? error.message : 'Failed to delete image';
    console.error('Error deleting image by key:', error);
    return {
      success: false,
      error: errorMessage,
    };
  }
}

/**
 * Delete multiple images from S3
 */
export async function deleteMultipleImages(imageUrls: string[]): Promise<{
  success: boolean;
  results: { url: string; success: boolean; error?: string }[];
}> {
  const results = [];

  for (const url of imageUrls) {
    const result = await deleteImageFromS3(url);
    results.push({
      url,
      success: result.success,
      error: result.error,
    });
  }

  const allSuccessful = results.every((r) => r.success);

  return {
    success: allSuccessful,
    results,
  };
}

/**
 * Generate presigned POST for direct client upload
 */
export async function generatePresignedPost(
  fileName: string,
  contentType: string,
  folder: keyof typeof IMAGE_CONFIG.folders
) {
  try {
    // Validate content type with type assertion
    if (
      !IMAGE_CONFIG.allowedTypes.includes(
        contentType as (typeof IMAGE_CONFIG.allowedTypes)[number]
      )
    ) {
      throw new Error(`Content type ${contentType} is not allowed`);
    }

    const key = generateFileKey(folder, fileName);

    const { url, fields } = await createPresignedPost(s3Client, {
      Bucket: S3_BUCKET_NAME,
      Key: key,
      Conditions: [
        ['content-length-range', 0, IMAGE_CONFIG.maxSize],
        ['starts-with', '$Content-Type', contentType],
      ],
      Fields: {
        acl: 'public-read',
        'Content-Type': contentType,
      },
      Expires: 600, // 10 minutes
    });

    return {
      success: true,
      uploadUrl: url,
      fields,
      key,
      finalUrl: getPublicUrl(key),
    };
  } catch (error) {
    const errorMessage =
      error instanceof Error ? error.message : 'Failed to generate upload URL';
    console.error('Error generating presigned POST:', error);
    return {
      success: false,
      error: errorMessage,
    };
  }
}

/**
 * Replace existing image (delete old, upload new)
 */
export async function replaceImage(
  oldImageUrl: string | null,
  newFile: File,
  folder: keyof typeof IMAGE_CONFIG.folders
): Promise<UploadResult> {
  try {
    // Upload new image first
    const uploadResult = await uploadImageFile(newFile, folder);

    if (!uploadResult.success) {
      return uploadResult;
    }

    // Delete old image if exists
    if (oldImageUrl) {
      const deleteResult = await deleteImageFromS3(oldImageUrl);
      if (!deleteResult.success) {
        console.warn('Failed to delete old image:', deleteResult.error);
        // Don't fail the operation if old image deletion fails
      }
    }

    return uploadResult;
  } catch (error) {
    const errorMessage =
      error instanceof Error ? error.message : 'Failed to replace image';
    console.error('Error replacing image:', error);
    return {
      success: false,
      error: errorMessage,
    };
  }
}
