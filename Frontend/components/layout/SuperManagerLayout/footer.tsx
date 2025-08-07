import Link from 'next/link';
import { FlowerIcon } from 'lucide-react';

export function SuperManagerFooter() {
  return (
    <footer className='bg-background py-4 px-4 md:px-6 border-t border-border'>
      <div className='flex flex-col md:flex-row items-center justify-between gap-2'>
        <Link
          href='/'
          className='flex items-center gap-2 font-bold text-primary hover:text-primary/80'
          prefetch={false}
        >
          <FlowerIcon className='h-5 w-5' />
          <span className='text-base'>D08 Floraa</span>
        </Link>
        <p className='text-sm text-muted-foreground'>
          &copy; {new Date().getFullYear()} D08 Floraa. All rights reserved.
        </p>
      </div>
    </footer>
  );
}
