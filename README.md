# LawLibraryManagementAPI

A C# DotNet application for library management system and digitalized library storage of books. This service serves as a data integration bridge, handling entities such as books, and users in library to make books available and knowing the number of books for user applications through standardized APIs.

## Overview

Zonda-Sync Service is designed to:

- Synchronize library entities between users
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
- **User** and Books


## Technical Stack

- **Framework**: Dotnet 8
- **Java Version**: c#
- **Database**: MSSQL (primary storage)
- **ORM**: EntityFrame work
- **Security**: Dotnet Security with JWT authentication
- **Documentation**: OpenAPI (Swagger)
- **Libraries**:
  - Automapper
