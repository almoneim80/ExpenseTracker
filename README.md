## Expense Tracker
Expense Tracker is a full-featured web application for tracking income and expenses, 
built using ASP.NET Core MVC and Entity Framework Core, with a modern interface using Syncfusion Charts.

## Table of Contents
Features
Requirements
Project Setup
Database
How to Use
Project Structure
Future Development
License

## Features
Category Management: Add, edit, delete categories, select type (Income/Expense), and add an icon.
Transaction Management: Record income and expenses, add notes, select category and date.
Dynamic Dashboard:
Total income, total expense, and balance.
Doughnut chart showing expenses by category.
Spline chart comparing daily income and expenses.
Display recent transactions.
Modern and organized UI with Sidebar and navigation menus.
Data validation on input.
Error handling with a user-friendly error page.

## Requirements
.NET 7 SDK or later.
SQL Server for storage.
Modern web browser supporting Syncfusion (Chrome/Edge/Firefox).
Internet connection for Syncfusion and FontAwesome libraries.

## Project Setup
Clone the repository:
git clone <repository-url>

Restore packages:
dotnet restore

Apply migrations to create the database:
dotnet ef database update

Run the application:
dotnet run

Open the browser at:
https://localhost:5001

## Database
Main Tables:
Categories
Id (PK)
Title
Icon
Type (Income/Expense)
Transactions
Id (PK)
Amount
Note
Date
CategoryId (FK)

## How to Use
Manage categories under Categories: create income or expense categories.
Record transactions under Transactions.
View the dashboard Dashboard:
Monitor current balance.
Analyze expenses by category.
Track recent transactions.
Categories and transactions can be edited or deleted as needed.

## Project Structure
Expense_Tracker/
├── Controllers/
├── Models/
├── Views/
├── wwwroot/
├── Migrations/
├── appsettings.json
└── Program.cs


Controllers: Handles application logic and views.
Models: Represents entities (Transaction, Category) and database context.
Views: Razor pages for UI.
wwwroot: CSS, JS, images, and icons.
Migrations: Database migration files.
Program.cs: Application configuration and service registration.

## Future Development
User authentication and multi-user support.
Export data to PDF and Excel.
Advanced reports and filters.
Multi-currency support.
Automatic alerts when exceeding budget.
Enhanced analytics and charts.
PWA or mobile app version.

## License
Open-source, modifiable and usable under MIT license or as agreed.