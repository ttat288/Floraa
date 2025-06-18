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
import { ModeToggle } from '../custom/button/mode-toggle-btn';
import { CartDropdown } from './components/cart-dropdown';
import { ProfileDropdown } from './components/profile-dropdown';
import { MegaMenu } from './components/mega-menu';

const menuData = {
  'chu-de': [
    { label: 'Hoa sinh nhật', href: '/dien-hoa/hoa-sinh-nhat' },
    { label: 'Hoa khai trương', href: '/dien-hoa/hoa-khai-truong' },
    { label: 'Hoa chúc mừng', href: '/dien-hoa/hoa-chuc-mung' },
    { label: 'Hoa chia buồn', href: '/dien-hoa/hoa-chia-buon' },
    { label: 'Hoa chúc sức khỏe', href: '/dien-hoa/hoa-chuc-suc-khoe' },
    { label: 'Hoa tình yêu', href: '/dien-hoa/hoa-tinh-yeu' },
    { label: 'Hoa cảm ơn', href: '/dien-hoa/hoa-cam-on' },
    {
      label: 'Hoa mừng tốt nghiệp',
      href: '/dien-hoa/hoa-chuc-mung-tot-nghiep',
    },
  ],
  'doi-tuong': [
    { label: 'Hoa tặng người yêu', href: '/doi-tuong/nguoi-yeu' },
    { label: 'Hoa tặng bạn bè', href: '/doi-tuong/ban-be' },
    { label: 'Hoa tặng vợ', href: '/doi-tuong/vo-yeu' },
    { label: 'Hoa tặng chồng', href: '/doi-tuong/chong-yeu' },
    { label: 'Hoa tặng mẹ', href: '/doi-tuong/me-yeu' },
    { label: 'Hoa tặng trẻ em', href: '/doi-tuong/tre-em' },
    { label: 'Hoa tặng cho nữ', href: '/doi-tuong/cho-nu' },
    { label: 'Hoa tặng cho nam', href: '/doi-tuong/cho-nam' },
    { label: 'Hoa tặng Sếp', href: '/doi-tuong/sep' },
    { label: 'Hoa tặng đồng nghiệp', href: '/doi-tuong/dong-nghiep' },
  ],
  'kieu-dang': [
    { label: 'Bó hoa tươi', href: '/shop-hoa/bo-hoa-tuoi' },
    { label: 'Giỏ hoa tươi', href: '/shop-hoa/gio-hoa-tuoi' },
    { label: 'Hộp hoa tươi', href: '/shop-hoa/hoa-tuoi-hop' },
    { label: 'Bình hoa tươi', href: '/shop-hoa/binh-hoa-tuoi' },
    { label: 'Hoa thả bình', href: '/shop-hoa/hoa-tha-binh' },
    { label: 'Lẵng hoa khai trương', href: '/shop-hoa/hoa-chuc-mung' },
    { label: 'Lẵng hoa chia buồn', href: '/shop-hoa/hoa-chia-buon' },
    { label: 'Hộp mica', href: '/shop-hoa/hop-mica' },
  ],
  'hoa-tuoi': [
    { label: 'Only rose', href: '/hoa-tuoi/only-rose' },
    { label: 'Hoa hồng', href: '/hoa-tuoi/hoa-hong' },
    { label: 'Hoa Hướng Dương', href: '/hoa-tuoi/huong-duong' },
    { label: 'Hoa đồng tiền', href: '/hoa-tuoi/hoa-dong-tien' },
    { label: 'Lan hồ điệp', href: '/hoa-tuoi/hoa-lan' },
    { label: 'Cẩm chướng', href: '/hoa-tuoi/hoa-cam-chuong' },
    { label: 'Hoa Cát Tường', href: '/hoa-tuoi/cat-tuong' },
    { label: 'Hoa ly', href: '/hoa-tuoi/hoa-ly' },
    { label: 'Baby flower', href: '/hoa-tuoi/hoa-baby' },
    { label: 'Hoa cúc', href: '/hoa-tuoi/hoa-cuc' },
    { label: 'Sen đá', href: '/hoa-tuoi/sen-da' },
  ],
  'mau-sac': [
    { label: 'Màu trắng', href: '/mau-hoa-tuoi/hoa-tuoi-mau-trang' },
    { label: 'Màu đỏ', href: '/mau-hoa-tuoi/hoa-tuoi-mau-do' },
    { label: 'Màu hồng', href: '/mau-hoa-tuoi/hoa-tuoi-mau-hong' },
    { label: 'Màu cam', href: '/mau-hoa-tuoi/hoa-tuoi-mau-cam' },
    { label: 'Màu tím', href: '/mau-hoa-tuoi/hoa-tuoi-mau-tim' },
    { label: 'Màu vàng', href: '/mau-hoa-tuoi/hoa-tuoi-mau-vang' },
    { label: 'Màu xanh', href: '/mau-hoa-tuoi/hoa-tuoi-mau-xanh' },
    { label: 'Kết hợp màu', href: '/mau-hoa-tuoi/hoa-tuoi-nhieu-mau' },
  ],
  'bo-suu-tap': [
    { label: 'Hoa Cao Cấp', href: '/cua-hang-hoa/hoa-cao-cap' },
    { label: 'Hoa sinh viên', href: '/cua-hang-hoa/hoa-sinh-vien' },
    { label: 'Mẫu hoa mới', href: '/cua-hang-hoa/mau-hoa-moi' },
    { label: 'Khuyến mãi', href: '/cua-hang-hoa/hoa-khuyen-mai' },
    {
      label: 'Ngày phụ nữ Việt Nam (20/10)',
      href: '/cua-hang-hoa/hoa-tuoi-20-10',
    },
    { label: 'Ngày nhà giáo', href: '/cua-hang-hoa/hoa-tuoi-20-11' },
    { label: 'Giáng Sinh', href: '/giang-sinh' },
    { label: 'Hoa Tết', href: '/cua-hang-hoa/hoa-tet' },
    { label: 'Hoa sự kiện', href: '/cua-hang-hoa/hoa-su-kien' },
    { label: 'Hoa tình yêu', href: '/hoa-tinh-yeu' },
    { label: 'Ngày Quốc tế Phụ nữ', href: '/ngay-phu-nu' },
  ],
  'qua-tang': [
    {
      label: 'Bánh kem Tous les Jours',
      href: '/qua-tang/banh-kem-tous-les-jours',
    },
    { label: 'Bánh kem Brodard', href: '/qua-tang/banh-kem-brodard' },
    { label: '4Gs Texas Bakery', href: '/qua-tang/4gs-texas-bakery' },
    { label: 'Chocolate', href: '/qua-tang/so-co-la-d-art' },
    { label: 'Trái cây', href: '/qua-tang/trai-cay' },
    { label: 'Gấu bông', href: '/qua-tang/gau-bong' },
    { label: 'Nến thơm', href: '/giang-sinh/nen-thom-va-hoa' },
  ],
};

export function Header() {
  const [isSearchDialogOpen, setIsSearchDialogOpen] = useState(false);

  return (
    <header className='sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60'>
      <div className='container mx-auto px-4'>
        <div className='flex h-16 items-center justify-between'>
          {/* Logo */}
          <Link
            href='/'
            className='flex items-center gap-2 font-bold text-flora-text'
            prefetch={false}
          >
            <FlowerIcon className='h-6 w-6' />
            <span className='text-xl'>D08 Floraa</span>
          </Link>

          {/* Desktop Navigation */}
          <nav className='hidden lg:flex items-center gap-1'>
            <Link
              href='/'
              className='text-sm font-medium hover:text-flora-primary transition-colors px-3 py-2 rounded-md'
              prefetch={false}
            >
              Trang chủ
            </Link>

            <MegaMenu title='Chủ đề' items={menuData['chu-de']} />
            <MegaMenu title='Đối tượng' items={menuData['doi-tuong']} />
            <MegaMenu title='Kiểu dáng' items={menuData['kieu-dang']} />
            <MegaMenu title='Hoa tươi' items={menuData['hoa-tuoi']} />
            <MegaMenu title='Màu sắc' items={menuData['mau-sac']} />
            <MegaMenu title='Bộ Sưu tập' items={menuData['bo-suu-tap']} />
            <MegaMenu title='Quà tặng kèm' items={menuData['qua-tang']} />

            <Link
              href='/hoa-cuoi'
              className='text-sm font-medium hover:text-flora-primary transition-colors px-3 py-2 rounded-md'
              prefetch={false}
            >
              Hoa cưới
            </Link>
            <Link
              href='/y-nghia-hoa'
              className='text-sm font-medium hover:text-flora-primary transition-colors px-3 py-2 rounded-md'
              prefetch={false}
            >
              Ý nghĩa hoa
            </Link>
          </nav>

          {/* Right side actions */}
          <div className='flex items-center gap-2'>
            {/* Search Dialog */}
            <Dialog
              open={isSearchDialogOpen}
              onOpenChange={setIsSearchDialogOpen}
            >
              <DialogTrigger asChild>
                <Button variant='ghost' size='icon' className='rounded-full'>
                  <SearchIcon className='h-5 w-5' />
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

            <ModeToggle />
            <ProfileDropdown />
            <CartDropdown />

            {/* Mobile Menu */}
            <Sheet>
              <SheetTrigger asChild>
                <Button variant='ghost' size='icon' className='lg:hidden'>
                  <MenuIcon className='h-6 w-6' />
                  <span className='sr-only'>Toggle navigation menu</span>
                </Button>
              </SheetTrigger>
              <SheetContent side='left' className='w-[280px] sm:w-[320px]'>
                <nav className='flex flex-col gap-4 py-6'>
                  <Link
                    href='/'
                    className='text-lg font-medium hover:text-flora-primary'
                    prefetch={false}
                  >
                    Trang chủ
                  </Link>
                  <Link
                    href='/dien-hoa'
                    className='text-lg font-medium hover:text-flora-primary'
                    prefetch={false}
                  >
                    Chủ đề
                  </Link>
                  <Link
                    href='/doi-tuong'
                    className='text-lg font-medium hover:text-flora-primary'
                    prefetch={false}
                  >
                    Đối tượng
                  </Link>
                  <Link
                    href='/shop-hoa'
                    className='text-lg font-medium hover:text-flora-primary'
                    prefetch={false}
                  >
                    Kiểu dáng
                  </Link>
                  <Link
                    href='/hoa-tuoi'
                    className='text-lg font-medium hover:text-flora-primary'
                    prefetch={false}
                  >
                    Hoa tươi
                  </Link>
                  <Link
                    href='/mau-hoa-tuoi'
                    className='text-lg font-medium hover:text-flora-primary'
                    prefetch={false}
                  >
                    Màu sắc
                  </Link>
                  <Link
                    href='/cua-hang-hoa'
                    className='text-lg font-medium hover:text-flora-primary'
                    prefetch={false}
                  >
                    Bộ Sưu tập
                  </Link>
                  <Link
                    href='/qua-tang'
                    className='text-lg font-medium hover:text-flora-primary'
                    prefetch={false}
                  >
                    Quà tặng kèm
                  </Link>
                  <Link
                    href='/hoa-cuoi'
                    className='text-lg font-medium hover:text-flora-primary'
                    prefetch={false}
                  >
                    Hoa cưới
                  </Link>
                  <Link
                    href='/y-nghia-hoa'
                    className='text-lg font-medium hover:text-flora-primary'
                    prefetch={false}
                  >
                    Ý nghĩa hoa
                  </Link>
                </nav>
              </SheetContent>
            </Sheet>
          </div>
        </div>
      </div>
    </header>
  );
}
