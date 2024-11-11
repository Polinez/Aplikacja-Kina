using System;
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
    /// Logika interakcji dla klasy SalaKinowa.xaml
    /// </summary>
    public partial class SalaKinowa : Window
    {
        public SalaKinowa()
        {
            InitializeComponent();
        }

        private void Rezerwuj_click(object sender, RoutedEventArgs e)
        {
            RezerwacjaSeansu rezerwacjaSeansu = new RezerwacjaSeansu();
            rezerwacjaSeansu.Show();
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
            
        }

        private void StronaGlowna_click(object sender, RoutedEventArgs e)
        {
            Strona_glowna strona_Glowna =new Strona_glowna();
            strona_Glowna.Show();
            this.Close();
        }
    }
}
