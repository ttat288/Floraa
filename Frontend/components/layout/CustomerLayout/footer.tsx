import Link from 'next/link';
import {
  FlowerIcon,
  FacebookIcon,
  InstagramIcon,
  TwitterIcon,
} from 'lucide-react';

export function Footer() {
  return (
    <footer className='bg-flora-background-light py-8 md:py-12'>
      <div className='grid grid-cols-1 md:grid-cols-4 gap-8 px-4 md:px-6'>
        {/* About Us */}
        <div className='space-y-4'>
          <Link
            href='/'
            className='flex items-center gap-2 font-bold text-flora-text'
            prefetch={false}
          >
            <FlowerIcon className='h-6 w-6' />
            <span className='text-xl'>D08 Floraa</span>
          </Link>
          <p className='text-sm text-muted-foreground'>
            Mang đến những bó hoa tươi đẹp nhất cho mọi khoảnh khắc đặc biệt.
          </p>
          <div className='flex gap-4'>
            <Link
              href='#'
              className='text-muted-foreground hover:text-flora-primary'
              prefetch={false}
            >
              <FacebookIcon className='h-6 w-6' />
              <span className='sr-only'>Facebook</span>
            </Link>
            <Link
              href='#'
              className='text-muted-foreground hover:text-flora-primary'
              prefetch={false}
            >
              <InstagramIcon className='h-6 w-6' />
              <span className='sr-only'>Instagram</span>
            </Link>
            <Link
              href='#'
              className='text-muted-foreground hover:text-flora-primary'
              prefetch={false}
            >
              <TwitterIcon className='h-6 w-6' />
              <span className='sr-only'>Twitter</span>
            </Link>
          </div>
        </div>

        {/* Quick Links */}
        <div className='space-y-4'>
          <h3 className='text-lg font-semibold text-flora-text'>
            Liên kết nhanh
          </h3>
          <nav className='grid gap-2'>
            <Link
              href='/products'
              className='text-sm text-muted-foreground hover:text-flora-primary'
              prefetch={false}
            >
              Sản phẩm
            </Link>
            <Link
              href='/categories'
              className='text-sm text-muted-foreground hover:text-flora-primary'
              prefetch={false}
            >
              Danh mục
            </Link>
            <Link
              href='/about'
              className='text-sm text-muted-foreground hover:text-flora-primary'
              prefetch={false}
            >
              Về chúng tôi
            </Link>
            <Link
              href='/contact'
              className='text-sm text-muted-foreground hover:text-flora-primary'
              prefetch={false}
            >
              Liên hệ
            </Link>
          </nav>
        </div>

        {/* Customer Service */}
        <div className='space-y-4'>
          <h3 className='text-lg font-semibold text-flora-text'>
            Dịch vụ khách hàng
          </h3>
          <nav className='grid gap-2'>
            <Link
              href='/faq'
              className='text-sm text-muted-foreground hover:text-flora-primary'
              prefetch={false}
            >
              Câu hỏi thường gặp
            </Link>
            <Link
              href='/shipping'
              className='text-sm text-muted-foreground hover:text-flora-primary'
              prefetch={false}
            >
              Vận chuyển & Giao hàng
            </Link>
            <Link
              href='/returns'
              className='text-sm text-muted-foreground hover:text-flora-primary'
              prefetch={false}
            >
              Chính sách đổi trả
            </Link>
            <Link
              href='/privacy'
              className='text-sm text-muted-foreground hover:text-flora-primary'
              prefetch={false}
            >
              Chính sách bảo mật
            </Link>
          </nav>
        </div>

        {/* Contact Info */}
        <div className='space-y-4'>
          <h3 className='text-lg font-semibold text-flora-text'>Liên hệ</h3>
          <div className='text-sm text-muted-foreground'>
            <p>Địa chỉ: 123 Đường Hoa, Quận 1, TP.HCM</p>
            <p>Điện thoại: (028) 123 4567</p>
            <p>Email: info@d08floraa.com</p>
          </div>
        </div>
      </div>
      <div className='mt-8 border-t pt-8 text-center text-sm text-muted-foreground'>
        &copy; {new Date().getFullYear()} D08 Floraa. Tất cả quyền được bảo lưu.
      </div>
    </footer>
  );
}
