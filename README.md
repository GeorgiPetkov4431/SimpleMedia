# 🚀 SimpleMedia

> A Simple Social Media Platform developed as part of SoftUni's ASP.NET Fundamentals course.

![.NET Version](https://img.shields.io/badge/.NET-8.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📋 Table of Contents

- [About the Project](#-about-the-project)
- [Technologies Used](#️-technologies-used)
- [Prerequisites](#-prerequisites)
- [Getting Started](#-getting-started)
- [Features](#-features)
- [Usage](#-usage)
- [Database Setup](#-database-setup)
- [Configuration](#-configuration)
- [Contributing](#-contributing)
- [License](#-license)
- [Contact](#-contact)

---

## 📖 About the Project

SimpleMedia is a simple social media application built as part of the *ASP.NET Fundamentals* course offered by SoftUni. It applies principles such as MVC architecture, code-driven database design with EF Core, and user session management. Users can make posts categorized by tags, comment on posts, and compete with other users to see who can amass the most likes!

---

## 🛠️ Technologies Used

| Technology            | Version  | Purpose                          |
|-----------------------|----------|----------------------------------|
| ASP.NET Core MVC      | 8.0      | Web framework                    |
| Entity Framework Core | 8.0      | ORM / Database access            |
| SQL Server / SQLite   | -        | Database                         |
| Bootstrap             | 5.3      | Frontend styling                 |
| Razor Pages / Views   | -        | Server-side HTML rendering       |

---

## ✅ Prerequisites

Make sure you have the following installed before running the project:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) or SQLite (if used)
- [Git](https://git-scm.com/)

---

## 🚀 Getting Started

Follow these steps to get the project running locally.

### 1. Clone the repository

```bash
git clone https://github.com/GeorgiPetkov4431/SimpleMedia.git
cd SimpleMedia
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Apply database migrations

```bash
dotnet ef database update
```

### 4. Run the application

```bash
dotnet run
```

The app will be available at `https://localhost:5001` or `http://localhost:5000`.

---

## ✨ Features

- [✅] User registration and login (ASP.NET Identity)
- [✅] CRUD operations for [main entity]
- [✅] RESTful API endpoints
- [✅] Input validation (server-side & client-side)
- [✅] Responsive UI with Bootstrap

---

## 💻 Usage

Describe how to use the main features of the app after launching it. Add screenshots if possible.

```
1. Navigate to /Register to create an account.
2. Log in at /Login.
3. Use the dashboard to manage your [entities].
```

> 💡 **Tip:** You can add screenshots using: `![Screenshot](docs/screenshot.png)`

---

## 🗄️ Database Setup

The project uses **Entity Framework Core** with a Code-First approach.

Connection string is configured in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=SimpleMedia;Trusted_Connection=True;Encrypt=False;"
}
```

This application uses the connection string configured in `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "SqlServerDev": "Server=KINGCRIMSON\\SQLEXPRESS;Database=SimpleMedia;Trusted_Connection=True;Encrypt=False;"
}
```

Simply change ```KINGCRIMSON\\SQLEXPRESS``` to the server name of your locally-hosted SQL Server Database to be able to create and seed the database.

To create and seed the database:

```bash
dotnet ef database update
```

Or if you're using the `EntityFrameworkCore.Tools` NuGet package, run the following command in the Package Manager Console (Change the Default Project to `SimpleMedia.Data`):

```bash
Update-Database
```

---

## ⚙️ Configuration

Key settings in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=SimpleMedia;Trusted_Connection=True;Encrypt=False;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Identity": {
    "Password": {
      "RequireDigit": true,
      "RequireLowercase": true,
      "RequireUppercase": true,
      "RequireNonAlphanumeric": true,
      "RequiredUniqueChars": 1,
      "RequiredLength": 8
    },
    "SignIn": {
      "RequireConfirmedAccount": true
    },
    "User": {
      "RequireUniqueEmail": true
    }
  }
}
```

---

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m "Add some feature"`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Open a Pull Request

---

## 📄 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## 📬 Contact

**Georgi Petkov** – [@GeorgiPetkov4431](https://github.com/GeorgiPetkov4431)

Project Link: [https://github.com/your-username/your-repo-name](https://github.com/your-username/your-repo-name)

---

*Built as part of the **ASP.NET Fundamentals** course.*
