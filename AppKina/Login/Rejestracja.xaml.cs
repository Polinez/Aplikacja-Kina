using System.Windows;


namespace AppKina
{

    public partial class Rejestracja : Window
    {
        public Rejestracja()
        {
            InitializeComponent();

        }

        private void button_powrot_Click(object sender, RoutedEventArgs e)
        {
            LogowanieRejestracja logowanieRejestracja = new LogowanieRejestracja();
            logowanieRejestracja.Show();
            this.Close();
        }

        private void button_zarezerwuj_Click(object sender, RoutedEventArgs e)
        {
            if (checkbox_Regulamin.IsChecked == false)
            {
                MessageBox.Show("Aby zarejestrować się musisz zaakceptować regulamin.");
            }
            else
            {
                Strona_glowna strona_glowna = new Strona_glowna();
                strona_glowna.Show();
                this.Close();
            }
        }
    }
}
