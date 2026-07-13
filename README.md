
---

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

## 🚧 Current Status

| Item | Status |
|------|--------|
| Current Sprint | 🔄 Sprint 11 – JWT Authentication |
| Project Status | 🚀 Active Development |
| Current Focus | Secure user authentication using JWT |
| Next Sprint | Sprint 12 – Role-Based Authorization |

---

# 📖 About This Project

Enterprise Inventory Management API is a **production-inspired learning project** built using **ASP.NET Core 10 (.NET 10)** and Clean Architecture principles.

Unlike tutorial-based applications that primarily focus on implementing features, this project emphasizes
  understanding the architectural reasoning behind each implementation decision.

The application is being developed incrementally through well-defined sprints. Each sprint introduces enterprise concepts, enabling a deep understanding of software architecture, design patterns, performance optimization, validation, security, logging, monitoring, and production-ready backend development.

The objective is not only to build a complete REST API but also to develop the mindset, coding standards, and engineering practices followed by experienced enterprise software developers.

---

## 🎯 Project Objectives

This project aims to:

- Build a production-ready ASP.NET Core 10 (.NET 10) REST API
- Apply Clean Architecture principles to build maintainable applications
- Follow SOLID principles and enterprise design patterns
- Build efficient data access using Entity Framework Core and SQL Server
- Implement Repository Pattern and Dependency Injection
- Implement centralized request validation using FluentValidation
- Implement object mapping using AutoMapper
- Implement global exception handling and standardized API responses
- Implement structured enterprise logging using Serilog
- Build secure APIs using Authentication and Authorization
- Understand performance optimization and scalability
- Follow professional Git and GitHub workflows
- Follow enterprise software engineering best practices

---

## ✨ Features Implemented

### ✅ Sprint 1 – Solution Architecture

* Enterprise solution structure
* Layered project organization

### ✅ Sprint 2 – Clean Architecture

* Domain, Application, Infrastructure, and API layers
* Separation of concerns

### ✅ Sprint 3 – Dependency Injection

* Built-in ASP.NET Core Dependency Injection
* Service registration through interfaces

### ✅ Sprint 4 – Entity Framework Core & SQL Server

* DbContext configuration
* SQL Server integration
* Entity configurations
* Database migrations

### ✅ Sprint 5 – Repository Pattern

* Repository abstraction
* Repository implementation
* Data access encapsulation

### ✅ Sprint 6 – CRUD APIs Foundation

* Initial CRUD API foundation
* Service and repository integration
* DTO-based request and response flow

### ✅ Sprint 7 – Enterprise CRUD APIs

* Product CRUD operations
* RESTful API implementation
* Improved enterprise API structure

### ✅ Sprint 8 – FluentValidation

* FluentValidation integration
* Automatic model validation
* Create and Update request validators
* Separation of model validation and business rules
* Business rule validation for duplicate product names

### ✅ Sprint 9 – AutoMapper & Mapping Strategies

* AutoMapper integration
* Mapping profiles
* Entity-to-DTO mapping
* Request DTO-to-entity mapping
* Separation of mapping concerns from business logic

### ✅ Sprint 10 – Global Exception Handling & Middleware

* Global exception handling middleware
* Custom application exceptions
* Standardized API error responses
* HTTP status code mapping
* TraceId correlation for troubleshooting

### ✅ Sprint 10.1 – Enterprise Logging & Monitoring

* Serilog integration
* Structured application logging
* Console logging
* Rolling file logging
* SQL Server logging
* Log-level configuration
* Exception logging with TraceId correlation

---

## 🔄 Currently In Progress

### Sprint 11 – JWT Authentication

* User authentication architecture
* User registration
* Secure password hashing
* User login
* JWT token generation
* JWT token validation
* Authentication middleware
* Authenticated user identity and claims

#### ✅ Commit 1 Completed – User Registration
- User authentication architecture
- User entity and database schema
- User registration endpoint
- User repository implementation
- Password hashing using ASP.NET Core Identity PasswordHasher
- Registration request & response DTOs
- AutoMapper configuration
- Dependency Injection registration
- Duplicate username validation
- Duplicate email validation

#### 🔄 Commit 2 – Login & JWT Generation (In Progress)

- User Login
- JWT Token Generation
- JWT Validation
- Authentication Middleware
- Claims-based Identity
---

> 🚀 Upcoming enterprise features include Role-Based Authorization, Refresh Tokens, Generic Repository discussions and trade-offs, Unit of Work, Pagination, Advanced EF Core Performance, Transactions, Optimistic Concurrency, Redis, Background Services, RabbitMQ, Docker, Azure Deployment, CI/CD, Production Readiness, and an Enterprise React Frontend.
