# 🚗 DVLD - Driving & Vehicle License Department System

![Status](https://img.shields.io/badge/Status-Completed-brightgreen) ![Platform](https://img.shields.io/badge/Platform-Windows-blue) ![Language](https://img.shields.io/badge/Language-C%23-green)

A comprehensive desktop application for managing the workflow of a Driving & Vehicle License Department System (DVLD). This system handles the complete lifecycle of issuing driving licenses, from initial application and scheduling tests to final issuance and management.

## 🛠️ Technology Stack
* **Language:** C# (.NET Framework)
* **GUI:** Windows Forms (WinForms)
* **Database:** Microsoft SQL Server
* **Data Access:** ADO.NET (Raw SQL for performance)
* **Architecture:** 3-Tier Architecture (Presentation Layer, Business Logic Layer, Data Access Layer)

## ✨ Key Features
* **People Management:** Full CRUD system to manage personal details, filtering, and searching.
* **User Management:** Secure system to add/edit users and change passwords.
* **Application Management:** Handle Local and International driving license applications.
* **License Services:**
    * Renew, Replace (Lost/Damaged), and Detain/Release licenses.
* **Test Workflow:**
    * Schedule Vision, Written, and Street tests.
    * Manage Retake Tests with automatic fee calculation.

## 📷 Project Gallery

### 1. Main System & Security
<details>
<summary><b>🔻 Click to view Login, Dashboard & User Management</b></summary>
<br>

| Login Screen | Main Dashboard |
| :---: | :---: |
| ![Login](screenshots/Login.png) | ![Main](screenshots/MainScreen.png) |

| Manage Users | User Details |
| :---: | :---: |
| ![Manage Users](screenshots/ManageUsers.png) | ![Show User](screenshots/ShowCurrentUser.png) |
| ![Add User](screenshots/AddNewUser.png) | ![Change Pass](screenshots/ChangePassword.png) |

</details>

---

### 2. People Management
<details>
<summary><b>🔻 Click to view People Management Screens</b></summary>
<br>

| Manage People List | Person Details |
| :---: | :---: |
| ![People List](screenshots/PeopleManagement.png) | ![Person Details](screenshots/PersonDetails.png) |

| Add New Person |
| :---: |
| ![Add Person](screenshots/AddNewPerson.png) |

</details>

---

### 3. Driving License Applications
<details>
<summary><b>🔻 Click to view Application Workflows</b></summary>
<br>

| Application Types | Manage Local Apps |
| :---: | :---: |
| ![App Types](screenshots/ManageApplicationTypes.png) | ![Manage Local](screenshots/LocalDrivingLicenseApplications.png) |

| New Local Application | Issue License |
| :---: | :---: |
| ![New App](screenshots/AddNewLocalDrivingLicenseApplication.png) | ![Issue](screenshots/IssueDrivingLicense.png) |

</details>

---

### 4. Tests & Scheduling
<details>
<summary><b>🔻 Click to view Test Scheduling Screens</b></summary>
<br>

| Manage Test Types | Schedule Vision Test |
| :---: | :---: |
| ![Test Types](screenshots/ManageTestTypes.png) | ![Vision](screenshots/ScheduleVisionTest.png) |

| Retake Written Test | Pass Street Test |
| :---: | :---: |
| ![Retake](screenshots/ScheduleRetakeWrittenTest.png) | ![Street Test](screenshots/PassingStreetTest.png) |

</details>

---

### 5. License Services (Renew, Replace, Detain)
<details>
<summary><b>🔻 Click to view License Operations</b></summary>
<br>

| License History | License Info |
| :---: | :---: |
| ![History](screenshots/LicensesHistory.png) | ![Info](screenshots/DrivingLicenseInfo.png) |

| Renew License | Replace License |
| :---: | :---: |
| ![Renew](screenshots/RenewLicenseApplication.png) | ![Replace](screenshots/ReplaceLicenseApplication.png) |

| Detain License | Release License |
| :---: | :---: |
| ![Detain](screenshots/DetainLicense.png) | ![Release](screenshots/ReleaseDetainedLicesnes.png) |
| ![List Detained](screenshots/ListDetainedLicenses.png) | |

</details>

---

### 6. International Licenses
<details>
<summary><b>🔻 Click to view International License Screens</b></summary>
<br>

| Manage International | New Application |
| :---: | :---: |
| ![Manage Int](screenshots/ManageInternationalLicenses.png) | ![New Int App](screenshots/InternationalLicenseApplication.png) |

| Show Info |
| :---: |
| ![Show Int](screenshots/ShowInternationalLicenseInfo.png) |

</details>

---

## 🚀 How to Run
1.  Clone the repository.
2.  Run the SQL Script included in the `Database` folder to generate the tables.
3.  Open `DVLD.sln` in Visual Studio.
4.  Update the Connection String in `clsDataAccessSettings.cs`.
5.  Build and Run!

---
*Created by Ahmad Edais*