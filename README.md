# Hospital Management System
Welcome to the Hospital Management System project. This application is designed to streamline hospital operations and enhance patient care.
## Table of Contents
1. [Features](#features)
2. [Technologies Used](#technologies-used)
3. [Installation & Setup](#installation--setup)
4. [Contribution](#contribution)

## Features

This application provides the following features:

### User Management ###
   - Role-based access control (RBAC) with different levels of permissions for administrators, doctors, and patients.
   - Secure authentication using JWT tokens.
   - Profile management for users, including personal details and contact information.

### Appointment Management ###
   - Schedule, update, and cancel appointments.
   - Notifications for upcoming appointments via email or SMS.
   - Overlapping and duplicate appointment prevention.
   - Doctor availability check and appointment status updates.

### Department Management ###
   - Management of hospital departments and their associated doctors.
   - Display of department-specific information.

### Background Jobs ###
   - Scheduling and execution of background tasks using Quartz.NET.

### Onion Architecture ###
   - Separation of concerns through layered architecture, promoting maintainability and scalability.
   - Integration with various external systems and services.

## Technologies used

The key technologies used include:

- C#
- Microsoft SQL Server
- ASP.NET Core
- Entity Framework Core
- Onion Architecture
- FluentValidation
- AutoMapper
- CQRS and MediatR
- Quartz.NET
- MassTransit
- JWT Authentication
- Stripe
- Swagger
- Postman
- Serilog
- Docker
- Azure Blob Storage
- RabbitMQ
- Git

## Installation & Setup

To get started with the project, make sure you have the required tools and dependencies installed.

### Installation ###

1.Clone this repository: 
- ```bash
  git clone https://github.com/i-revan/Hospital-Management-System.git

2. Navigate to the project directory:
- ```bash
  cd Hospital-Management-System

3. Install Dependencies
- ```bash
  dotnet restore

### Database Setup ###
1. Configure the connection string in appsettings.json.
2. Run the migrations:
- ```bash
  dotnet ef database update

### Run the Application ###
- ```bash
  dotnet run

### Swagger UI ###
Visit the following link for API documentation and testing: [https://localhost:5001/swagger](https://localhost:5001/swagger)

## Contribution ##
Contributions are welcome. Please create an issue or pull request if you'd like to contribute to this project.




