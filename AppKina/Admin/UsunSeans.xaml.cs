using Microsoft.Data.Sqlite;
using System.Windows;
using WpfApp;

namespace AppKina.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy UsunSeans.xaml
    /// </summary>
    public partial class UsunSeans : Window
    {
        private const string SciezkaDoBazy = @"Data Source=KinoDB.db";
        public UsunSeans()
        {
            InitializeComponent();
            ZaladujSeanse();
        }

        private void BTusun(object sender, RoutedEventArgs e)
        {
            if (LBSeans.SelectedItem is Seans wybranySeans)
            {
                try
                {
                    using (var polaczenie = new SqliteConnection(SciezkaDoBazy))
                    {
                        polaczenie.Open();
                        string zapytanie = "DELETE FROM Seanse WHERE MovieID = @MovieID AND Date = @Date AND StartTime = @StartTime";

                        using (var komenda = new SqliteCommand(zapytanie, polaczenie))
                        {
                            komenda.Parameters.AddWithValue("@MovieID", wybranySeans.MovieID);
                            komenda.Parameters.AddWithValue("@Date", wybranySeans.Date);
                            komenda.Parameters.AddWithValue("@StartTime", wybranySeans.StartTime);

                            int usuniete = komenda.ExecuteNonQuery();

                            if (usuniete > 0)
                            {
                                MessageBox.Show("Seans został usunięty.");
                                ZaladujSeanse(); // Odśwież ListBox
                            }
                            else
                            {
                                MessageBox.Show("Nie udało się usunąć seansu.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas usuwania seansu: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Wybierz seans do usunięcia.");
            }
        }


        private void BTpowrot(object sender, RoutedEventArgs e)
        {
            var glownaStronaPracownika = new GlownaStronaPracownika();
            glownaStronaPracownika.Show();
            this.Close();
        }
        private void ZaladujSeanse()
        {
            LBSeans.Items.Clear();
            try
            {
                using (var polaczenie = new SqliteConnection(SciezkaDoBazy))
                {
                    polaczenie.Open();

                    string zapytanie = "SELECT MovieID, Date, StartTime, Format, Price FROM Seanse";
                    using (var komenda = new SqliteCommand(zapytanie, polaczenie))
                    using (var reader = komenda.ExecuteReader())
                    {
                        //int licznik = 0;

                        while (reader.Read())
                        {
                            // Wyświetl odczytywane dane dla debugowania
                            //MessageBox.Show($"Odczytano: {reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}, {reader.GetString(3)}, {reader.GetDouble(4)}");

                            var seans = new Seans
                            {
                                MovieID = reader.GetInt32(0),
                                Date = reader.GetString(1),
                                StartTime = reader.GetString(2),
                                Format = reader.GetString(3),
                                Price = reader.GetDouble(4)
                            };

                            LBSeans.Items.Add(seans);
                            //licznik++;
                        }

                        //if (licznik == 0)
                        //{
                        //    MessageBox.Show("Nie znaleziono żadnych seansów.");
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}{ex.StackTrace}");
            }
        }



        private void SeansListBox_wybor(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            usunSeans.IsEnabled = LBSeans.SelectedItem != null;
        }
    }
}
