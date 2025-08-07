'use client';

import { useState } from 'react';
import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { ChevronDownIcon } from 'lucide-react';

interface MegaMenuProps {
  title: string;
  items: Array<{
    label: string;
    href: string;
  }>;
}

export function MegaMenu({ title, items }: MegaMenuProps) {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div
      className='relative'
      onMouseEnter={() => setIsOpen(true)}
      onMouseLeave={() => setIsOpen(false)}
    >
      <Button
        variant='ghost'
        className='text-sm font-medium hover:text-flora-primary px-3 py-2 h-auto flex items-center gap-1'
      >
        {title}
        <ChevronDownIcon className='h-3 w-3' />
      </Button>

      {isOpen && (
        <div className='absolute top-full left-0 z-50 bg-background border rounded-lg shadow-lg p-2 min-w-[200px] max-w-[300px]'>
          <div className='grid gap-1'>
            {items.map((item, index) => (
              <Link
                key={index}
                href={item.href}
                className='block px-3 py-2 text-sm hover:bg-accent hover:text-accent-foreground rounded-md transition-colors'
              >
                {item.label}
              </Link>
            ))}
          </div>
        </div>
      )}
    </div>
  );
}
