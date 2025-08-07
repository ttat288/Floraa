'use client';

import { Button } from '@/components/ui/button';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu';
import { Badge } from '@/components/ui/badge';
import {
  ShoppingCartIcon,
  Trash2Icon,
  PlusIcon,
  MinusIcon,
} from 'lucide-react';
import { useState } from 'react';
import Image from 'next/image';
import Link from 'next/link';

interface CartItem {
  id: string;
  name: string;
  price: number;
  quantity: number;
  image: string;
}

const fakeCartData: CartItem[] = [
  {
    id: '1',
    name: 'Bó hoa hồng đỏ',
    price: 350000,
    quantity: 1,
    image: '/placeholder.svg?height=60&width=60',
  },
  {
    id: '2',
    name: 'Hoa sinh nhật mix',
    price: 450000,
    quantity: 2,
    image: '/placeholder.svg?height=60&width=60',
  },
  {
    id: '3',
    name: 'Hộp hoa tươi cao cấp',
    price: 750000,
    quantity: 1,
    image: '/placeholder.svg?height=60&width=60',
  },
];

export function CartDropdown() {
  const [cartItems, setCartItems] = useState<CartItem[]>(fakeCartData);

  const updateQuantity = (id: string, newQuantity: number) => {
    if (newQuantity === 0) {
      setCartItems(cartItems.filter((item) => item.id !== id));
    } else {
      setCartItems(
        cartItems.map((item) =>
          item.id === id ? { ...item, quantity: newQuantity } : item
        )
      );
    }
  };

  const totalItems = cartItems.reduce((sum, item) => sum + item.quantity, 0);
  const totalPrice = cartItems.reduce(
    (sum, item) => sum + item.price * item.quantity,
    0
  );

  const formatPrice = (price: number) => {
    return new Intl.NumberFormat('vi-VN', {
      style: 'currency',
      currency: 'VND',
    }).format(price);
  };

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant='outline' size='icon' className='relative'>
          <ShoppingCartIcon className='h-5 w-5' />
          {totalItems > 0 && (
            <Badge
              variant='destructive'
              className='absolute -top-2 -right-2 h-5 w-5 flex items-center justify-center p-0 text-xs'
            >
              {totalItems}
            </Badge>
          )}
          <span className='sr-only'>Giỏ hàng</span>
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align='end' className='w-80 p-0'>
        <div className='p-4 border-b'>
          <h3 className='font-semibold text-lg'>Giỏ hàng của bạn</h3>
          <p className='text-sm text-muted-foreground'>{totalItems} sản phẩm</p>
        </div>

        {cartItems.length === 0 ? (
          <div className='p-6 text-center'>
            <ShoppingCartIcon className='h-12 w-12 mx-auto text-muted-foreground mb-4' />
            <p className='text-muted-foreground'>Giỏ hàng trống</p>
          </div>
        ) : (
          <>
            <div className='max-h-64 overflow-y-auto'>
              {cartItems.map((item) => (
                <div key={item.id} className='p-4 border-b last:border-b-0'>
                  <div className='flex items-center gap-3'>
                    <Image
                      src={item.image || '/placeholder.svg'}
                      alt={item.name}
                      width={60}
                      height={60}
                      className='rounded-md object-cover'
                    />
                    <div className='flex-1 min-w-0'>
                      <h4 className='font-medium text-sm truncate'>
                        {item.name}
                      </h4>
                      <p className='text-sm text-muted-foreground'>
                        {formatPrice(item.price)}
                      </p>
                      <div className='flex items-center gap-2 mt-2'>
                        <Button
                          variant='outline'
                          size='icon'
                          className='h-6 w-6'
                          onClick={() =>
                            updateQuantity(item.id, item.quantity - 1)
                          }
                        >
                          <MinusIcon className='h-3 w-3' />
                        </Button>
                        <span className='text-sm font-medium w-8 text-center'>
                          {item.quantity}
                        </span>
                        <Button
                          variant='outline'
                          size='icon'
                          className='h-6 w-6'
                          onClick={() =>
                            updateQuantity(item.id, item.quantity + 1)
                          }
                        >
                          <PlusIcon className='h-3 w-3' />
                        </Button>
                        <Button
                          variant='ghost'
                          size='icon'
                          className='h-6 w-6 ml-auto text-destructive hover:text-destructive'
                          onClick={() => updateQuantity(item.id, 0)}
                        >
                          <Trash2Icon className='h-3 w-3' />
                        </Button>
                      </div>
                    </div>
                  </div>
                </div>
              ))}
            </div>

            <div className='p-4 border-t bg-muted/50'>
              <div className='flex justify-between items-center mb-3'>
                <span className='font-semibold'>Tổng cộng:</span>
                <span className='font-bold text-lg'>
                  {formatPrice(totalPrice)}
                </span>
              </div>
              <div className='flex gap-2'>
                <Button variant='outline' className='flex-1' asChild>
                  <Link href='/cart'>Xem giỏ hàng</Link>
                </Button>
                <Button className='flex-1' asChild>
                  <Link href='/checkout'>Thanh toán</Link>
                </Button>
              </div>
            </div>
          </>
        )}
      </DropdownMenuContent>
    </DropdownMenu>
  );
}
