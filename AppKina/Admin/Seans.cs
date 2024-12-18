namespace WpfApp
{
    public class Seans
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string Format { get; set; }
        public double Price { get; set; }



        public override string ToString()
        {
            return $"ID seansu: {ID}, ID filmu: {MovieID}, Data: {Date}, Godzina rozpoczęcia: {StartTime}, Format: {Format}, Cena: {Price}";
        }
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
