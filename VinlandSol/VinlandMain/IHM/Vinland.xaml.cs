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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VinlandMain.IHM;
using VinlandSol.IHM;

namespace VinlandSol
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OuvrirCreationCompte_Click(object sender, RoutedEventArgs e)
        {
            CreationCompte pagecreation = new CreationCompte();
            pagecreation.Show();
            Vinland.Close();
        }

        private void OuvrirVisionCampagne_Click(object sender, RoutedEventArgs e)
        {
            VisionCampagne pagecreation = new VisionCampagne();
            pagecreation.Show();
            Vinland.Close();
        }
    }
}
