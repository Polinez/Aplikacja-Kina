using Microsoft.Data.Sqlite;
using System.Windows;

namespace WpfApp
{
    public class Seans
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string Format { get; set; }
        public double Price { get; set; }

        private string GetMovieTitle()
        {
            string databasePath = @"Data Source=KinoDB.db";
            try
            {

                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();

                    string query = $"SELECT Movie.Title FROM Movies WHERE Movie.ID={MovieID}";
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Title = reader.GetString(0);
                        }
                    }
                    connection.Close();

                    return Title;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //public override string ToString()
        //{
        //    return $"ID seansu: {ID}, ID filmu: {MovieID}, Data: {Date}, Godzina rozpoczęcia: {StartTime}, Format: {Format}, Cena: {Price}";
        //}
        //public Seans(int movieID, string date, string startTime, string format, double price)
        //{
        //    MovieID = movieID;
        //    Date = date;
        //    StartTime = startTime;
        //    Format = format;
        //    Price = price;

        //}
    }

}
