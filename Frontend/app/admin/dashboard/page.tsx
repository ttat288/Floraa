function AdminDashboard() {
  return (
    <div className='flex flex-col items-center justify-center min-h-[calc(100vh-64px)] p-4'>
      <h1 className='text-4xl font-bold text-flora-text mb-4'>
        Admin Dashboard
      </h1>
      <p className='text-lg text-muted-foreground'>
        Welcome, Admin! This is your control panel.
      </p>
    </div>
  );
}

export default AdminDashboard;
