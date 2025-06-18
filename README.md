Group1 POS (Point of Sale) System

A desktop-based Point of Sale (POS) system built with C# and Windows Forms. Designed to handle core retail operations like managing products, sales, users, and transactions through a simple UI and a SQL Server backend.

🚀 Features

User Management
Add, update, and deactivate user accounts with roles and permissions.
Product Management
Add new products, manage categories, track inventory, and update stock in real-time.
Sales Processing
Fast and reliable sales transactions, including discounts, tax, and receipt generation.
Reporting
Generate sales reports, inventory summaries, and user activity logs.
Database Integration
Built using SQL Server with ADO.NET for data access.
Secure Login
Simple authentication system to restrict access based on user roles.
🛠️ Tech Stack

Language: C#
Framework: .NET (Windows Forms)
Database: SQL Server
Data Access: ADO.NET (System.Data.SqlClient)
UI Framework: Windows Forms (System.Windows.Forms)
📁 Project Structure

Group1_POS/
│
├── models/                # Business logic and data models
│   ├── User.cs
│   ├── Product.cs
│   └── Sale.cs
│
├── views/                 # WinForms UI files
│   ├── LoginForm.cs
│   ├── MainForm.cs
│   └── ReportsForm.cs
│
├── controllers/           # Interfaces and logic for handling actions
│
├── Program.cs             # Entry point
└── App.config             # DB connection settings
🧪 Getting Started

Prerequisites
Visual Studio (2022 or later recommended)
SQL Server (Express or full version)
.NET Framework installed (matching project version)
