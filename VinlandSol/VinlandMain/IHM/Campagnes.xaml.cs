using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VinlandSol.IHM;
using VinlandSol.Métier;
using VinlandSol.BDD;

namespace VinlandMain.IHM
{
    /// <summary>
    /// Logique d'interaction pour VisionCampagne.xaml
    /// </summary>
    public partial class Campagnes : Window
    {
        private FakeDAO fakeDAO;
        
        /// <summary>
        /// Constructeur de la fenêtre
        /// </summary>
        public Campagnes()
        {
            InitializeComponent();
            fakeDAO = new FakeDAO();
            CampagnesListe.ItemsSource = fakeDAO.GetCampagnes();
            NomNouvCamp.TextChanged += NomNouvCamp_TextChanged;
            //NomCampTextBox.TextChanged += NomCampTextBox_TextChanged;
            SupprimerCamp.Click += SupprimerCamp_Click;
        }

        private void NomNouvCamp_TextChanged(object sender, TextChangedEventArgs e)
        {
            NomCampTextBlock.Text = NomNouvCamp.Text;
        }

        private void CampagnesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CampagnesListe.SelectedItem != null)
            {
                string selectedCampagneName = (string)CampagnesListe.SelectedItem;
                Campagne selectedCampagne = fakeDAO.GetCampagnes().FirstOrDefault(c => c.Nom == selectedCampagneName);
                NomCampTextBox.Text = selectedCampagne.Nom;

                if (selectedCampagne != null)
                {
                    NomCampTextBlock.Text = selectedCampagne.Nom;
                    DateCreationTextBlock.Text = selectedCampagne.DateCreation.ToString();
                    DateModificationTextBlock.Text = selectedCampagne.DateModification.ToString();
                    NombreCartesTextBlock.Text = selectedCampagne.NombreCartes.ToString();
                    NombrePersonnagesTextBlock.Text = selectedCampagne.NombrePersonnages.ToString();
                }
            }
        }

        private void UpdateCampagneDetails()
        {
            // Mettez à jour les informations de la campagne actuellement sélectionnée
            if (CampagnesListe.SelectedItem != null)
            {
                Campagne selectedCampagne = (Campagne)CampagnesListe.SelectedItem;
                NomCampTextBlock.Text = selectedCampagne.Nom;
                DateModificationTextBlock.Text = selectedCampagne.DateModification.ToString();
            }
        }
        /*
        private void NomCampTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = NomCampTextBox.Text;
            text = NomCampTextBox.Text;
        }
        */

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
        /// Ajoute la nouvelle campagne dans le .txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            string newCampagneName = NomNouvCamp.Text;
            Campagne newCampagne = new Campagne(fakeDAO.GetCampagnes().Count + 1, newCampagneName);
            newCampagne.OnNomChanged += (s, args) => UpdateCampagneDetails();

            fakeDAO.AjouterCampagne(newCampagne);

            NomNouvCamp.Visibility = Visibility.Collapsed;
            Valider.Visibility = Visibility.Collapsed;

            // Mettez à jour la source de données de la ListBox avec des noms de campagne
            CampagnesListe.ItemsSource = fakeDAO.GetCampagnes().Select(c => c.Nom).ToList();
        }
        /// <summary>
        /// Affiche les options d'édition des informations de la campagne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
                AfficherElements();
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
            if (CampagnesListe.SelectedItem != null)
            {
                Campagne selectedCampagne = (Campagne)CampagnesListe.SelectedItem;
                string editedCampaignName = NomCampTextBox.Text;

                // Mettre à jour la liste des campagnes dans le FakeDAO avec le nouveau nom
                fakeDAO.UpdateCampagneName(selectedCampagne.ID, editedCampaignName);

                // Mettre à jour l'interface utilisateur
                UpdateCampagneDetails();
                MasquerElements();
            }
        }

        /// <summary>
        /// Ouvre la fenêtre Personnages et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirPersonnages_Click(object sender, RoutedEventArgs e)
        {
            Personnages pagecreation = new Personnages();
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
            if (CampagnesListe.SelectedItem != null)
            {
                // Supprimer la campagne sélectionnée de la liste
                Campagne selectedCampagne = (Campagne)CampagnesListe.SelectedItem;
                fakeDAO.DeleteCampagne(selectedCampagne.ID);

                // Mettez à jour la source de données de la ListBox
                CampagnesListe.ItemsSource = fakeDAO.GetCampagnes().Select(c => c.Nom).ToList();

                // Effacez les détails dans les TextBlocks
                NomCampTextBlock.Text = "";
                DateCreationTextBlock.Text = "";
                DateModificationTextBlock.Text = "";
                NombreCartesTextBlock.Text = "";
                NombrePersonnagesTextBlock.Text = "";

                // Masquez les éléments d'édition
                MasquerElements();
            }
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
            RejoidCompS.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Collapsed;
            Sauv.Visibility = Visibility.Collapsed;
            SupprimerCamp.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Méthode pour afficher les éléments du menu d'édition
        /// </summary>
        private void AfficherElements()
        {
            RejoidComp.Visibility = Visibility.Collapsed;
            RejoidCompS.Visibility = Visibility.Visible;
            NomCampTextBox.Visibility = Visibility.Visible;
            RejoidCompS.Visibility = Visibility.Visible;
            SupprimerCamp.Visibility = Visibility.Visible;
            Sauv.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Collapsed;
        }
    }
}
