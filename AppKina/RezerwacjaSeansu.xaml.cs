﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy RezerwacjaSeansu.xaml
    /// </summary>
    public partial class RezerwacjaSeansu : Window
    {
        public RezerwacjaSeansu()
        {
            InitializeComponent();
        }

        private void button_stronaGlowna_Click(object sender, RoutedEventArgs e)
        {
            Strona_glowna stronaGlowna = new Strona_glowna();
            stronaGlowna.Show();
            this.Close();
        }

        private void button_zarezerwuj_Click(object sender, RoutedEventArgs e)
        {
            Zarezerwuj zarezerwuj = new Zarezerwuj();
            zarezerwuj.Show();
            this.Close();
        }
        private void button_mojeRezerwacje_Click(object sender, RoutedEventArgs e)
        {
            //Strona_glowna stronaGlowna = new Strona_glowna();
            //stronaGlowna.Show();
            //this.Close();
        }

        private void button_mojeKonto_Click(object sender, RoutedEventArgs e)
        {
            //Strona_glowna stronaGlowna = new Strona_glowna();
            //stronaGlowna.Show();
            //this.Close();
        }

        private void button_dalej_Click(object sender, RoutedEventArgs e)
        {
            //Strona_glowna stronaGlowna = new Strona_glowna();
            //stronaGlowna.Show();
            //this.Close();
        }
    }
}
