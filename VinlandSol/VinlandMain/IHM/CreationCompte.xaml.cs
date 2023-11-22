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
            string? roleSelectionne = ((ComboBoxItem)ChoixRole.SelectedItem)?.Content.ToString();
            string? messageCheckFail = null;
            int comboCheckFail = 0;

            #region Checks

            if (fakeDAO.UsernameTaken(username) == false) { messageCheckFail = "Nom d'utilisateur déjà pris"; comboCheckFail++;}
            if (string.IsNullOrWhiteSpace(username)) { messageCheckFail = "Veuillez remplir le champ Nom d'Utilisateur"; comboCheckFail++; }
            if (string.IsNullOrWhiteSpace(password)) { messageCheckFail = "Veuillez remplir le champ Mot de Passe"; comboCheckFail++; }
            if (roleSelectionne == null) { messageCheckFail = "Veuillez sélectionner un rôle pour votre compte"; comboCheckFail++; }

            if(comboCheckFail >= 2) messageCheckFail = "Plusieurs des champs nécéssaires ne sont pas renseignés";
            if (comboCheckFail >= 1) 
            {
                CustomMessageBox messagebox = new CustomMessageBox(messageCheckFail);
                messagebox.ShowDialog();
            } 

            #endregion

            if(comboCheckFail == 0) // Si tout va bien
            {
                if (roleSelectionne == "Maitre du jeu") { fakeDAO.CreateMJ(username, password); }
                else if (roleSelectionne == "Joueur") { fakeDAO.CreateJoueur(username, password); }
                MainWindow pagecreation = new MainWindow();
                pagecreation.Left = this.Left;
                pagecreation.Top = this.Top;
                pagecreation.Show();
                Creation_Compte.Close();
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
