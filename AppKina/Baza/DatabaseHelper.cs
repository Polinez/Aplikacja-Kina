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
                CreateReservationsTable(connection);
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

        private static void CreateReservationsTable(SqliteConnection connection)
        {
            try
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Reservations (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserID INT NOT NULL,
                        ProjectionID INT NOT NULL,
                        Seats TEXT NOT NULL,
                        FOREIGN KEY(UserID) REFERENCES Users(ID),
                        FOREIGN KEY(ProjectionID) REFERENCES Seanse(ID)
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


        public static List<Film> GetAllMovies()
        {
            var movies = new List<Film>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT ID, Title, Genre, Director, \"Cast\", Duration, Description, PosterPath FROM Movies";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string tytul = reader.GetString(1);
                        string gatunek = reader.GetString(2);
                        string rezyser = reader.GetString(3);
                        string obsada = reader.GetString(4);
                        int czasTrwania = reader.GetInt32(5);
                        string opis = reader.GetString(6);
                        string sciezkaPlakatu = reader.GetString(7);



                        var film = new Film(tytul, gatunek, rezyser, obsada, czasTrwania, opis, sciezkaPlakatu)
                        {
                            ID = id
                        };

                        movies.Add(film);
                    }

                }
            }
            return movies;
        }
        public static Film GetMovieById(int id)
        {
            try
            {
                // Połączenie z bazą danych (przykład SQL)
                using (var connection = new SqliteConnection("Data Source=KinoDB.db"))
                {
                    connection.Open();
                    var command = new SqliteCommand("SELECT * FROM Movies WHERE ID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Tworzymy obiekt Film z danych z bazy
                            return new Film
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Tytul = reader["Title"].ToString(),
                                Gatunek = reader["Genre"].ToString(),
                                Opis = reader["Description"].ToString(),
                                SciezkaPlakatu = reader["PosterPath"].ToString(),
                                Rezyser = reader["Director"].ToString(),
                                Obsada = reader["Cast"].ToString(),
                                CzasTrwania = Convert.ToInt32(reader["Duration"])
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas ładowania filmu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public static int GetRandomMovieId()
        {
            try
            {
                using (var connection = new SqliteConnection("Data Source=KinoDB.db"))
                {
                    connection.Open();
                    var command = new SqliteCommand("SELECT ID FROM Movies ORDER BY RANDOM() LIMIT 1", connection);

                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result); // Poprawione rzutowanie
                    }
                    else
                    {
                        return -1; // Brak wyników
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas losowania ID filmu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }




    }
}
