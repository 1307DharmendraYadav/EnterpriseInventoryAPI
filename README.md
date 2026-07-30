# 🚀 Enterprise Inventory Management API

> **A production-inspired ASP.NET Core 10.0 Web API built using Clean Architecture, Entity Framework Core, SQL Server, FluentValidation, AutoMapper, Serilog, JWT Authentication, and enterprise software engineering best practices.**

![.NET](https://img.shields.io/badge/.NET-10.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-10.0-blue)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-10.0-green)
![SQL Server](https://img.shields.io/badge/SQL-Server-red)
![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-success)
![FluentValidation](https://img.shields.io/badge/Validation-FluentValidation-orange)
![AutoMapper](https://img.shields.io/badge/Mapping-AutoMapper-blue)
![Serilog](https://img.shields.io/badge/Logging-Serilog-blue)
![JWT](https://img.shields.io/badge/Security-JWT-orange)
![RBAC](https://img.shields.io/badge/Authorization-RBAC-red)
![License](https://img.shields.io/badge/License-MIT-lightgrey)

---

# 🚧 Current Status

| Item                  | Status                                                        |
| --------------------- | ------------------------------------------------------------- |
| Current Sprint        | 🚧 Sprint 12B – Role & Permission Management APIs             |
| Last Completed Sprint | ✅ Sprint 12A – Enterprise Permission-Based Authorization      |
| Project Status        | 🚀 Active Development                                         |
| Current Focus         | Completing Permission, User-Role & Role-Permission Management APIs |

### Sprint 12B Progress

| Capability                      | Status      |
| ------------------------------- | ----------- |
| Role CRUD APIs                  | ✅ Completed |
| Role Service                    | ✅ Completed |
| Role Repository                 | ✅ Completed |
| Role DTOs                       | ✅ Completed |
| Role Validation                 | ✅ Completed |
| Role AutoMapper Profile         | ✅ Completed |
| Permission-Protected Role APIs  | ✅ Completed |
| Permission Management APIs      | ⏳ Pending   |
| User Role Assignment APIs       | ⏳ Pending   |
| Role Permission Assignment APIs | ⏳ Pending   |

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

* What problem it solves
* Why it exists
* Why enterprise applications use it
* How it should be implemented
* What trade-offs it introduces

This project aims to:

* Build a production-ready ASP.NET Core 10 REST API
* Apply Clean Architecture principles
* Follow SOLID principles
* Implement enterprise design patterns
* Build scalable REST APIs
* Use Entity Framework Core with SQL Server
* Implement Repository Pattern
* Implement Dependency Injection
* Implement FluentValidation
* Implement AutoMapper
* Implement Global Exception Handling
* Implement Enterprise Logging using Serilog
* Build secure APIs using JWT Authentication & Authorization
* Implement Role-Based Access Control (RBAC)
* Implement dynamic permission-based authorization
* Develop enterprise security architecture
* Learn production-ready backend architecture
* Follow professional Git & GitHub workflow

---

# 🏗 Architecture

This project follows **Clean Architecture** to achieve maintainability, scalability, testability, and separation of concerns.

```text
                         ┌──────────────────────────┐
                         │       API Layer          │
                         │ EnterpriseInventory.API │
                         │                          │
                         │ Controllers              │
                         │ Middleware               │
                         │ Authentication           │
                         │ Authorization            │
                         │ Swagger                  │
                         └────────────┬─────────────┘
                                      │
                                      ▼
                         ┌──────────────────────────┐
                         │   Application Layer      │
                         │                          │
                         │ Business Logic           │
                         │ DTOs                     │
                         │ Validators               │
                         │ Services                 │
                         │ Interfaces               │
                         │ AutoMapper Profiles      │
                         │ Authorization Policies   │
                         └────────────┬─────────────┘
                                      │
                                      ▼
                         ┌──────────────────────────┐
                         │      Domain Layer        │
                         │                          │
                         │ Entities                 │
                         │ Core Business Rules      │
                         │ Domain Models             │
                         └──────────────────────────┘
                                      ▲
                                      │
                         ┌────────────┴─────────────┐
                         │ Infrastructure Layer     │
                         │                          │
                         │ EF Core                  │
                         │ SQL Server               │
                         │ Repositories             │
                         │ Authentication Services  │
                         │ Password Hashing          │
                         │ JWT Generation            │
                         │ Logging                  │
                         └──────────────────────────┘
```

## Layer Responsibilities

### API Layer

Responsible for presentation and HTTP concerns.

* Controllers
* Middleware
* Authentication configuration
* Authorization configuration
* Swagger/OpenAPI
* API response handling
* Dependency Injection composition root

### Application Layer

Responsible for application business logic and use cases.

* Application services
* DTOs
* Interfaces
* Validators
* AutoMapper Profiles
* Authorization policies
* Authorization requirements
* Authorization handlers
* Application-level exceptions

### Domain Layer

Responsible for the core domain model.

* Entities
* Domain models
* Core business rules
* Domain-level abstractions

### Infrastructure Layer

Responsible for external systems and technical implementations.

* Entity Framework Core
* SQL Server
* Repository implementations
* Database configurations
* Authentication implementations
* Password hashing
* JWT token generation
* Logging infrastructure
* External infrastructure dependencies

---

# 🧩 Architectural Patterns & Principles

The project currently follows and demonstrates several enterprise software engineering concepts.

## Clean Architecture

Separates:

```text
Presentation
    ↓
Application
    ↓
Domain
    ↑
Infrastructure
```

The goal is to keep business logic independent from infrastructure and presentation concerns.

---

## SOLID Principles

The project applies SOLID principles throughout the architecture.

Examples include:

* Interfaces for services and repositories
* Dependency Injection
* Separation of responsibilities
* Dependency inversion
* Small, focused classes
* Abstraction between business logic and infrastructure

---

## Repository Pattern

Repositories encapsulate database access.

Example:

```text
IRoleRepository
      ↓
RoleRepository
      ↓
EF Core / DbContext
      ↓
SQL Server
```

This keeps persistence concerns outside the Application layer.

---

## Dependency Injection

ASP.NET Core built-in Dependency Injection is used throughout the application.

Example:

```text
IRoleService
     ↓
RoleService
```

and:

```text
IRoleRepository
     ↓
RoleRepository
```

---

## DTO Pattern

DTOs are used to prevent API contracts from being tightly coupled to domain entities.

Example:

```text
CreateRoleRequest
UpdateRoleRequest
RoleResponse
```

---

## AutoMapper

AutoMapper is used to separate object mapping concerns from business logic.

Example:

```text
CreateRoleRequest → Role

UpdateRoleRequest → Role

Role → RoleResponse
```

---

## FluentValidation

Request validation is implemented using FluentValidation.

Example:

```text
CreateRoleRequest
       ↓
CreateRoleRequestValidator
       ↓
Validation Result
```

Validation rules are kept separate from controllers and services.

---

## Global Exception Handling

The API uses centralized exception handling through middleware.

```text
Service
   ↓
Exception
   ↓
Global Exception Middleware
   ↓
Standardized API Response
```

This avoids duplicating exception handling logic across controllers.

---

## Enterprise Logging

Serilog is used for structured enterprise logging.

Logging includes:

* Console logging
* Rolling file logging
* SQL Server logging
* Structured logging
* Exception logging
* TraceId correlation

---

# 🔐 Authentication & Authorization Architecture

The security architecture is divided into two major concepts:

```text
Authentication
      ↓
"Who are you?"

Authorization
      ↓
"What are you allowed to do?"
```

---

# 🔐 Sprint 11 – JWT Authentication ✅ Completed

Sprint 11 introduced enterprise JWT-based authentication.

## Features Implemented

* User Authentication Architecture
* User Registration
* Password Hashing
* User Login
* Password Verification
* JWT Token Generation
* JWT Validation
* Authentication Middleware
* Claims-based Identity
* Swagger JWT Integration
* Controller-level Authorization
* Enterprise JWT Configuration using Options Pattern
* Token Expiration Handling
* Enterprise JWT Claims
* `JwtTokenGenerator`
* `JwtTokenResult`

---

## Sprint 11 Commit History

### ✅ Commit 1 – User Registration

Implemented:

* User authentication architecture
* User entity
* User repository
* User registration endpoint
* Password hashing
* Registration DTOs
* AutoMapper
* Dependency Injection
* Duplicate username validation
* Duplicate email validation

---

### ✅ Commit 2 – Login & JWT Authentication

Implemented:

* User Login
* Password Verification
* JWT Token Generation
* JWT Validation
* Authentication Middleware
* Claims-based Identity
* Swagger Bearer Authentication
* Enterprise `JwtTokenGenerator`
* Enterprise `JwtTokenResult`
* Enterprise JWT Claims
* Controller-level Authorization
* Token expiration handling

---

# 🔐 Sprint 12 – Enterprise Authorization

Sprint 12 extends authentication into enterprise authorization.

The main objective is to implement **Role-Based Access Control (RBAC)** and dynamic permission-based authorization.

---

# ✅ Sprint 12A – Enterprise Permission-Based Authorization

Sprint 12A established the **authorization engine**.

The focus of Sprint 12A was:

> **"Can this authenticated user perform this operation?"**

## Features Implemented

* Enterprise Role-Based Access Control (RBAC)
* Dynamic Permission-Based Authorization
* Role Entity
* Permission Entity
* UserRole Entity
* RolePermission Entity
* EF Core Configurations
* Security Database Seeders
* JWT Permission Claims
* Dynamic Policy Provider
* Custom Authorization Handler
* Custom Permission Attribute
* Permission-Protected Product APIs
* Repository support for loading user permissions

---

## Sprint 12A Authorization Flow

```text
User Login
    ↓
JWT Token
    ↓
User Claims
    ↓
Permission Claims
    ↓
[HasPermission("Product.Create")]
    ↓
Dynamic Policy Provider
    ↓
Permission Requirement
    ↓
Permission Authorization Handler
    ↓
Access Granted / Denied
```

---

## Sprint 12A Commit History

### ✅ Commit 1 – Enterprise Permission-Based Authorization

Implemented:

* Role entity
* Permission entity
* UserRole entity
* RolePermission entity
* EF Core configurations
* Security seeders
* Permission constants
* Permission requirement
* Permission authorization handler
* Dynamic policy provider
* Custom `HasPermission` attribute
* JWT permission claims
* Repository support for loading user permissions
* Dependency Injection registration
* Permission-protected Product APIs

---

# 🚧 Sprint 12B – Role & Permission Management APIs

Sprint 12B focuses on the **management side of RBAC**.

While Sprint 12A established the authorization engine, Sprint 12B provides APIs to manage the authorization data.

The distinction is:

```text
Sprint 12A
Authorization Engine
        ↓
"Can this user perform this action?"

Sprint 12B
Authorization Management
        ↓
"How do we manage Roles and Permissions?"
```

---

# ✅ Sprint 12B – Role Management Implemented

The first Sprint 12B milestone implements **Role Management APIs**.

## Role Management Features

### Role CRUD APIs

Implemented:

* Get all roles
* Get role by ID
* Create role
* Update role
* Delete role

---

## Role Architecture

```text
RoleController
      ↓
IRoleService
      ↓
RoleService
      ↓
IRoleRepository
      ↓
RoleRepository
      ↓
Entity Framework Core
      ↓
SQL Server
```

---

## Role DTOs

Implemented:

```text
CreateRoleRequest
UpdateRoleRequest
RoleResponse
```

This keeps the API contract separate from the `Role` domain entity.

---

## Role Validation

Implemented:

```text
CreateRoleRequestValidator
UpdateRoleRequestValidator
```

Validation includes:

* Required role name
* Maximum role name length
* FluentValidation integration
* Cascade validation behavior

---

## Role AutoMapper Profile

Implemented:

```text
RoleProfile
```

Mappings include:

```text
CreateRoleRequest → Role

UpdateRoleRequest → Role

Role → RoleResponse
```

---

## Role Authorization

Role APIs are protected using permission-based authorization.

Permissions include:

```text
Role.View
Role.Create
Role.Update
Role.Delete
```

Example:

```csharp
[HasPermission(PermissionConstants.Role.View)]
```

This ensures that creating management APIs does not bypass the authorization architecture introduced in Sprint 12A.

---

## Role Controller

The `RoleController` is protected with authentication and permission-based authorization.

```text
[Authorize]
      ↓
[HasPermission(...)]
      ↓
RoleController
```

Authentication verifies that the caller is authenticated.

Permission authorization verifies whether the caller has the required permission.

---

## Role Dependency Injection

Role services and repositories are registered through Dependency Injection.

```text
IRoleService → RoleService

IRoleRepository → RoleRepository
```

This maintains loose coupling and follows the Dependency Inversion Principle.

---

# ⏳ Sprint 12B – Remaining Work

The following capabilities are still planned for Sprint 12B:

## Permission Management APIs

Planned:

* Get all permissions
* Get permission by ID
* Create permission
* Update permission
* Delete permission

---

## User Role Assignment APIs

Planned:

* Assign role to user
* Remove role from user
* Get roles assigned to a user
* Manage user-role relationships

---

## Role Permission Assignment APIs

Planned:

* Assign permission to role
* Remove permission from role
* Get permissions assigned to a role
* Manage role-permission relationships

---

# 🔒 RBAC Target Architecture

After Sprint 12B is completed, the expected authorization management flow will be:

```text
                    ┌──────────────┐
                    │     User     │
                    └──────┬───────┘
                           │
                           │ UserRole
                           ▼
                    ┌──────────────┐
                    │     Role     │
                    └──────┬───────┘
                           │
                           │ RolePermission
                           ▼
                    ┌──────────────┐
                    │  Permission  │
                    └──────────────┘
```

Authorization:

```text
User
 ↓
Roles
 ↓
Permissions
 ↓
JWT Permission Claims
 ↓
HasPermission
 ↓
Dynamic Policy
 ↓
Authorization Handler
 ↓
API Access
```

Management:

```text
Role Management APIs
        ↓
Permission Management APIs
        ↓
User Role Assignment APIs
        ↓
Role Permission Assignment APIs
```

---

# 🧪 Validation & Quality Checks

Before committing changes, the project follows basic quality checks such as:

```bash
dotnet build
```

Git validation:

```bash
git diff --cached --check
```

The project also contains unit and integration test projects.

```text
EnterpriseInventory.UnitTests
EnterpriseInventory.IntegrationTests
```

---

# 🌿 Git Branching Strategy

The project uses feature/sprint-based Git branches.

Examples:

```text
main

feature/sprint-8-fluentvalidation
feature/sprint-9-automapper
feature/sprint-10-enterprise-logging
feature/sprint-11-jwt-authentication
feature/sprint-12-role-based-authorization

sprint-12b-role-based-access-control
```

The project follows a commit-oriented development approach where significant architectural or functional changes are kept in separate commits.

Example:

```text
feat(rbac): implement role-based access control management
```

Documentation changes are maintained separately when appropriate.

---

# 📚 Sprint-by-Sprint Learning Approach

The project is intentionally developed incrementally.

Each sprint focuses on understanding:

```text
Problem
   ↓
Why the problem exists
   ↓
Enterprise solution
   ↓
Design decision
   ↓
Implementation
   ↓
Testing
   ↓
Git Commit
   ↓
Documentation
```

This approach ensures that the project is not simply a collection of APIs but a practical study of enterprise backend architecture.

---

# 🛣 Project Roadmap

| Sprint                                                 | Status         |
| ------------------------------------------------------ | -------------- |
| Sprint 1 – Solution Architecture                       | ✅ Completed    |
| Sprint 2 – Clean Architecture                          | ✅ Completed    |
| Sprint 3 – Dependency Injection                        | ✅ Completed    |
| Sprint 4 – Entity Framework Core                       | ✅ Completed    |
| Sprint 5 – Repository Pattern                          | ✅ Completed    |
| Sprint 6 – CRUD Foundation                             | ✅ Completed    |
| Sprint 7 – Enterprise CRUD APIs                        | ✅ Completed    |
| Sprint 8 – FluentValidation                            | ✅ Completed    |
| Sprint 9 – AutoMapper                                  | ✅ Completed    |
| Sprint 10 – Global Exception Handling                  | ✅ Completed    |
| Sprint 10.1 – Enterprise Logging                       | ✅ Completed    |
| Sprint 11 – JWT Authentication                         | ✅ Completed    |
| Sprint 12A – Enterprise Permission-Based Authorization | ✅ Completed    |
| Sprint 12B – Role & Permission Management APIs         | 🚧 In Progress |
| Sprint 13 – Refresh Tokens                             | ⏳ Planned      |
| Sprint 14 – Generic Repository Discussion              | ⏳ Planned      |
| Sprint 15 – Unit of Work                               | ⏳ Planned      |
| Sprint 16 – Pagination                                 | ⏳ Planned      |
| Sprint 17 – Advanced EF Core Performance               | ⏳ Planned      |
| Sprint 18 – Transactions                               | ⏳ Planned      |
| Sprint 19 – Optimistic Concurrency                     | ⏳ Planned      |
| Sprint 20 – Redis Caching                              | ⏳ Planned      |
| Sprint 21 – Background Services                        | ⏳ Planned      |
| Sprint 22 – RabbitMQ                                   | ⏳ Planned      |
| Sprint 23 – Docker                                     | ⏳ Planned      |
| Sprint 24 – Azure Deployment                           | ⏳ Planned      |
| Sprint 25 – CI/CD                                      | ⏳ Planned      |
| Sprint 26 – Production Readiness                       | ⏳ Planned      |
| Sprint 27 – Enterprise React Frontend                  | ⏳ Planned      |

---

# 🏆 Enterprise Concepts Covered So Far

The project currently demonstrates:

* Clean Architecture
* SOLID Principles
* Dependency Injection
* Repository Pattern
* REST API Design
* DTO Pattern
* Entity Framework Core
* SQL Server
* FluentValidation
* AutoMapper
* Global Exception Handling
* Custom Exceptions
* Enterprise Logging
* Serilog
* TraceId Correlation
* JWT Authentication
* Claims-Based Identity
* Options Pattern
* Role-Based Access Control
* Permission-Based Authorization
* Dynamic Authorization Policies
* Custom Authorization Requirements
* Custom Authorization Handlers
* Custom Permission Attributes
* Security Seeders
* Feature-oriented organization
* Professional Git branching
* Commit-oriented development
* API documentation with Swagger/OpenAPI

---

# 👨‍💻 Development Philosophy

This project is intentionally developed **like a real enterprise application**, where every sprint introduces a significant architectural, security, database, or backend engineering concept and explains both **how it is implemented and why it is designed that way**.

The goal is not simply to complete features or build CRUD endpoints.

The goal is to understand:

> **Why enterprise applications are designed this way, what problems each architectural decision solves, where each responsibility belongs, and what engineering trade-offs those decisions introduce.**

The project follows an incremental, sprint-based engineering approach:

```text
Problem
   ↓
Why the problem exists
   ↓
Enterprise solution
   ↓
Architectural / Design Decision
   ↓
Implementation
   ↓
Validation & Testing
   ↓
Git Commit
   ↓
Documentation
```

Each sprint therefore focuses on developing both **technical implementation skills and engineering judgment**.

The project emphasizes:

* Clean Architecture
* SOLID principles
* Separation of concerns
* Maintainability
* Security
* Scalability
* Testability
* Performance
* Enterprise design patterns
* Production-oriented coding practices
* Database architecture
* API design
* Professional Git workflow
* Incremental and commit-oriented development
* Production readiness

The objective is to build more than a collection of APIs; this repository is intended to serve as a **practical study of enterprise backend architecture and software engineering**, progressively evolving from a CRUD-based application toward a production-inspired backend platform.


---

# 🚀 Future Direction

The project will progressively evolve from a basic enterprise CRUD API into a more complete production-inspired backend platform.

Planned areas include:

```text
Authentication
      ↓
Authorization
      ↓
Refresh Tokens
      ↓
Advanced Repository Concepts
      ↓
Pagination
      ↓
EF Core Performance
      ↓
Transactions
      ↓
Concurrency
      ↓
Redis Caching
      ↓
Background Processing
      ↓
RabbitMQ
      ↓
Docker
      ↓
Azure
      ↓
CI/CD
      ↓
Production Readiness
      ↓
React Frontend
```

---

# ⭐ Project Goal

The ultimate goal of **Enterprise Inventory Management API** is to build a realistic enterprise backend while developing a deep understanding of:

**ASP.NET Core + Clean Architecture + SOLID + Design Patterns + Security + Database Architecture + Performance + Distributed Systems + DevOps**

rather than simply learning how to create CRUD endpoints.

---

