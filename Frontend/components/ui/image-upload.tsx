'use client';

import type React from 'react';

import { useState, useRef } from 'react';
import Image from 'next/image';
import { Button } from '@/components/ui/button';
import { Progress } from '@/components/ui/progress';
import { useImageUpload } from '@/hooks/use-image-upload';
import { IMAGE_CONFIG } from '@/lib/aws/s3-config';
import { UploadIcon, XIcon, ImageIcon } from 'lucide-react';
import { cn } from '@/lib/utils';

interface ImageUploadProps {
  folder: keyof typeof IMAGE_CONFIG.folders;
  currentImageUrl?: string;
  onImageChange: (url: string | null) => void;
  className?: string;
  disabled?: boolean;
  multiple?: boolean;
}

export function ImageUpload({
  folder,
  currentImageUrl,
  onImageChange,
  className,
  disabled = false,
  multiple = false,
}: ImageUploadProps) {
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [dragActive, setDragActive] = useState(false);

  const { uploading, progress, error, uploadImage, deleteImage } =
    useImageUpload({
      folder,
      onSuccess: (url) => {
        onImageChange(url);
      },
      onError: (error) => {
        console.error('Upload error:', error);
      },
    });

  const handleFileSelect = async (files: FileList | null) => {
    if (!files || files.length === 0) return;

    const file = files[0];
    await uploadImage(file);
  };

  const handleDrop = (e: React.DragEvent) => {
    e.preventDefault();
    setDragActive(false);

    if (disabled) return;

    const files = e.dataTransfer.files;
    handleFileSelect(files);
  };

  const handleDragOver = (e: React.DragEvent) => {
    e.preventDefault();
    if (!disabled) {
      setDragActive(true);
    }
  };

  const handleDragLeave = (e: React.DragEvent) => {
    e.preventDefault();
    setDragActive(false);
  };

  const handleRemoveImage = async () => {
    if (currentImageUrl) {
      const success = await deleteImage(currentImageUrl);
      if (success) {
        onImageChange(null);
      }
    } else {
      onImageChange(null);
    }
  };

  const openFileDialog = () => {
    if (!disabled) {
      fileInputRef.current?.click();
    }
  };

  return (
    <div className={cn('space-y-4', className)}>
      <input
        ref={fileInputRef}
        type='file'
        accept={IMAGE_CONFIG.allowedTypes.join(',')}
        onChange={(e) => handleFileSelect(e.target.files)}
        className='hidden'
        disabled={disabled}
        multiple={multiple}
      />

      {currentImageUrl ? (
        <div className='relative group'>
          <div className='relative w-full h-48 rounded-lg overflow-hidden border'>
            <Image
              src={currentImageUrl || '/placeholder.svg'}
              alt='Uploaded image'
              fill
              className='object-cover'
            />
            {!disabled && (
              <div className='absolute inset-0 bg-black/50 opacity-0 group-hover:opacity-100 transition-opacity flex items-center justify-center'>
                <Button
                  variant='destructive'
                  size='sm'
                  onClick={handleRemoveImage}
                  className='gap-2'
                >
                  <XIcon className='h-4 w-4' />
                  Remove
                </Button>
              </div>
            )}
          </div>
        </div>
      ) : (
        <div
          className={cn(
            'border-2 border-dashed rounded-lg p-8 text-center transition-colors',
            dragActive
              ? 'border-primary bg-primary/5'
              : 'border-muted-foreground/25',
            disabled
              ? 'opacity-50 cursor-not-allowed'
              : 'cursor-pointer hover:border-primary/50',
            className
          )}
          onDrop={handleDrop}
          onDragOver={handleDragOver}
          onDragLeave={handleDragLeave}
          onClick={openFileDialog}
        >
          <div className='flex flex-col items-center gap-4'>
            <div className='p-4 rounded-full bg-muted'>
              {uploading ? (
                <UploadIcon className='h-8 w-8 animate-spin' />
              ) : (
                <ImageIcon className='h-8 w-8 text-muted-foreground' />
              )}
            </div>

            {uploading ? (
              <div className='w-full max-w-xs space-y-2'>
                <Progress value={progress} className='h-2' />
                <p className='text-sm text-muted-foreground'>
                  Uploading... {progress}%
                </p>
              </div>
            ) : (
              <div className='space-y-2'>
                <p className='text-sm font-medium'>
                  Drop your image here, or click to browse
                </p>
                <p className='text-xs text-muted-foreground'>
                  Supports: {IMAGE_CONFIG.allowedExtensions.join(', ')}
                  (Max {IMAGE_CONFIG.maxSize / (1024 * 1024)}MB)
                </p>
              </div>
            )}
          </div>
        </div>
      )}

      {error && <p className='text-sm text-destructive'>{error}</p>}
    </div>
  );
}
