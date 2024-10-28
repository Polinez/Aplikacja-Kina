using System.Windows;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy GlownaStronaPracownika.xaml
    /// </summary>
    public partial class GlownaStronaPracownika : Window
    {
        public GlownaStronaPracownika()
        {
            InitializeComponent();
        }

        private void Dodaj_film_Click(object sender, RoutedEventArgs e)
        {
            DodajFilm dodajFilm = new DodajFilm();
            dodajFilm.Show();
            this.Close();
        }
    }
}
