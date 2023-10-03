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
    /// Logique d'interaction pour Personnages.xaml
    /// </summary>
    public partial class Personnages : Window
    {
        private AjouterPersonnage pagecreationperso;

        public Personnages()
        {
            InitializeComponent();
            pagecreationperso = new AjouterPersonnage();
        }

        private void OuvrirCampagnes_Click(object sender, RoutedEventArgs e)
        {
            Campagnes pagecreation = new Campagnes();
            pagecreation.Show();
            PersonnagesWindow.Close();
        }

        private void OuvrirCartes_Click(object sender, RoutedEventArgs e)
        {
            Cartes pagecreation = new Cartes();
            pagecreation.Show();
            PersonnagesWindow.Close();
        }

        private void OuvrirAjouterPersonnage_Click(object sender, RoutedEventArgs e)
        {
            if (pagecreationperso.IsVisible == false)
            {
                pagecreationperso = new AjouterPersonnage();
                pagecreationperso.Show();
            }
        }

        private void EditionNom(object sender, RoutedEventArgs e)
        {
            
        }

        private void ValiderNom(object sender, RoutedEventArgs e)
        {

        }
    }
}
