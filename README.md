# 🧩 Fullstack Starter Project – ASP.NET Core API + Next.js Frontend

This is a fully structured, production-ready backend built with **ASP.NET Core**, following **Clean Architecture** and modern design patterns. It includes authentication features and is ready to integrate with a Next.js frontend (using **shadcn/ui**). More features will be added soon.

---

## 🏗️ Tech Stack Overview

### Backend
- **.NET 8** with ASP.NET Core Web API
- **Clean Architecture**: Domain, Application, Infrastructure, API
- **Entity Framework Core** (Code First)
- **CQRS Pattern** with MediatR
- **JWT Authentication + Refresh Token**
- **Role-based Authorization**
- **Soft Delete**, **Global Exception Handling**

### Frontend (Planned & Partially Integrated)
- **Next.js (TypeScript)**
- **shadcn/ui** component library
- Full auth flow integration (coming next)

---

## ✨ Implemented Features

- ✅ Clean architecture setup
- ✅ Initial migration & database schema ready
- ✅ Register/Login with hashed password
- ✅ JWT + Refresh token flow
- ✅ Role-based access control
- ✅ BaseEntity with audit fields
- ✅ Consistent API responses

---

## 📂 Project Structure

```
src/
├── API/            # ASP.NET Core Web API (entry point)
├── Application/    # CQRS, DTOs, Business logic
├── Domain/         # Entities, Enums, Interfaces
├── Infrastructure/ # EF Core, DB context, Services
```

---

## 🚀 Getting Started

1. **Clone project**
   ```bash
   git clone <your-repo-url>
   ```

2. **Set up DB connection**
   Update `DefaultConnection` in `appsettings.json`.

3. **Run migrations**
   ```bash
   dotnet ef database update
   ```

4. **Launch API**
   ```bash
   dotnet run --project src/API
   ```

API will be available at: `https://localhost:7000`

---

## 🔐 Auth Endpoints (Sample)

- `POST /api/auth/register` – Create new account
- `POST /api/auth/login` – Authenticate and receive token

---

## 🧪 Example Request

```json
POST /api/auth/register
{
  "email": "user@example.com",
  "password": "Password123!",
  "firstName": "John",
  "lastName": "Doe"
}
```

---

## 🧭 Future Roadmap

- 🌐 Integrate full Next.js frontend
- 🧾 CRUD modules for key business entities
- 📦 Docker support
- 🔒 OAuth login options

---

## 📌 About Me

This is not just a learning project – it's a base I built from scratch to use in real-world applications and future SaaS products. Clean architecture, security, and scalability are at the core of this setup.

---

## 📫 Contact

If you're a recruiter or hiring manager looking for a fullstack .NET + React/Next.js engineer who builds clean and scalable systems – **I'm ready for the interview**.