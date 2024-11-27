using AppKina.Admin;
using Microsoft.Win32;
using System.Windows;
using WpfApp;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy DodajFilm.xaml
    /// </summary>
    public partial class DodajFilm : Window
    {
        // Zmienna do przechowywania ścieżki wybranego plakatu
        private string sciezkaPlakatu;
        public DodajFilm()
        {
            InitializeComponent();
        }

        private void WyborZjd_Click(object sender, RoutedEventArgs e)
        {
            // Tworzenie nowego obiektu OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Ustawienia filtru plików na JPEG i PNG
            openFileDialog.Filter = "Image Files (*.jpeg;*.jpg;*.png)|*.jpeg;*.jpg;*.png";

            // Otwieranie okna dialogowego
            bool? result = openFileDialog.ShowDialog();

            // Sprawdzenie, czy użytkownik wybrał plik
            if (result == true)
            {
                // Ścieżka do wybranego pliku
                sciezkaPlakatu = openFileDialog.FileName;

            }
        }

        private void powrot_click(object sender, RoutedEventArgs e)
        {
            GlownaStronaPracownika glownaStronaPracownika = new GlownaStronaPracownika();
            glownaStronaPracownika.Show();

            this.Close();
        }

        private void DodajFilm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Walidacja i konwersja danych wejściowych
                string tytul = TBtytul.Text;
                string gatunek = TBgatunek.Text;
                string rezyser = TBRezyser.Text;
                string obsada = TBObsada.Text;
                string opis = TBopis.Text;
                int czasTrwania = 0;

                if (string.IsNullOrWhiteSpace(tytul) ||
                    string.IsNullOrWhiteSpace(gatunek) ||
                    string.IsNullOrWhiteSpace(rezyser) ||
                    string.IsNullOrWhiteSpace(obsada) ||
                    string.IsNullOrWhiteSpace(opis) ||
                    !int.TryParse(TBczasTrwania.Text, out czasTrwania) ||
                    czasTrwania <= 0)
                {
                    MessageBox.Show("Proszę poprawnie wypełnić wszystkie pola.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Tworzenie nowego obiektu klasy Film
                Film nowyFilm = new Film(tytul, gatunek, rezyser, obsada, czasTrwania, opis, sciezkaPlakatu);


                // Przykład dodania obiektu do bazy danych lub kolekcji
                // Zapis filmu do bazy danych
                if (DatabaseHelper.AddFilm(nowyFilm))
                {
                    MessageBox.Show("Film został dodany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }


                // Opcjonalne czyszczenie formularza
                TBtytul.Clear();
                TBgatunek.Clear();
                TBRezyser.Clear();
                TBObsada.Clear();
                TBczasTrwania.Clear();
                TBopis.Clear();
                sciezkaPlakatu = null;
            }
            catch (Exception ex)
            {
                // Obsługa błędów
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
