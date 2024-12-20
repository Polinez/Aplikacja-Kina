﻿using AppKina.MainPage;
using AppKina.Reservation;
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
            else
            {
                string title = listBox_MovieTitles.SelectedItem.ToString();
                if (AnyProjection(title) > 0)
                {
                    RezerwacjaSeansu rezerwacjaSeansu = new RezerwacjaSeansu(title);
                    rezerwacjaSeansu.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nie ma seansów");
                }
            }
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

        private int AnyProjection(string title)
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();

                    List<string> projections = new List<string>();

                    string query = $"SELECT Seanse.ID FROM Seanse JOIN Movies ON Movies.ID=Seanse.MovieID WHERE Movies.Title='{title}'";
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string projection = reader.GetString(0);
                            projections.Add(projection);
                        }

                    }
                    connection.Close();
                    return projections.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
    }
}
