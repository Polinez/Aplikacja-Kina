using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;

namespace AppKina
{
    /// <summary>
    /// Logika interakcji dla klasy Logowanie.xaml
    /// </summary>
    public partial class Logowanie : Window
    {
        public Logowanie()
        {
            InitializeComponent();
            SetImageSource();
        }


        private void SetImageSource()
        {
            // Ścieżka do folderu projektu (Repozytorium/AppKina)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\.."));

            // Połączenie ze ścieżką do folderu "elementy graficzne"
            string imagePath = Path.Combine(projectDirectory, "elementyGraficzne", "KinoLogo.png");

            if (File.Exists(imagePath))
            {
                logo.Source = new BitmapImage(new Uri(imagePath));
            }
            else
            {
                MessageBox.Show($"Obraz nie został znaleziony. ");
            }
        }
    }


}
