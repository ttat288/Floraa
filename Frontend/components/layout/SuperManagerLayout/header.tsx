import { ModeToggle } from '@/components/custom/button/mode-toggle-btn';
import { ProfileDropdown } from '@/components/layout/CustomerLayout/components/profile-dropdown'; // Import the shared ProfileDropdown

export function SuperManagerHeader() {
  return (
    <header className='flex items-center justify-between p-4 border-b bg-background'>
      <div className='text-lg font-semibold'>Super Manager Dashboard</div>
      <div className='flex items-center gap-2'>
        <ModeToggle />
        <ProfileDropdown />
      </div>
    </header>
  );
}
