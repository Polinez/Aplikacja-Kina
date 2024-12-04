using AppKina.Admin;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows;

namespace WpfApp
{
    public class DatabaseHelper
    {
        private const string DatabaseFileName = "KinoDB.db";

        /// <summary>
        /// Inicjalizuje bazę danych.
        /// </summary>
        public static void InitializeDatabase()
        {
            try
            {
                if (!File.Exists(DatabaseFileName))
                {
                    CreateDatabase();
                    MessageBox.Show("Baza danych została utworzona i wypełniona domyślnymi danymi.",
                                    "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    EnsureMoviesTableExists();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Wystąpił błąd: {ex.Message}");
            }
        }

        /// <summary>
        /// Tworzy bazę danych z tabelami i domyślnymi danymi.
        /// </summary>
        private static void CreateDatabase()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                CreateUsersTable(connection);
                SeedUsers(connection);
                CreateMoviesTable(connection);
                CreateSeanseTable(connection);
            }
        }

        /// <summary>
        /// Tworzy tabelę użytkowników.
        /// </summary>
        private static void CreateUsersTable(SqliteConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE Users (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Email TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL,
                    Role TEXT NOT NULL DEFAULT 'user'
                );
            ";
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Dodaje domyślnych użytkowników do tabeli.
        /// </summary>
        private static void SeedUsers(SqliteConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Users (Username, Email, Password, Role) VALUES
                ('admin', 'admin@admin.pl', 'admin', 'admin'),
                ('test', 'test@test.pl', 'test', 'user');
            ";
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Tworzy tabelę filmów, jeśli nie istnieje.
        /// </summary>
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
                        Duration INTEGER NOT NULL,
                        Description TEXT,
                        PosterPath TEXT
                    );
                ";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ShowError($"Wystąpił błąd podczas tworzenia tabeli filmów: {ex.Message}");
            }
        }

        /// <summary>
        /// Sprawdza istnienie tabeli filmów w istniejącej bazie danych.
        /// </summary>
        private static void EnsureMoviesTableExists()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                CreateMoviesTable(connection);
            }
        }

        /// <summary>
        /// Dodaje film do bazy danych.
        /// </summary>
        /// <param name="film">Obiekt reprezentujący film.</param>
        /// <returns>True, jeśli dodano film; w przeciwnym razie False.</returns>
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

                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Wystąpił błąd podczas dodawania filmu do bazy danych: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Tworzy nowe połączenie z bazą danych.
        /// </summary>
        /// <returns>Obiekt SqliteConnection.</returns>
        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection($"Data Source={DatabaseFileName}");
        }

        /// <summary>
        /// Wyświetla komunikat o błędzie.
        /// </summary>
        /// <param name="message">Treść komunikatu.</param>
        private static void ShowError(string message)
        {
            MessageBox.Show(message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private static void CreateSeanseTable(SqliteConnection connection) //funkcja tworząca tabelę seanse
        {
            try
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Seanse (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    MovieID INTEGER NOT NULL,
                    Date TEXT NOT NULL,
                    StartTime TEXT NOT NULL,
                    Format TEXT NOT NULL,
                    Price REAL NOT NULL,
                    FOREIGN KEY(MovieID) REFERENCES Movies(ID) ON DELETE CASCADE
                );
            ";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ShowError($"Wystąpił błąd podczas tworzenia tabeli Seanse: {ex.Message}");
            }
        }
        /// <summary>
        /// Dodaje seans do tabeli Seanse.
        /// </summary>
        /// <param name="seans">Obiekt reprezentujący seans.</param>
        /// <returns>True, jeśli dodano seans; w przeciwnym razie False.</returns>
        /// 
        public class Seans
        {
            public int MovieID { get; set; }
            public string Date { get; set; }
            public string StartTime { get; set; }
            public string Format { get; set; }
            public double Price { get; set; }
        }
        public static bool AddSeans(Seans seans)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                INSERT INTO Seanse (MovieID, Date, StartTime, Format, Price)
                VALUES (@MovieID, @Date, @StartTime, @Format, @Price);
            ";

                    command.Parameters.AddWithValue("@MovieID", seans.MovieID);
                    command.Parameters.AddWithValue("@Date", seans.Date);
                    command.Parameters.AddWithValue("@StartTime", seans.StartTime);
                    command.Parameters.AddWithValue("@Format", seans.Format);
                    command.Parameters.AddWithValue("@Price", seans.Price);

                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Wystąpił błąd podczas dodawania seansu do bazy danych: {ex.Message}");
                return false;
            }
        }
        public static bool IsSeansTimeAvailable(int movieID, string date, TimeSpan startTime, TimeSpan movieDuration) //funckja rozwiązująca problemy z nakładaniem się seansów
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                SELECT StartTime FROM Seanse
                WHERE Date = @Date
            ";
                    command.Parameters.AddWithValue("@Date", date);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var existingStartTime = TimeSpan.Parse(reader.GetString(0));
                            var existingEndTime = existingStartTime + movieDuration + TimeSpan.FromMinutes(20);

                            if (startTime < existingEndTime && startTime + movieDuration > existingStartTime)
                                return false; // Konflikt czasowy
                        }
                    }
                }
                return true; // Brak konfliktów
            }
            catch (Exception ex)
            {
                ShowError($"Wystąpił błąd podczas walidacji czasu: {ex.Message}");
                return false;
            }
        }
        public static List<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT ID, Title, Genre, Duration FROM Movies";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movie
                        {
                            ID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Genre = reader.GetString(2),
                            Duration = reader.GetInt32(3)
                        });
                    }
                }
            }
            return movies;
        }






    }
}
