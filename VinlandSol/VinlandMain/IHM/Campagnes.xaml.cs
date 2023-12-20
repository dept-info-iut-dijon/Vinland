using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VinlandSol.BDD;
using VinlandSol.IHM;
using VinlandSol.Métier;

namespace VinlandMain.IHM
{
    /// <summary>
    /// Logique d'interaction pour VisionCampagne.xaml
    /// </summary>
    /// <Author>Baptiste</Author>
    public partial class Campagnes : Window
    {
        #region Attributs

        private FakeDAO fakeDAO = FakeDAO.Instance;
        private int idUser; // L'id de l'utilisateur identifié
        private string roleUser; // Le role de l'utilisateur identifié

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la fenêtre
        /// </summary>
        /// <Author>Baptiste</Author>
        public Campagnes(int idUser, string roleUser)
        {
            InitializeComponent();
            this.idUser = idUser;
            this.roleUser = roleUser;
            MettreAJourListBox(); // La listBox des campagnes est remplie par les campagnes de l'utilisateur
        }

        #endregion

        #region Liste Campagnes

        /// <summary>
        /// Affiche les informations sur la droite de l'écran par rapport à la campagne sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Baptiste</Author>
        private void CampagnesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CampagnesListe.SelectedItem != null)
            {
                Edit.Visibility = Visibility.Visible; // On peut éditer la campagne selectionnée
                RejoidComp.IsEnabled = true; // On peut rejoindre la campagne selectionnée
            }
            AfficherInfos(CampagnesListe.SelectedIndex); // On affiche sur la partie droite de l'écran les informations de la campagne selectionnée
        }

        /// <summary>
        /// Mets à jour CampagnesListe selon les données du DAO 
        /// </summary>
        /// <Author>Baptiste</Author>
        private void MettreAJourListBox()
        {
            List<Campagne> campagnes = fakeDAO.GetCurrentCampagnes(roleUser, idUser); // On récupère les campagnes depuis le fakeDAO

            CampagnesListe.Items.Clear(); // On efface les éléments existants dans la ListBox

            foreach (Campagne campagne in campagnes) // On ajoute chaque campagne de la liste
            {
                CampagnesListe.Items.Add(campagne);
            }
            CampagnesListe.DisplayMemberPath = "Nom"; // On affiche le contenu de la propriété 'Nom' des campagnes
        }

        #endregion

        #region Informations Campagne

        /// <summary>
        /// Permet d'actualiser les informations de la campagne selectionnée/modifiée
        /// </summary>
        /// <param name="index">index de la Campagne sélectionnée</param>
        /// <Author>Baptiste</Author>
        private void AfficherInfos(int index)
        {
            if (index >= 0 && index < fakeDAO.GetCurrentCampagnes(roleUser, idUser).Count)
            {
                Campagne selectedCampagne = fakeDAO.GetCurrentCampagnes(roleUser, idUser)[index]; // On récupère la campagne
                NomCampTextBlock.Text = selectedCampagne.Nom; // On affiche le nom
                DateCreationTextBlock.Text = selectedCampagne.DateCreation.ToString("dd/MM/yyyy HH:mm:ss"); // On affiche la date de création
                DateModificationTextBlock.Text = selectedCampagne.DateModification.ToString("dd/MM/yyyy HH:mm:ss"); // On affiche la date de modification
                NombreCartesTextBlock.Text = selectedCampagne.IDCartes.Count().ToString(); // On affiche le nombre de cartes
                NombrePersonnagesTextBlock.Text = selectedCampagne.IDPersonnages.Count().ToString(); // On affiche le nombre de personnages
            }
        }

        #endregion

        #region Creation Campagne

        /// <summary>
        /// Affiche les éléments permettant de créer une campagne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Baptiste</Author>
        private void NouvelleCampagne_Click(object sender, RoutedEventArgs e)
        {
            NomNouvCamp.Visibility = Visibility.Visible;
            Valider.Visibility = Visibility.Visible;

            // Inversion de l'affichage des boutons - On peut annuler
            AnnulerNouvCamp.Visibility = Visibility.Visible;
            NouvCamp.Visibility = Visibility.Collapsed;

            // On ne peut pas rejoindre une campagne ni en éditer.
            Edit.Visibility = Visibility.Collapsed;
            RejoidComp.IsEnabled = false;

            // Mise en place du Label empêchant les interactions avec la liste des Campagnes
            CacheCampagnesListe.Visibility = Visibility.Visible;
            CacheCampagnesListe.Background = Brushes.Transparent;

        }

        /// <summary>
        /// Fonction inverse de NouvelleCampagne_Click - Cache les élements de création de campagne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Aaron</Author>
        private void AnnulerNouvCamp_Click(object sender, RoutedEventArgs e)
        {
            NomNouvCamp.Visibility = Visibility.Collapsed;
            Valider.Visibility = Visibility.Collapsed;

            // Inversion de l'affichage des boutons - On peut créer une campagne
            NouvCamp.Visibility = Visibility.Visible;
            AnnulerNouvCamp.Visibility = Visibility.Collapsed;

            // On peut de nouveau rejoindre une campagne ou en éditer une.
            RejoidComp.IsEnabled = true;
            if (CampagnesListe.SelectedIndex != -1) // Cas rare : Il n'y a pas du tout de campagne mais on annule tout de même la création
            {
                Edit.Visibility = Visibility.Visible;
            }

            // On peut de nouveau intéragir avec la liste des campagnes
            CacheCampagnesListe.Visibility = Visibility.Collapsed;
            CacheCampagnesListe.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f5d9a9"));
        }

        /// <summary>
        /// Ajoute la nouvelle campagne au fakeDAO si possible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Baptiste</Author>
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            string contenu = NomNouvCamp.Text; // On récupère le nom de la campagne
            bool okCheck = true;

            #region Checks

            string? messageCheckFail = null;
            // Un seul check à la fois, on ne veut pas aggresser l'utilisateur avec des popups en chaine
            if (string.IsNullOrWhiteSpace(contenu)) { messageCheckFail = "Le nom de la campagne ne peut pas être vide"; okCheck = false; }
            if (fakeDAO.CampagneTaken(contenu, idUser) == false) { messageCheckFail = "Une campagne du même nom existe déjà"; okCheck = false; }

            if (!okCheck) // Si un problème est rencontré, on en informe l'utilisateur
            {
                CustomMessageBox messageBox = new CustomMessageBox(messageCheckFail);
                messageBox.ShowDialog();
            }

            #endregion

            if (okCheck) // Si tout va bien 
            {
                fakeDAO.CreateCampagne(contenu, idUser); // On créé la campagne
                MettreAJourListBox(); // On fait apparaître la noouvelle campagne dans la liste des campagnes

                // On remet les éléments à leur état d'origine
                NouvCamp.Visibility = Visibility.Visible;
                AnnulerNouvCamp.Visibility = Visibility.Collapsed;
                NomNouvCamp.Visibility = Visibility.Collapsed;
                Valider.Visibility = Visibility.Collapsed;
                CacheCampagnesListe.Visibility = Visibility.Collapsed;
                CacheCampagnesListe.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f5d9a9"));

                CampagnesListe.SelectedIndex = CampagnesListe.Items.Count - 1; // La campagne selectionnée est la nouvelle - On voit ses informations directement
            }
        }

        #endregion

        #region Edition Campagne

        /// <summary>
        /// Affiche les options d'édition des informations de la campagne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Baptiste</Author>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Campagne selectedCampagne = (Campagne)CampagnesListe.SelectedItem; // On récupère la campagne selectionnée
            // On affiche les options d'édition
            SupprimerCamp.Visibility = Visibility.Visible;
            NomCampTextBox.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Visible;
            Sauv.Visibility = Visibility.Visible;

            // On cache les options de non édition
            RejoidComp.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Collapsed;
            NouvCamp.IsEnabled = false; // NouvCamp est uniquement désactivé pour l'aspect visuel

            CacheCampagnesListe.Visibility = Visibility.Visible; // On empêche toute interaction avec la liste des campagnes
            CacheCampagnesListe.Content = "Vous modifiez la campagne : \n" + selectedCampagne.Nom; // On indique à l'utilisateur le nom originel de sa campagne
            NomCampTextBox.Text = selectedCampagne.Nom;
            NomCampTextBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Cache les options d'édition des informations de la campagne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Baptiste</Author>
        private void EditS_Click(object sender, RoutedEventArgs e)
        {
            MasquerElements();
        }

        /// <summary>
        /// Edite la campagne sélectionnée avec les nouvelles informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Baptiste</Author>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Campagne campagneEdit = (Campagne)CampagnesListe.SelectedItem; // On récupère la campagne selectionnée
            string nouveauNomCampagne = NomCampTextBox.Text; // On récupère le nouveau nom de la campagne
            bool okCheck = true;

            #region Checks 

            string? messageCheckFail = null;

            // Un seul check à la fois, on ne veut pas aggresser l'utilisateur avec des popups en chaine
            if (fakeDAO.CampagneTaken(nouveauNomCampagne, idUser) == false) { messageCheckFail = "Vous avez déjà une campagne portant ce nom"; okCheck = false; }
            else if (string.IsNullOrWhiteSpace(nouveauNomCampagne)) { messageCheckFail = "Le nom de votre campagne ne peut pas être vide"; okCheck = false; }

            if (!okCheck) // Les checks ne sont pas passés
            {
                CustomMessageBox messageBox = new CustomMessageBox(messageCheckFail); // On en informe l'utilisateur
                messageBox.ShowDialog();
            }

            #endregion

            if (okCheck) // Si tout va bien
            {
                campagneEdit.Nom = nouveauNomCampagne;
                fakeDAO.UpdateCampagne(campagneEdit.ID, campagneEdit); // On met à jour la campagne
                
                AfficherInfos(CampagnesListe.SelectedIndex); // On met à jour les informations affichées
                MettreAJourListBox(); // On met à jour la liste des campagnes
                MasquerElements(); // On masque les élements d'édition
            }
        }

        /// <summary>
        /// Supprime la campagne sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Baptiste</Author>
        private void SupprimerCamp_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox("Etes-vous sûr(e)?\nCette action est irréversible", true); // Supprimer une campagne a de grandes conséquences , on demande confirmation
            customMessageBox.YesButtonClicked += (sender, e) => // Si l'utilisateur réponds Oui
            {
                Campagne selection = (Campagne)CampagnesListe.SelectedItem; // On récupère la campagne actuelle
                int idCampagne = selection.ID; // On récupère l'ID de la campagne
                fakeDAO.DeleteCampagne(idCampagne); // On supprime la campagne

                // Champs qui se réinitialisent 
                NomCampTextBlock.Text = "";
                DateCreationTextBlock.Text = "";
                DateModificationTextBlock.Text = "";
                MettreAJourListBox(); // On met à jour la liste des campagnes
                MasquerElements(); // On masque les éléments d'édition
                // Aucune campagne n'est selectionnée, il n'y a rien à éditer ou rejoindre
                Edit.Visibility = Visibility.Collapsed;
                RejoidComp.IsEnabled = false;
            };

            customMessageBox.ShowDialog();
        }

        #endregion

        #region Utilitaires

        /// <summary>
        /// Méthode pour masquer les éléments du menu d'édition
        /// </summary>
        /// <Author>Baptiste</Author>
        private void MasquerElements()
        {
            NomCampTextBox.Visibility = Visibility.Collapsed;
            Valider.Visibility = Visibility.Collapsed;
            RejoidComp.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Collapsed;
            Sauv.Visibility = Visibility.Collapsed;
            SupprimerCamp.Visibility = Visibility.Collapsed;
            CacheCampagnesListe.Visibility = Visibility.Collapsed;
            CacheCampagnesListe.Content = "";
            NouvCamp.IsEnabled = true;
        }

        #endregion

        #region Transitions de page

        /// <summary>
        /// Ouvre la fenêtre Personnages et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Baptiste</Author>
        private void OuvrirPersonnages_Click(object sender, RoutedEventArgs e)
        {
            Campagne campagne = (Campagne)CampagnesListe.SelectedItem; // On récupère la campagne sélectionnée
            int idCampagne = campagne.ID; // On récupère son ID

            // La page Personnages est créée
            Personnages pagecreation = new Personnages(idUser, roleUser, idCampagne);
            pagecreation.Show();
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;

            CampagnesWindow.Close(); // On ferme la fenêtre actuelle
        }

        #endregion
    }
}