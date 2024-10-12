# Peace Dental Web-API

Peace Dental web api is a dotnet C# server designed to power the Peace Dental web application. This robust server handles user authentication, data management, and API interactions with a PostgreSQL database using Entity Framework. The server follows the MVC architecture and incorporates JWT for secure authentication and authorization using dotnet Identity and JWT Bearer authentication.

## Table of Contents

- [Peace Dental Web-API](#peace-dental-web-api)
  - [Table of Contents](#table-of-contents)
  - [Features](#features)
  - [Technologies](#technologies)

## Features

- **User Authentication**: JWT-based authentication to secure user sessions.
- **Patient Management**: CRUD operations for managing patient records.
- **Appointment Management**: Create, update, and delete patient appointments.
- **Invoice Management**: Handle invoice creation and updates.
- **Revenue Metrics**: Access revenue data, restricted to admin users.
- **Middleware for Authentication**: Protect routes and validate refresh tokens.
- **Error Handling**: Comprehensive error handling for API responses.

## Technologies

- **Dotnet Core**: Server-side development using C#.
- **MVC**: Model-View-Controller architecture for web application development.
- **PostgreSQL**: Relational database for data storage.
- **EntityFramework**: Object-relational mapping (ORM) for database interactions.
- **Identity**: User authentication and authorization.
- **JWTBearer**: JWT Authentication token for secure user sessions.
- **Other Libraries**: `dotenv` for environment variable management.
