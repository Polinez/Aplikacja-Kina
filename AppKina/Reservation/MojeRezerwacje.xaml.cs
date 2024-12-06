using AppKina.Admin;
using AppKina.MainPage;
using Microsoft.Data.Sqlite;
using System.Windows;

namespace AppKina
{
    public partial class MojeRezerwacje : Window
    {
        private const string databasePath = @"Data Source=KinoDB.db";
        public MojeRezerwacje()
        {
            InitializeComponent();
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
                    string zapytanie = "SELECT Reservations.ID, Date, Seanse.MovieID FROM Reservations JOIN Seanse ON Reservations.ProjectionID=Seanse.ID";
                    using (var komenda = new SqliteCommand(zapytanie, connection))
                    using (var reader = komenda.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nazwaFilmu = reader.GetString(0);
                            listBox_Reservations.Items.Add(nazwaFilmu);
                        }
                        if (listBox_Reservations.Items.Count == 0) 
                        {
                            MessageBox.Show("Nie masz żadnych rezerwacji.");
                            this.Close();
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
