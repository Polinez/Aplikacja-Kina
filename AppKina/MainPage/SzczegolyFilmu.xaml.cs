using AppKina.MainPage;
using System.Windows;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy SzczegolyFilmu.xaml
    /// </summary>
    public partial class SzczegolyFilmu : Window
    {
        public SzczegolyFilmu()
        {
            InitializeComponent();
        }

        private void StronaGlowna_Click(object sender, RoutedEventArgs e)
        {
            Strona_glowna strona_Glowna = new Strona_glowna();
            strona_Glowna.Show();
            this.Close();
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

        private void click_zobacz_seanse(object sender, RoutedEventArgs e)
        {
            RezerwacjaSeansu rezerwacjaSeansu = new RezerwacjaSeansu();
            rezerwacjaSeansu.Show();
            this.Close();
        }
    }
}
