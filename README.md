# UserManagementAPI

A sample ASP.NET Core Web API project built for **TechHive Solutions** as part of a Coursera backend module.  
This project demonstrates three phases of development:

1. **CRUD API Implementation** – Basic user management endpoints.
2. **Debugging & Validation** – Input validation, error handling, and crash protection.
3. **Middleware Integration** – Logging, standardized error handling, and token-based authentication.

---

## Getting Started

### Prerequisites

- [.NET 10 Preview SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Git](https://git-scm.com/)
- [Postman](https://www.postman.com/) or any API testing tool

### Clone the Repository

```bash
git clone https://github.com/Syed-Muhammad-Hussnain-Raza/UserManagementAPI.git
cd UserManagementAPI
```

### Run the Project

```bash
dotnet restore
dotnet run
```

The API will start on `https://localhost:5050` (Swagger UI available at `/swagger`).

---

## Project Structure

```
UserManagementAPI/
├── Controllers/
│   └── UsersController.cs
├── Middleware/
│   ├── LoggingMiddleware.cs
│   ├── ErrorHandlingMiddleware.cs
│   └── AuthenticationMiddleware.cs
├── Models/
│   └── User.cs
├── Properties/
│   └── launchSettings.json
├── Program.cs
├── README.md
└── UserManagementAPI.csproj
```

---

## Assignment 1 – CRUD API

### Endpoints

| Method   | Route             | Description         |
| -------- | ----------------- | ------------------- |
| `GET`    | `/api/users`      | Retrieve all users  |
| `GET`    | `/api/users/{id}` | Retrieve user by ID |
| `POST`   | `/api/users`      | Add new user        |
| `PUT`    | `/api/users/{id}` | Update user         |
| `DELETE` | `/api/users/{id}` | Delete user         |

### Valid Test Cases

**POST** `/api/users` with:

```json
{ "name": "Alice", "email": "alice@example.com" }
```

→ Returns `201 Created`.

**GET** `/api/users/1` → Returns user object.

### Invalid Test Cases

**GET** `/api/users/999` → Returns `404 Not Found`.

**POST** `/api/users` with:

```json
{ "name": "", "email": "" }
```

→ Returns `400 Bad Request`.

---

## Assignment 2 – Debugging & Validation

### Enhancements

- Added `[Required]`, `[EmailAddress]`, and `[MinLength]` validation attributes in `User.cs`.
- Wrapped controller actions in `try-catch` blocks.
- Added global exception handling middleware in `Program.cs`.

### Valid Test Cases

**POST** `/api/users` with:

```json
{ "name": "Bob", "email": "bob@example.com" }
```

→ Returns `201 Created`.

**PUT** `/api/users/1` with:

```json
{ "name": "Bob Updated", "email": "bob.updated@example.com" }
```

→ Returns `204 No Content`.

### Invalid Test Cases

**POST** `/api/users` with:

```json
{ "name": "A", "email": "not-an-email" }
```

→ Returns `400 Bad Request`.

Malformed JSON:

```json
{ "name": "Charlie", "email": "charlie@example.com"
```

→ Returns `500 Internal Server Error`.

---

## Assignment 3 – Middleware Integration

### Middleware Added

1. **ErrorHandlingMiddleware** → Catches unhandled exceptions, returns JSON `{ "error": "Internal server error." }`.
2. **AuthenticationMiddleware** → Validates tokens, returns `401 Unauthorized` if missing/invalid.  
   _(For assignment purposes, any non-empty token is accepted.)_
3. **LoggingMiddleware** → Logs request method/path and response status code.

### Pipeline Order

1. Error handling
2. Authentication
3. Logging

### Valid Test Cases

Request with header:

```
Authorization: Bearer test123
```

→ Endpoints work normally.

Trigger exception (e.g., malformed JSON) → Returns:

```json
{ "error": "Internal server error." }
```

### Invalid Test Cases

Request without header → Returns:

```
Unauthorized: Token missing
```

Request with expired/invalid token (if JWT validation added later) → Returns:

```
Unauthorized: Invalid token
```

---

## Verification Guide

### Step 1: Run API

```bash
dotnet run
```

### Step 2: Test CRUD

- Valid: `POST /api/users` with proper JSON → User created.
- Invalid: `POST /api/users` with empty fields → `400 Bad Request`.

### Step 3: Test Debugging

- Valid: `GET /api/users/1` → Returns user.
- Invalid: `GET /api/users/999` → `404 Not Found`.

### Step 4: Test Middleware

- Valid: Request with `Authorization: Bearer test123` → Success.
- Invalid: Request without token → `401 Unauthorized`.

---

## Copilot Contributions

Microsoft Copilot assisted by:

- Scaffolding boilerplate code (`Program.cs`, controllers).
- Autocompleting CRUD endpoints.
- Suggesting validation attributes and error handling patterns.
- Generating middleware templates for logging, error handling, and authentication.
- Optimizing middleware pipeline order.

---

## Notes

- This project uses in-memory storage for simplicity.
- For production, integrate Entity Framework Core with a database.
- JWT authentication is simplified; any non-empty token is accepted for testing.

---

## Author

**Syed Muhammad Hussnain Raza**  
Made with the help of Microsoft Copilot
