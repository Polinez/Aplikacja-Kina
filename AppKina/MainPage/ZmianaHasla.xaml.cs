using System.Windows;
using WpfApp;

namespace AppKina.MainPage
{

    public partial class ZmianaHasla : Window
    {
        public static string Email { get; set; }

        public ZmianaHasla()
        {
            InitializeComponent();
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



        private void Zmien_click(object sender, RoutedEventArgs e)
        {
            string stareHaslo = stareHasloPB.Password.Trim();
            string noweHaslo = noweHasloPB.Password.Trim();

            if (string.IsNullOrEmpty(stareHaslo) || string.IsNullOrEmpty(noweHaslo))
            {
                MessageBox.Show("Proszę wypełnić oba pola hasła.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //weryfikacja hasla i jego zmiana 
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    // Sprawdzenie, czy podano poprawne stare hasło
                    var checkCommand = connection.CreateCommand();
                    checkCommand.CommandText = @"
                SELECT COUNT(*) FROM Users
                WHERE Email = @Email AND Password = @Password;
            ";
                    checkCommand.Parameters.AddWithValue("@Email", Email);
                    checkCommand.Parameters.AddWithValue("@Password", stareHaslo);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count == 0)
                    {
                        MessageBox.Show("Podano niepoprawne aktualne hasło.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Aktualizacja hasła w bazie
                    var updateCommand = connection.CreateCommand();
                    updateCommand.CommandText = @"
                UPDATE Users
                SET Password = @NewPassword
                WHERE Email = @Email;
            ";
                    updateCommand.Parameters.AddWithValue("@NewPassword", noweHaslo);
                    updateCommand.Parameters.AddWithValue("@Email", Email);

                    updateCommand.ExecuteNonQuery();
                    MessageBox.Show("Hasło zostało pomyślnie zmienione.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas zmiany hasła: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
