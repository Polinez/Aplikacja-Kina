using System.Windows;

namespace AppKina
{
    public partial class Zarezerwuj : Window
    {
        public Zarezerwuj()
        {
            InitializeComponent();
        }

        private void button_stronaGlowna_Click(object sender, RoutedEventArgs e)
        {
            Strona_glowna stronaGlowna = new Strona_glowna();
            stronaGlowna.Show();
            this.Close();
        }

        private void button_zarezerwuj_Click(object sender, RoutedEventArgs e)
        {

        }
        private void button_mojeRezerwacje_Click(object sender, RoutedEventArgs e)
        {
            MojeRezerwacje mojeRezerwacje = new MojeRezerwacje();
            mojeRezerwacje.Show();
            this.Close();
        }

        private void button_mojeKonto_Click(object sender, RoutedEventArgs e)
        {
            //Strona_glowna stronaGlowna = new Strona_glowna();
            //stronaGlowna.Show();
            //this.Close();
        }

        private void button_dalej_Click(object sender, RoutedEventArgs e)
        {
            RezerwacjaSeansu rezerwacjaSeansu = new RezerwacjaSeansu();
            rezerwacjaSeansu.Show();
            this.Close();
        }
    }
}
