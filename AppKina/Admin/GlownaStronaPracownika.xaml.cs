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

        private void Dodaj_seans_click(object sender, RoutedEventArgs e)
        {
            DodajSeans dodajSeans = new DodajSeans();
            dodajSeans.Show();

            this.Close();
        }



        private void Usun_film_click(object sender, RoutedEventArgs e)
        {
            UsunFilm usunFilm = new UsunFilm();
            usunFilm.Show();

            this.Close();
        }

        private void Usun_seans_click(object sender, RoutedEventArgs e)
        {
            UsunSeans usunSeans = new UsunSeans();
            usunSeans.Show();

            this.Close();
        }
    }
}
