using System.Windows;
using WpfApp;



namespace AppKina
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DatabaseHelper.InitializeDatabase(); // tworzenie bazy danych z pliku Baza/DatabaseHelper.cs
        }

        private void Rozpocznij(object sender, RoutedEventArgs e)
        {
            LogowanieRejestracja logowanieRejestracja = new LogowanieRejestracja();
            logowanieRejestracja.Show();
            this.Close();
        }
    }
}