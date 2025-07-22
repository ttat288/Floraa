import { S3Client } from '@aws-sdk/client-s3';

// S3 Configuration
export const s3Config = {
  region: process.env.AWS_REGION || 'ap-southeast-1',
  credentials: {
    accessKeyId: process.env.AWS_ACCESS_KEY_ID || '',
    secretAccessKey: process.env.AWS_SECRET_ACCESS_KEY || '',
  },
};

export const s3Client = new S3Client(s3Config);

export const S3_BUCKET_NAME = process.env.AWS_BUCKET_NAME || '';
export const S3_REGION = process.env.AWS_REGION || 'ap-southeast-1';

// Image upload configuration
export const IMAGE_CONFIG = {
  maxSize: 10 * 1024 * 1024, // 10MB
  allowedTypes: ['image/jpeg', 'image/jpg', 'image/png', 'image/webp'],
  allowedExtensions: ['.jpg', '.jpeg', '.png', '.webp'],
  folders: {
    products: 'products',
    categories: 'categories',
    users: 'users',
    temp: 'temp',
  },
} as const;
