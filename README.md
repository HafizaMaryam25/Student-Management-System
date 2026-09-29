# 🎓 Student Management System

A full-stack **Student Management System** developed using **ASP.NET Core MVC**, **Firebase Authentication**, and **Cloud Firestore**.

This project demonstrates practical experience in building secure, role-based web applications with authentication, authorization, CRUD operations, cloud database integration, and a structured MVC architecture.

---

## 🌐 Live Demo

**Live Application:**  http://studentmanagementsystem.runasp.net/

---

## 📌 Project Overview

The Student Management System is designed to manage student information through a secure and role-based web application.

The system provides separate functionality for **Administrators** and **Students**, ensuring that users can access features according to their assigned role.

The project focuses on implementing real-world web development concepts including authentication, authorization, database management, service-based architecture, validation, and cloud integration.

---

## ✨ Features

* 🔐 Firebase Authentication
* 👥 Role-based access control
* 👨‍💼 Admin management panel
* 🎓 Student profile management
* ➕ Create student records
* 👀 View student information
* ✏️ Update student records
* 🗑️ Delete student records
* ☁️ Cloud Firestore integration
* 🛡️ Protected routes and authorization
* ✅ Form validation
* 📱 Responsive user interface
* 🚫 Unauthorized access handling

---

## 🛠️ Technology Stack

### Backend

* C#
* ASP.NET Core MVC
* .NET 10
* Firebase Admin SDK
* Google Cloud Firestore

### Frontend

* Razor Views
* HTML5
* CSS3
* Bootstrap
* JavaScript
* jQuery Validation

### Authentication & Database

* Firebase Authentication
* Cloud Firestore

### Tools

* Visual Studio
* Visual Studio Code
* Git
* GitHub

---

## 🏗️ Project Architecture

The application follows the **Model-View-Controller (MVC)** architecture with dedicated services for Firebase Authentication and Firestore operations.

```text
StudentManagementSystem/
│
├── Areas/
│   └── Identity/
│       └── Pages/
│           └── Account/
│
├── Controllers/
│   ├── AdminController.cs
│   ├── HomeController.cs
│   └── StudentDetailsController.cs
│
├── Data/
│   └── AdminSeeder.cs
│
├── Models/
│   ├── ErrorViewModel.cs
│   └── StudentDetail.cs
│
├── Services/
│   ├── FirebaseAuthService.cs
│   └── FirestoreService.cs
│
├── Views/
│   ├── Home/
│   ├── Shared/
│   └── StudentDetails/
│
├── wwwroot/
├── Program.cs
└── StudentManagementSystem.csproj
```

---

## 🔐 Authentication & Authorization

Firebase Authentication is used to securely authenticate users.

The application implements role-based authorization with two primary roles.

### Admin

Administrators can:

* Manage student records
* View student information
* Create student records
* Update student records
* Delete student records
* Access administrative functionality

### Student

Students can:

* Register an account
* Sign in securely
* Create their profile
* View their profile
* Update their personal information

Role-based authorization ensures that protected functionality is only accessible to authorized users.

---

## ☁️ Firebase & Firestore Integration

The application uses **Firebase Authentication** for identity management and **Cloud Firestore** for persistent application data.

Example data structure:

```text
users/
└── {firebaseUid}
    ├── Email
    ├── Role
    └── CreatedAt

students/
└── {firebaseUid}
    ├── UserId
    ├── FullName
    ├── Email
    ├── Address
    ├── FatherName
    ├── Contact
    └── Age
```

The Firebase UID is used to associate authenticated users with their corresponding student records.

---

## 💡 Key Development Concepts

This project provided practical experience with:

* ASP.NET Core MVC application development
* C# backend development
* Firebase Authentication
* Cloud Firestore
* Role-based authorization
* CRUD operations
* Service-based architecture
* Form validation
* Secure configuration management
* Git version control
* GitHub repository management

---

## 🚀 Getting Started

### Prerequisites

Make sure you have the required .NET SDK installed.

Check your version:

```bash
dotnet --version
```

### Clone the Repository

```bash
git clone YOUR_GITHUB_REPOSITORY_URL
cd Student-Management-System
```

### Restore Dependencies

```bash
dotnet restore
```

### Firebase Configuration

Create a Firebase project and enable:

* Firebase Authentication
* Email/Password Authentication
* Cloud Firestore

Add the Firebase service-account credentials locally:

```text
firebase-service-account.json
```

> **Important:** 

The project's `.gitignore` is configured to exclude this file.

### Run the Application

```bash
dotnet run
```

The application URL will be displayed in the terminal.

---

## 🔒 Security

Sensitive credentials and generated files are excluded from version control.

The following Firebase credential file is ignored:

```text
firebase-service-account.json
```

Production secrets should be managed through environment variables or a secure secret-management solution.



---

## 📸 Screenshots

Screenshots can be added to showcase the application's interface.

Recommended screenshots:

```text
docs/
├── login.png
├── registration.png
├── student-dashboard.png
├── student-profile.png
└── admin-dashboard.png
```

---

## 📦 Project Status

**Status:** Completed

The project is maintained as part of my software development portfolio and demonstrates practical implementation of ASP.NET Core, Firebase, Firestore, authentication, authorization, and CRUD-based application development.

---

## 📄 License

This project is licensed under the **MIT License**.

See the [LICENSE](LICENSE) file for the complete license terms.

---

## ⭐ Repository

If you find this project useful or interesting, feel free to explore the source code and follow my GitHub profile for more projects.
