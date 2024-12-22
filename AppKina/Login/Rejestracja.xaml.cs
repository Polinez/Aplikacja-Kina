using Microsoft.Data.Sqlite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace AppKina
{
    public partial class Rejestracja : Window
    {
        private const string databasePath = $"Data Source=KinoDB.db";
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
        private void button_zarejestruj_Click(object sender, RoutedEventArgs e)
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
            string email;
            List<string> emails = GetUsersEmails();
            if (!emails.Contains(textBox_nowyEmail.Text.Trim()))
            {
                email = textBox_nowyEmail.Text.Trim();
            }
            else 
            {
                MessageBox.Show("Konto z takim adresem email już istnieje.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string haslo = textBox_noweHaslo.Password.ToString();
            string powtorzHaslo = textBox_powtorzNoweHaslo.Password.ToString();


            // Walidacja danych
            if (string.IsNullOrEmpty(imie) || string.IsNullOrEmpty(nazwisko) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(haslo) || string.IsNullOrEmpty(powtorzHaslo))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (haslo.Length < 8) 
            {
                MessageBox.Show("Hasło musi zawierać minimum 8 znaków.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                LogowanieRejestracja logowanieRejestracja = new LogowanieRejestracja();
                logowanieRejestracja.Show();
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
                command.Parameters.AddWithValue("@Role", "user"); //domyslnie tworzy usera 

                command.ExecuteNonQuery(); // Wykonanie komendy wstawiającej dane
                connection.Close();
            }
        }

        private List<string> GetUsersEmails()
        {
            List<string> emails = new List<string>();

            try
            {

                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();

                    string query = $"SELECT Users.Email FROM Users";
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            emails.Add(reader.GetString(0));
                        }
                    }
                    connection.Close();

                    return emails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Window regulaminWindow = new Window
            {
                Title = "Regulamin",
                Height = 400,
                Width = 600,
                Background = new SolidColorBrush(Colors.Black),
                Foreground = new SolidColorBrush(Colors.White),
                FontFamily = new FontFamily("Segoe UI Variable Small Semibold"),
                Content = new ScrollViewer
                {
                    Content = new TextBlock
                    {
                        Text = "Regulamin Korzystania z Aplikacji Kina\r\n\r\n§1. Postanowienia ogólne\r\n\r\nNiniejszy regulamin określa zasady korzystania z aplikacji mobilnej kina (dalej: \"Aplikacja\").\r\n\r\nAplikacja jest własnością Kina Światowid z siedzibą w Katowicach, NIP: [numer], REGON: [numer].\r\n\r\nPobranie, instalacja i korzystanie z Aplikacji oznacza akceptację niniejszego regulaminu.\r\n\r\n§2. Funkcjonalności Aplikacji\r\n\r\nAplikacja umożliwia użytkownikom:\r\na. Przeglądanie repertuaru kina.\r\nb. Zakup biletów na seanse filmowe.\r\nc. Korzystanie z programów lojalnościowych.\r\nd. Otrzymywanie powiadomień o nowościach i promocjach.\r\ne. Rezerwację miejsc na wybrane seanse.\r\n\r\nKorzystanie z funkcji wymaga założenia konta użytkownika.\r\n\r\n§3. Rejestracja i logowanie\r\n\r\nRejestracja w Aplikacji jest dobrowolna i bezpłatna.\r\n\r\nUżytkownik zobowiązany jest do podania prawdziwych danych podczas rejestracji.\r\n\r\nLogowanie do Aplikacji odbywa się za pomocą adresu e-mail i hasła lub innego udostępnionego mechanizmu (np. logowania za pośrednictwem portali społecznościowych).\r\n\r\n§4. Zakup biletów\r\n\r\nZakup biletów poprzez Aplikację jest równoznaczny z zawarciem umowy między użytkownikiem a kinem.\r\n\r\nPłatności za bilety dokonywane są za pomocą zintegrowanych systemów płatności.\r\n\r\nKino zastrzega sobie prawo do anulowania rezerwacji w przypadku braku otrzymania płatności w wyznaczonym czasie.\r\n\r\nZwrot biletów możliwy jest zgodnie z polityką zwrotów dostępną w regulaminie kina.\r\n\r\n§5. Ochrona danych osobowych\r\n\r\nAdministratorem danych osobowych użytkowników jest [Nazwa Kina].\r\n\r\nDane osobowe przetwarzane są zgodnie z obowiązującymi przepisami prawa, w tym z Rozporządzeniem Parlamentu Europejskiego i Rady (UE) 2016/679 (RODO).\r\n\r\nSzczegółowe informacje na temat przetwarzania danych osobowych zawarte są w Polityce Prywatności dostępnej w Aplikacji.\r\n\r\n§6. Obowiązki użytkownika\r\n\r\nUżytkownik zobowiązuje się do korzystania z Aplikacji w sposób zgodny z prawem oraz niniejszym regulaminem.\r\n\r\nZabrania się dostarczania treści o charakterze bezprawnym za pośrednictwem Aplikacji.\r\n\r\nUżytkownik ponosi odpowiedzialność za poufność swojego hasła i innych danych logowania.\r\n\r\n§7. Postanowienia końcowe\r\n\r\nKino zastrzega sobie prawo do dokonywania zmian w regulaminie. O wszelkich zmianach użytkownicy zostaną poinformowani za pośrednictwem Aplikacji.\r\n\r\nWszelkie spory wynikające z korzystania z Aplikacji będą rozstrzygane przez sąd właściwy dla siedziby kina.\r\n\r\nW przypadku pytań lub wątpliwości dotyczących Aplikacji, prosimy o kontakt pod adresem e-mail: kino@swiatowid.katowice.pl.\r\n\r\n",
                        TextWrapping = TextWrapping.Wrap,
                        Padding = new Thickness(10)
                    },
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                }
            };
            regulaminWindow.ShowDialog();
            e.Handled = true;
        }
    }
}
