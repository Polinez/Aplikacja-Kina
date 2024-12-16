using AppKina.Admin;
using AppKina.MainPage;
using Microsoft.Data.Sqlite;
using System.Windows;

namespace AppKina
{
    public partial class MojeRezerwacje : Window
    {
        private const string databasePath = @"Data Source=KinoDB.db";
        private string userEmail = Account.UserEmail;
        private int userID;
        public MojeRezerwacje()
        {
            InitializeComponent();
            GetUserID();
            ShowReservations();
        }

        private void button_stronaGlowna_Click(object sender, RoutedEventArgs e)
        {
            Strona_glowna stronaGlowna = new Strona_glowna();
            stronaGlowna.Show();
            this.Close();
        }

        private void button_zarezerwuj_Click(object sender, RoutedEventArgs e)
        {
            Zarezerwuj zarezerwuj = new Zarezerwuj();
            zarezerwuj.Show();
            this.Close();
        }
        private void button_mojeRezerwacje_Click(object sender, RoutedEventArgs e)
        {
            MojeRezerwacje mojeRezerwacje = new MojeRezerwacje();
            mojeRezerwacje.Show();
            this.Close();
        }

        private void button_mojeKonto_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account();
            account.Show();
            this.Close();
        }

        private void ShowReservations()
        {
            listBox_Reservations.Items.Clear();
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string zapytanie = $"SELECT Reservations.ID, Seanse.Date, Seanse.StartTime, Movies.Title, Reservations.Seats FROM Reservations JOIN Seanse ON Reservations.ProjectionID=Seanse.ID JOIN Movies ON Seanse.MovieID=Movies.ID WHERE Reservations.UserID={userID}";
                    using (var komenda = new SqliteCommand(zapytanie, connection))
                    using (var reader = komenda.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            listBox_Reservations.Items.Add(new Reservation.ReservationClass
                            {
                                id = int.Parse(reader.GetString(0)),
                                dateTime = reader.GetString(1) + " " + reader.GetString(2),
                                title = reader.GetString(3),
                                seats = reader.GetString(4)
                            });
                        }
                        if (listBox_Reservations.Items.Count == 0) 
                        {
                            MessageBox.Show("Nie masz żadnych rezerwacji.");
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
