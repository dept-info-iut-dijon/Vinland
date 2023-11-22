using System;
using System.Windows;
using System.Windows.Media.Imaging;
using VinlandMain.IHM;
using VinlandSol.BDD;
using VinlandSol.IHM;
using VinlandSol.Métier;

namespace VinlandSol
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FakeDAO fakeDAO = FakeDAO.Instance;
        private bool isPasswordVisible = false;
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

            (var userId, var userRole) = fakeDAO.VerifyUserAccount(username, password);

            if (userRole == "Joueur")
            {
                Campagnes pagecreation = new Campagnes(fakeDAO.GetJoueur(userId));
                pagecreation.Left = this.Left;
                pagecreation.Top = this.Top;
                pagecreation.Show();
                Vinland.Close();
            }
            if (userRole == "MJ")
            {
                Campagnes pagecreation = new Campagnes(fakeDAO.GetMJ(userId));
                pagecreation.Left = this.Left;
                pagecreation.Top = this.Top;
                pagecreation.Show();
                Vinland.Close();
            }
            if (userRole == "Non trouvé")
            {
                CustomMessageBox messagebox = new CustomMessageBox("Utilisateur non trouvé ou un des champs est incorrect");
                messagebox.ShowDialog();
            }
        }

        /// <summary>
        /// Cache ou Révèle le mot de passe en fonction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MdpVisibilityChanged(object sender, RoutedEventArgs e)
        {
            if (isPasswordVisible) // Si le mot de passe est visible - le rendre invisible
            {
                TBVisibleMdp.Visibility = Visibility.Collapsed;
                TBMdp.Visibility = Visibility.Visible;
                Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeil.png", UriKind.RelativeOrAbsolute));
            }
            else // Si le mot de passe est invisible - le rendre visible
            {
                TBMdp.Visibility = Visibility.Collapsed;
                TBVisibleMdp.Visibility = Visibility.Visible;
                Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeilbarre.png", UriKind.RelativeOrAbsolute));
            }
            isPasswordVisible = !isPasswordVisible;
        }

        /// <summary>
        /// Synchronise le texte visible par rapport à celui invisible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TBMdp_Synchronisation(object sender, RoutedEventArgs e)
        {
            TBVisibleMdp.Text = TBMdp.Password;
        }

        /// <summary>
        /// Synchronise le texte invisible par rapport à celui visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TBVisibleMdp_Synchronisation(object sender, RoutedEventArgs e)
        {
            TBMdp.Password = TBVisibleMdp.Text;
        }
    }
}
