# *PL*

# 🍿 Aplikacja-Kina 🎬

## 📄 Opis

**Projekt Uniwersytecki** mający na celu stworzenie aplikacji dla kina, aby usprawnić proces sprzedaży biletów.  

[Plan Inżynieryjny](https://docs.google.com/document/d/1pygJBoeeNCG1zv98azIWQqD3U0pOZEZQltmLeD2ybUw/edit?usp=sharing)

[Prezentacja](https://www.canva.com/design/DAGbJ5bLvho/Vph9NzE1OMoDjGWbOjMFpA/edit?utm_content=DAGbJ5bLvho&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton)


---

## 🛠️ Instalacja

### 1. Wymagania systemowe

- **System operacyjny:** Windows (aplikacja WPF).
- **Zainstalowana wersja .NET:** .NET 8.0.
- **Visual Studio 2022 lub nowszy:** Zainstalowane z komponentami do tworzenia aplikacji WPF i obsługi SQLite.
- **Pakiet NuGet:** `Microsoft.Data.Sqlite` (szczegóły w sekcji [Instalacja zależności](#2-instalacja-zależności)).

---

### 2. Instalacja zależności

Aby aplikacja mogła korzystać z bazy danych SQLite, należy zainstalować odpowiednią bibliotekę.

#### Instalacja `Microsoft.Data.Sqlite`

1. Otwórz projekt w Visual Studio.
2. Przejdź do **Tools** → **NuGet Package Manager** → **Manage NuGet Packages for Solution...**.
3. Wyszukaj **Microsoft.Data.Sqlite** i zainstaluj tę paczkę w swoim projekcie.

Alternatywnie, możesz skorzystać z **Package Manager Console** w Visual Studio:

```powershell
Install-Package Microsoft.Data.Sqlite
```

# 🗂️ Struktura projektu

Aplikacja używa klasy `DatabaseHelper`, która automatycznie tworzy plik bazy danych `KinoDB.db` po uruchomieniu1 raz aplikacji w katalogu:

```
bin/Debug/net8.0/KinoDB.db
```

plik jest baza danych ktora jest lokalna domyslna wesja naszej aplikacji, aby łatwiej zarzadzać bazą polecam instalacje [DB Browser](https://sqlitebrowser.org).

# 🔑 Logowanie w aplikacji

Domyślne dane logowania dla kont testowych:

- Konto testowe:
```
Login: test@test.pl
Hasło: test
```
- Konto administratora:
```
Login: admin@admin.pl
Hasło: admin
```



# *ENG*

# 🍿 Cinema-App 🎬

## 📄 Description

A **university project** aimed at creating an application for cinemas to streamline the ticket sales process.  
**Project status:** Under construction.

---

## 🛠️ Installation

### 1. System Requirements

- **Operating System:** Windows (WPF application).
- **Installed .NET Version:** .NET 8.0.
- **Visual Studio 2022 or later:** Installed with components for WPF application development and SQLite support.
- **NuGet Package:** `Microsoft.Data.Sqlite` (details in the [Dependency Installation](#2-dependency-installation) section).

---

### 2. Dependency Installation

To use the SQLite database in the application, you need to install the appropriate library.

#### Installing `Microsoft.Data.Sqlite`

1. Open the project in Visual Studio.
2. Go to **Tools** → **NuGet Package Manager** → **Manage NuGet Packages for Solution...**.
3. Search for **Microsoft.Data.Sqlite** and install the package in your project.

Alternatively, you can use the **Package Manager Console** in Visual Studio:

```
Install-Package Microsoft.Data.Sqlite
```

# 🗂️ Project Structure

The application uses the `DatabaseHelper` class, which automatically creates the database file `KinoDB.db` upon the first run of the application.  
- 📂 The database file is located in the following directory:
```
bin/Debug/net8.0/KinoDB.db
```

- 🗄️ This is the default local version of the database used in the application.  
- **🔍 Database Management:** It is recommended to install [DB Browser for SQLite](https://sqlitebrowser.org) for easier database management.

---

# 🔑 Application Login

Default login credentials for test accounts:

- Test account:
```
Login: test@test.pl
Password: test
```
- Admin account:
```
Login: admin@admin.pl
Password: admin
```



