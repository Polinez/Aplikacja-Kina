using System.Windows;
﻿using AppKina.Admin;
using AppKina.MainPage;
using System.Windows;
using System.Windows.Media.Imaging;


namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy SzczegolyFilmu.xaml
    /// </summary>
    public partial class SzczegolyFilmu : Window
    {
        public SzczegolyFilmu()
        {
            InitializeComponent();
        }

        public SzczegolyFilmu(Film szczegolyFilmu) : this()
        {
            InitializeComponent();
            // Ustaw dane filmu w interfejsie użytkownika
            UstawSzczegolyFilmu(szczegolyFilmu);
        }
        private void UstawSzczegolyFilmu(Film film)
        {
            // Zakładam, że w interfejsie masz kontrolki jak np. TextBlock do wyświetlania danych
            TytulTB.Text = film.Tytul;
            GatunekTB.Text = film.Gatunek;
            OpisTB.Text = "Opis: " + film.Opis;
            zdjeieIM.Source = new BitmapImage(new Uri(film.SciezkaPlakatu, UriKind.RelativeOrAbsolute));
            RezyserTB.Text = "Reżyser: " + film.Rezyser;
            ObsadaTB.Text = "Obsada: " + film.Obsada;
            czasTrwaniaTB.Text = "Czas trwania: " + film.CzasTrwania + " min";
        }


        private void StronaGlowna_Click(object sender, RoutedEventArgs e)
        {
            Strona_glowna strona_Glowna = new Strona_glowna();
            strona_Glowna.Show();
            this.Close();
        }

        private void Rezerwuj_click(object sender, RoutedEventArgs e)
        {
            Zarezerwuj zarezerwuj = new Zarezerwuj();
            zarezerwuj.Show();
            this.Close();
        }

        private void MojeRezerwacje_click(object sender, RoutedEventArgs e)
        {
            MojeRezerwacje mojeRezerwacje = new MojeRezerwacje();
            mojeRezerwacje.Show();
            this.Close();
        }

        private void MojeKonto_click(object sender, RoutedEventArgs e)
        {
            Account account = new Account();
            account.Show();
            this.Close();
        }

        private void click_zobacz_seanse(object sender, RoutedEventArgs e)
        {
            RezerwacjaSeansu rezerwacjaSeansu = new RezerwacjaSeansu("tytuł"); //dodać przekazywanie tytułu danego filmu
            rezerwacjaSeansu.Show();
            this.Close();
        }

    }
}
