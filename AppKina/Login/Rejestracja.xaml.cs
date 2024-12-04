using Microsoft.Data.Sqlite;
using System.Windows;

namespace AppKina
{
    public partial class Rejestracja : Window
    {
        public Rejestracja()
        {
            InitializeComponent();
        }

        // Obsługa kliknięcia przycisku "Powrót"
        private void button_powrot_Click(object sender, RoutedEventArgs e)
        {
            LogowanieRejestracja logowanieRejestracja = new LogowanieRejestracja();
            logowanieRejestracja.Show();
            this.Close();
        }

        // Obsługa kliknięcia przycisku "Zarejestruj"
        private void button_zarezerwuj_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdzamy, czy użytkownik zaakceptował regulamin
            if (checkbox_Regulamin.IsChecked == false)
            {
                MessageBox.Show("Aby zarejestrować się musisz zaakceptować regulamin.");
                return;
            }

            // Pobieranie danych z formularza
            string imie = textBox_imie.Text.Trim();
            string nazwisko = textBox_nazwisko.Text.Trim();
            string email = textBox_nowyEmail.Text.Trim();
            string haslo = textBox_noweHaslo.Text.Trim();
            string powtorzHaslo = textBox_powtorzNoweHaslo.Text.Trim();

            // Walidacja danych
            if (string.IsNullOrEmpty(imie) || string.IsNullOrEmpty(nazwisko) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(haslo) || string.IsNullOrEmpty(powtorzHaslo))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (haslo != powtorzHaslo)
            {
                MessageBox.Show("Hasła nie są takie same.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Dodanie użytkownika do bazy danych
            try
            {
                AddUserToDatabase(imie, nazwisko, email, haslo);
                MessageBox.Show("Rejestracja zakończona sukcesem!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                Strona_glowna strona_glowna = new Strona_glowna();
                strona_glowna.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metoda do dodania użytkownika do bazy danych
        private void AddUserToDatabase(string imie, string nazwisko, string email, string haslo)
        {
            using (var connection = new SqliteConnection($"Data Source=KinoDB.db"))
            {
                connection.Open();

                // Tworzymy komendę SQL do wstawienia danych
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Users (Username, Email, Password, Role)
                    VALUES (@Username, @Email, @Password, @Role);
                ";

                // Dodajemy parametry
                string username = string.Concat(imie, " ", nazwisko);
                command.Parameters.AddWithValue("@Username", username); // Używamy imienia i nazwiska jako nazwy użytkownika
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", haslo); // Pamiętaj, że hasło w prawdziwej aplikacji powinno być haszowane!
                command.Parameters.AddWithValue("@Role", "user");//domyslnie tworzy usera 

                command.ExecuteNonQuery(); // Wykonanie komendy wstawiającej dane
                connection.Close();
            }
        }
    }
}
