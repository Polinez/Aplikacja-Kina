using System.Windows;
using WpfApp;
using static WpfApp.DatabaseHelper;

namespace AppKina.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy DodajSeans.xaml
    /// </summary>
    public partial class DodajSeans : Window
    {
        public DodajSeans()
        {
            InitializeComponent();


            try
            {
                // Załaduj listę filmów
                var movies = DatabaseHelper.GetAllMovies();
                LBMovies.ItemsSource = movies;

                // Formaty seansów
                LBFormat.ItemsSource = new List<string> { "2D", "3D", "IMAX" };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas ładowania danych: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BTdodaj(object sender, RoutedEventArgs e)
        {
            try
            {
                // Pobranie danych z formularza
                var selectedMovie = (Film)LBMovies.SelectedItem;
                if (selectedMovie == null)
                {
                    MessageBox.Show("Wybierz film!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (TBdata.SelectedDate == null)
                {
                    MessageBox.Show("Wprowadź poprawną datę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var parsedDate = TBdata.SelectedDate.Value;

                if (string.IsNullOrWhiteSpace(TBgodzina.Text) || !TimeSpan.TryParse(TBgodzina.Text, out var startTime))
                {
                    MessageBox.Show("Wprowadź poprawną godzinę w formacie HH:MM!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var format = (string)LBFormat.SelectedItem;
                if (format == null)
                {
                    MessageBox.Show("Wybierz format seansu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TBcena.Text) || !double.TryParse(TBcena.Text, out var price))
                {
                    MessageBox.Show("Wprowadź poprawną cenę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!MovieExists(selectedMovie.ID))
                {
                    MessageBox.Show("Wybrany film nie istnieje w bazie danych!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Obliczenie końca seansu
                var movieDuration = TimeSpan.FromMinutes(selectedMovie.CzasTrwania);
                var endTime = startTime.Add(movieDuration);

                // Walidacja czasów seansów
                if (!IsSeansTimeAvailable(selectedMovie.ID, parsedDate.ToString("yyyy-MM-dd"), startTime, endTime))
                {
                    MessageBox.Show("Czas seansu koliduje z innym seansem!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int MovieID = selectedMovie.ID;
                string Date = parsedDate.ToString("yyyy-MM-dd");
                string StartTime = startTime.ToString(@"hh\:mm");
                string Format = format;
                double Price = price;

                // Utworzenie obiektu seansu
                var seans = new Seans(MovieID, Date, StartTime, Format, Price);



                // Dodanie seansu do bazy
                if (AddSeans(seans))
                {
                    MessageBox.Show("Seans został dodany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    TBcena.Text = "";
                    TBdata.Text = "";
                    TBgodzina.Text = "";
                    LBFormat.Text = "";
                    LBMovies.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BTpowrot(object sender, RoutedEventArgs e)
        {
            var glownaStronaPracownika = new GlownaStronaPracownika();
            glownaStronaPracownika.Show();
            this.Close();
        }


        public static bool MovieExists(int movieID)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT COUNT(*) FROM Movies WHERE ID = @MovieID";
                    command.Parameters.AddWithValue("@MovieID", movieID);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas sprawdzania istnienia filmu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
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
                MessageBox.Show($"Wystąpił błąd podczas dodawania seansu do bazy danych: {ex.Message}");
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
                MessageBox.Show($"Wystąpił błąd podczas walidacji czasu: {ex.Message}");
                return false;
            }
        }
    }

}
