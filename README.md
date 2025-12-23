# 🚗 DVLD - Driving & Vehicle License Department System

![Status](https://img.shields.io/badge/Status-Work_in_Progress-yellow) ![Platform](https://img.shields.io/badge/Platform-Windows-blue) ![Language](https://img.shields.io/badge/Language-C%23-green)

A comprehensive desktop application for managing the workflow of a Driving & Vehicle License Department System (DVLD). This system handles the complete lifecycle of issuing driving licenses, from the initial application and scheduling tests to the final issuance and license management.

**🚧 Note:** This project is currently under active development.

## 🛠️ Technology Stack
* **Language:** C# (.NET Framework)
* **GUI:** Windows Forms (WinForms)
* **Database:** Microsoft SQL Server
* **Data Access:** ADO.NET (Raw SQL for performance)
* **Architecture:** 3-Tier Architecture (Presentation Layer, Business Logic Layer, Data Access Layer)

## ✨ Key Features
* **People Management:** Full CRUD system to manage personal details, filtering, and searching.
* **User Management:** Full system to add, edit, and manage system users and change passwords.
* **Application Management:** Handle Local and International driving license applications.
* **License Issuance:** Workflow to issue both **Local** and **International** driving licenses.
* **License Services:**
    * Renew expiring licenses.
    * Replace lost or damaged licenses.
    * **Detain & Release:** Administrative tools to detain licenses (due to violations) and release them after fines are paid.
* **Test Workflow:**
    * Schedule Vision, Written, and Street tests.
    * Manage Retake Tests with automatic fee calculation.
    * Lock appointments after pass/fail results.
* **System Security:** User authentication, role-based access, and activity logging.
## 🏗️ Architecture
The project follows a strict **3-Layer Architecture** to ensure clean code and maintainability:
1.  **Data Access Layer (DAL):** interacting directly with MS SQL Server using `SqlDataReader` and `ExecuteScalar`.
2.  **Business Logic Layer (BLL):** Handles validation, calculations (fees, dates), and business rules.
3.  **Presentation Layer (PL):** User-friendly Windows Forms interface.

## 📷 Screenshots
*(Coming Soon)*

## 🚀 How to Run
1.  Clone the repository.
2.  Run the SQL Script included in the `Database` folder to generate the tables and stored procedures.
3.  Open the solution file (`DVLD.sln`) in Visual Studio.
4.  Update the Connection String in `clsDataAccessSettings.cs` to match your local SQL Server.
5.  Build and Run!

---
*Created by Ahmad Edais*
