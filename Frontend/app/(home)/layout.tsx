import { Footer } from '@/components/layout/footer';
import { Header } from '@/components/layout/header';
import React from 'react';

export default function CustomerLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className='min-h-screen flex flex-col'>
      <Header />
      <main className='z-0 flex-1 flex justify-center'>{children}</main>
      <Footer />
    </div>
  );
}
