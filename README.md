# Aplikacja-Kina

PL---

Projekt Uniwersytecki w którym tworzymy aplikacje dla kina aby usprawnić sprzedaż biletów
(W trakcie budowy)

ENG ---

University project in which we create applications for cinemas to improve sales.
(Under construction)

## Requirements

To run this application, the following tools and libraries are needed:

1. **.NET 8.0 SDK**  
   This project is built using the **.NET 8.0 SDK**. If you don't have it installed, you can download it from the official Microsoft website:  
   [Download .NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet).

2. **Visual Studio 2022 (or later)**  
   A development environment like **Visual Studio** is recommended for building and running this project. Make sure you have the **.NET Desktop Development** workload installed. You can download Visual Studio from:  
   [Download Visual Studio](https://visualstudio.microsoft.com/downloads/).

3. **SQLite Database**  
   The application uses **SQLite** as its database to store user information and reservation data. To manage SQLite databases, you can use a tool like [DB Browser for SQLite](https://sqlitebrowser.org/dl/). This tool allows you to inspect and modify the SQLite database easily.

4. **SQLite Data Provider for .NET**  
   The project utilizes **SQLite** via the **System.Data.SQLite** provider to interact with the database. You need to install the NuGet package for SQLite in the project. This can be done using the NuGet Package Manager in Visual Studio or running the following command in the **Package Manager Console**:
   
   ```bash
   Install-Package System.Data.SQLite
   
