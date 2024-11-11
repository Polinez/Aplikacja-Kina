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

    }
}
