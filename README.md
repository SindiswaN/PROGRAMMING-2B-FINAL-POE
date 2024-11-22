Contract Monthly Claim System (CMCS)
Table of Contents
Overview
Features
Technology Stack
Setup and Installation
Usage
Roles and Functionalities
Advanced Features
Known Issues
Contributing
License
Overview
The Contract Monthly Claim System (CMCS) is a web-based application developed to streamline the process of submitting, approving, and managing monthly claims for independent contractor lecturers. It provides an efficient platform for:

Lecturers to submit claims.
Programme Coordinators and Academic Managers to verify claims.
HR to generate reports and manage lecturer data.
Features
Lecturer View
Automated claim submission with file uploads.
Auto-calculation of total payment based on hours worked and hourly rate.
Validation checks to ensure data accuracy.
Programme Coordinator and Academic Manager View
Automated claim validation based on predefined criteria.
Approve or reject claims with reasons for rejection.
HR View
Manage lecturer data (add, edit, delete lecturers).
Generate invoices and advanced reports for approved claims.
Integration with Crystal Reports and SSRS for enhanced reporting.
Technology Stack
Backend:
ASP.NET Core MVC (C#)
Entity Framework Core
MySQL
Frontend:
Razor Pages
Bootstrap
jQuery
Reporting Tools:
EPPlus (Excel generation)
Crystal Reports (PDF reports)
SSRS (SQL Server Reporting Services)
Setup and Installation
Prerequisites
.NET 6 SDK or later.
MySQL Server or any compatible database.
Visual Studio 2022 (recommended).
Crystal Reports or SSRS (optional for advanced reporting).

Steps
1. Clone the Repository
2. Set Up the Database:

-Open MySQL Workbench or phpMyAdmin.
-Create a new database
-Update the appsettings.json with your database credentials:

3. Apply Migrations: Run the following commands in the terminal
Run the Application
Navigate to http://localhost:5000 in your browser.

Usage
1. Lecturer
- Log in with lecturer credentials.
- Submit a claim with the required fields and supporting document.
- View the status of submitted claims.
2. Programme Coordinator/Manager
- Log in with coordinator/manager credentials.
- Review pending claims.
- Approve or reject claims with optional notes.
3. HR
- Access the HR dashboard.
- Manage lecturer data and generate reports.
