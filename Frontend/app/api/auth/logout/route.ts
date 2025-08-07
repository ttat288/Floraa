import { clearCookies } from '@/helper/utils/tokenUtils';
import { NextResponse } from 'next/server';

// Define the names of ALL cookies you want to clear on logout
const AUTH_COOKIE_NAMES = ['roleToken', 'refreshToken', 'accessToken'];

// Common logout handler function to avoid code duplication
async function handleLogout() {
  try {
    // Clear server-side cookies using the utility function
    await clearCookies(AUTH_COOKIE_NAMES, {
      path: '/',
    });

    // Create response with headers to clear client-side cookies as well
    const response = NextResponse.json(
      {
        success: true,
        message: 'Logged out successfully',
        timestamp: new Date().toISOString(),
      },
      { status: 200 }
    );

    // Add Set-Cookie headers to explicitly clear cookies in the browser too
    AUTH_COOKIE_NAMES.forEach((name) => {
      response.cookies.delete(name);
      // Also add an explicit header to handle any edge cases
      response.headers.append(
        'Set-Cookie',
        `${name}=; Path=/; Expires=Thu, 01 Jan 1970 00:00:00 GMT; SameSite=Lax`
      );
    });

    return response;
  } catch (error) {
    console.error('Lỗi trong quá trình logout:', error);

    // Return error response
    return NextResponse.json(
      {
        success: false,
        message: 'Logout failed due to server error',
        error: error instanceof Error ? error.message : 'Unknown error',
      },
      { status: 500 }
    );
  }
}

// Support both GET and POST methods for logout
export function GET() {
  console.log('GET method called for logout');
  return handleLogout();
}

export function POST() {
  console.log('POST method called for logout');
  return handleLogout();
}
