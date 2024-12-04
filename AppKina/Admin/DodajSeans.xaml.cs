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
                var selectedMovie = (Movie)LBMovies.SelectedItem;
                if (selectedMovie == null)
                {
                    MessageBox.Show("Wybierz film!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TBdata.Text) || !DateTime.TryParse(TBdata.Text, out var parsedDate))
                {
                    MessageBox.Show("Wprowadź poprawną datę w formacie YYYY-MM-DD!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

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

                // Obliczenie końca seansu
                var movieDuration = TimeSpan.FromMinutes(selectedMovie.Duration);
                var endTime = startTime.Add(movieDuration);

                // Walidacja czasów seansów
                if (!DatabaseHelper.IsSeansTimeAvailable(selectedMovie.ID, parsedDate.ToString("yyyy-MM-dd"), startTime, endTime))
                {
                    MessageBox.Show("Czas seansu koliduje z innym seansem!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Utworzenie obiektu seansu
                var seans = new Seans
                {
                    MovieID = selectedMovie.ID,
                    Date = parsedDate.ToString("yyyy-MM-dd"),
                    StartTime = startTime.ToString(@"hh\:mm"),
                    Format = format,
                    Price = price
                };

                // Dodanie seansu do bazy
                if (DatabaseHelper.AddSeans(seans))
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

        private void BTanuluj(object sender, RoutedEventArgs e)
        {
            var glownaStronaPracownika = new GlownaStronaPracownika();
            glownaStronaPracownika.Show();
            this.Close();
        }
    }
}
