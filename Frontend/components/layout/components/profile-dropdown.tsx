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
} from 'lucide-react';
import Link from 'next/link';

export function ProfileDropdown() {
  const user = {
    name: 'Nguyễn Văn A',
    email: 'nguyenvana@example.com',
    avatar: '/placeholder.svg?height=40&width=40',
  };

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant='outline' size='icon' className='relative'>
          <Avatar className='h-6 w-6'>
            <AvatarImage
              src={user.avatar || '/placeholder.svg'}
              alt={user.name}
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
              src={user.avatar || '/placeholder.svg'}
              alt={user.name}
            />
            <AvatarFallback>
              <UserIcon className='h-4 w-4' />
            </AvatarFallback>
          </Avatar>
          <div className='flex flex-col space-y-1'>
            <p className='text-sm font-medium leading-none'>{user.name}</p>
            <p className='text-xs leading-none text-muted-foreground'>
              {user.email}
            </p>
          </div>
        </div>
        <DropdownMenuSeparator />
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
        <DropdownMenuItem asChild>
          <Link href='/settings' className='flex items-center gap-2'>
            <SettingsIcon className='h-4 w-4' />
            Cài đặt
          </Link>
        </DropdownMenuItem>
        <DropdownMenuSeparator />
        <DropdownMenuItem className='flex items-center gap-2 text-destructive focus:text-destructive'>
          <LogOutIcon className='h-4 w-4' />
          Đăng xuất
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}
