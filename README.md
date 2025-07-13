# LawLibraryManagementAPI

A C# .NET application for a library management system and digitalized book storage.
This service acts as a data integration bridge, managing entities such as books and users to ensure availability of books and accurate inventory tracking. It exposes standardized APIs for seamless interaction with user-facing applications.

## Overview

Library Management Service is designed to:

- Synchronize library entities between users and books
- Provide standardized, ready-to-use APIs for different applications

## Core Features

### Data Synchronization
- Support for bulk data operations
- Error handling and retry mechanisms

### Entity Transformation
- Mapping between library data models and standardized formats
- Business rule application during transformation
-


### Supported Entities
-  User and Books


## Technical Stack

- **Framework**: Dotnet 8
- **Programming Language**: c#
- **Database**: MSSQL (primary storage)
- **ORM**: EntityFrame work
- **Security**: Dotnet Security with JWT authentication
- **Documentation**: OpenAPI (Swagger)
- **Libraries**:
  - Automapper
  - JWT
  
## To Run the application
Replace the connection string with your connection and run the application
