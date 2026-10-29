# EnterpriseInventory

A simple and robust inventory management system built with **ASP.NET Core** and **Entity Framework Core**, combining a Web API with a Razor Pages admin interface.

---

## Tech Stack

* **Backend:** C# .NET, ASP.NET Core (Web API & Razor Pages)
* **Database & ORM:** Entity Framework Core (with Migrations)
* **Frontend:** HTML, CSS, Razor Pages
* **Architecture:** DTOs for data decoupling, Dependency Injection, and LINQ queries

---

## Key Features
* **Product & Category Management:** Organize inventory items by categories.
* **Warehouse Control:** Track stock across multiple locations using many-to-many relationships (`ProductWarehouse`).
* **Orders & Customers:** Manage customer/supplier orders and related order items.
* **Dynamic Filtering:** Optimized LINQ queries for filtering and searching stock data.
* **Localization:** JSON-based resource localization support.

---

## Project Structure
* **`Controllers/`** – API endpoints
* **`Pages/`** – Razor Pages UI
* **`DTO/`** – Data Transfer Objects
* **`Entity/`** – Database models (Products, Warehouses, Orders, Suppliers)
* **`Data/`** – `AppDbContext` and EF Core migrations
