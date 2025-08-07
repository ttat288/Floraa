'use client';

import { Button } from '@/components/ui/button';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu';
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import {
  UserIcon,
  SettingsIcon,
  HeartIcon,
  ShoppingBagIcon,
  LogOutIcon,
  CreditCardIcon,
  BellIcon,
  DollarSignIcon,
  WalletIcon,
  LineChartIcon,
} from 'lucide-react';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation'; // Import useRouter
import { verify } from '@/lib/jwt';
import { env } from '@/env.mjs';

// Define user roles with numerical values for consistency
enum UserRole {
  Customer = 1,
  Staff = 2,
  Manager = 3,
  SuperManager = 4,
  Admin = 5,
}

interface UserProfile {
  name: string;
  email: string;
  avatar: string;
  role: UserRole | null;
  // Placeholder for financial data
  totalMoney?: number;
  walletBalance?: number;
  totalGeneralIncome?: number;
}

export function ProfileDropdown() {
  const router = useRouter(); // Initialize useRouter
  const [userProfile, setUserProfile] = useState<UserProfile>({
    name: 'Guest',
    email: 'guest@example.com',
    avatar: '/placeholder.svg?height=40&width=40',
    role: null,
  });
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchUserProfile = async () => {
      setIsLoading(true);
      try {
        // In a real app, you'd fetch user data from an API
        // For now, let's simulate fetching and decoding role from cookie
        const roleToken = document.cookie
          .split('; ')
          .find((row) => row.startsWith('roleToken='))
          ?.split('=')[1];

        let role: UserRole | null = null;
        if (roleToken) {
          try {
            const decoded = await verify<{ role: string }>(
              roleToken,
              env.NEXT_PUBLIC_JWT_SECRET
            );
            role = Number.parseInt(decoded.role, 10) as UserRole;
          } catch (tokenError) {
            console.error('Failed to decode role token:', tokenError);
            // Token invalid, treat as guest
          }
        }

        // Simulate user data based on role
        let name = 'Người dùng';
        let email = 'user@example.com';
        const avatar = '/placeholder.svg?height=40&width=40';
        let totalMoney = 0;
        let walletBalance = 0;
        let totalGeneralIncome = 0;

        switch (role) {
          case UserRole.Admin:
            name = 'Admin User';
            email = 'admin@floraa.com';
            totalGeneralIncome = 1234567890; // Example large income
            break;
          case UserRole.SuperManager:
            name = 'Super Manager';
            email = 'supermanager@floraa.com';
            totalGeneralIncome = 987654321;
            break;
          case UserRole.Manager:
            name = 'Manager User';
            email = 'manager@floraa.com';
            totalGeneralIncome = 500000000;
            break;
          case UserRole.Staff:
            name = 'Staff Member';
            email = 'staff@floraa.com';
            totalGeneralIncome = 100000000;
            break;
          case UserRole.Customer:
            name = 'Khách hàng';
            email = 'customer@example.com';
            totalMoney = 500000;
            walletBalance = 150000;
            break;
          default:
            // Guest or unauthenticated
            name = 'Khách';
            email = 'guest@example.com';
            break;
        }

        setUserProfile({
          name,
          email,
          avatar,
          role,
          totalMoney,
          walletBalance,
          totalGeneralIncome,
        });
      } catch (error) {
        console.error('Error fetching user profile:', error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchUserProfile();
  }, []);

  const handleLogout = async () => {
    try {
      // Call your logout API endpoint
      await fetch('/api/auth/logout', { method: 'POST' });
      // Redirect to login page after successful logout
      router.push('/auth/login');
      router.refresh(); // Refresh the page to clear client-side state and re-evaluate middleware
    } catch (error) {
      console.error('Logout failed:', error);
      // Optionally show a toast notification for logout failure
    }
  };

  const formatCurrency = (amount: number) => {
    return new Intl.NumberFormat('vi-VN', {
      style: 'currency',
      currency: 'VND',
    }).format(amount);
  };

  if (isLoading) {
    return (
      <Button variant='outline' size='icon' className='relative animate-pulse'>
        <Avatar className='h-6 w-6'>
          <AvatarFallback>
            <UserIcon className='h-4 w-4' />
          </AvatarFallback>
        </Avatar>
      </Button>
    );
  }

  const isManagementRole =
    userProfile.role === UserRole.Admin ||
    userProfile.role === UserRole.SuperManager ||
    userProfile.role === UserRole.Manager ||
    userProfile.role === UserRole.Staff;

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant='outline' size='icon' className='relative'>
          <Avatar className='h-6 w-6'>
            <AvatarImage
              src={userProfile.avatar || '/placeholder.svg'}
              alt={userProfile.name}
            />
            <AvatarFallback>
              <UserIcon className='h-4 w-4' />
            </AvatarFallback>
          </Avatar>
          <span className='sr-only'>Tài khoản</span>
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align='end' className='w-56'>
        <div className='flex items-center gap-2 p-2'>
          <Avatar className='h-8 w-8'>
            <AvatarImage
              src={userProfile.avatar || '/placeholder.svg'}
              alt={userProfile.name}
            />
            <AvatarFallback>
              <UserIcon className='h-4 w-4' />
            </AvatarFallback>
          </Avatar>
          <div className='flex flex-col space-y-1'>
            <p className='text-sm font-medium leading-none'>
              {userProfile.name}
            </p>
            <p className='text-xs leading-none text-muted-foreground'>
              {userProfile.email}
            </p>
          </div>
        </div>
        <DropdownMenuSeparator />

        {isManagementRole && userProfile.totalGeneralIncome !== undefined && (
          <DropdownMenuItem
            className='flex items-center gap-2 cursor-default'
            disabled
          >
            <LineChartIcon className='h-4 w-4' />
            Tổng thu nhập: {formatCurrency(userProfile.totalGeneralIncome)}
          </DropdownMenuItem>
        )}

        {userProfile.role === UserRole.Customer && (
          <>
            {userProfile.totalMoney !== undefined && (
              <DropdownMenuItem
                className='flex items-center gap-2 cursor-default'
                disabled
              >
                <DollarSignIcon className='h-4 w-4' />
                Tổng tiền: {formatCurrency(userProfile.totalMoney)}
              </DropdownMenuItem>
            )}
            {userProfile.walletBalance !== undefined && (
              <DropdownMenuItem
                className='flex items-center gap-2 cursor-default'
                disabled
              >
                <WalletIcon className='h-4 w-4' />
                Số dư ví: {formatCurrency(userProfile.walletBalance)}
              </DropdownMenuItem>
            )}
          </>
        )}

        {(isManagementRole || userProfile.role === UserRole.Customer) && (
          <DropdownMenuSeparator />
        )}

        <DropdownMenuItem asChild>
          <Link href='/profile' className='flex items-center gap-2'>
            <UserIcon className='h-4 w-4' />
            Thông tin cá nhân
          </Link>
        </DropdownMenuItem>
        <DropdownMenuItem asChild>
          <Link href='/orders' className='flex items-center gap-2'>
            <ShoppingBagIcon className='h-4 w-4' />
            Đơn hàng của tôi
          </Link>
        </DropdownMenuItem>
        {userProfile.role === UserRole.Customer && (
          <>
            <DropdownMenuItem asChild>
              <Link href='/wishlist' className='flex items-center gap-2'>
                <HeartIcon className='h-4 w-4' />
                Danh sách yêu thích
              </Link>
            </DropdownMenuItem>
            <DropdownMenuItem asChild>
              <Link href='/payment-methods' className='flex items-center gap-2'>
                <CreditCardIcon className='h-4 w-4' />
                Phương thức thanh toán
              </Link>
            </DropdownMenuItem>
            <DropdownMenuItem asChild>
              <Link href='/notifications' className='flex items-center gap-2'>
                <BellIcon className='h-4 w-4' />
                Thông báo
              </Link>
            </DropdownMenuItem>
          </>
        )}
        <DropdownMenuItem asChild>
          <Link href='/settings' className='flex items-center gap-2'>
            <SettingsIcon className='h-4 w-4' />
            Cài đặt
          </Link>
        </DropdownMenuItem>
        <DropdownMenuSeparator />
        <DropdownMenuItem
          onClick={handleLogout} // Added onClick handler
          className='flex items-center gap-2 text-destructive focus:text-destructive cursor-pointer'
        >
          <LogOutIcon className='h-4 w-4' />
          Đăng xuất
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}
