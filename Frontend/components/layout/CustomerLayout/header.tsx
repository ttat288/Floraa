'use client';

import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { FlowerIcon, SearchIcon, MenuIcon } from 'lucide-react';
import { Sheet, SheetContent, SheetTrigger } from '@/components/ui/sheet';
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog';
import { useState } from 'react';
import { ModeToggle } from '@/components/custom/button/mode-toggle-btn';
import { CartDropdown } from './components/cart-dropdown';
import { ProfileDropdown } from './components/profile-dropdown'; // Updated import
import { MegaMenu } from './components/mega-menu';

// Simplified menu structure - grouped logically
const menuData = {
  'san-pham': [
    { label: 'Tất cả sản phẩm', href: '/products' },
    { label: 'Sản phẩm nổi bật', href: '/products/featured' },
    { label: 'Sản phẩm mới', href: '/products/new' },
    { label: 'Khuyến mãi', href: '/products/sale' },
  ],
  'theo-dip': [
    { label: 'Hoa sinh nhật', href: '/occasions/birthday' },
    { label: 'Hoa khai trương', href: '/occasions/opening' },
    { label: 'Hoa chúc mừng', href: '/occasions/congratulations' },
    { label: 'Hoa chia buồn', href: '/occasions/condolence' },
    { label: 'Hoa tình yêu', href: '/occasions/love' },
    { label: 'Hoa cưới', href: '/occasions/wedding' },
    { label: 'Ngày 20/10', href: '/occasions/womens-day' },
    { label: 'Ngày 8/3', href: '/occasions/international-womens-day' },
  ],
  'loai-hoa': [
    { label: 'Hoa hồng', href: '/flowers/roses' },
    { label: 'Hoa ly', href: '/flowers/lilies' },
    { label: 'Hoa hướng dương', href: '/flowers/sunflowers' },
    { label: 'Hoa cúc', href: '/flowers/chrysanthemums' },
    { label: 'Hoa tulip', href: '/flowers/tulips' },
    { label: 'Hoa cẩm chướng', href: '/flowers/carnations' },
    { label: 'Lan hồ điệp', href: '/flowers/orchids' },
    { label: 'Hoa baby', href: '/flowers/baby-breath' },
  ],
  'kieu-dang': [
    { label: 'Bó hoa', href: '/styles/bouquet' },
    { label: 'Giỏ hoa', href: '/styles/basket' },
    { label: 'Hộp hoa', href: '/styles/box' },
    { label: 'Bình hoa', href: '/styles/vase' },
    { label: 'Lẵng hoa', href: '/styles/wreath' },
    { label: 'Hoa để bàn', href: '/styles/table' },
  ],
};

export function Header() {
  const [isSearchDialogOpen, setIsSearchDialogOpen] = useState(false);

  return (
    <header className='sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60'>
      <div className='container mx-auto px-2 sm:px-4'>
        <div className='flex h-14 sm:h-16 items-center justify-between gap-2 sm:gap-4'>
          {/* Logo - Compact on mobile */}
          <Link
            href='/'
            className='flex items-center gap-1 sm:gap-2 font-bold text-flora-text shrink-0'
            prefetch={false}
          >
            <FlowerIcon className='h-5 w-5 sm:h-6 sm:w-6' />
            <span className='text-base sm:text-xl whitespace-nowrap'>
              D08 Floraa
            </span>
          </Link>

          {/* Desktop Navigation - Simplified */}
          <nav className='hidden lg:flex items-center gap-1 flex-1 justify-center max-w-2xl'>
            <Link
              href='/'
              className='text-sm font-medium hover:text-flora-primary transition-colors px-2 py-2 rounded-md whitespace-nowrap'
              prefetch={false}
            >
              Trang chủ
            </Link>

            <MegaMenu title='Sản phẩm' items={menuData['san-pham']} />
            <MegaMenu title='Theo dịp' items={menuData['theo-dip']} />
            <MegaMenu title='Loại hoa' items={menuData['loai-hoa']} />
            <MegaMenu title='Kiểu dáng' items={menuData['kieu-dang']} />

            <Link
              href='/about'
              className='text-sm font-medium hover:text-flora-primary transition-colors px-2 py-2 rounded-md whitespace-nowrap'
              prefetch={false}
            >
              Về chúng tôi
            </Link>
          </nav>

          {/* Right side actions - Compact */}
          <div className='flex items-center gap-1 shrink-0'>
            {/* Search Dialog */}
            <Dialog
              open={isSearchDialogOpen}
              onOpenChange={setIsSearchDialogOpen}
            >
              <DialogTrigger asChild>
                <Button
                  variant='ghost'
                  size='icon'
                  className='h-8 w-8 sm:h-9 sm:w-9'
                >
                  <SearchIcon className='h-4 w-4' />
                  <span className='sr-only'>Tìm kiếm</span>
                </Button>
              </DialogTrigger>
              <DialogContent className='sm:max-w-[425px]'>
                <DialogHeader>
                  <DialogTitle>Tìm kiếm sản phẩm</DialogTitle>
                </DialogHeader>
                <div className='grid gap-4 py-4'>
                  <Input
                    id='search'
                    placeholder='Nhập từ khóa tìm kiếm...'
                    className='col-span-3'
                    onKeyDown={(e) => {
                      if (e.key === 'Enter') {
                        console.log(
                          'Searching for:',
                          (e.target as HTMLInputElement).value
                        );
                        setIsSearchDialogOpen(false);
                      }
                    }}
                  />
                </div>
              </DialogContent>
            </Dialog>

            {/* Hide theme toggle on very small screens */}
            <div className='hidden sm:block'>
              <ModeToggle />
            </div>

            <ProfileDropdown />
            <CartDropdown />

            {/* Mobile Menu */}
            <Sheet>
              <SheetTrigger asChild>
                <Button
                  variant='ghost'
                  size='icon'
                  className='lg:hidden h-8 w-8 sm:h-9 sm:w-9'
                >
                  <MenuIcon className='h-4 w-4 sm:h-5 sm:w-5' />
                  <span className='sr-only'>Menu</span>
                </Button>
              </SheetTrigger>
              <SheetContent
                side='left'
                className='w-[280px] sm:w-[320px] overflow-y-auto'
              >
                <div className='flex flex-col gap-4 py-6'>
                  <Link
                    href='/'
                    className='text-lg font-medium hover:text-flora-primary px-2 py-1'
                    prefetch={false}
                  >
                    Trang chủ
                  </Link>

                  {/* Mobile menu sections */}
                  <div className='space-y-4'>
                    <div>
                      <h3 className='font-semibold text-flora-text mb-2 px-2'>
                        Sản phẩm
                      </h3>
                      <div className='space-y-1'>
                        {menuData['san-pham'].map((item, index) => (
                          <Link
                            key={index}
                            href={item.href}
                            className='block text-sm hover:text-flora-primary px-4 py-1 hover:bg-accent rounded-md'
                            prefetch={false}
                          >
                            {item.label}
                          </Link>
                        ))}
                      </div>
                    </div>

                    <div>
                      <h3 className='font-semibold text-flora-text mb-2 px-2'>
                        Theo dịp
                      </h3>
                      <div className='space-y-1'>
                        {menuData['theo-dip'].map((item, index) => (
                          <Link
                            key={index}
                            href={item.href}
                            className='block text-sm hover:text-flora-primary px-4 py-1 hover:bg-accent rounded-md'
                            prefetch={false}
                          >
                            {item.label}
                          </Link>
                        ))}
                      </div>
                    </div>

                    <div>
                      <h3 className='font-semibold text-flora-text mb-2 px-2'>
                        Loại hoa
                      </h3>
                      <div className='space-y-1'>
                        {menuData['loai-hoa'].map((item, index) => (
                          <Link
                            key={index}
                            href={item.href}
                            className='block text-sm hover:text-flora-primary px-4 py-1 hover:bg-accent rounded-md'
                            prefetch={false}
                          >
                            {item.label}
                          </Link>
                        ))}
                      </div>
                    </div>

                    <div>
                      <h3 className='font-semibold text-flora-text mb-2 px-2'>
                        Kiểu dáng
                      </h3>
                      <div className='space-y-1'>
                        {menuData['kieu-dang'].map((item, index) => (
                          <Link
                            key={index}
                            href={item.href}
                            className='block text-sm hover:text-flora-primary px-4 py-1 hover:bg-accent rounded-md'
                            prefetch={false}
                          >
                            {item.label}
                          </Link>
                        ))}
                      </div>
                    </div>
                  </div>

                  <div className='border-t pt-4 mt-4'>
                    <Link
                      href='/about'
                      className='block text-lg font-medium hover:text-flora-primary px-2 py-1'
                      prefetch={false}
                    >
                      Về chúng tôi
                    </Link>
                    <Link
                      href='/contact'
                      className='block text-lg font-medium hover:text-flora-primary px-2 py-1'
                      prefetch={false}
                    >
                      Liên hệ
                    </Link>
                  </div>

                  {/* Mobile-only theme toggle */}
                  <div className='border-t pt-4 mt-4 sm:hidden'>
                    <div className='px-2'>
                      <ModeToggle />
                    </div>
                  </div>
                </div>
              </SheetContent>
            </Sheet>
          </div>
        </div>
      </div>
    </header>
  );
}
