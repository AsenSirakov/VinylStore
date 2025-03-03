### **VinylStore 🎵**

*A Vinyl Collection Management System built with C#/.NET and MongoDB.*

## 📌 **Overview**

VinylStore is a backend application for managing **vinyl records and songs**. It supports **CRUD operations** and advanced search capabilities based on **artist, genre, or song name**.

## 🚀 **Features**

- **🔹 RESTful API** – Structured and well-documented API with **Swagger**.
- **🔹 MongoDB Integration** – Efficient NoSQL storage for scalability.
- **🔹 Advanced Search & Filtering** – Query records by song, artist, or genre.
- **🔹 Layered Architecture** – Separation of **Models, Business Logic, and Data Layer**.
- **🔹 Input Validation** – Uses **FluentValidation** to ensure data integrity.
- **🔹 Dependency Injection** – Ensures modularity and maintainability.
- **🔹 Automated Unit Testing** – Uses **xUnit & Moq** to test controllers and services.
- **🔹 Health Monitoring** – Includes **HealthChecks** to monitor system status.

## 🛠 **Tech Stack**

| Technology               | Purpose              |
| ------------------------ | -------------------- |
| **C# .NET**              | API & Business Logic |
| **MongoDB**              | Database Storage     |
| **Swagger**              | API Documentation    |
| **xUnit & Moq**          | Unit Testing         |
| **FluentValidation**     | Input Validation     |
| **Dependency Injection** | Service Management   |

## 📦 **Installation & Setup**

### **1⃣ Clone the Repository**

```bash
git clone https://github.com/AsenSirakov/VinylStore.git
cd VinylStore
```

### **2⃣ Configure Database**

- Ensure **MongoDB** is installed and running.
- Update `appsettings.json` with your MongoDB connection string.

### **3⃣ Run the Application**

```bash
dotnet run
```

### **4⃣ Access the API**

- Open **Swagger UI** at [`http://localhost:5000/swagger`](http://localhost:5000/swagger) to explore the API endpoints.

## 🤖 **Running Tests**

```bash
dotnet test
```

This will execute **unit tests for controllers and services** to ensure the system functions as expected.

## 📚 **API Endpoints**

| Method     | Endpoint           | Description           |
| ---------- | ------------------ | --------------------- |
| **GET**    | `/api/vinyls`      | Get all vinyl records |
| **GET**    | `/api/vinyls/{id}` | Get a vinyl by ID     |
| **POST**   | `/api/vinyls`      | Add a new vinyl       |
| **PUT**    | `/api/vinyls/{id}` | Update a vinyl        |
| **DELETE** | `/api/vinyls/{id}` | Delete a vinyl        |

*For a full list of endpoints, visit Swagger UI.*


---

