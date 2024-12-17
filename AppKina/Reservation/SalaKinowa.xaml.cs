using AppKina.Admin;
using AppKina.MainPage;
using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy SalaKinowa.xaml
    /// </summary>
    public partial class SalaKinowa : Window
    {
        private const string databasePath = $"Data Source=KinoDB.db";
        private int projectionID;
        private string userEmail = Account.UserEmail;
        private int userID;
        private string? seats;
        public SalaKinowa(int projectionID)
        {
            InitializeComponent();
            GenerateSeats();
            this.projectionID = projectionID;
            seats = null;
            GetUserID();
        }

        private void GenerateSeats()
        {
            for (int i = 1; i <= 100; i++) // 100 miejsc
            {
                CheckBox seatCheckBox = new CheckBox
                {
                    Content = i.ToString(),
                    Background = Brushes.Green,
                    Foreground = Brushes.Black,
                    Margin = new Thickness(2), // Marginesy między checkboxami
                    VerticalAlignment = VerticalAlignment.Center
                };
                seatCheckBox.Checked += WyborMiejsca_Checked;
                seatCheckBox.Unchecked += WyborMiejsca_Unchecked;
                SeatGrid.Children.Add(seatCheckBox);
            }
        }

        private void WyborMiejsca_Checked(object sender, RoutedEventArgs e)
        {
            // Obsługuje zaznaczenie miejsca
            CheckBox checkBox = (CheckBox)sender;
            checkBox.Background = Brushes.Black;
        }

        private void WyborMiejsca_Unchecked(object sender, RoutedEventArgs e)
        {
            // Obsługuje odznaczenie miejsca
            CheckBox checkBox = (CheckBox)sender;
            checkBox.Background = Brushes.Green;
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
            Account account = new Account();
            account.Show();
            this.Close();
        }

        private void StronaGlowna_click(object sender, RoutedEventArgs e)
        {
            Strona_glowna strona_Glowna = new Strona_glowna();
            strona_Glowna.Show();
            this.Close();
        }

        private void Zarezerwuj_click(object sender, RoutedEventArgs e)
        {
            // Zbieranie wybranych miejsc i logika rezerwacji
            var selectedSeats = new List<string>();
            foreach (CheckBox seat in SeatGrid.Children)
            {
                if (seat.IsChecked == true)
                {
                    selectedSeats.Add(seat.Content.ToString());
                }
            }

            if (selectedSeats.Count > 0)
            {
                seats = string.Join(", ", selectedSeats);
                MessageBox.Show("Wybrane miejsca: " + seats);
                try
                {
                    AddReservationToDatabase();
                    MessageBox.Show("Rezerwacja została złożona. Szczegóły rezerwacji możesz sprawdzić w zakładce \"Moje rezerwacje\"", "Rezerwacja przebiegła pomyślnie",MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }

                Strona_glowna strona_Glowna = new Strona_glowna();
                strona_Glowna.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Proszę wybrać miejsca.");
            }
        }

        private void AddReservationToDatabase()
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = @"INSERT INTO Reservations (UserID, ProjectionID, Seats) VALUES (@UserID, @ProjectionID, @Seats)";

                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@ProjectionID", projectionID);
                    command.Parameters.AddWithValue("@Seats", seats);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetUserID()
        {
            try
            {
                
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();

                    string query = $"SELECT Users.ID FROM Users WHERE Users.Email='{userEmail}'";
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userID = int.Parse(reader.GetString(0));
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
