# Parking Management System

Technical test developed for the Semi Senior Developer selection process.

---

# Project Overview

This application allows the management of a parking lot system through a modern web application built with Angular and .NET 8.

The system supports:

- Vehicle entry registration
- Vehicle exit registration
- Parking fee calculation
- Active vehicle monitoring
- Dashboard statistics
- REST API integration
- Responsive user interface
- Relational database design with MySQL

The project was developed applying clean architecture principles, SOLID practices, clean code standards, and scalable project organization.

---

# Technologies Used

## Frontend
- Angular 20
- Angular Material
- RxJS
- Reactive Forms
- Standalone Components

## Backend
- .NET 8 Web API
- Entity Framework Core
- Swagger/OpenAPI
- Dependency Injection

## Database
- MySQL

## Version Control
- Git
- GitHub

---

# Project Architecture

## Backend Structure

The backend follows a layered architecture approach:

- ParkingApp.Api
- ParkingApp.Application
- ParkingApp.Domain
- ParkingApp.Infrastructure

### Responsibilities

#### ParkingApp.Api
Handles controllers, dependency injection configuration, Swagger, and application startup.

#### ParkingApp.Application
Contains DTOs, interfaces, and application contracts.

#### ParkingApp.Domain
Contains entities, enums, constants, helpers, and business rules.

#### ParkingApp.Infrastructure
Contains Entity Framework implementation, services, persistence logic, and external integrations.

---

# Frontend Structure

The frontend was organized using feature-based architecture:

- Core
- Shared
- Features
- Services
- Layouts

The application uses Angular Standalone Components and Angular Material for a clean and maintainable UI structure.

---

# Features

## Dashboard
Displays real-time parking statistics:
- Active vehicles
- Cars count
- Motorcycles count
- Estimated revenue

---

## Vehicle Entry Registration

Allows registering:
- Vehicle type
- Plate number
- Entry date and time

Validations included:
- Required fields
- Duplicate active vehicle prevention
- Plate format validation

---

## Active Vehicles

Displays:
- Active vehicles list
- Vehicle type
- Entry date
- Quick exit registration actions

---

## Vehicle Exit Registration

Allows:
- Vehicle exit registration
- Automatic parking time calculation
- Automatic payment calculation

Business rule:
- $50 COP per minute

---

# Business Rules

- Only one active parking record per plate is allowed
- Exit time is generated automatically
- Payment is calculated based on total minutes
- Vehicle plates are normalized to uppercase
- Plate validation is enforced on backend and frontend

---

# API Endpoints

## Register Vehicle Entry

POST

```http
/api/vehicles/entry
```

Example body:

```json
{
  "plate": "ABC123",
  "vehicleType": 1
}
```

VehicleType values:
- 1 = Car
- 2 = Motorcycle

---

## Register Vehicle Exit

POST

```http
/api/vehicles/exit/{plate}
```

---

## Get Active Vehicles

GET

```http
/api/vehicles/active
```

---

## Get Dashboard Statistics

GET

```http
/api/vehicles/dashboard-stats
```

---

# Database Configuration

## Create Database

```sql
CREATE DATABASE parking_db;
```

---

## Execute SQL Script

Run:

```text
database.sql
```

The script contains:
- Table creation
- Indexes
- Relational structure

---

# Backend Setup

Navigate to backend folder:

```bash
cd backend
```

Restore dependencies:

```bash
dotnet restore
```

Run project:

```bash
dotnet run --project ParkingApp.Api
```

Backend URL:

```text
http://localhost:5205
```

Swagger URL:

```text
http://localhost:5205/swagger
```

---

# Frontend Setup

Navigate to frontend folder:

```bash
cd frontend
```

Install dependencies:

```bash
npm install
```

Run Angular application:

```bash
ng serve
```

Frontend URL:

```text
http://localhost:4200
```

---

# Email API Integration

The external email API integration was successfully implemented using token-based authentication and HTTP requests.
The API responds successfully (HTTP 200) during vehicle exit processing.

External API used:

https://dev-sites.similtech.co/api-email/swagger/index.html
---
# Validations and Error Handling

The application includes:

- FluentValidation for request validation
- Global exception middleware
- HTTP status code handling
- Duplicate vehicle prevention
- Invalid plate format validation
- Clean API error responses

  
# Technical Highlights

- Clean Architecture approach
- SOLID principles
- Feature-based frontend architecture
- Angular Standalone Components
- Angular Material UI
- DTO pattern
- Service layer separation
- RESTful API design
- Exception handling
- MySQL relational database
- Entity Framework Core
- Dependency Injection
- Responsive interface

---


# Repository

GitHub repository:

```text
https://github.com/SantiagoOliveros/parking-management-system
```

---

# Author

Developed by:

Santiago Oliveros
