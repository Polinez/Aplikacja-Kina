using AppKina.Admin;
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

        private void powrot_click(object sender, RoutedEventArgs e)
        {
            LogowanieRejestracja logowanieRejestracja = new LogowanieRejestracja();
            logowanieRejestracja.Show();
            this.Close();
        }

        private void BTdodajSeans(object sender, RoutedEventArgs e)
        {
            DodajSeans dodajSeans = new DodajSeans();
            dodajSeans.Show();
            this.Close();
        }

        private void BTusunSeans(object sender, RoutedEventArgs e)
        {
            UsunSeans usunSeans = new UsunSeans();
            usunSeans.Show();
            this.Close();
        }
    }
}
