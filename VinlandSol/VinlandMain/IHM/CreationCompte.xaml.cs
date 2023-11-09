using System.Windows;
using VinlandSol.Métier;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour CreationCompte.xaml
    /// </summary>
    public partial class CreationCompte : Window
    {
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
            string password = TBMdp.Password;

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                UserAccountManager accountManager = new UserAccountManager();
                accountManager.CreateUserAccount(username, password);
                MainWindow pagecreation = new MainWindow();
                pagecreation.Left = this.Left;
                pagecreation.Top = this.Top;
                pagecreation.Show();
                Creation_Compte.Close();
            }
        }
    }
}
