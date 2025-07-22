import { type JWTPayload, jwtVerify, SignJWT } from "jose"

export async function sign<T extends JWTPayload>(
  payload: T,
  secret: string
): Promise<string> {
  const iat = Math.floor(Date.now() / 1000)
  const exp = iat + 60 * 60 // 1 giờ

  return new SignJWT({ ...payload })
    .setProtectedHeader({ alg: "HS256", typ: "JWT" })
    .setExpirationTime(exp)
    .setIssuedAt(iat)
    .setNotBefore(iat)
    .sign(new TextEncoder().encode(secret))
}

export async function verify<T extends JWTPayload>(
  token: string,
  secret: string
): Promise<T> {
  const { payload } = await jwtVerify(token, new TextEncoder().encode(secret))
  return payload as T // Ép kiểu về T
}
