namespace AppKina.Admin
{
    public class Film
    {
        public int ID { get; set; }
        // Tytuł filmu
        public string Tytul { get; set; }

        // Gatunek filmu
        public string Gatunek { get; set; }

        // Reżyser filmu
        public string Rezyser { get; set; }

        // Obsada filmu
        public string Obsada { get; set; }

        // Czas trwania filmu (w minutach)
        public int CzasTrwania { get; set; }

        // Opis filmu
        public string Opis { get; set; }

        // Ścieżka do plakatu filmu
        public string SciezkaPlakatu { get; set; }



        // Konstruktor parametrowy
        public Film(string tytul, string gatunek, string rezyser, string obsada, int czasTrwania, string opis, string sciezkaPlakatu)
        {
            Tytul = tytul;
            Gatunek = gatunek;
            Rezyser = rezyser;
            Obsada = obsada;
            CzasTrwania = czasTrwania;
            Opis = opis;
            SciezkaPlakatu = sciezkaPlakatu;
        }

        public override string ToString()
        {
            return $"Tytuł: {Tytul}, Gatunek: {Gatunek}, Reżyser: {Rezyser}, Obsada: {Obsada}, " +
                   $"Czas Trwania: {CzasTrwania} min, Opis: {Opis}";
        }
    }
}
