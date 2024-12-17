using System.Windows;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy SzczegolyFilmu.xaml
    /// </summary>
    public partial class SzczegolyFilmu : Window
    {
        //public static Film Film { get; set; }
        public SzczegolyFilmu()
        {
            InitializeComponent();
        }
<<<<<<< Updated upstream
=======
        public SzczegolyFilmu(Film szczegolyFilmu) : this()
        {
            InitializeComponent();
            // Ustaw dane filmu w interfejsie użytkownika
            UstawSzczegolyFilmu(szczegolyFilmu);
        }
        private void UstawSzczegolyFilmu(Film film)
        {
            // Zakładam, że w interfejsie masz kontrolki jak np. TextBlock do wyświetlania danych
            TytulTB.Text = film.Tytul;
            GatunekTB.Text = film.Gatunek;
            OpisTB.Text = "Opis: " + film.Opis;
            zdjeieIM.Source = new BitmapImage(new Uri(film.SciezkaPlakatu, UriKind.RelativeOrAbsolute));
            RezyserTB.Text = "Reżyser: " + film.Rezyser;
            ObsadaTB.Text = "Obsada: " + film.Obsada;
            czasTrwaniaTB.Text = "Czas trwania: " + film.CzasTrwania + " min";
        }
>>>>>>> Stashed changes

        private void StronaGlowna_Click(object sender, RoutedEventArgs e)
        {
            Strona_glowna strona_Glowna = new Strona_glowna();
            strona_Glowna.Show();
            this.Close();
        }
    }
}
