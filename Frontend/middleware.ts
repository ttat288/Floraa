import { NextResponse, type NextRequest } from 'next/server';
import { verify } from '@/lib/jwt';
import { env } from '@/env.mjs';

// Define user roles with numerical values for easy comparison
enum UserRole {
  Customer = 0,
  Staff = 1,
  Manager = 2,
  SuperManager = 3,
  Admin = 4,
}

// Paths that are accessible to anyone (unauthenticated or any role)
const publicPaths = [
  '/', // Home page
  '/auth/login',
  '/auth/register',
  '/products', // Public product listing
  /^\/products\/[^/]+$/, // Dynamic product detail page (e.g., /products/123)
  '/categories', // Public category listing
  '/about', // About Us page
  '/contact', // Contact Us page
  '/faq', // FAQ page
  '/shipping', // Shipping page
  '/returns', // Returns page
  '/privacy', // Privacy Policy page
  // API routes that are public or handle authentication
  /^\/api\/auth\/login$/,
  /^\/api\/auth\/logout$/,
  /^\/api\/auth\/refresh-token$/,
  /^\/api\/auth\/get-access-token$/,
  /^\/api\/upload\/.+$/, // All upload API routes (adjust if specific upload routes need protection)
  /^\/api\/Categories\/active$/, // Public API for active categories
  /^\/api\/Products\/active$/, // Public API for active products
];

// Paths that require a minimum role. Order matters for more specific paths.
const protectedPaths: { regex: RegExp; minRole: UserRole }[] = [
  // Admin-specific paths
  { regex: /^\/admin(\/.*)?$/, minRole: UserRole.Admin },
  // SuperManager-specific paths (includes Admin)
  { regex: /^\/super-manager(\/.*)?$/, minRole: UserRole.SuperManager },
  // Manager-specific paths (includes SuperManager, Admin)
  { regex: /^\/manager(\/.*)?$/, minRole: UserRole.Manager },
  // Staff-specific paths (includes Manager, SuperManager, Admin)
  { regex: /^\/staff(\/.*)?$/, minRole: UserRole.Staff },
  // Customer-specific paths (includes Staff, Manager, SuperManager, Admin)
  { regex: /^\/cart(\/.*)?$/, minRole: UserRole.Customer },
  { regex: /^\/checkout(\/.*)?$/, minRole: UserRole.Customer },
  { regex: /^\/profile(\/.*)?$/, minRole: UserRole.Customer },
  { regex: /^\/orders(\/.*)?$/, minRole: UserRole.Customer },
  { regex: /^\/wishlist(\/.*)?$/, minRole: UserRole.Customer },
  { regex: /^\/payment-methods(\/.*)?$/, minRole: UserRole.Customer },
  { regex: /^\/notifications(\/.*)?$/, minRole: UserRole.Customer },
  { regex: /^\/settings(\/.*)?$/, minRole: UserRole.Customer },
];

export async function middleware(request: NextRequest) {
  const pathname = request.nextUrl.pathname;

  // 1. Check if the path is public
  const isPublicPath = publicPaths.some((p) =>
    typeof p === 'string' ? p === pathname : p.test(pathname)
  );

  if (isPublicPath) {
    return NextResponse.next();
  }

  // 2. Get the role token from cookies
  const roleToken = request.cookies.get('roleToken')?.value;
  let userRole: UserRole | null = null;

  if (roleToken) {
    try {
      // Verify the token using your JWT utility
      const decoded = await verify<{ role: string }>(
        roleToken,
        env.NEXT_PUBLIC_JWT_SECRET
      );
      userRole = Number.parseInt(decoded.role, 10) as UserRole;
    } catch (error) {
      console.error('Invalid or expired role token:', error);
      // If token is invalid or expired, clear cookies and redirect to login
      const response = NextResponse.redirect(
        new URL('/auth/login', request.url)
      );
      response.cookies.delete('roleToken');
      response.cookies.delete('accessToken');
      response.cookies.delete('refreshToken');
      return response;
    }
  }

  // 3. Determine the minimum required role for the current path
  let requiredRole: UserRole | null = null;
  for (const { regex, minRole } of protectedPaths) {
    if (regex.test(pathname)) {
      requiredRole = minRole;
      break;
    }
  }

  // 4. Authorization Logic
  if (requiredRole !== null) {
    // This path is explicitly protected
    if (userRole === null || userRole < requiredRole) {
      // User is not authenticated OR user's role is insufficient
      if (userRole === null) {
        // Not authenticated, redirect to login
        return NextResponse.redirect(new URL('/auth/login', request.url));
      } else {
        // Authenticated but insufficient role, redirect to home page
        return NextResponse.redirect(new URL('/', request.url));
      }
    }
    // User is authenticated and has sufficient role, allow access
    return NextResponse.next();
  } else {
    // This path is not explicitly listed in protectedPaths, but it's also not public.
    // This implies it requires at least a logged-in user (Customer role or higher).
    if (userRole === null) {
      // Not authenticated, redirect to login
      return NextResponse.redirect(new URL('/auth/login', request.url));
    }
    // Authenticated, allow access
    return NextResponse.next();
  }
}

// Configure the matcher to run middleware on all paths except static files and internal Next.js paths
export const config = {
  matcher: ['/((?!_next/static|_next/image|favicon.ico).*)'],
};
