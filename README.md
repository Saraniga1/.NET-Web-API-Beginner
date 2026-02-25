# FirstAPI

A modern ASP.NET Core REST API for managing products and weather forecasts with Entity Framework Core, AutoMapper, and comprehensive error handling.

## Features

- **Product Management**: Full CRUD operations for product management
- **Weather Forecast API**: Weather prediction endpoints
- **Entity Framework Core**: Database abstraction with migrations
- **AutoMapper**: Object-to-object mapping for DTOs
- **Global Exception Handling**: Middleware for consistent error responses
- **Service Layer Architecture**: Clean separation of concerns
- **Data Transfer Objects (DTOs)**: Request/response validation

## Prerequisites

- [.NET 10](https://dotnet.microsoft.com/download)
- SQL Server or SQLite (configured in `appsettings.json`)
- Visual Studio 2022, Visual Studio Code, or JetBrains Rider

## Installation & Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd FirstAPISolution
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Configure Database

Update `appsettings.json` with your database connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(local);Database=FirstApiDb;Trusted_Connection=true;"
  }
}
```

### 4. Run Migrations

```bash
dotnet ef database update --project FirstAPI
```

Or using the .NET CLI:
```bash
cd FirstAPI
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run --project FirstAPI
```

## API Endpoints

### Products

| `GET` | `/api/products` | Get all products |
| `GET` | `/api/products/{id}` | Get a specific product |
| `POST` | `/api/products` | Create a new product |
| `PUT` | `/api/products/{id}` | Update a product |
| `DELETE` | `/api/products/{id}` | Delete a product |

### Weather Forecast

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/weatherforecast` | Get weather forecasts |

##  Architecture

### Service Layer
The `IProductService` interface defines business logic operations, with `ProductService` providing the implementation. This pattern enables:
- Easier unit testing
- Loose coupling between components
- Clear separation of concerns

### Data Access
- **AppDbContext**: Entity Framework Core DbContext for data operations
- **Models**: Domain entities (Product)
- **DTOs**: Transfer data between API and clients

### Mapping
- **MappingProfile**: AutoMapper configuration for model-to-DTO conversions

### Middleware
- **ExceptionMiddleware**: Catches unhandled exceptions and returns consistent error responses

## Development

### Build the Project

```bash
dotnet build
```

### Run Tests (if configured)

```bash
dotnet test
```

### Add a New Migration

```bash
dotnet ef migrations add MigrationName --project FirstAPI
dotnet ef database update --project FirstAPI
```

### Test API Endpoints

Use the `FirstAPI.http` file in VS Code with the REST Client extension, or use:
- **Postman**: Import HTTP file or create requests manually
- **cURL**: Command-line HTTP client
- **Thunder Client**: VS Code extension

## Configuration

### appsettings.json
- Database connection strings
- Logging levels
- API-specific settings

### appsettings.Development.json
- Development-specific settings
- Detailed logging configuration

## Error Handling

The application includes global exception handling through `ExceptionMiddleware`, which:
- Catches unhandled exceptions
- Returns structured error responses
- Logs errors for debugging

## Dependencies

Key NuGet packages:
- **Microsoft.EntityFrameworkCore**: ORM for data access
- **Microsoft.EntityFrameworkCore.SqlServer**: SQL Server provider
- **AutoMapper.Extensions.Microsoft.DependencyInjection**: Dependency injection for AutoMapper
- **Microsoft.AspNetCore.OpenApi**: OpenAPI/Swagger support

View all dependencies in `FirstAPI.csproj`

---

**Framework**: .NET 10
