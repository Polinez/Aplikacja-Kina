using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Windows;

namespace WpfApp
{
    public class DatabaseHelper
    {
        private const string DatabaseFileName = "KinoDB.db";  // Zmieniona nazwa bazy danych

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
                                Password TEXT NOT NULL
                            );
                        ";
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Baza danych została utworzona.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Baza danych już istnieje.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                // W przypadku błędu, pokazujemy komunikat z wyjątkiem
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metoda do połączenia z bazą danych
        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection($"Data Source={DatabaseFileName}");
        }
    }
}
