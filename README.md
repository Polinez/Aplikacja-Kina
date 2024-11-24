# Aplikacja-Kina

PL---

Projekt Uniwersytecki w którym tworzymy aplikacje dla kina aby usprawnić sprzedaż biletów
(W trakcie budowy)

1. Wymagania systemowe

- **System operacyjny**: Windows (dla aplikacji WPF).
- **Zainstalowana wersja .NET**: .NET 8.0.
- **Visual Studio 2022 lub nowszy**: Zainstalowane z odpowiednimi komponentami do tworzenia aplikacji WPF i pracy z SQLite.
- **Zainstalowana zależność NuGet**: `Microsoft.Data.Sqlite` (więcej informacji w sekcji [Instalacja zależności](#2-instalacja-zależności)).

2. Instalacja zależności

Aby korzystać z bazy danych SQLite w aplikacji WPF, musisz zainstalować odpowiednią bibliotekę NuGet.

### Instalacja `Microsoft.Data.Sqlite`

1. Otwórz projekt w Visual Studio.
2. Przejdź do **Tools** → **NuGet Package Manager** → **Manage NuGet Packages for Solution...**.
3. Wyszukaj **Microsoft.Data.Sqlite** i zainstaluj tę paczkę w swoim projekcie.
   
   Alternatywnie, możesz zainstalować tę paczkę używając **Package Manager Console** w Visual Studio:

   ```bash
   Install-Package Microsoft.Data.Sqlite

# Struktura projektu

Aplikacja używa klasy `DatabaseHelper`, która automatycznie tworzy plik bazy danych `KinoDB.db` w katalogu:

```
bin/Debug/net8.0/KinoDB.db
```

plik jest baza danych ktora jest lokalna domyslna wesja naszej aplikacji, aby łatwiej zarzadzać bazą polecam instalacje [DB Browser](https://sqlitebrowser.org).


ENG ---

University project in which we create applications for cinemas to improve sales.
(Under construction)


