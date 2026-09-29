# 🎓 Student Management System

A role-based **Student Management System** built with **ASP.NET Core MVC**, **Firebase Authentication**, and **Cloud Firestore**.

The application provides secure authentication, role-based authorization, and student profile management with separate **Admin** and **Student** access.

---

## 🌐 Live Demo

🔗 **Live Application:** http://studentmanagementsystem.runasp.net/



---

## ✨ Features

* 🔐 Firebase Email/Password Authentication
* 👤 Student Registration & Login
* 🛡️ Role-Based Authorization
* 👨‍💼 Admin Dashboard
* 🎓 Student Dashboard
* 📋 Student Profile Management
* ➕ Create Student Profile
* 👀 View Student Details
* ✏️ Edit Student Information
* 🗑️ Delete Student Records
* ☁️ Cloud Firestore Database
* 🔥 Firebase Authentication
* 📱 Responsive UI
* 🚫 Unauthorized Access Protection
* ✅ Form Validation

---

## 🧰 Technologies Used

| Technology                 | Purpose                     |
| -------------------------- | --------------------------- |
| C#                         | Application Development     |
| ASP.NET Core MVC           | Web Framework               |
| .NET 10                    | Target Framework            |
| Firebase Authentication    | User Authentication         |
| Cloud Firestore            | Database                    |
| Firebase Admin SDK         | Firebase Server Integration |
| Google Cloud Firestore SDK | Firestore Operations        |
| Razor Views                | Frontend                    |
| Bootstrap                  | UI & Responsive Design      |
| jQuery Validation          | Client-side Validation      |
| Visual Studio / VS Code    | Development Environment     |

---

## 🏗️ Project Structure

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
│
├── Program.cs
├── appsettings.json
└── StudentManagementSystem.csproj
```

---

## 🔐 User Roles

### 👨‍💼 Admin

The Admin role provides access to student management functionality.

Admin users can:

* View all student records
* View student details
* Create student records
* Edit student records
* Delete student records
* Access administrative functionality

### 🎓 Student

Student users can:

* Register an account
* Login securely
* Create their profile
* View their profile
* Edit their profile
* Access functionality allowed for their role

---

## 🔥 Firebase Integration

The application uses **Firebase Authentication** for user authentication and **Cloud Firestore** for storing student-related data.

### Firebase Authentication

Firebase Authentication handles:

* User registration
* User login
* User identity
* Firebase UID

### Cloud Firestore

Student information is stored in Firestore.

Example structure:

```text
users/
└── {firebaseUid}
    ├── Email
    ├── Role
    └── CreatedAt
```

Student records:

```text
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

---

## 🚀 Installation & Setup

### 1. Clone the Repository

```bash
git clone YOUR_GITHUB_REPOSITORY_URL
```

Then enter the project directory:

```bash
cd Student-Management-System
```

---

### 2. Install .NET SDK

Make sure the required .NET SDK is installed.

Check your installed version:

```bash
dotnet --version
```

---

### 3. Configure Firebase

Create a Firebase project and enable:

* Firebase Authentication
* Email/Password Authentication
* Cloud Firestore

Download your Firebase service-account credentials.

Place the file locally inside the project:

```text
firebase-service-account.json
```

⚠️ **Never upload this file to GitHub.**

The `.gitignore` file included with this project already ignores:

```gitignore
firebase-service-account.json
**/firebase-service-account.json
```

---

### 4. Configure Application Settings

Configure your Firebase project settings according to your local environment.

For production deployment, sensitive credentials should be stored using:

* Environment Variables
* Secret Manager
* Hosting Provider Secrets

Avoid storing private credentials directly in a public GitHub repository.

---

### 5. Restore Dependencies

```bash
dotnet restore
```

---

### 6. Build the Project

```bash
dotnet build
```

---

### 7. Run the Application

```bash
dotnet run
```

The terminal will display the local application URL.

Example:

```text
https://localhost:7000
```

---

## 🔒 Security

This project uses Firebase services and therefore requires proper handling of credentials.

### Never commit:

```text
firebase-service-account.json
```

or any file containing:

```text
private_key
client_secret
password
API credentials
service account credentials
```

The included `.gitignore` is configured to prevent common secret and generated files from being committed.

### Important

If a Firebase service-account credential has **already been pushed to GitHub**, simply deleting the file is not enough.

The exposed credential should be **revoked/rotated** from the Firebase/Google Cloud project.

---

## 🧪 Development Commands

Restore dependencies:

```bash
dotnet restore
```

Build:

```bash
dotnet build
```

Run:

```bash
dotnet run
```

Clean:

```bash
dotnet clean
```

---

## 📸 Screenshots

Screenshots can be added here to showcase the application.

Recommended screenshots:

```text
docs/
├── login.png
├── registration.png
├── dashboard.png
├── student-profile.png
└── admin-dashboard.png
```

---

## 🌐 Deployment

The application can be deployed to a suitable ASP.NET-compatible hosting platform.

After deployment, update the Live Demo section:

```text
🔗 Live Application: https://your-live-domain.com
```

---

## 👨‍💻 Author

**Hafiza Maryam**

Aspiring Software Engineer
Aptech Learning, Karachi

---

## 📄 License

This project is available for educational and portfolio purposes.

If you plan to distribute the project publicly, consider adding an appropriate open-source `LICENSE` file.

---

## ⭐ Show Your Support

If you find this project useful, consider giving the repository a ⭐ on GitHub.
# 🎓 Student Management System

A modern **role-based Student Management System** built with **ASP.NET Core MVC**, **Firebase Authentication**, and **Cloud Firestore**.

This project demonstrates my ability to build a secure web application with authentication, authorization, cloud database integration, CRUD operations, and a structured MVC architecture.

---

## 🌐 Live Demo

🔗 **Live Demo:** `ADD_YOUR_LIVE_LINK_HERE`

> Replace the link above with the deployed application URL.

---

## 📌 About The Project

The **Student Management System** is a full-stack web application designed to manage student information through a secure role-based system.

The application supports two primary roles:

* **Admin** — manages student records and has administrative access.
* **Student** — manages and views their own profile information.

The project focuses on implementing real-world concepts such as **authentication, authorization, database operations, role management, validation, and cloud services**.

---

## ✨ Key Features

* 🔐 Secure Firebase Authentication
* 👥 Admin & Student role-based access
* 🛡️ Authorization-protected functionality
* 👨‍💼 Admin student management
* 🎓 Student profile management
* ➕ Create student records
* 👀 View student details
* ✏️ Update student information
* 🗑️ Delete student records
* ☁️ Cloud Firestore database integration
* 📱 Responsive user interface
* ✅ Form validation
* 🚫 Unauthorized access handling

---

## 🛠️ Tech Stack

### Backend

* **C#**
* **ASP.NET Core MVC**
* **.NET 10**
* **Firebase Admin SDK**
* **Google Cloud Firestore**

### Frontend

* **Razor Views**
* **HTML5**
* **CSS3**
* **Bootstrap**
* **JavaScript**
* **jQuery Validation**

### Services

* **Firebase Authentication**
* **Cloud Firestore**

### Development Tools

* **Visual Studio**
* **Visual Studio Code**
* **Git**
* **GitHub**

---

## 🏗️ Application Architecture

The project follows the **MVC (Model-View-Controller)** architecture.

```text
StudentManagementSystem
│
├── Areas
│   └── Identity
│       └── Pages
│           └── Account
│
├── Controllers
│   ├── AdminController.cs
│   ├── HomeController.cs
│   └── StudentDetailsController.cs
│
├── Data
│   └── AdminSeeder.cs
│
├── Models
│   ├── ErrorViewModel.cs
│   └── StudentDetail.cs
│
├── Services
│   ├── FirebaseAuthService.cs
│   └── FirestoreService.cs
│
├── Views
│   ├── Home
│   ├── Shared
│   └── StudentDetails
│
├── wwwroot
│
├── Program.cs
└── StudentManagementSystem.csproj
```

---

## 🔐 Authentication & Authorization

Firebase Authentication is used to handle user authentication.

After authentication, users are assigned roles that determine which parts of the application they can access.

### Admin

Admin users can:

* View student records
* Add student information
* Edit student information
* Delete student records
* Access administrative functionality

### Student

Student users can:

* Register an account
* Login
* Create their profile
* View their profile
* Update their information

This role-based approach ensures that users only have access to functionality appropriate for their role.

---

## ☁️ Firebase & Firestore

The application uses **Firebase Authentication** for identity management and **Cloud Firestore** for storing application data.

Example Firestore structure:

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

---

## 💡 What I Learned

Through this project, I gained practical experience with:

* Building applications using **ASP.NET Core MVC**
* Implementing **Firebase Authentication**
* Working with **Cloud Firestore**
* Implementing **role-based authorization**
* Building CRUD functionality
* Structuring applications using MVC
* Managing application services
* Handling user authentication and access control
* Integrating third-party cloud services
* Using Git and GitHub for version control

---

## 🚀 Getting Started

### Clone the repository

```bash
git clone YOUR_GITHUB_REPOSITORY_URL
cd Student-Management-System
```

### Restore dependencies

```bash
dotnet restore
```

### Configure Firebase

Create your Firebase project and enable:

* Firebase Authentication
* Email/Password Authentication
* Cloud Firestore

Add your Firebase service-account credentials locally.

```text
firebase-service-account.json
```

⚠️ **Never commit this file to GitHub.**

The project's `.gitignore` is configured to exclude Firebase service-account credentials.

### Run the application

```bash
dotnet run
```

The terminal will provide the local application URL.

---

## 🔒 Security

Sensitive credentials are intentionally excluded from version control.

The following file is ignored:

```text
firebase-service-account.json
```

Production credentials should be managed through environment variables or a secure secret-management system.

---

## 📸 Screenshots

You can add screenshots of the application here to showcase the UI.

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

## 🌐 Live Project

**Live Demo:** `ADD_YOUR_LIVE_LINK_HERE`

**Source Code:** `ADD_YOUR_GITHUB_REPOSITORY_LINK_HERE`

---

## 👨‍💻 About Me

Hi! I'm **Hafiza Maryam**, an aspiring software engineer passionate about building web applications and learning modern software development technologies.

I'm particularly interested in:

* 💻 Web Development
* ⚙️ Backend Development
* ☁️ Cloud Technologies
* 🗄️ Databases
* 🔐 Authentication & Authorization
* 🚀 Building real-world projects

This project is part of my development portfolio and demonstrates my practical experience with **C#, ASP.NET Core, Firebase, Firestore, and Git/GitHub**.

---

## 📬 Connect With Me

**GitHub:** `ADD_YOUR_GITHUB_PROFILE`

**LinkedIn:** `ADD_YOUR_LINKEDIN_PROFILE`

**Email:** `ADD_YOUR_EMAIL`

---

## ⭐ Feedback

If you find this project interesting, feel free to explore the source code and share your feedback.

---

## 📄 License

This project is licensed under the **MIT License**.

See the [LICENSE](LICENSE) file for the complete license terms.


