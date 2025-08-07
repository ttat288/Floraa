import React from 'react';
import { SuperManagerFooter } from '@/components/layout/SuperManagerLayout/footer';
import { SuperManagerHeader } from '@/components/layout/SuperManagerLayout/header';

export default function SuperManagerLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className='min-h-screen flex flex-col'>
      <SuperManagerHeader />
      <main className='z-0 flex-1 flex justify-center'>{children}</main>
      <SuperManagerFooter />
    </div>
  );
}
