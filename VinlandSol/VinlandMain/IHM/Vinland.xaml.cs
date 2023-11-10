using System;
using System.Windows;
using System.Windows.Media.Imaging;
using VinlandMain.IHM;
using VinlandSol.BDD;
using VinlandSol.IHM;

namespace VinlandSol
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            FakeDAO accountManager = new FakeDAO();
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

        private void MdpVisibilityChanged(object sender, RoutedEventArgs e)
        {
            if (isPasswordVisible)
            {
                TBVisibleMdp.Visibility = Visibility.Collapsed;
                TBMdp.Visibility = Visibility.Visible;
                Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeil.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                TBMdp.Visibility = Visibility.Collapsed;
                TBVisibleMdp.Visibility = Visibility.Visible;
                Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeilbarre.png", UriKind.RelativeOrAbsolute));
            }

            // Inversez l'état
            isPasswordVisible = !isPasswordVisible;
        }

        private void TBMdp_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TBVisibleMdp.Text = TBMdp.Password;
        }
    }
}
