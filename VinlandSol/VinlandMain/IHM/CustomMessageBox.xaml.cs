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

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string message)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; // La fenêtre de départ est ouverte au centre de l'écran
            MessageAffiche.Content = message;
        }


        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
