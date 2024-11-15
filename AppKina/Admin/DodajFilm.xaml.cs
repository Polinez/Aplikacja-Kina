using Microsoft.Win32;
using System.Windows;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy DodajFilm.xaml
    /// </summary>
    public partial class DodajFilm : Window
    {
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
                string selectedFilePath = openFileDialog.FileName;

                // Możesz teraz wykorzystać wybraną ścieżkę, np. wyświetlić w TextBlocku
                // lub załadować obraz do Image itp.
                MessageBox.Show("Wybrano plik: " + selectedFilePath); // Dla przykładu, wyświetlenie ścieżki w oknie
            }
        }

        private void anuluj_click(object sender, RoutedEventArgs e)
        {
            GlownaStronaPracownika glownaStronaPracownika = new GlownaStronaPracownika();
            glownaStronaPracownika.Show();

            this.Close();
        }

    }
}
