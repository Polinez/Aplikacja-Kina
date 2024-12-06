using AppKina.Admin;
using AppKina.MainPage;
using AppKina.Reservation;
using Microsoft.Data.Sqlite;
using System.Windows;
using WpfApp;

namespace AppKina
{
    public partial class RezerwacjaSeansu : Window
    {
        private const string databasePath = @"Data Source=KinoDB.db";
        public RezerwacjaSeansu(string title)
        {
            InitializeComponent();
            loadProjections(title);
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

        private void button_dalej_Click(object sender, RoutedEventArgs e)
        {
            SalaKinowa salaKinowa = new SalaKinowa();
            salaKinowa.Show();
            this.Close();
        }

        private void button_powrot_Click(object sender, RoutedEventArgs e)
        {
            Zarezerwuj zarezerwuj = new Zarezerwuj();
            zarezerwuj.Show();
            this.Close();
        }

        //private void comboBox_date()
        //{
        //    try
        //    {
        //        loadProjections();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void loadProjections(string title)
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    
                    string query = $"SELECT Seanse.Date FROM Seanse JOIN Movies ON Movies.ID=Seanse.MovieID WHERE Title={title}";
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        List<string> dates = new List<string>();
                        while (reader.Read())
                        {
                            string date = reader.GetString(2);
                            dates.Add(date);
                            listBox_date.Items.Add(date);
                        }
                        if (dates.Count == 0)
                        {
                            MessageBox.Show("Nie ma dostępnych seansów na wybrany film");
                            return;
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
