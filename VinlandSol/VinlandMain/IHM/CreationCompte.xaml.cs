using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using VinlandSol.BDD;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour CreationCompte.xaml
    /// </summary>
    /// <Author>Alexis</Author>
    public partial class CreationCompte : Window
    {
        #region Attributs

        private FakeDAO fakeDAO = FakeDAO.Instance;
        private bool isPasswordVisible = false;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise la fenêtre 
        /// </summary>
        /// <Author>Alexis</Author>
        public CreationCompte()
        {
            InitializeComponent();
            UpdatePasswordVisibility();
        }

        #endregion

        #region Creation Compte

        /// <summary>
        /// Ajoute le nouveau compte à la liste des comptes et ouvre la fenêtre Vinland et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Alexis</Author>
        private void Creer_Click(object sender, RoutedEventArgs e)
        {
            string username = TBNomUtilisateur.Text; // On récupère le nom du compte
            string password;
            if (!isPasswordVisible) { password = TBMdp.Password; } else { password = TBVisibleMdp.Text; } // On récupère le texte que l'utilisateur a rentré dans le champ Mot de passe 
            string? roleSelectionne = ((ComboBoxItem)ChoixRole.SelectedItem)?.Content.ToString(); // On récupère le rôle selectionné
            bool okCheck = true;

            #region Checks

            string? messageCheckFail = null;
            // Un seul check à la fois, on ne veut pas aggresser l'utilisateur avec des popups en chaine
            if (fakeDAO.UsernameTaken(username) == false) { messageCheckFail = "Nom d'utilisateur déjà pris"; okCheck = false; }
            else if (string.IsNullOrWhiteSpace(username)) { messageCheckFail = "Veuillez remplir le champ Nom d'Utilisateur"; okCheck = false; }
            else if (string.IsNullOrWhiteSpace(password)) { messageCheckFail = "Veuillez remplir le champ Mot de Passe"; okCheck = false; }
            else if (roleSelectionne == null) { messageCheckFail = "Veuillez sélectionner un rôle pour votre compte"; okCheck = false; }

            if (!okCheck) // Si un problème est rencontré, on en informe l'utilisateur
            {
                CustomMessageBox messagebox = new CustomMessageBox(messageCheckFail);
                messagebox.ShowDialog();
            }

            #endregion

            if (okCheck) // Si tout va bien
            {
                // On créé un IUser selon le rôle selectionné
                if (roleSelectionne == "Maitre du jeu") { fakeDAO.CreateMJ(username, password); }
                else if (roleSelectionne == "Joueur") { fakeDAO.CreateJoueur(username, password); }

                // La page Vinland est créée
                MainWindow pagecreation = new MainWindow();
                pagecreation.Left = this.Left;
                pagecreation.Top = this.Top;
                pagecreation.Show();

                Creation_Compte.Close(); // On ferme la fenêtre actuelle
            }
        }

        #endregion

        #region Visibilité mot de passe

        /// <summary>
        /// Cache ou Révèle le mot de passe en fonction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Alexis</Author>
        private void MdpVisibilityChanged(object sender, RoutedEventArgs e)
        {
            isPasswordVisible = !isPasswordVisible; // On inverse
            UpdatePasswordVisibility();
        }

        /// <summary>
        /// Met à jour la visibilité du mot de passe en fonction de isPasswordVisible
        /// </summary>
        /// <Author>Alexis + Aaron</Author>
        private void UpdatePasswordVisibility()
        {
            if (isPasswordVisible) // Si le mot de passe est visible
            {
                TBVisibleMdp.Text = TBMdp.Password; // La textBox avec le texte visible prends la valeur de celle avec le texte caché
                // On inverse la visibilité des textBox
                TBMdp.Visibility = Visibility.Collapsed;
                TBVisibleMdp.Visibility = Visibility.Visible;

                Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeilbarre.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                TBMdp.Password = TBVisibleMdp.Text; // La textBox avec le texte caché prends la valeur de celle avec le texte visible 
                // On inverse la visibilité des textBox
                TBVisibleMdp.Visibility = Visibility.Collapsed;
                TBMdp.Visibility = Visibility.Visible;

                Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeil.png", UriKind.RelativeOrAbsolute));
            }
        }

        #endregion

        #region Transitions de page

        /// <summary>
        /// Ouvre la fenêtre Vinland et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Alexis</Author>
        private void AnnuleCreaCompte_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pagecreation = new MainWindow();
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            pagecreation.Show();
            Creation_Compte.Close();
        }

        #endregion
    }
}
