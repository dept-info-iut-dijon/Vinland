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
using VinlandMain.IHM;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour Cartes.xaml
    /// </summary>
    public partial class Cartes : Window
    {
        public Cartes()
        {
            InitializeComponent();
        }
        private void OuvrirPersonnages_Click(object sender, RoutedEventArgs e)
        {
            Personnages pagecreation = new Personnages();
            pagecreation.Show();
            CartesWindow.Close();
        }

        private void OeilChange(object sender, RoutedEventArgs e)
        {
           Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeilbarre.png", UriKind.RelativeOrAbsolute));
        }
    }
}
