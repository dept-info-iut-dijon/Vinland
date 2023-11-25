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
    /// <author>Aaron</author>
    public partial class MainWindow : Window
    {
        #region Attributs

        private FakeDAO fakeDAO = FakeDAO.Instance;
        private bool isPasswordVisible = false;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise la fenêtre
        /// </summary>
        /// <author>Aaron</author>
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; // La fenêtre de départ est ouverte au centre de l'écran
            UpdatePasswordVisibility();
        }

        #endregion

        #region Visibilité mot de passe

        /// <summary>
        /// Cache ou Révèle le mot de passe en fonction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Aaron</author>
        private void MdpVisibilityChanged(object sender, RoutedEventArgs e)
        {
            isPasswordVisible = !isPasswordVisible; // On inverse
            UpdatePasswordVisibility();
        }

        /// <summary>
        /// Met à jour la visibilité du mot de passe en fonction de isPasswordVisible
        /// </summary>
        /// <author>Alexis + Aaron</author>
        private void UpdatePasswordVisibility()
        {
            if (isPasswordVisible) // Si le mot de passe est visible
            {
                TBVisibleMdp.Text = TBMdp.Password; // La textBox avec le texte visible prends la valeur de celle avec le texte caché
                // On inverse la visibilité des textBox
                TBMdp.Visibility = Visibility.Collapsed;
                TBVisibleMdp.Visibility = Visibility.Visible;

                Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeilbarre.png", UriKind.RelativeOrAbsolute)); // Le bouton change d'image
            }
            else
            {
                TBMdp.Password = TBVisibleMdp.Text; // La textBox avec le texte caché prends la valeur de celle avec le texte visible 
                // On inverse la visibilité des textBox
                TBVisibleMdp.Visibility = Visibility.Collapsed;
                TBMdp.Visibility = Visibility.Visible;

                Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeil.png", UriKind.RelativeOrAbsolute)); // Le bouton change d'image
            }
        }

        #endregion

        #region Transitions de page

        /// <summary>
        /// Ouvre la fenêtre CreationCompte et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Aaron</Author>
        private void OuvrirCreationCompte_Click(object sender, RoutedEventArgs e)
        {
            CreationCompte pagecreation = new CreationCompte(); // On créé la nouvelle page
            // La page s'ouvrira à la même position que la fenêtre actuelle
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;

            pagecreation.Show(); // On ouvre la nouvelle page
            Vinland.Close(); // La fenêtre actuelle est fermée
        }

        /// <summary>
        /// Ouvre la fenêtre Campagnes et ferme la fenêtre actuelle si le compte est présent dans la liste des comptes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Aaron</Author>
        private void OuvrirCampagnes_Click(object sender, RoutedEventArgs e)
        {
            string username = TBNomUtilisateur.Text; // On récupère le texte que l'utilisateur a rentré dans le champ Nom d'utilisateur
            string password;
            if (!isPasswordVisible) { password = TBMdp.Password; } else { password = TBVisibleMdp.Text; } // On récupère le texte que l'utilisateur a rentré dans le champ Mot de passe 

            (var userId, var userRole) = fakeDAO.VerifyUserAccount(username, password); // On vérifie les informations

            if (userRole == "Joueur" || userRole == "MJ") // Si les informations sont correctes
            {
                Campagnes pagecreation = new Campagnes(userId, userRole);
                // La page s'ouvrira à la même position que la fenêtre actuelle
                pagecreation.Left = this.Left;
                pagecreation.Top = this.Top;

                pagecreation.Show();
                Vinland.Close();
            }
            if (userRole == "Non trouvé") // Les informations ne sont pas correctes
            {
                CustomMessageBox messagebox = new CustomMessageBox("Utilisateur non trouvé ou un des champs est incorrect"); // On en informe l'utilisateur
                messagebox.ShowDialog();
            }
        }
        #endregion
    }
}
