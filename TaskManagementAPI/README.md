# Task Management API

A RESTful Task Management API built with ASP.NET Core 8 and MySQL following Clean Architecture principles such as Repository Pattern, Service Layer, Dependency Injection, and DTOs.

---

## Features

- Create Task
- Get All Tasks
- Get Task by Id
- Update Task
- Delete Task
- Filter Tasks by Status
- Automatic Task Expiry using Background Service
- Global Exception Handling
- Custom Exception Support
- Logging using ILogger
- Swagger API Documentation

---

## Technologies Used

- ASP.NET Core 8
- Entity Framework Core
- MySQL
- Pomelo MySQL Provider
- Swagger
- Dependency Injection
- Repository Pattern
- Service Layer
- DTO Pattern
- BackgroundService

---

## Project Structure

```
TaskManagementAPI
│
├── BackgroundJobs
├── Controllers
├── Data
├── DTOs
├── Exceptions
├── Interfaces
├── Middleware
├── Models
├── Repositories
├── Services
├── Migrations
├── Program.cs
└── appsettings.json
```

---

## Database

Database Name

```
TaskManagement
```

---

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/Tasks | Get All Tasks |
| GET | /api/Tasks/{id} | Get Task By Id |
| POST | /api/Tasks | Create Task |
| PUT | /api/Tasks/{id} | Update Task |
| DELETE | /api/Tasks/{id} | Delete Task |

---

## Background Job

A hosted BackgroundService runs every one minute.

If:

- Status = Pending
- DueDate < Current DateTime

Then:

```
Status = Expired
```

---

## Exception Handling

Implemented Global Exception Middleware.

Supports:

- BadRequestException
- KeyNotFoundException
- Internal Server Error

---

## Logging

Implemented using ASP.NET Core ILogger.

Logs:

- Task Created
- Task Updated
- Task Deleted
- Background Service Execution

---

## API Documentation

Swagger UI available at

```
https://localhost:xxxx/swagger
```

---

## Author

**Naveenraja S R**

.NET Developer