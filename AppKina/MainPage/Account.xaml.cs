using System.Windows;

namespace AppKina.MainPage
{
    /// <summary>
    /// Logika interakcji dla klasy Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        public static string UserEmail { get; set; }

        public Account()
        {
            InitializeComponent();
            WyswietlEmail();  // Wyświetl e-mail użytkownika po otwarciu okna
        }

        private void WyswietlEmail()
        {
            // Sprawdzamy, czy e-mail jest dostępny w statycznej zmiennej UserEmail
            if (!string.IsNullOrEmpty(UserEmail))
            {
                UserEmailTextBlock.Text = UserEmail;  // Wyświetlamy e-mail w kontrolce TextBlock
            }
            else
            {
                UserEmailTextBlock.Text = "Brak zalogowanego użytkownika";  // Komunikat, gdy nikt nie jest zalogowany
            }
        }



        private void StronaGlowna_Click(object sender, RoutedEventArgs e)
        {
            Strona_glowna strona_Glowna = new Strona_glowna();
            strona_Glowna.Show();
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
            //nic ma sie nie dziac
        }

        private void ZmienHaslo_click(object sender, RoutedEventArgs e)
        {
            //function that changes password
        }

        private void Wyloguj_click(object sender, RoutedEventArgs e)
        {
            // Zresetuj statyczną zmienną przechowującą e-mail
            UserEmail = null;

            // Zaktualizuj kontrolkę
            UserEmailTextBlock.Text = "Nie jesteś zalogowany.";

            // Przejdź do okna logowania
            LogowanieRejestracja logowanieRejestracja = new LogowanieRejestracja();
            logowanieRejestracja.Show();
            this.Close();
        }
    }
}

