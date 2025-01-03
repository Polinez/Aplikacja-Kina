﻿using Microsoft.Data.Sqlite;
using System.Windows;

namespace AppKina.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy UsunFilm.xaml
    /// </summary>
    public partial class UsunFilm : Window
    {
        private const string SciezkaDoBazy = @"Data Source=KinoDB.db";
        public UsunFilm()
        {
            InitializeComponent();
            ZaladujFilmy();
        }

        private void powrot_click(object sender, RoutedEventArgs e)
        {
            GlownaStronaPracownika glownaStronaPracownika = new GlownaStronaPracownika();
            glownaStronaPracownika.Show();

            this.Close();
        }

        private void click_usun_film(object sender, RoutedEventArgs e)
        {
            if (FilmListBox.SelectedItem == null)
            {
                MessageBox.Show("Wybierz film do usunięcia.");
                return;
            }
            
            string wybranyFilm = FilmListBox.SelectedItem.ToString();
            bool seanse = CzySaSeanse(wybranyFilm);
            
            try
            {
                if (seanse)
                {
                    using (var polaczenie = new SqliteConnection(SciezkaDoBazy))
                    {
                        polaczenie.Open();
                        string zapytanie = "DELETE FROM Movies WHERE Title = @Title";
                        using (var komenda = new SqliteCommand(zapytanie, polaczenie))
                        {
                            komenda.Parameters.AddWithValue("@Title", wybranyFilm);
                            komenda.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show($"Film {wybranyFilm} zostal usunięty.");
                    ZaladujFilmy();
                }
                else
                {
                    MessageBox.Show($"Nie można usunąć {wybranyFilm} ponieważ są na niego seanse.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ZaladujFilmy()
        {
            FilmListBox.Items.Clear();
            try
            {
                using (var polaczenie = new SqliteConnection(SciezkaDoBazy))
                {
                    polaczenie.Open();
                    string zapytanie = "SELECT Title FROM Movies";
                    using (var komenda = new SqliteCommand(zapytanie, polaczenie))
                    using (var reader = komenda.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nazwaFilmu = reader.GetString(0);
                            FilmListBox.Items.Add(nazwaFilmu);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FilmListBox_wybor(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            usunFilm.IsEnabled = FilmListBox.SelectedItem != null;
        }

        private bool CzySaSeanse(string wybranyFilm)
        {
            List<int> seanse = new List<int>();
            bool result = false;
            try
            {
                using (var connection = new SqliteConnection(SciezkaDoBazy))
                {
                    connection.Open();
                    string query = $"Select Seanse.ID FROM Seanse JOIN Movies ON Movies.ID=Seanse.MovieID WHERE Movies.Title='{wybranyFilm}'";
                    using (var komenda = new SqliteCommand(query, connection))
                    using (var reader = komenda.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idSeansu = reader.GetInt32(0);
                            seanse.Add(idSeansu);
                        }
                    }
                    connection.Close();
                }
                if (seanse.Count > 0)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }

            return result;
        }
    }
}
