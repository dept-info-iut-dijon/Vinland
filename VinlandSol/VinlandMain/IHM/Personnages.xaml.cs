using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using VinlandMain.IHM;
using VinlandSol.BDD;
using VinlandSol.Métier;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour Personnages.xaml
    /// </summary>
    public partial class Personnages : Window
    {
        private FakeDAO fakeDAO = FakeDAO.Instance;
        private int idUser;
        private string roleUser;
        private int idCampagne;
        private AjouterPersonnage? pageajouterPerso;
        private bool ajouterPersoOpen = false;

        /// <summary>
        /// Constructeur de la fenêtre
        /// </summary>
        public Personnages(int idUser, string roleUser, int idCampagne)
        {
            InitializeComponent();
            this.idCampagne = idCampagne;
            this.idUser = idUser;
            this.roleUser = roleUser;
            MettreAJourListBox();
            Closed += ShutdownEnForce;
        }

        /// <summary>
        /// Permet d'afficher les informations du personnage selectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonnagesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PersonnagesListe.SelectedItem != null) { Edit.Visibility = Visibility.Visible; }
            AfficherInformationsPersonnage(PersonnagesListe.SelectedIndex);
        }

        /// <summary>
        /// Permet d'afficher les informations du personnage selectionné/modifié
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void AfficherInformationsPersonnage(int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < fakeDAO.GetCurrentPersonnages(idCampagne).Count)
            {
                Personnage personnage = fakeDAO.GetCurrentPersonnages(idCampagne)[selectedIndex];
                NomUtilisateurTextBlock.Text = fakeDAO.GetJoueur(personnage.IDJoueur).Nom;
                NomPersonnageTextBlock.Text = personnage.Nom;
                DateCreationTextBlock.Text = personnage.DateCreation.ToString("dd/MM/yyyy HH:mm:ss"); ;
            }
        }



        /// <summary>
        /// Supprime le personnage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerPersonnage(object sender, RoutedEventArgs e)
        {
            Personnage selectedIndex = (Personnage)PersonnagesListe.SelectedItem;
            int idPersonnage = selectedIndex.ID;

            fakeDAO.DeletePersonnage(idPersonnage);

            NomUtilisateurTextBlock.Text = "";
            NomPersonnageTextBlock.Text = "";
            DateCreationTextBlock.Text = "";
            MettreAJourListBox();
            MasquerElements();
            Edit.Visibility = Visibility.Collapsed;

        }

        /// <summary>
        /// Ouvre les options d'édition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Personnage selectedPersonnage = (Personnage)PersonnagesListe.SelectedItem; // On récupère le personnage selectionnée
            // On affiche les options d'édition
            NomPersonnageTextBox.Visibility = Visibility.Visible;
            ValiderButton.Visibility = Visibility.Visible;
            BoutonSuppression.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Visible;
            // On cache les options de non édition
            Edit.Visibility = Visibility.Collapsed;
            SelectCarte.Visibility = Visibility.Collapsed;
            // Uniquement désactivés pour l'aspect visuel
            AjoutPerso.IsEnabled = false;
            Retour.IsEnabled = false;

            CachePersonnagesListe.Visibility = Visibility.Visible; // On empêche toute interaction avec la liste des personnages
            CachePersonnagesListe.Content = "Vous modifiez le nom du personnage : \n" + selectedPersonnage.Nom; // On indique à l'utilisateur le nom originel de son personnage
            NomPersonnageTextBox.Text = selectedPersonnage.Nom;
            NomPersonnageTextBox.Visibility = Visibility.Visible;

        }
        /// <summary>
        /// Ferme les options d'édition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditS_Click(object sender, RoutedEventArgs e)
        {
            MasquerElements();
        }

        /// <summary>
        /// Mets à jour les informations modifiées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ValiderButton_Click(object sender, RoutedEventArgs e)
        {
            Personnage personnageEdit = (Personnage)PersonnagesListe.SelectedItem; // On récupère le personnage selectionné
            int idPersonnageEdit = personnageEdit.ID; // On récupère l'ID du personnage
            string nouvNomPersonnage = NomPersonnageTextBox.Text;
            bool okCheck = true;

            #region Checks

            string? messageCheckFail = null;
            // Un seul check à la fois, on ne veut pas aggresser l'utilisateur avec des popups en chaine
            if (fakeDAO.PersonnageTaken(nouvNomPersonnage, idCampagne) == false) { messageCheckFail = "Vous avez déjà un personnage portant ce nom"; okCheck = false; }
            if (string.IsNullOrWhiteSpace(nouvNomPersonnage)) { messageCheckFail = "Le nom du personnage ne peut pas être vide"; okCheck = false; }

            if (!okCheck) // Si un problème est rencontré, on en informe l'utilisateur
            {
                CustomMessageBox customMessageBox = new CustomMessageBox(messageCheckFail);
                customMessageBox.ShowDialog();
            }

            #endregion

            if (okCheck) // Si tout va bien
            {
                fakeDAO.UpdatePersonnageName(idPersonnageEdit, nouvNomPersonnage);

                NomPersonnageTextBox.Text = "";
                AfficherInformationsPersonnage(PersonnagesListe.SelectedIndex);
                MettreAJourListBox();
                MasquerElements();
            }

        }

        /// <summary>
        /// Méthode pour masquer les éléments du menu d'édition
        /// </summary>
        private void MasquerElements()
        {
            NomPersonnageTextBox.Visibility = Visibility.Collapsed;
            ValiderButton.Visibility = Visibility.Collapsed;
            BoutonSuppression.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Collapsed;
            AjoutPerso.Visibility = Visibility.Visible;
            SelectCarte.Visibility = Visibility.Visible;
            CachePersonnagesListe.Visibility = Visibility.Collapsed;
            CachePersonnagesListe.Content = "";
            AjoutPerso.IsEnabled = true;
            Retour.IsEnabled = true;
        }

        /// <summary>
        /// Mets à jour PersonnagesListe selon les données du DAO 
        /// </summary>
        public void MettreAJourListBox()
        {
            List<Personnage> personnages = fakeDAO.GetCurrentPersonnages(idCampagne); // On récupère les personnages depuis le fakeDAO

            PersonnagesListe.Items.Clear(); // On efface les éléments existants dans la ListBox

            foreach (Personnage personnage in personnages) // On ajoute chaque personnage de la liste
            {
                PersonnagesListe.Items.Add(personnage);
            }
            PersonnagesListe.DisplayMemberPath = "Nom"; // On affiche le contenu de la propriété 'Nom' des personnages
        }

        #region Ajouter Personnage

        /// <summary>
        /// Ouvre la fenêtre AjouterPersonnage et limite cette action tant que cette dernière n'est pas fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirAjouterPersonnage_Click(object sender, RoutedEventArgs e)
        {
            if (ajouterPersoOpen == false)
            {
                this.IsEnabled = false;
                pageajouterPerso = new AjouterPersonnage(this, idCampagne);
                pageajouterPerso.Closed += AjouterPerso_Closed;
                pageajouterPerso.Left = this.Left;
                pageajouterPerso.Top = this.Top;
                pageajouterPerso.Show();
                ajouterPersoOpen = true;
            }
        }

        /// <summary>
        /// Force le shutdown de l'application quand AjouterPersonnage est la dernière fenêtre a être fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterPerso_Closed(object sender, EventArgs e)
        {
            ajouterPersoOpen = false;
            this.IsEnabled = true;
            if (Application.Current.Windows.Count == 1)
            {
                Application.Current.Shutdown();
            }
        }

        #endregion

        #region Sortie Personnages

        /// <summary>
        /// Ouvre la fenêtre Campagnes et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCampagnes_Click(object sender, RoutedEventArgs e)
        {
            Campagnes pagecreation = new Campagnes(idUser, roleUser);
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            pagecreation.Show();

            PersonnagesWindow.Hide(); // Evite de voir la fenetre se fermer en retard
            var timer = new System.Timers.Timer(100); // Delai de 100 millisecondes 
            timer.Elapsed += (s, args) =>
            {
                timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    PersonnagesWindow.Close(); // On ferme cette fenêtre en retard pour éviter que le if de ShutdownEnForce ne passe.
                });
            };
            timer.Start();
        }

        /// <summary>
        /// Ouvre la fenêtre Cartes et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCartes_Click(object sender, RoutedEventArgs e)
        {
            Cartes pagecreation = new Cartes(idUser, roleUser, idCampagne);
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            pagecreation.Show();

            PersonnagesWindow.Hide(); // Evite de voir la fenêtre se fermer en retard
            var timer = new System.Timers.Timer(100); // Délai de 100 millisecondes 
            timer.Elapsed += (s, args) =>
            {
                timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    PersonnagesWindow.Close(); // On ferme cette fenêtre en retard pour éviter que le if de ShutdownEnForce ne passe.
                });
            };
            timer.Start();
        }

        /// <summary>
        /// Force le shutdown de l'application quand cette fenêtre est la dernière a être fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShutdownEnForce(object sender, EventArgs e)
        {
            if (Application.Current.Windows.Count == 1)
            {

                Application.Current.Shutdown();
            }
        }

        #endregion

    }
}
