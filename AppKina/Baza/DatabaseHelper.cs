using AppKina.Admin;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows;

namespace WpfApp
{
    public class DatabaseHelper
    {
        private const string DatabaseFileName = "KinoDB.db";  // Zmieniona nazwa bazy danych, plik jest w folderze bin/Debug/net8.0/KinoDB.db      

        // Metoda do inicjalizacji bazy danych
        public static void InitializeDatabase()
        {
            try
            {
                // Sprawdzamy, czy plik bazy danych istnieje
                if (!File.Exists(DatabaseFileName))
                {
                    // Tworzymy bazę danych
                    using (var connection = new SqliteConnection($"Data Source={DatabaseFileName}"))
                    {
                        connection.Open();

                        // Tworzymy tabelę użytkowników
                        var command = connection.CreateCommand();
                        command.CommandText = @"
                            CREATE TABLE Users (
                                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                Username TEXT NOT NULL UNIQUE,
                                Email TEXT NOT NULL UNIQUE,
                                Password TEXT NOT NULL,
                                Role TEXT NOT NULL DEFAULT 'user' -- Domyślna rola
                            );
                        ";
                        command.ExecuteNonQuery();

                        // Dodajemy domyślnych użytkowników
                        command.CommandText = @"
                            INSERT INTO Users (Username, Email, Password, Role) VALUES
                            ('admin', 'admin@admin.pl', 'admin', 'admin'),
                            ('test', 'test@test.pl', 'test', 'user');
                        ";
                        command.ExecuteNonQuery();

                        // Tworzymy tabelę filmów
                        CreateMoviesTable(connection);
                    }

                    MessageBox.Show("Baza danych została utworzona i wypełniona domyślnymi danymi.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Jeżeli baza już istnieje, upewniamy się, że tabela filmów istnieje
                    using (var connection = GetConnection())
                    {
                        connection.Open();
                        CreateMoviesTable(connection);
                    }
                }
            }
            catch (Exception ex)
            {
                // W przypadku błędu, pokazujemy komunikat z wyjątkiem
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metoda do tworzenia tabeli filmów
        private static void CreateMoviesTable(SqliteConnection connection)
        {
            try
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Movies (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Genre TEXT NOT NULL,
                        Director TEXT NOT NULL,
                        Cast TEXT NOT NULL,
                        Duration INTEGER NOT NULL, -- czas trwania w minutach
                        Description TEXT,
                        PosterPath TEXT -- ścieżka do plakatu filmu
                    );
                ";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas tworzenia tabeli filmów: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metoda do połączenia z bazą danych
        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection($"Data Source={DatabaseFileName}");
        }

        // Metoda do dodawania filmu do bazy danych
        public static bool AddFilm(Film film)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                INSERT INTO Movies (Title, Genre, Director, Cast, Duration, Description, PosterPath) 
                VALUES (@Title, @Genre, @Director, @Cast, @Duration, @Description, @PosterPath);
            ";

                    command.Parameters.AddWithValue("@Title", film.Tytul);
                    command.Parameters.AddWithValue("@Genre", film.Gatunek);
                    command.Parameters.AddWithValue("@Director", film.Rezyser);
                    command.Parameters.AddWithValue("@Cast", film.Obsada);
                    command.Parameters.AddWithValue("@Duration", film.CzasTrwania);
                    command.Parameters.AddWithValue("@Description", film.Opis);
                    command.Parameters.AddWithValue("@PosterPath", film.SciezkaPlakatu ?? string.Empty);

                    command.ExecuteNonQuery();
                }

                return true; // Sukces
            }
            catch (Exception ex)
            {
                // Obsługa błędów, można logować wyjątek lub wyświetlać komunikat
                MessageBox.Show($"Wystąpił błąd podczas dodawania filmu do bazy danych: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; // Błąd
            }
        }



    }
}