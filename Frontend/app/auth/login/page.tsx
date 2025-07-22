import { LoginHeader } from './components/login-header';
import { LoginForm } from './components/login-form';
import Image from 'next/image';

function LoginPage() {
  return (
    <div className='min-h-screen flex items-center justify-center py-12 px-4 relative overflow-hidden'>
      {/* Background Image */}
      <div className='absolute inset-0 z-0'>
        <Image
          src='/roseHeroBackground.png'
          alt='Rose Background'
          fill
          className='object-cover opacity-5'
          priority
        />
      </div>

      {/* Login Content */}
      <div className='w-full max-w-md space-y-8 relative z-10'>
        <LoginHeader />
        <LoginForm />
      </div>
    </div>
  );
}

export default LoginPage;
