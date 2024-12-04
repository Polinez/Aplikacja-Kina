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

        private void ZaladujFilmy()
        {
            listBox_MovieTitle.Items.Clear();
            try
            {
                using (var polaczenie = new SqliteConnection(databasePath))
                {
                    polaczenie.Open();
                    string zapytanie = "SELECT Title FROM Movies";
                    using (var komenda = new SqliteCommand(zapytanie, polaczenie))
                    using (var reader = komenda.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nazwaFilmu = reader.GetString(0);
                            listBox_MovieTitle.Items.Add(nazwaFilmu);
                        }
                    }
                    polaczenie.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
