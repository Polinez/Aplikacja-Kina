using AppKina.MainPage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy SalaKinowa.xaml
    /// </summary>
    public partial class SalaKinowa : Window
    {
        public SalaKinowa()
        {
            InitializeComponent();
            GenerateSeats();
        }

        private void GenerateSeats()
        {
            for (int i = 1; i <= 100; i++) // 100 miejsc
            {
                CheckBox seatCheckBox = new CheckBox
                {
                    Content = i.ToString(),
                    Background = Brushes.Green,
                    Foreground = Brushes.Black,
                    Margin = new Thickness(2), // Marginesy między checkboxami
                    VerticalAlignment = VerticalAlignment.Center
                };
                seatCheckBox.Checked += WyborMiejsca_Checked;
                seatCheckBox.Unchecked += WyborMiejsca_Unchecked;
                SeatGrid.Children.Add(seatCheckBox);
            }
        }

        private void WyborMiejsca_Checked(object sender, RoutedEventArgs e)
        {
            // Obsługuje zaznaczenie miejsca
            CheckBox checkBox = (CheckBox)sender;
            checkBox.Background = Brushes.Black;
        }

        private void WyborMiejsca_Unchecked(object sender, RoutedEventArgs e)
        {
            // Obsługuje odznaczenie miejsca
            CheckBox checkBox = (CheckBox)sender;
            checkBox.Background = Brushes.Green;
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

        private void StronaGlowna_click(object sender, RoutedEventArgs e)
        {
            Strona_glowna strona_Glowna = new Strona_glowna();
            strona_Glowna.Show();
            this.Close();
        }

        private void Zarezerwuj_click(object sender, RoutedEventArgs e)
        {
            // Zbieranie wybranych miejsc i logika rezerwacji
            var selectedSeats = new List<int>();
            foreach (CheckBox seat in SeatGrid.Children)
            {
                if (seat.IsChecked == true)
                {
                    selectedSeats.Add(int.Parse(seat.Content.ToString()));
                }
            }

            if (selectedSeats.Count > 0)
            {
                // Przetwarzanie rezerwacji
                MessageBox.Show("Wybrane miejsca: " + string.Join(", ", selectedSeats));
            }
            else
            {
                MessageBox.Show("Proszę wybrać miejsca.");
            }
        }
    }
}
