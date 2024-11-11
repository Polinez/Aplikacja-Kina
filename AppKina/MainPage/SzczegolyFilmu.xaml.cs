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
    }
}
