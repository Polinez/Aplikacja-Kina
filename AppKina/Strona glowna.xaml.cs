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
    /// Logika interakcji dla klasy Strona_glowna.xaml
    /// </summary>
    public partial class Strona_glowna : Window
    {
        public Strona_glowna()
        {
            InitializeComponent();
        }

        private void WiecejBttn1(object sender, RoutedEventArgs e)
        {
            SzczegolyFilmu szczegolyFil = new SzczegolyFilmu();
            szczegolyFil.Show();

        }
        private void WiecejBttn2(object sender, RoutedEventArgs e)
        {
            SzczegolyFilmu szczegolyFil = new SzczegolyFilmu();
            szczegolyFil.Show();
        }
    }
}
