﻿using AppKina.MainPage;
using Microsoft.Data.Sqlite;
using System.Windows;

namespace AppKina
{
    public partial class Zarezerwuj : Window
    {
        private const string databasePath = @"Data Source=KinoDB.db";
        public Zarezerwuj()
        {
            InitializeComponent();
            ShowMovies();
        }

        private void button_stronaGlowna_Click(object sender, RoutedEventArgs e)
        {
            Strona_glowna stronaGlowna = new Strona_glowna();
            stronaGlowna.Show();
            this.Close();
        }

        private void button_zarezerwuj_Click(object sender, RoutedEventArgs e)
        {

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
            if (listBox_MovieTitles.SelectedItem == null)
            {
                MessageBox.Show("Wybierz film aby przejść dalej.");
                return;
            }
            RezerwacjaSeansu rezerwacjaSeansu = new RezerwacjaSeansu();
            rezerwacjaSeansu.Show();
            this.Close();
        }

        private void ShowMovies()
        {
            listBox_MovieTitles.Items.Clear();
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string zapytanie = "SELECT Title FROM Movies";
                    using (var komenda = new SqliteCommand(zapytanie, connection))
                    using (var reader = komenda.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nazwaFilmu = reader.GetString(0);
                            listBox_MovieTitles.Items.Add(nazwaFilmu);
                        }
                        if (listBox_MovieTitles.Items.Count == 0)
                        {
                            MessageBox.Show("Nie ma żadnych dostępnych filmów");
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
