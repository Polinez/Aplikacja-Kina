using System.Windows;

namespace AppKina.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy DodajSeans.xaml
    /// </summary>
    public partial class DodajSeans : Window
    {
        public DodajSeans()
        {
            InitializeComponent();
        }

        private void BTdodaj(object sender, RoutedEventArgs e)
        {

        }

        private void BTanuluj(object sender, RoutedEventArgs e)
        {
            GlownaStronaPracownika glownaStronaPracownika = new GlownaStronaPracownika();
            glownaStronaPracownika.Show();
            this.Close();
        }
    }
}
