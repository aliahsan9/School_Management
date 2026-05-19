# School Management System - Backend API

A **multi-tenant School Management System** built with **ASP.NET Core Web API**, **Entity Framework Core**, and **Clean Architecture principles**.  
This backend provides secure, scalable APIs for managing students, teachers, classes, attendance, fees, subjects, and more.

---

## Table of Contents
- Overview
- Features
- Tech Stack
- Architecture
- Modules
- Setup Instructions
- Configuration
- Database Migrations
- API Authentication
- API Endpoints
- Common Issues
- Future Improvements

---

## Overview

This backend system is designed to handle complete school operations including:

- Student management
- Teacher management
- Class and subject handling
- Attendance tracking
- Fee management with payments
- Multi-tenant data isolation
- Secure authentication and authorization

It is designed to work seamlessly with an Angular frontend or any REST-based client.

---

## Features

- Clean Architecture (Domain, Application, Infrastructure, API)
- Multi-tenant support (Tenant-based data isolation)
- JWT Authentication & Authorization
- Role-based access control (Admin, Staff, etc.)
- RESTful APIs
- Entity Framework Core with SQL Server
- DTO-based request/response separation
- Attendance tracking system
- Fee tracking with payment support
- Scalable service-based structure

---

## Tech Stack

- ASP.NET Core Web API (.NET 7 / .NET 8)
- Entity Framework Core
- SQL Server
- JWT Authentication
- LINQ
- Swagger (API testing)
- Dependency Injection

---

## Architecture

The project follows **Clean Architecture**:

SchoolManagement
│
├── SchoolManagement.Domain
│ └── Entities, Enums, Common Base Classes
│
├── SchoolManagement.Application
│ └── DTOs, Interfaces, Business Logic Contracts
│
├── SchoolManagement.Infrastructure
│ └── EF Core DbContext, Services, Repository Implementations
│
└── SchoolManagement.API
└── Controllers, Middleware, Program.cs


---

## Modules

### Student Module
- Create Student
- Update Student
- Delete Student
- Get All Students
- Get Student By ID

### Teacher Module
- CRUD operations
- Subject assignment (if implemented)

### Cla
ss Module
- Class creation
- Teacher assignment

### Subject Module
- Subject management

### Attendance Module
- Mark attendance for class
- Get class attendance by date
- Get student attendance history

### Fee Module
- Create fee record
- View fee status
- Calculate paid / remaining amount
- Track payments

---

## Setup Instructions

### 1. Clone Repository

```bash

git clone https://github.com/your-repo/school-management-backend.git
cd school-management-backend

2. Install Dependencies

Ensure you have:

.NET SDK installed
SQL Server running
3. Configure Database

Update appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY",
    "Issuer": "SchoolAPI",
    "Audience": "SchoolClient"
  }
}

Database Migrations

Run the following commands:

dotnet ef migrations add InitialCreate
dotnet ef database update

If schema changes occur:

dotnet ef migrations add UpdatedSchema
dotnet ef database update

