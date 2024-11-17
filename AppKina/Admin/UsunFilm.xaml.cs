using System.Windows;

namespace AppKina.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy UsunFilm.xaml
    /// </summary>
    public partial class UsunFilm : Window
    {
        public UsunFilm()
        {
            InitializeComponent();
        }

        private void anuluj_click(object sender, RoutedEventArgs e)
        {
            GlownaStronaPracownika glownaStronaPracownika = new GlownaStronaPracownika();
            glownaStronaPracownika.Show();

            this.Close();
        }
    }
}
