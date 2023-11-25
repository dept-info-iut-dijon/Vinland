using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VinlandSol.BDD;
using VinlandSol.IHM;
using VinlandSol.Métier;

namespace VinlandMain.IHM
{
    /// <summary>
    /// Logique d'interaction pour VisionCampagne.xaml
    /// </summary>
    public partial class Campagnes : Window
    {
        private FakeDAO fakeDAO = FakeDAO.Instance;
        private int idUser;
        private string roleUser;
        private int indiceCampagneEnEdition = -1;

        /// <summary>
        /// Constructeur de la fenêtre
        /// </summary>
        public Campagnes(int idUser, string roleUser)
        {
            InitializeComponent();
            this.idUser = idUser;
            this.roleUser = roleUser;
            MettreAJourListBox();
            CampagnesListe.SelectionChanged += CampagnesListe_SelectionChanged;
        }

        /// <summary>
        /// Affiche les informations sur la droite de l'écran par rapport à la campagne sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CampagnesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CampagnesListe.SelectedItem != null) 
            { 
                Edit.Visibility = Visibility.Visible; 
                RejoidComp.IsEnabled = true;
            }
            AfficherInfos(CampagnesListe.SelectedIndex);
        }

        /// <summary>
        /// Permet d'actualiser les informations de la campagne selectionnée/modifiée
        /// </summary>
        /// <param name="index"></param>
        private void AfficherInfos(int index)
        {            
            if (index >= 0 && index < fakeDAO.GetCurrentCampagnes(roleUser, idUser).Count)
            {
                Campagne selectedCampagne = fakeDAO.GetCurrentCampagnes(roleUser, idUser)[index];
                NomCampTextBlock.Text = selectedCampagne.Nom;
                DateCreationTextBlock.Text = selectedCampagne.DateCreation.ToString("dd/MM/yyyy HH:mm:ss");
                DateModificationTextBlock.Text = selectedCampagne.DateModification.ToString("dd/MM/yyyy HH:mm:ss");
                NombreCartesTextBlock.Text = selectedCampagne.IDCartes.Count().ToString();
                NombrePersonnagesTextBlock.Text = selectedCampagne.IDPersonnages.Count().ToString();
            }
        }

        /// <summary>
        /// Affiche les éléments permettant de créer une campagne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NouvelleCampagne_Click(object sender, RoutedEventArgs e)
        {
            NomNouvCamp.Visibility = Visibility.Visible;
            Valider.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Ajoute la nouvelle campagne au fakeDAO si possible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            string contenu = NomNouvCamp.Text;
            bool okCheck = true;

            #region Checks

            string? messageCheckFail = null;


            if (string.IsNullOrWhiteSpace(contenu)) { messageCheckFail = "Le nom de la campagne ne peut pas être vide"; okCheck = false; }
            if (fakeDAO.CampagneTaken(contenu,idUser) == false) { messageCheckFail = "Une campagne du même nom existe déjà"; okCheck = false; }

            if (!okCheck)
            {
                CustomMessageBox messageBox = new CustomMessageBox(messageCheckFail);
                messageBox.ShowDialog();
                return; // Le MJ peut encore essayer de créer une campagne valide.
            }

            #endregion

            // Si tout va bien 
            fakeDAO.CreateCampagne(contenu,idUser);
            MettreAJourListBox();
            NomNouvCamp.Visibility = Visibility.Collapsed;
            Valider.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Affiche les options d'édition des informations de la campagne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = CampagnesListe.SelectedIndex;
            RejoidComp.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Collapsed;
            EditS.Visibility = Visibility.Visible;
            Sauv.Visibility = Visibility.Visible;
            NouvCamp.IsEnabled = false;
            SupprimerCamp.Visibility = Visibility.Visible;
            NomCampTextBox.Visibility = Visibility.Visible;
            NomCampTextBlock.Visibility = Visibility.Collapsed;

            if (selectedIndex >= 0 && selectedIndex < fakeDAO.GetCurrentCampagnes(roleUser, idUser).Count)
            {
                Campagne selectedCampagne = fakeDAO.GetCurrentCampagnes(roleUser, idUser)[selectedIndex];
                NomCampTextBox.Text = selectedCampagne.Nom;
                NomCampTextBox.Visibility = Visibility.Visible;
                NomCampTextBlock.Visibility = Visibility.Collapsed;
                indiceCampagneEnEdition = selectedIndex;
            }
        }

        /// <summary>
        /// Cache les options d'édition des informations de la campagne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditS_Click(object sender, RoutedEventArgs e)
        {
            MasquerElements();
        }

        /// <summary>
        /// Edite la campagne sélectionnée avec les nouvelles informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Campagne campagneEdit = (Campagne)CampagnesListe.SelectedItem;
            int idCampagneEdit = campagneEdit.ID;
            string newCampaignName = NomCampTextBox.Text;
            bool okCheck = true;

            #region Checks 

            string? messageCheckFail = null;

            // Un seul check à la fois, on ne veut pas aggresser l'utilisateur avec des popups en chaine
            if (fakeDAO.CampagneTaken(newCampaignName,idUser) == false) { messageCheckFail = "Vous avez déjà une campagne portant ce nom"; okCheck = false; }
            else if (string.IsNullOrWhiteSpace(newCampaignName)) { messageCheckFail = "Le nom de votre campagne ne peut pas être vide"; okCheck = false; }

            if (!okCheck)
            {
                CustomMessageBox messageBox = new CustomMessageBox(messageCheckFail);
                messageBox.ShowDialog();
            }

            #endregion

            if(okCheck)
            {
                fakeDAO.UpdateCampagneName(idCampagneEdit, newCampaignName);

                AfficherInfos(indiceCampagneEnEdition);
                indiceCampagneEnEdition = -1;
                MettreAJourListBox();
                MasquerElements();
                Edit.Visibility = Visibility.Collapsed;
                RejoidComp.IsEnabled = false;
            }
        }

        /// <summary>
        /// Ouvre la fenêtre Personnages et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirPersonnages_Click(object sender, RoutedEventArgs e)
        {
            int idCampagne = CampagnesListe.SelectedIndex+1;
            Personnages pagecreation = new Personnages(idUser,roleUser, idCampagne);
            pagecreation.Show();
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            CampagnesWindow.Close();
        }

        /// <summary>
        /// Supprime la campagne sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerCamp_Click(object sender, RoutedEventArgs e)
        {
            Campagne selection = (Campagne)CampagnesListe.SelectedItem;

            int idCampagne = selection.ID ;
            fakeDAO.DeleteCampagne(idCampagne);

            // Champs qui se réinitialisent 
            NomCampTextBlock.Text = "";
            DateCreationTextBlock.Text = "";
            DateModificationTextBlock.Text = "";
            MettreAJourListBox();
            MasquerElements();
            Edit.Visibility = Visibility.Collapsed;
            RejoidComp.IsEnabled = false;
        }

        /// <summary>
        /// Méthode pour masquer les éléments du menu d'édition
        /// </summary>
        private void MasquerElements()
        {
            NomCampTextBox.Visibility = Visibility.Collapsed;
            NomCampTextBlock.Visibility = Visibility.Visible;
            Valider.Visibility = Visibility.Collapsed;
            RejoidComp.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Collapsed;
            Sauv.Visibility = Visibility.Collapsed;
            SupprimerCamp.Visibility = Visibility.Collapsed;
            NouvCamp.IsEnabled = true;
        }

        /// <summary>
        /// Mets à jour CampagnesListe selon les données du DAO 
        /// </summary>
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

        
    }
}