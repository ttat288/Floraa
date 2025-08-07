'use client';

import type React from 'react';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Card, CardContent, CardFooter } from '@/components/ui/card';
import { Alert, AlertDescription } from '@/components/ui/alert';
import { EyeIcon, EyeOffIcon, LoaderIcon, AlertCircleIcon } from 'lucide-react';
import Link from 'next/link';
import type { LoginResponse } from '@/app/api/auth/login/route'; // Import the LoginResponse type

interface LoginFormData {
  email: string;
  password: string;
}

// Define user roles with numerical values for consistency with backend/middleware
enum UserRole {
  Customer = 1,
  Staff = 2,
  Manager = 3,
  SuperManager = 4,
  Admin = 5,
}

export function LoginForm() {
  const router = useRouter();
  const [formData, setFormData] = useState<LoginFormData>({
    email: '',
    password: '',
  });
  const [showPassword, setShowPassword] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
    if (error) setError(null);
  };

  const validateForm = (): boolean => {
    if (!formData.email.trim()) {
      setError('Vui lòng nhập email');
      return false;
    }
    if (!formData.email.includes('@')) {
      setError('Email không hợp lệ');
      return false;
    }
    if (!formData.password.trim()) {
      setError('Vui lòng nhập mật khẩu');
      return false;
    }
    return true;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!validateForm()) return;

    setIsLoading(true);
    setError(null);

    try {
      const response = await fetch('/api/auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email: formData.email,
          password: formData.password,
        }),
      });

      const data: LoginResponse = await response.json(); // Cast to LoginResponse type

      if (!response.ok || !data.success) {
        setError(data.message || 'Có lỗi xảy ra, vui lòng thử lại');
        return;
      }

      // Login successful - determine redirect based on role
      const userRole = data.data.user.role;
      let redirectPath = '/'; // Default redirect for customers and unknown roles

      switch (userRole) {
        case UserRole.Admin:
          redirectPath = '/admin/dashboard';
          break;
        case UserRole.SuperManager:
          redirectPath = '/super-manager/dashboard';
          break;
        case UserRole.Manager:
          redirectPath = '/manager/dashboard';
          break;
        case UserRole.Staff:
          redirectPath = '/staff/dashboard';
          break;
        // For Customer role, redirect to home page (or /profile if preferred)
        case UserRole.Customer:
          redirectPath = '/'; // Or '/profile' if you want them to land on their profile
          break;
        default:
          redirectPath = '/'; // Fallback for unknown roles
          break;
      }

      router.push(redirectPath);
      router.refresh(); // Refresh the page to re-evaluate middleware and layout
    } catch (error) {
      console.error('Login error:', error);
      setError('Có lỗi xảy ra, vui lòng thử lại');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <Card className='w-full backdrop-blur-sm bg-background/80 border-flora-primary/20 shadow-xl'>
      <form onSubmit={handleSubmit}>
        <CardContent className='space-y-4 pt-6'>
          {error && (
            <Alert variant='destructive'>
              <AlertCircleIcon className='h-4 w-4' />
              <AlertDescription>{error}</AlertDescription>
            </Alert>
          )}

          <div className='space-y-2'>
            <label htmlFor='email' className='text-sm font-medium'>
              Email
            </label>
            <Input
              id='email'
              name='email'
              type='email'
              placeholder='Nhập email của bạn'
              value={formData.email}
              onChange={handleInputChange}
              disabled={isLoading}
              className='focus-visible:ring-flora-primary/50'
              required
            />
          </div>

          <div className='space-y-2'>
            <label htmlFor='password' className='text-sm font-medium'>
              Mật khẩu
            </label>
            <div className='relative'>
              <Input
                id='password'
                name='password'
                type={showPassword ? 'text' : 'password'}
                placeholder='Nhập mật khẩu của bạn'
                value={formData.password}
                onChange={handleInputChange}
                disabled={isLoading}
                className='pr-10 focus-visible:ring-flora-primary/50'
                required
              />
              <Button
                type='button'
                variant='ghost'
                size='icon'
                className='absolute right-0 top-0 h-full px-3 py-2 hover:bg-transparent'
                onClick={() => setShowPassword(!showPassword)}
                disabled={isLoading}
              >
                {showPassword ? (
                  <EyeOffIcon className='h-4 w-4 text-muted-foreground' />
                ) : (
                  <EyeIcon className='h-4 w-4 text-muted-foreground' />
                )}
              </Button>
            </div>
          </div>
        </CardContent>

        <CardFooter className='flex flex-col space-y-4'>
          <Button
            type='submit'
            className='w-full bg-flora-primary hover:bg-flora-primary/90 text-flora-primary-foreground shadow-lg'
            disabled={isLoading}
          >
            {isLoading ? (
              <>
                <LoaderIcon className='mr-2 h-4 w-4 animate-spin' />
                Đang đăng nhập...
              </>
            ) : (
              'Đăng nhập'
            )}
          </Button>

          <div className='text-center text-sm text-muted-foreground'>
            Chưa có tài khoản?{' '}
            <Link
              href='/auth/register'
              className='text-flora-primary hover:underline font-medium'
            >
              Đăng ký ngay
            </Link>
          </div>
        </CardFooter>
      </form>
    </Card>
  );
}
