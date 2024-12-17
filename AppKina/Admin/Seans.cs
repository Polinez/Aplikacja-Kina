namespace WpfApp
{
    public class Seans
    {
        public int MovieID { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string Format { get; set; }
        public double Price { get; set; }


        public Seans(int movieID, string date, string startTime, string format, double price)
        {
            MovieID = movieID;
            Date = date;
            StartTime = startTime;
            Format = format;
            Price = price;
        }
    }
}
