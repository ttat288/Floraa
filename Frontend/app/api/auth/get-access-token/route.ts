import { cookies } from 'next/headers';
import { NextResponse } from 'next/server';

export async function GET(): Promise<Response> {
  try {
    const cookieStore = await cookies();
    const accessToken = cookieStore.get('accessToken')?.value;
    if (!accessToken) {
      return NextResponse.json(
        {
          isSuccess: false,
          message: 'No access token found',
        },
        { status: 401 }
      );
    }

    return NextResponse.json(
      {
        isSuccess: true,
        accessToken,
      },
      { status: 200 }
    );
  } catch (error) {
    console.error('Error getting access token:', error);
    return NextResponse.json(
      {
        isSuccess: false,
        message: 'Error retrieving access token',
      },
      { status: 500 }
    );
  }
}
