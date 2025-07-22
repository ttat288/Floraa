import Link from 'next/link';
import { FlowerIcon } from 'lucide-react';

export function LoginHeader() {
  return (
    <div className='text-center space-y-6'>
      <Link
        href='/'
        className='inline-flex items-center gap-2 font-bold text-flora-text hover:opacity-80 transition-opacity'
      >
        <FlowerIcon className='h-8 w-8' />
        <span className='text-2xl'>D08 Floraa</span>
      </Link>
      <div className='space-y-2'>
        <h1 className='text-2xl font-bold text-flora-text'>Đăng nhập</h1>
        <p className='text-muted-foreground'>Vui lòng đăng nhập để tiếp tục</p>
      </div>
    </div>
  );
}
