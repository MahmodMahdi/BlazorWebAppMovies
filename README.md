# 🎬 Movie App – Blazor Server

A full-featured **Movie Management Application** built with **Blazor Server** following clean backend practices. The project focuses on **maintainability**, **scalability**, and **clean separation of concerns**, while implementing real-world features like authentication, pagination, image upload, validation, and standardized API responses.

---

## 🚀 Overview

This project is a **Movie App** that allows users to manage movies and genres with full **CRUD operations**. It applies common enterprise patterns such as **Unit of Work**, **Generic Repository**, and **Result Pattern** to ensure clean code and predictable behavior.

The application is designed as a **single-layer architecture** but still respects clean coding principles through clear folder separation and service abstraction.

---

## 🧱 Project Architecture

The project follows a **One-Layer Architecture** with a **clear logical separation** that fits Blazor Server applications. The structure is designed to be simple, readable, and scalable while applying clean code principles.

```
│── Components
│   └── Pages
│       ├── Movies
│       │   ├── Index.razor
│       │   ├── Create.razor
│       │   ├── Edit.razor
│       │   ├── Details.razor
│       │   └── Delete.razor
│       └── Genres
│           ├── Index.razor
│           ├── Create.razor
│           ├── Edit.razor
│           ├── Details.razor
│           └── Delete.razor
│
│── Data
│   └── BlazorWebAppMoviesContext.cs
│
│── Dtos
│   ├── AuthDto
│   │   ├── LoginDto.cs
│   │   └── RegisterDto.cs
│   ├── Genre
│   │   ├── GenreReadDto.cs
│   │   ├── GenreCreateDto.cs
│   │   └── GenreUpdateDto.cs
│   └── Movie
│       ├── MovieReadDto.cs
│       ├── MovieCreateDto.cs
│       └── MovieUpdateDto.cs
│
│── GenericRepo
│   ├── GenericRepository.cs
│   └── IGenericRepository.cs
│
│── Middlewares
│   └── ExceptionHandlingMiddleware.cs
│
│── Migrations
│
│── Models
│   ├── ApplicationUser.cs
│   ├── Genre.cs
│   └── Movie.cs
│
│── Response
│   ├── Result.cs
│   └── PagedResult.cs
│
│── Seeding
│   └── SeedData.cs
│
│── Services
│   ├── AuthenticationService
│   │   ├── AuthService.cs
│   │   └── IAuthService.cs
│   ├── GenreService
│   │   ├── GenreService.cs
│   │   └── IGenreService.cs
│   ├── MovieService
│   │   ├── MovieService.cs
│   │   └── IMovieService.cs
│   └── FileService
│       ├── IImageService.cs
│       └── ImageService.cs
│
│── UnitOfWork
│   ├── IUnitOfWork.cs
│   └── UnitOfWork.cs
```

This structure keeps UI components, business logic, data access, and infrastructure concerns well-organized without introducing unnecessary complexity.

---

## ✨ Features

### 🎥 Movie Management

* Create, Read, Update, Delete movies
* Assign movies to genres
* Upload and store movie images
* Update movie poster

### 🗂 Genre Management

* Full CRUD operations
* Used as a reference entity for movies

### 🔐 Authentication & Authorization

* User registration and login
* Based on `ApplicationUser`
* Authentication service abstraction

### 📄 Pagination

* Server-side pagination
* Implemented using `PagedResult<T>`
* Efficient data loading for large datasets

### 🖼 Image Upload

* Upload images via `ImageService`
* Centralized image handling logic
* Clean abstraction through `IImageService`

### ✅ Validation

* DTO-level validation
* Prevents invalid data from reaching business logic

---

## 🧩 Design Patterns Used

### 🔁 Generic Repository Pattern

```csharp
IGenericRepository<T>
```

* Reusable CRUD logic
* Reduces code duplication

### 🧠 Unit of Work Pattern

```csharp
IUnitOfWork
```

* Manages transactions
* Ensures consistency across repositories

### 📦 Result Pattern

```csharp
Result<T>
```

* Standardized success & failure responses
* Clean error handling

### 📑 DTO Pattern

* Separate input/output models
* Protects domain entities

---

## 🛠 Tech Stack

* **.NET 10**
* **Blazor Server**
* **Entity Framework Core**
* **SQL Server**
* **ASP.NET Identity**
* **C#**

---

## ⚙️ Middleware

### 🛑 Global Exception Handling

* Centralized exception handling
* Clean error responses

```csharp
ExceptionHandlingMiddleware
```

---

## 📁 Important Folders Explained

### 📦 Models

* `Movie`
* `Genre`
* `ApplicationUser`

### 📦 Dtos

* `MovieCreateDto`
* `MovieReadDto`
* `MovieUpdateDto`

### 📦 Services

* `AuthService`
* `MovieService`
* `GenreService`
* `ImageService`

### 📦 Response

* `Result<T>`
* `PagedResult<T>`

---

## 🧪 Database & Seeding

* Entity Framework Core Migrations
* Initial data seeding supported

---

## ▶️ Getting Started

### 1️⃣ Clone the repository

```bash
git clone https://github.com/your-username/movie-app-blazor.git
```

### 2️⃣ Update Connection String

Edit `appsettings.json`

### 3️⃣ Apply Migrations

```bash
dotnet ef database update
```

### 4️⃣ Run the Application

```bash
dotnet run
```

---

## 📌 Future Improvements

* Role-based authorization
* Caching
* Unit testing
* Docker support
* API layer separation

---

## 👨‍💻 Author

**Mahmoud Amin**
.NET Backend Developer

📧 Email: [MahmoudElmahdy555@gmail.com](mailto:MahmoudElmahdy555@gmail.com)
🔗 GitHub | LinkedIn

---

⭐ If you like this project, don’t forget to give it a star!
