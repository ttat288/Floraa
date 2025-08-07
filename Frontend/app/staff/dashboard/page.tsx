function StaffDashboard() {
  return (
    <div className='flex flex-col items-center justify-center min-h-[calc(100vh-64px)] p-4'>
      <h1 className='text-4xl font-bold text-flora-text mb-4'>
        Staff Dashboard
      </h1>
      <p className='text-lg text-muted-foreground'>
        Welcome, Staff! Here are your daily tasks.
      </p>
    </div>
  );
}

export default StaffDashboard;
