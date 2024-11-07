# Order Processing System 

The **Order Processing System** is a simplified e-commerce application built with .NET Core 3.1. It allows for the management of customers and orders. The project includes a REST API that supports basic CRUD operations for managing these entities, as well as business logic for handling orders and calculating totals. Entity Framework Core is used for database management, and Swagger is included for interactive API documentation and testing.

## Features

- **Customer Management**: Retrieve and manage customer details.
- **Order Management**: Create orders, calculate order total, and retrieve order details.
- **API Documentation**: Interactive API documentation with Swagger for testing API endpoints.

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

- **.NET Core 3.1 SDK**: Install from the [official .NET website](https://dotnet.microsoft.com/download).
- **SQL Server** (or any other supported database): Update connection strings accordingly if you're using a different database.
- **Visual Studio** or another code editor (e.g., VS Code with C# extension).

## Installation

Follow these steps to set up the project on your local machine:

### 1. Clone the Repository

```bash
git clone https://github.com/YourUsername/OrderProcessingSystem.git
cd OrderProcessingSystem
```

### 2. Restore Project Dependencies

```bash
dotnet restore
```

### 3. Update Connection String

Update the connection string in `appsettings.json` to point to your SQL Server (or another database):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
  }
}
```

### 4. Apply Database Migrations

Initialize the database by applying the migrations:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Run the Application

Start the application:

```bash
dotnet run
```

The API will be available at `https://localhost:5001` (or another port depending on your environment).

---

## API Endpoints

### Customers

#### `GET /api/customers`
Retrieve all customers.

**Response**:

```json
[
  {
    "id": 1,
    "name": "John Doe",
    "email": "johndoe@example.com"
  },
  ...
]
```

#### `GET /api/customers/{id}`
Retrieve details for a specific customer, including their orders.

**Response**:

```json
{
  "id": 1,
  "name": "John Doe",
  "email": "johndoe@example.com",
  "orders": [
    {
      "id": 1,
      "totalPrice": 100.00,
      "orderDate": "2023-10-10T14:30:00"
    },
    ...
  ]
}
```

---

### Orders

#### `POST /api/orders`
Create a new order for a customer, specifying the customer ID and a list of product IDs.

**Request Body**:

```json
{
  "customerId": 1,
  "productIds": [1, 2, 3]
}
```

**Response**:

```json
{
  "id": 1,
  "totalPrice": 150.00,
  "orderDate": "2023-10-10T14:45:00"
}
```

#### `GET /api/orders/{id}`
Retrieve details for a specific order, including the total price and associated products.

**Response**:

```json
{
  "id": 1,
  "totalPrice": 150.00,
  "orderDate": "2023-10-10T14:45:00",
  "products": [
    {
      "id": 1,
      "name": "Product 1",
      "price": 50.00
    },
    {
      "id": 2,
      "name": "Product 2",
      "price": 100.00
    }
  ]
}
```

---

## Swagger UI

You can test the API interactively using Swagger UI by navigating to:

```
https://localhost:5001/
```

Here, you can explore all available endpoints, send requests, and view responses.


---

## Error Handling

The application handles various types of errors gracefully:

- **400 Bad Request**: Invalid data provided in the request.
- **404 Not Found**: The requested resource (e.g., customer, order) does not exist.
- **500 Internal Server Error**: General server errors or unexpected failures.

---

## Logging

The application uses **Serilog** for logging. Logs are written to both the console and a file (`logs/log.txt`). You can adjust the logging configuration in `Program.cs` to suit your needs.

---



