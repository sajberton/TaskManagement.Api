TaskManagement API Documentation

Project Overview

TaskManagement is a clean architecture-based API for managing tasks with the following features:
·	Create, read, update, and delete tasks
·	Task prioritization (Low, Medium, High)
·	Task status tracking (NotStarted, InProgress, Completed)
·	Data validation
·	CORS support for frontend integration

Architecture
The solution is organized into multiple projects following clean architecture principles:
1.	TaskManagement.Domain: Contains core business entities and enums
2.	TaskManagement.Application: Contains DTOs, interfaces, and validation logic
3.	TaskManagement.Infrastructure: Contains data access, EF Core configuration, and services
4.	TaskManagement.Api: Contains controllers and API configuration

Getting Started
Prerequisites
·	.NET 8 SDK
·	SQL Server (LocalDB or full SQL Server instance)
·	Visual Studio 2022 or another compatible IDE
Step 1: Clone the Repository
Step 2: Configure the Database Connection
1.	Open TaskManagement.Api/appsettings.json
2.	Update the connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Step 3: Create the Database
Using Visual Studio Package Manager Console:
1.	Open the solution in Visual Studio
2.	Open Package Manager Console (Tools > NuGet Package Manager > Package Manager Console)
3.	Set the Default project to TaskManagement.Infrastructure
4.	Run the following commands:
Update-Database

Step 4: Run the Application
Using Visual Studio:
1.	Set TaskManagement.Api as the startup project
2.	Press F5 or click the Run button
The API will be available at:
·	https://localhost:5001/api/tasks 
·	Swagger UI: https://localhost:5001/swagger
API Endpoints

 GET    | /api/tasks | Get all tasks 
 GET    | /api/tasks/{id} | Get task by ID 
POST   | /api/tasks | Create a new task 
PUT    | /api/tasks/{id} | Update a task 
 DELETE | /api/tasks/{id} | Delete a task 


Sample Request (Creating a Task)
POST /api/tasks HTTP/1.1
Host: localhost:5001
Content-Type: application/json

{
  "title": "Complete Documentation",
  "description": "Write comprehensive documentation for the TaskManagement API",
  "status": 0,
  "priority": 1,
  "dueDate": "2023-12-31T23:59:59Z"
}

Frontend Integration
The API includes CORS configuration to support integration with a React frontend running on http://localhost:3000.

Troubleshooting
·	Migration Errors: Ensure all required packages are installed in the Infrastructure project
·	Connection Issues: Verify your SQL Server is running and the connection string is correct
·	Validation Errors: Check the request payload against the DTO validation requirements

