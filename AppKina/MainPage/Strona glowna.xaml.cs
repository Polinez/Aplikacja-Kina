
﻿using System.Windows;
﻿using AppKina.Admin;
using AppKina.MainPage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApp;


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
        }

        /// <summary>
        /// Wczytuje filmy z bazy danych i dodaje je dynamicznie do interfejsu użytkownika.
        /// </summary>
        private void LoadMovies()
        {
            List<Film> films = DatabaseHelper.GetAllMovies();

            foreach (var film in films)
            {
                // Tworzenie kontenera na pojedynczy film
                StackPanel filmPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 10, 0, 10)
                };

                // Obraz filmu
                Image poster = new Image
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
                    Text = film.Opis,
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
    }
}
