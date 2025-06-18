import { ResponseCookie } from 'next/dist/compiled/@edge-runtime/cookies';
import { cookies } from 'next/headers';

export const setCookie = async (
  name: string,
  value: string,
  options: Record<string, any>
): Promise<void> => {
  const cookieStore = await cookies();
  cookieStore.set(name, value, options);
};

export const getCookie = async (name: string): Promise<string | null> => {
  const cookieStore = await cookies();
  return cookieStore.get(name)?.value || '';
};

export const clearCookies = async (
  names: string[],
  options?: Pick<ResponseCookie, 'path' | 'domain'>
): Promise<void> => {
  const cookieStore = await cookies();
  console.log(
    'clearCookies function called with names:',
    names,
    'and options:',
    options
  );

  try {
    const allCookies = cookieStore.getAll();
    console.log(
      'Current cookies before deletion:',
      allCookies.map((c) => c.name)
    );
  } catch (e) {
    console.error('Error reading cookies:', e);
  }

  const deleteOptions = {
    path: options?.path ?? '/',
    domain: options?.domain,
  };

  names.forEach((name) => {
    try {
      console.log(
        `Attempting to delete cookie: ${name} with options:`,
        deleteOptions
      );
      cookieStore.delete({
        name,
        ...deleteOptions,
      });
      console.log(`Cookie deletion attempt completed for: ${name}`);
    } catch (error) {
      console.error(`Failed to delete cookie "${name}":`, error);
    }
  });

  try {
    const remainingCookies = cookieStore.getAll();
    console.log(
      'Cookies after deletion attempt:',
      remainingCookies.map((c) => c.name)
    );
  } catch (e) {
    console.error('Error reading cookies after deletion:', e);
  }
};
