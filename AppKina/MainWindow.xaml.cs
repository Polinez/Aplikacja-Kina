<<<<<<< HEAD
﻿using System.Windows;
=======
﻿using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Path = System.IO.Path;
>>>>>>> 4a326a81a1f80181e22d676a0b8019f2550a52c0

namespace AppKina
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
            string imagePath = Path.Combine(projectDirectory, "elementyGraficzne", "KinoLogoKolo.png");

            if (File.Exists(imagePath))
            {
                LogoWKole.Source = new BitmapImage(new Uri(imagePath));
            }
            else
            {
                MessageBox.Show($"Obraz nie został znaleziony. ");
            }
        }

        private void Rozpocznij(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Rozpocznij");
        }


        private void Logowanie_Click(object sender, RoutedEventArgs e)
        {
            Logowanie logowanie = new Logowanie();
            logowanie.Show();

        }

        private void Rejestracja_Click(object sender, RoutedEventArgs e)
        {
            Rejestracja rejestracja = new Rejestracja();
            rejestracja.Show();
        }

        private void Glowna_Click(object sender, RoutedEventArgs e)
        {
            Strona_glowna strona_Glowna = new Strona_glowna();

            strona_Glowna.Show();
        }
    }
}