using System.Windows;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy LogowanieRejestracja.xaml
    /// </summary>
    public partial class LogowanieRejestracja : Window
    {
        public LogowanieRejestracja()
        {
            InitializeComponent();
        }

        private void btnLogowanie_Click(object sender, RoutedEventArgs e)
        {
            Logowanie logowanie = new Logowanie();
            logowanie.Show();
            this.Close();
        }

        private void btnRejestracja_Click(object sender, RoutedEventArgs e)
        {
            Rejestracja rejestracja = new Rejestracja();
            rejestracja.Show();
            this.Close();

        }

        private void btnStronaGlowna_Click(object sender, RoutedEventArgs e)
        {
            Strona_glowna strona_Glowna = new Strona_glowna();
            strona_Glowna.Show();
            this.Close();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            GlownaStronaPracownika glownaStronaPracownika = new GlownaStronaPracownika();
            glownaStronaPracownika.Show();
            this.Close();
        }

        private void Uzytkownik_Click(object sender, RoutedEventArgs e)
        {
            Strona_glowna strona_Glowna = new Strona_glowna();
            strona_Glowna.Show();
            this.Close();
        }
    }
}
