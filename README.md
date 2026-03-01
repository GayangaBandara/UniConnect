# UniConnect - Student-Mentor Guidance Platform

UniConnect is a full-stack web application that connects students with mentors for academic guidance and support. The platform enables students to post guidance requests and mentors to accept and provide assistance.

## 📋 Table of Contents

- [Project Overview](#project-overview)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Backend Setup](#backend-setup)
- [Frontend Setup](#frontend-setup)
- [API Endpoints](#api-endpoints)
- [Features](#features)
- [Running the Application](#running-the-application)

## 🎯 Project Overview

UniConnect is a platform designed to facilitate knowledge sharing between students and mentors. Key features include:

- **User Management**: Register and manage students, mentors, and admins
- **Guidance Requests**: Students can create and manage requests for guidance
- **Mentor Matching**: Mentors can view and accept guidance requests
- **Authentication**: Secure JWT-based authentication with refresh tokens
- **Role-Based Access**: Different permissions for students, mentors, and admins

## 🛠️ Technology Stack

### Backend
- **Framework**: ASP.NET Core 9.0
- **Database**: SQL Server (LocalDB)
- **ORM**: Entity Framework Core 9.x
- **Authentication**: JWT (JSON Web Tokens)
- **API Documentation**: Swagger/OpenAPI
- **Architecture**: Repository Pattern with Service Layer

### Frontend
- **Framework**: Next.js 14.2.35
- **Language**: TypeScript
- **Styling**: Tailwind CSS 3.4.1
- **UI Library**: React 18
- **Language**: TypeScript 5

## 📁 Project Structure

```
uniconnect/
├── backend/                          # ASP.NET Core Backend
│   ├── controllers/                  # API Controllers
│   │   ├── AuthController.cs         # Authentication endpoints
│   │   ├── UserController.cs         # User management
│   │   └── GuidanceRequestsController.cs
│   ├── models/                       # Database models
│   │   ├── User.cs                   # User entity
│   │   ├── GuidanceRequest.cs        # Guidance request entity
│   │   └── RefreshToken.cs           # Refresh token entity
│   ├── dtos/                         # Data Transfer Objects
│   │   ├── auth/                     # Auth DTOs
│   │   ├── user/                     # User DTOs
│   │   └── guidancerequest/          # Guidance request DTOs
│   ├── services/                     # Business logic
│   │   ├── interfaces/
│   │   ├── AuthenticationService.cs
│   │   ├── UsersService.cs
│   │   ├── GuidanceRequestService.cs
│   │   └── RefreshTokenService.cs
│   ├── repositories/                 # Data access layer
│   │   ├── interfaces/
│   │   ├── UserRepository.cs
│   │   ├── GuidanceRequestRepository.cs
│   │   └── RefreshTokenRepository.cs
│   ├── data/                         # Database context
│   │   └── AppDbContext.cs           # EF Core context
│   ├── Migrations/                   # Database migrations
│   ├── Program.cs                    # Application startup
│   ├── appsettings.json              # Configuration
│   └── backend.csproj                # Project file
│
└── frontend/                         # Next.js Frontend
    ├── app/                          # Next.js app directory
    │   ├── page.tsx                  # Home page
    │   ├── layout.tsx                # Root layout
    │   └── globals.css               # Global styles
    ├── src/
    │   ├── components/               # React components
    │   ├── services/                 # API services
    │   │   ├── auth.service.ts
    │   │   ├── user.service.ts
    │   │   └── guidance-request.service.ts
    │   ├── hooks/                    # Custom React hooks
    │   │   └── useAuth.ts            # Authentication hook
    │   ├── lib/                      # Utilities
    │   │   └── http-client.ts        # Axios HTTP client
    │   ├── types/                    # TypeScript interfaces
    │   ├── constants/                # Constants
    │   │   └── api.ts                # API endpoints
    │   └── styles/                   # Tailwind & custom styles
    ├── package.json
    ├── tsconfig.json
    └── README.md
```

## 🚀 Backend Setup

### Prerequisites
- .NET 9.0 SDK
- SQL Server or LocalDB
- Visual Studio 2022 or VS Code with C# extension

### Installation Steps

1. **Navigate to backend directory**
   ```bash
   cd backend
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Configure Database Connection**
   - Edit `appsettings.json` to set your connection string
   - Default: `(localdb)\MSSQLLocalDB`

4. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the backend**
   ```bash
   dotnet run
   ```

The backend API will be available at `http://localhost:8080` with Swagger documentation at `http://localhost:8080/swagger`.

### Configuration Files

**appsettings.json** - Contains:
- Database connection string
- JWT settings (secret key, issuer, audience, expiration)
- Logging configuration
- CORS settings

## 🎨 Frontend Setup

### Prerequisites
- Node.js 18.x or higher
- npm, yarn, pnpm, or bun

### Installation Steps

1. **Navigate to frontend directory**
   ```bash
   cd frontend
   ```

2. **Install dependencies**
   ```bash
   npm install
   # or
   yarn install
   # or
   pnpm install
   ```

3. **Configure API endpoints**
   - Edit `src/constants/api.ts` to set your backend URL
   - Default: `http://localhost:8080`

4. **Run development server**
   ```bash
   npm run dev
   # or
   yarn dev
   # or
   pnpm dev
   ```

Open [http://localhost:3000](http://localhost:3000) with your browser to see the application.

### Build for Production
```bash
npm run build
npm start
```

## 📡 API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user (student/mentor)
- `POST /api/auth/login` - Login user
- `POST /api/auth/refresh` - Refresh access token

### Users
- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `PUT /api/users/{id}` - Update user profile
- `DELETE /api/users/{id}` - Delete user

### Guidance Requests
- `GET /api/guidancerequests` - Get all guidance requests
- `GET /api/guidancerequests/{id}` - Get specific guidance request
- `POST /api/guidancerequests` - Create new guidance request (Student)
- `PUT /api/guidancerequests/{id}` - Update guidance request
- `POST /api/guidancerequests/{id}/accept` - Accept guidance request (Mentor)
- `POST /api/guidancerequests/{id}/close` - Close guidance request

## ✨ Features

### Current Features
- ✅ User Registration (Student/Mentor)
- ✅ User Authentication with JWT
- ✅ Token Refresh Mechanism
- ✅ Create Guidance Requests
- ✅ Accept Guidance Requests
- ✅ Manage User Profiles
- ✅ View Guidance Request History
- ✅ Role-Based Access Control

### Coming Soon
- 🔄 Real-time Notifications
- 💬 Chat/Messaging System
- ⭐ Ratings and Reviews
- 📊 Analytics Dashboard
- 📱 Mobile App

## 🏃 Running the Application

### Option 1: Run Separately
**Terminal 1 - Backend:**
```bash
cd backend
dotnet run
```

**Terminal 2 - Frontend:**
```bash
cd frontend
npm run dev
```

### Option 2: Using Docker (Optional)
Create a docker-compose file to run both services with a containerized SQL Server.

## 📝 Database Schema

### Users Table
- `Id` (PK)
- `FullName`
- `Email` (Unique)
- `PasswordHash`
- `PasswordSalt`
- `Role` (student/mentor/admin)
- `Expertise` (for mentors)
- `Bio`
- `IsAvailable`
- `CreatedAt`

### GuidanceRequests Table
- `Id` (PK)
- `Title`
- `Description`
- `Status` (pending/accepted/closed)
- `StudentId` (FK)
- `MentorId` (FK, nullable)
- `CreatedAt`

### RefreshTokens Table
- `Id` (PK)
- `UserId` (FK)
- `Token`
- `ExpiryDate`

## 🔒 Security Features

- JWT-based authentication
- Secure password hashing with salt
- Refresh token rotation
- SQL injection prevention via EF Core
- CORS configuration
- Role-based authorization

## 🐛 Troubleshooting

### Backend Issues
- **Database connection error**: Check `appsettings.json` connection string
- **Port already in use**: Change port in `launchSettings.json`
- **Migration errors**: Run `dotnet ef migrations remove` then reapply migrations

### Frontend Issues
- **API connection errors**: Verify backend is running and check `src/constants/api.ts`
- **Port 3000 in use**: Run `npm run dev -- -p 3001`
- **Module not found**: Run `npm install` again

## 📚 Additional Resources

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/dotnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Next.js Documentation](https://nextjs.org/docs)
- [JWT Authentication](https://jwt.io)
- [Tailwind CSS](https://tailwindcss.com)

## 📄 License

This project is part of a .NET course and may be subject to specific licensing terms.

## 👥 Development Team

Built with ❤️ for the UniConnect Platform

---

**Last Updated**: March 2026
**Version**: 0.1.0
