using System.Windows;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy GlownaStronaPracownika.xaml
    /// </summary>
    public partial class GlownaStronaPracownika : Window
    {
        public GlownaStronaPracownika()
        {
            InitializeComponent();
        }

        private void powrot_click(object sender, RoutedEventArgs e)
        {
            LogowanieRejestracja logowanieRejestracja = new LogowanieRejestracja();
            logowanieRejestracja.Show();
            this.Close();
        }
    }
}
