namespace WpfApp
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public int Duration { get; set; } // czas trwania w minutach
        public string Description { get; set; }
        public string PosterPath { get; set; }
    }
}
