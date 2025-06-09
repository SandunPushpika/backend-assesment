# Backend Application Assesment (.NET 8)

Welcome to the backend system built with **.NET 8**, for retrieving movie details.  
This project does **not** use Entity Framework Core or Dapper — instead, it uses **raw SQL via SQL Connector** for complete control over data operations.

---

##  Prerequisites

Before getting started, make sure the following are installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [IntelliJ Rider](https://www.jetbrains.com/rider/) or [Visual Studio 2022+](https://visualstudio.microsoft.com/) (Optional for IDE-based development)
- A running SQL Server instance (MS SQL for this)

---

## Getting Started (Using an IDE)

### 1. Clone the Repository

```bash
git clone https://github.com/SandunPushpika/backend-assesment.git
```
### 2. Open the Project

* IntelliJ Rider: Open the `.sln` solution file.
* Visual Studio: Use `Open a project or solution` and select the .sln file.

### 3. Configure the Connection String

* Edit below part and include your database connection string.
```json
{
  "ConnectionString": {
    "DefaultConnection": "your-connection-string-here"
  }
}
```
* You can find db.sql in the reposioty

### 4. Now you can run the project

## Running Without an IDE (CLI)

### 1. Restore the dependencies
```bash
    dotnet restore
```

### 2. Build the project
```bash
    dotnet build
```

### 2. Run the application
```bash
    dotnet run --project ./LiquidLabAssesment.csproj
```

## Database Integration
* **No ORM** Used: This project does not use **EF Core or Dapper**.
* Data access is performed using raw **SQL queries** via **SqlConnection** from ADO.NET.
* The connection string is located in **appsettings.Development.json** and can be modified as needed.

## Endpoints
### Fetch all Movies
```Curl
http://localhost:5097/api/movies
```
### Fetch Movie By Id
```Curl
http://localhost:5097/api/movies/{id}
```

# Thank you