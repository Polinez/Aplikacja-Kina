using System.Windows;
using WpfApp;

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

            // Załaduj listę filmów
            //var movies = DatabaseHelper.GetAllMovies();
            //LBMovies.ItemsSource = movies;

            //Przykładowe Filmy bo import nie działa
            LBMovies.ItemsSource = new List<string> { "Venom 3", "Alicaj w krainie czarów", "Faceci w czerni 4" };
            // Formaty seansów
            LBFormat.ItemsSource = new List<string> { "2D", "3D", "IMAX" };
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

                var date = TBdata.Text;
                var startTime = TimeSpan.Parse(TBgodzina.Text);
                var format = (string)LBFormat.SelectedItem;
                if (!double.TryParse(TBcena.Text, out var price))
                {
                    MessageBox.Show("Wprowadź poprawną cenę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var movieDuration = TimeSpan.FromMinutes(selectedMovie.Duration);

                // Walidacja konfliktów czasowych
                if (!DatabaseHelper.IsSeansTimeAvailable(selectedMovie.ID, date, startTime, movieDuration))
                {
                    MessageBox.Show("Czas seansu koliduje z innym seansem!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Dodanie seansu
                if (DatabaseHelper.AddSeans(selectedMovie.ID, date, startTime.ToString(), format, price))
                {
                    MessageBox.Show("Seans został dodany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close(); // Zamknięcie okna po dodaniu
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BTanuluj(object sender, RoutedEventArgs e)
        {
            GlownaStronaPracownika glownaStronaPracownika = new GlownaStronaPracownika();
            glownaStronaPracownika.Show();
            this.Close();
        }
    }
}
