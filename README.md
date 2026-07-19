# 🚀 Enterprise Inventory Management API

> **A production-inspired ASP.NET Core 10.0 Web API built using Clean Architecture, Entity Framework Core, SQL Server, FluentValidation, AutoMapper, Serilog, and enterprise software engineering best practices.**

![.NET](https://img.shields.io/badge/.NET-10.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-10.0-blue)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-10.0-green)
![SQL Server](https://img.shields.io/badge/SQL-Server-red)
![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-success)
![FluentValidation](https://img.shields.io/badge/Validation-FluentValidation-orange)
![Serilog](https://img.shields.io/badge/Logging-Serilog-blue)
![License](https://img.shields.io/badge/License-MIT-lightgrey)

---

# 🚧 Current Status

| Item | Status |
|------|--------|
| Current Sprint | 🚧 Sprint 12 – Authorization |
| Last Completed Sprint | ✅ Sprint 11 – JWT Authentication |
| Project Status | 🚀 Active Development |
| Current Focus | Enterprise Authorization (Roles, Policies & Permissions) |

---

# 📖 About This Project

Enterprise Inventory Management API is a **production-inspired learning project** built using **ASP.NET Core 10 (.NET 10)** and **Clean Architecture** principles.

Unlike tutorial-based applications that primarily focus on implementing features, this project emphasizes understanding **why enterprise applications are designed the way they are**, not just how they are implemented.

The project is being developed incrementally through well-defined sprints. Each sprint introduces one enterprise concept and explains the architectural reasoning behind every implementation.

The objective is not only to build a production-ready REST API but also to develop the engineering mindset followed by experienced enterprise software developers.

---

# 🎯 Project Objectives

This repository is intentionally developed sprint-by-sprint to simulate how enterprise software evolves in real-world teams.

Every sprint introduces one major architectural concept and explains:

- What problem it solves
- Why it exists
- Why enterprise applications use it
- How it should be implemented

This project aims to:

- Build a production-ready ASP.NET Core 10 REST API
- Apply Clean Architecture principles
- Follow SOLID principles
- Implement enterprise design patterns
- Build scalable REST APIs
- Use Entity Framework Core with SQL Server
- Implement Repository Pattern
- Implement Dependency Injection
- Implement FluentValidation
- Implement AutoMapper
- Implement Global Exception Handling
- Implement Enterprise Logging using Serilog
- Build secure APIs using JWT Authentication & Authorization
- Learn production-ready backend architecture
- Follow professional Git & GitHub workflow

---

# 🏗 Architecture

This project follows **Clean Architecture** to achieve maintainability, scalability, and separation of concerns.

```text
                 Presentation Layer
             (EnterpriseInventory.API)
                        │
                        ▼
              Application Layer
        (Business Logic & Use Cases)
                        │
                        ▼
                 Domain Layer
          (Entities & Business Rules)
                        ▲
                        │
             Infrastructure Layer
(Database, EF Core, Repositories,
 Authentication, Logging, Security)
```

## Layer Responsibilities

### API
- Controllers
- Middleware
- Authentication
- Authorization
- Swagger
- Dependency Injection Composition Root

### Application
- Business Logic
- DTOs
- Interfaces
- Validators
- AutoMapper Profiles
- Application Services

### Domain
- Entities
- Core Business Rules

### Infrastructure
- EF Core
- SQL Server
- Repositories
- Authentication
- Password Hashing
- JWT Generation
- Logging

---

# ✨ Features Implemented

## ✅ Sprint 1 – Solution Architecture

- Enterprise solution structure
- Layered project organization

---

## ✅ Sprint 2 – Clean Architecture

- Domain Layer
- Application Layer
- Infrastructure Layer
- API Layer
- Separation of Concerns

---

## ✅ Sprint 3 – Dependency Injection

- Built-in ASP.NET Core Dependency Injection
- Service registration through interfaces

---

## ✅ Sprint 4 – Entity Framework Core & SQL Server

- DbContext configuration
- SQL Server integration
- Entity configurations
- Database migrations

---

## ✅ Sprint 5 – Repository Pattern

- Repository abstraction
- Repository implementation
- Data access encapsulation

---

## ✅ Sprint 6 – CRUD API Foundation

- Initial CRUD implementation
- DTO flow
- Service layer
- Repository integration

---

## ✅ Sprint 7 – Enterprise CRUD APIs

- Product CRUD
- RESTful APIs
- Enterprise API structure

---

## ✅ Sprint 8 – FluentValidation

- FluentValidation integration
- Automatic validation
- Create & Update validators
- Business Rule Validation
- Duplicate Product validation

---

## ✅ Sprint 9 – AutoMapper

- AutoMapper integration
- Mapping Profiles
- DTO ↔ Entity mapping
- Separation of mapping concerns

---

## ✅ Sprint 10 – Global Exception Handling

- Global Exception Middleware
- Custom Exceptions
- Standardized API Responses
- HTTP Status Mapping
- TraceId Correlation

---

## ✅ Sprint 10.1 – Enterprise Logging

- Serilog integration
- Console Logging
- Rolling File Logging
- SQL Server Logging
- Structured Logging
- Exception Logging
- TraceId Correlation

---

# 🔐 Sprint 11 – JWT Authentication ✅ Completed

## Features Implemented

- User Authentication Architecture
- User Registration
- Password Hashing
- User Login
- JWT Token Generation
- JWT Validation
- Authentication Middleware
- Claims-based Identity
- Swagger JWT Integration
- Controller-level Authorization
- Enterprise JWT Configuration using Options Pattern
- Token Expiration Handling

---

## Sprint 11 Commit History

### ✅ Commit 1 – User Registration

- User authentication architecture
- User entity
- User repository
- User registration endpoint
- Password hashing
- Registration DTOs
- AutoMapper
- Dependency Injection
- Duplicate username validation
- Duplicate email validation

---

### ✅ Commit 2 – Login & JWT Authentication

- User Login
- Password Verification
- JWT Token Generation
- JWT Validation
- Authentication Middleware
- Claims-based Identity
- Swagger Bearer Authentication
- Enterprise JwtTokenGenerator
- Enterprise JwtTokenResult
- Enterprise JWT Claims
- Controller-level Authorization

---

# 🚀 Sprint 12 – Authorization (Current Sprint)

## Planned Features

- Role-Based Authorization
- Policy-Based Authorization
- Claims-Based Authorization
- Permission-Based Authorization
- Refresh Tokens

---

# 🛣 Project Roadmap

| Sprint | Status |
|---------|--------|
| Sprint 1 – Solution Architecture | ✅ Completed |
| Sprint 2 – Clean Architecture | ✅ Completed |
| Sprint 3 – Dependency Injection | ✅ Completed |
| Sprint 4 – Entity Framework Core | ✅ Completed |
| Sprint 5 – Repository Pattern | ✅ Completed |
| Sprint 6 – CRUD Foundation | ✅ Completed |
| Sprint 7 – Enterprise CRUD APIs | ✅ Completed |
| Sprint 8 – FluentValidation | ✅ Completed |
| Sprint 9 – AutoMapper | ✅ Completed |
| Sprint 10 – Exception Handling | ✅ Completed |
| Sprint 10.1 – Enterprise Logging | ✅ Completed |
| Sprint 11 – JWT Authentication | ✅ Completed |
| Sprint 12 – Authorization | 🚧 In Progress |
| Sprint 13 – Refresh Tokens | ⏳ Planned |
| Sprint 14 – Generic Repository Discussion | ⏳ Planned |
| Sprint 15 – Unit of Work | ⏳ Planned |
| Sprint 16 – Pagination | ⏳ Planned |
| Sprint 17 – Advanced EF Core Performance | ⏳ Planned |
| Sprint 18 – Transactions | ⏳ Planned |
| Sprint 19 – Optimistic Concurrency | ⏳ Planned |
| Sprint 20 – Redis Caching | ⏳ Planned |
| Sprint 21 – Background Services | ⏳ Planned |
| Sprint 22 – RabbitMQ | ⏳ Planned |
| Sprint 23 – Docker | ⏳ Planned |
| Sprint 24 – Azure Deployment | ⏳ Planned |
| Sprint 25 – CI/CD | ⏳ Planned |
| Sprint 26 – Production Readiness | ⏳ Planned |
| Sprint 27 – Enterprise React Frontend | ⏳ Planned |

---

# 👨‍💻 Development Philosophy

This project is intentionally developed **like a real enterprise application**, where every sprint introduces a single architectural concept and explains both the implementation and the reasoning behind it.

The goal is not simply to complete features, but to understand **why enterprise applications are built this way** and to build production-ready software engineering skills.

---