# Sprint 12A — Dynamic Permission-Based Authorization

## Highlights

- Added Role, Permission, UserRole and RolePermission domain entities
- Configured EF Core entity mappings and database migrations
- Seeded default roles, permissions and administrator account
- Added PermissionConstants for centralized permission management
- Implemented PermissionRequirement
- Implemented PermissionAuthorizationHandler
- Added dynamic PermissionPolicyProvider
- Introduced HasPermission authorization attribute
- Enhanced JWT generation with role and permission claims
- Added repository support for loading user permissions
- Integrated authorization services through dependency injection
- Secured Product APIs using permission-based authorization
- Verified authorization flow for:
  - Admin
  - Manager
  - Operator
  - Viewer

## Result

Sprint 12A successfully introduced enterprise-grade dynamic permission-based authorization using ASP.NET Core Authorization Policies, custom authorization handlers, JWT permission claims, and Role-Based Access Control (RBAC) infrastructure.