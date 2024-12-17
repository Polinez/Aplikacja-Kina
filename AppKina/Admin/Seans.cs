namespace WpfApp
{
    public class Seans
    {
        public int MovieID { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string Format { get; set; }
        public double Price { get; set; }


        public override string ToString()
        {
            return $"ID filmu: {MovieID}, Data: {Date}, Godzina rozpoczęcia: {StartTime}, Format: {Format}, Cena: {Price}";
        }
    }

}
