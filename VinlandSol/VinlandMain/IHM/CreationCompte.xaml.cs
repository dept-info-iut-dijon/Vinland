using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VinlandMain.IHM;
using VinlandSol.BDD;
using VinlandSol.IHM;
using VinlandSol.Métier;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour CreationCompte.xaml
    /// </summary>
    public partial class CreationCompte : Window
    {
        private FakeDAO fakeDAO = FakeDAO.Instance;
        private bool isPasswordVisible = false;

        public CreationCompte()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ouvre la fenêtre CreationCompte et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnuleCreaCompte_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pagecreation = new MainWindow();
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            pagecreation.Show();
            Creation_Compte.Close();
        }

        /// <summary>
        /// Ajoute le nouveau compte à la liste des comptes et ouvre la fenêtre CreationCompte et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Creer_Click(object sender, RoutedEventArgs e)
        {
            string username = TBNomUtilisateur.Text;
            string password = TBVisibleMdp.Text;

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                fakeDAO.CreateMJ(username, password);
                MainWindow pagecreation = new MainWindow();
                pagecreation.Left = this.Left;
                pagecreation.Top = this.Top;
                pagecreation.Show();
                Creation_Compte.Close();
            }
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
