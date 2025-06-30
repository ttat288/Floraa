# ASP.NET Core RESTful API - Clean Architecture

Đây là một source base ASP.NET Core RESTful API hoàn chỉnh được xây dựng theo Clean Architecture với CQRS pattern và các best practices.

## 🏗️ Kiến trúc

### Clean Architecture Layers:
- **Domain**: Chứa entities, enums, interfaces và business logic core
- **Application**: Chứa use cases, DTOs, CQRS commands/queries
- **Infrastructure**: Implement các interfaces, database, external services
- **API**: Web API controllers và configuration

## 🚀 Tính năng chính

### Design Patterns & Techniques:
- ✅ **Clean Architecture**
- ✅ **CQRS Pattern** (Command Query Responsibility Segregation)
- ✅ **Unit of Work Pattern**
- ✅ **Repository Pattern**
- ✅ **MediatR** cho CQRS implementation
- ✅ **BaseEntity** với audit fields
- ✅ **BaseResponse** cho consistent API responses
- ✅ **Global Exception Handling**
- ✅ **JWT Authentication & Authorization**
- ✅ **Password Hashing** với BCrypt
- ✅ **Soft Delete** implementation
- ✅ **Entity Framework Core** với Code First

### Security Features:
- 🔐 JWT Token Authentication
- 🔐 Password Hashing với BCrypt
- 🔐 Refresh Token mechanism
- 🔐 Role-based Authorization
- 🔐 Global Exception Handling

## 📁 Cấu trúc thư mục

\`\`\`
src/
├── API/                    # Web API Layer
│   ├── Controllers/        # API Controllers
│   ├── Middleware/         # Custom Middleware
│   └── Program.cs          # Application entry point
├── Application/            # Application Layer
│   ├── Common/            # Common interfaces & exceptions
│   ├── DTOs/              # Data Transfer Objects
│   └── Features/          # CQRS Commands & Queries
├── Domain/                # Domain Layer
│   ├── Common/            # Base classes
│   ├── Entities/          # Domain entities
│   ├── Enums/             # Domain enums
│   └── Interfaces/        # Domain interfaces
└── Infrastructure/        # Infrastructure Layer
    ├── Data/              # Database context
    ├── Repositories/      # Repository implementations
    └── Services/          # Service implementations
\`\`\`

## 🛠️ Cài đặt và chạy

### Prerequisites:
- .NET 8.0 SDK
- SQL Server (LocalDB hoặc SQL Server)
- Visual Studio 2022 hoặc VS Code

### Bước 1: Clone repository
\`\`\`bash
git clone <repository-url>
cd MyAPI
\`\`\`

### Bước 2: Restore packages
\`\`\`bash
dotnet restore
\`\`\`

### Bước 3: Update database connection string
Cập nhật connection string trong `appsettings.json`:
\`\`\`json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyApiDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
\`\`\`

### Bước 4: Create và run migrations
\`\`\`bash
cd src/API
dotnet ef migrations add InitialCreate
dotnet ef database update
\`\`\`

### Bước 5: Run application
\`\`\`bash
dotnet run
\`\`\`

API sẽ chạy tại: `https://localhost:7000` hoặc `http://localhost:5000`

## 📚 API Endpoints

### Authentication
- `POST /api/auth/register` - Đăng ký user mới
- `POST /api/auth/login` - Đăng nhập

### Products
- `GET /api/products` - Lấy danh sách products (public)
- `POST /api/products` - Tạo product mới (requires auth)

## 🔧 Configuration

### JWT Settings (appsettings.json):
\`\`\`json
{
  "Jwt": {
    "Key": "your-super-secret-key-that-is-at-least-32-characters-long",
    "Issuer": "MyAPI",
    "Audience": "MyAPI-Users"
  }
}
\`\`\`

## 📖 Cách sử dụng

### 1. Đăng ký user mới:
\`\`\`bash
POST /api/auth/register
{
  "email": "user@example.com",
  "password": "Password123!",
  "confirmPassword": "Password123!",
  "firstName": "John",
  "lastName": "Doe"
}
\`\`\`

### 2. Đăng nhập:
\`\`\`bash
POST /api/auth/login
{
  "email": "user@example.com",
  "password": "Password123!"
}
\`\`\`

### 3. Tạo product (cần JWT token):
\`\`\`bash
POST /api/products
Authorization: Bearer <your-jwt-token>
{
  "name": "Sample Product",
  "description": "Product description",
  "price": 99.99,
  "stock": 10,
  "categoryId": "category-guid-here"
}
\`\`\`

## 🎯 Các pattern và kỹ thuật được áp dụng

1. **Clean Architecture**: Tách biệt rõ ràng các layer
2. **CQRS**: Tách biệt Command và Query
3. **Unit of Work**: Quản lý transactions
4. **Repository Pattern**: Abstraction cho data access
5. **BaseEntity**: Common properties cho tất cả entities
6. **BaseResponse**: Consistent API response format
7. **Global Exception Handling**: Centralized error handling
8. **JWT Authentication**: Secure API endpoints
9. **Password Hashing**: Secure password storage
10. **Soft Delete**: Logical deletion thay vì physical deletion

## 🚀 Mở rộng

Để thêm tính năng mới:

1. **Thêm Entity mới**: Tạo trong `Domain/Entities`
2. **Thêm Repository**: Update `IUnitOfWork` và `UnitOfWork`
3. **Thêm Commands/Queries**: Tạo trong `Application/Features`
4. **Thêm Controller**: Tạo trong `API/Controllers`

## 📝 Notes

- Project sử dụng Entity Framework Core với Code First approach
- Tất cả entities đều inherit từ `BaseEntity` để có audit fields
- Global exception middleware xử lý tất cả lỗi và trả về consistent response format
- JWT tokens có thời hạn 1 giờ, refresh tokens có thời hạn 7 ngày
- Soft delete được implement ở database level với global query filters

Đây là một source base hoàn chỉnh và chuyên nghiệp, phù hợp để học hỏi và áp dụng trong các dự án thực tế!
