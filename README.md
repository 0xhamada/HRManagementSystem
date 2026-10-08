# HR Management System

ASP.NET Core MVC application for managing employees, leave, attendance and resignations.

## Features
- Authentication and role-based authorization (Admin / Employee) with ASP.NET Core Identity
- Employee and Department management (CRUD, soft delete)
- Email-based account activation and password reset
- Leave management with an approval workflow
- Attendance check-in / check-out
- Resignation requests with admin approval
- AJAX profile modal

## Tech Stack
ASP.NET Core MVC (.NET 10), C#, Entity Framework Core, SQL Server, ASP.NET Core Identity, AutoMapper, AdminLTE 4 / Bootstrap, MailKit

## Architecture
3-tier: PL (Presentation) → BLL (Business Logic) → DAL (Data Access)

