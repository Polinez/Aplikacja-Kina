using System.Windows;

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
        }
<<<<<<< Updated upstream
=======

        private void WiecejBttn1(object sender, RoutedEventArgs e)
        {
            SzczegolyFilmu szczegolyFil = new SzczegolyFilmu();
            szczegolyFil.Show();
            this.Close();

        }
        private void WiecejBttn2(object sender, RoutedEventArgs e)
        {
            SzczegolyFilmu szczegolyFil = new SzczegolyFilmu();
            szczegolyFil.Show();
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


        }
>>>>>>> Stashed changes
    }
}
