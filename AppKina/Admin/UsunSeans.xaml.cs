using System.Windows;

namespace AppKina.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy UsunSeans.xaml
    /// </summary>
    public partial class UsunSeans : Window
    {
        public UsunSeans()
        {
            InitializeComponent();
        }

        private void BTusun(object sender, RoutedEventArgs e)
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
