using Microsoft.Data.Sqlite;
using System.Windows;

namespace AppKina
{
    public partial class Logowanie : Window
    {
        public Logowanie()
        {
            InitializeComponent();
        }

        // Obsługuje kliknięcie przycisku "Powrót"
        private void button_powrot_Click(object sender, RoutedEventArgs e)
        {
            // Powrót do strony rejestracji
            LogowanieRejestracja logowanieRejestracja = new LogowanieRejestracja();
            logowanieRejestracja.Show();
            this.Close();
        }

        // Obsługuje kliknięcie przycisku "Zaloguj"
        private void buttom_zaloguj_Click(object sender, RoutedEventArgs e)
        {
            string email = textBox_email.Text.Trim();
            string haslo = textBox_password.Password.Trim();

            // Walidacja danych wejściowych
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(haslo))
            {
                MessageBox.Show("Proszę wprowadzić email i hasło.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Próba logowania użytkownika
            try
            {
                string Role = LoginUser(email, haslo);
                if (Role == "admin")
                {
                    MessageBox.Show("Zalogowano pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    GlownaStronaPracownika glownaStronaPracownika = new GlownaStronaPracownika();
                    glownaStronaPracownika.Show();
                    this.Close();
                }
                else if (Role == "user")
                {
                    MessageBox.Show("Zalogowano pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    Strona_glowna strona_glowna = new Strona_glowna();
                    strona_glowna.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Niepoprawny email lub hasło.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metoda do logowania użytkownika
        private string LoginUser(string email, string haslo)
        {
            using (var connection = new SqliteConnection($"Data Source=KinoDB.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT Role 
                    FROM Users 
                    WHERE Email = @Email AND Password = @Password;
                ";

                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", haslo);

                var result = command.ExecuteScalar(); // Zwraca liczbę pasujących rekordów
                return result != null ? result.ToString() : null;
            }
        }
    }
}
