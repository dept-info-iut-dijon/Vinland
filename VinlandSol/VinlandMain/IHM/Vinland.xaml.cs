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
using VinlandSol.Métier;

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
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; // La fenêtre de départ est ouverte au centre de l'écran
        }

        /// <summary>
        /// Ouvre la fenêtre CreationCompte et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCreationCompte_Click(object sender, RoutedEventArgs e)
        {
            CreationCompte pagecreation = new CreationCompte();
            pagecreation.Left = this.Left; 
            pagecreation.Top = this.Top; 
            pagecreation.Show();
            Vinland.Close();
        }

        /// <summary>
        /// Ouvre la fenêtre Campagnes et ferme la fenêtre actuelle si le compte est présent dans la liste des comptes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCampagnes_Click(object sender, RoutedEventArgs e)
        {
            string username = TBNomUtilisateur.Text;
            string password = TBMdp.Password;

            UserAccountManager accountManager = new UserAccountManager();
            if (accountManager.VerifyUserAccount(username, password))
            {
                Campagnes pagecreation = new Campagnes();
                pagecreation.Left = this.Left;
                pagecreation.Top = this.Top;
                pagecreation.Show();
                Vinland.Close();
            }
        }

        /// <summary>
        /// Ouvre la fenêtre Campagnes et ferme la fenêtre actuelle en passant l'authentification comme un véritable hacker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Skip_Click(object sender, RoutedEventArgs e)
        {
            Campagnes pagecreation = new Campagnes();
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            pagecreation.Show();
            Vinland.Close();
        }
    }
}
