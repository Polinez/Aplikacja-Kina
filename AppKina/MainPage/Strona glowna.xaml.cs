using AppKina.Admin;
using AppKina.MainPage;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApp;
using static System.Net.Mime.MediaTypeNames;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy Strona_glowna.xaml
    /// </summary>
    public partial class Strona_glowna : Window
    {
        public Strona_glowna()
        {
            InitializeComponent();
            LoadMovies();
            PokazLosowyFilm();
        }

        /// <summary>
        /// Wczytuje filmy z bazy danych i dodaje je dynamicznie do interfejsu użytkownika.
        /// </summary>
        private void LoadMovies()
        {
            List<Film> films = GetAllMoviesWithProjections();

            foreach (var film in films)
            {
                // Tworzenie kontenera na pojedynczy film
                StackPanel filmPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 10, 0, 10)
                };

                // Obraz filmu
                System.Windows.Controls.Image poster = new System.Windows.Controls.Image
                {
                    Width = 100,
                    Height = 150,
                    Margin = new Thickness(0, 0, 10, 0),
                    Source = new BitmapImage(new System.Uri(film.SciezkaPlakatu, System.UriKind.RelativeOrAbsolute)),
                    Stretch = System.Windows.Media.Stretch.Fill
                };

                // Szczegóły filmu
                StackPanel detailsPanel = new StackPanel();

                TextBlock title = new TextBlock
                {
                    Text = film.Tytul,
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Foreground = System.Windows.Media.Brushes.White
                };

                TextBlock genre = new TextBlock
                {
                    Text = film.Gatunek,
                    FontSize = 14,
                    Foreground = System.Windows.Media.Brushes.Gray
                };

                TextBlock description = new TextBlock
                {
                    Text = film.Opis.Length > 200 ? film.Opis.Substring(0, 200) + "..." : film.Opis,
                    FontSize = 12,
                    Foreground = System.Windows.Media.Brushes.White,
                    TextWrapping = TextWrapping.Wrap,
                    MaxWidth = 420
                };

                Button readMoreButton = new Button
                {
                    Content = "Czytaj więcej",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Background = System.Windows.Media.Brushes.Black,
                    Foreground = System.Windows.Media.Brushes.Gray,
                    FontSize = 12,
                    FontStyle = FontStyles.Italic,
                    Tag = film // Przekazanie obiektu filmu jako "Tag"
                };

                readMoreButton.Click += ReadMoreButton_Click;

                // Dodanie szczegółów filmu do panelu szczegółów
                detailsPanel.Children.Add(title);
                detailsPanel.Children.Add(genre);
                detailsPanel.Children.Add(description);
                detailsPanel.Children.Add(readMoreButton);

                // Dodanie obrazu i szczegółów do panelu filmu
                filmPanel.Children.Add(poster);
                filmPanel.Children.Add(detailsPanel);

                // Dodanie panelu filmu do głównego kontenera w UI
                MainMoviesStackPanel.Children.Add(filmPanel);
            }
        }

        /// <summary>
        /// Obsługuje kliknięcie przycisku "Czytaj więcej" i otwiera szczegóły filmu.
        /// </summary>
        private void ReadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag is Film selectedFilm)
            {
                SzczegolyFilmu szczegolyFilmu = new SzczegolyFilmu(selectedFilm);
                szczegolyFilmu.Show();
                this.Close();
            }
        }

        private void Rezerwuj_click(object sender, RoutedEventArgs e)
        {
            Zarezerwuj zarezerwuj = new Zarezerwuj();
            zarezerwuj.Show();
            this.Close();
        }

        private void MojeRezerwacje_click(object sender, RoutedEventArgs e)
        {
            MojeRezerwacje mojeRezerwacje = new MojeRezerwacje();
            mojeRezerwacje.Show();
            this.Close();
        }

        private void MojeKonto_click(object sender, RoutedEventArgs e)
        {
            Account account = new Account();
            account.Show();
            this.Close();

        }

        // Funkcja do wyświetlania losowego filmu
        private void PokazLosowyFilm()
        {
            try
            {
                // Pobieranie losowego ID filmu
                int randomFilmId = DatabaseHelper.GetRandomMovieId();

                if (randomFilmId != -1)
                {
                    // Pobieranie filmu na podstawie wylosowanego ID
                    Film losowyFilm = DatabaseHelper.GetMovieById(randomFilmId);

                    if (losowyFilm != null)
                    {
                        // Wyświetlanie szczegółów filmu
                        tytul_textblock.Text = losowyFilm.Tytul;
                        gatunek_textblock.Text = losowyFilm.Gatunek;
                        opis_textblock.Text = losowyFilm.Opis;

                        // Wczytanie plakatu filmu
                        if (!string.IsNullOrEmpty(losowyFilm.SciezkaPlakatu) && File.Exists(losowyFilm.SciezkaPlakatu))
                        {
                            plakat_image.Source = new BitmapImage(new Uri(losowyFilm.SciezkaPlakatu, UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            plakat_image.Source = new BitmapImage(new Uri("path_to_default_image.jpg", UriKind.RelativeOrAbsolute));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nie udało się znaleźć losowego filmu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Brak filmów w bazie danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public static List<Film> GetAllMoviesWithProjections()
        {
            var movies = new List<Film>();
            using (var connection = new SqliteConnection("Data Source=KinoDB.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT ID, Title, Genre, Director, \"Cast\", Duration, Description, PosterPath FROM Movies WHERE Movies.ID IN (SELECT Seanse.MovieID FROM Seanse) ";
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
    }
}
