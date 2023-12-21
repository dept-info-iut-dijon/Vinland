using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using VinlandSol.BDD;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour Cartes.xaml
    /// </summary>
    public partial class Cartes : Window
    {
        private FakeDAO fakeDAO = FakeDAO.Instance;
        private int idUser;
        private string roleUser;
        private int idCampagne;
        private CreationCarte? pagecreationcarte;
        private bool creaCarteOpen = false;

        /// <summary>
        /// Constructeur de la page
        /// </summary>
        public Cartes(int idUser, string roleUser, int idCampagne)
        {
            InitializeComponent();
            this.idUser = idUser;
            this.roleUser = roleUser;
            this.idCampagne = idCampagne;
            MettreAJourListBox();
            Closed += ShutdownEnForce; // ShutdownEnForce est appelé à la fermeture de cette fenêtre

        }


        /// <summary>
        /// Actualise les informations de la carte selectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CartesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Edit.Visibility = Visibility.Visible;
            RejoidCarte.IsEnabled = true;
            AfficherInformationsCarte(CartesListe.SelectedIndex);
        }

        /// <summary>
        /// Permet d'afficher les informations de la carte selectionnée
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void AfficherInformationsCarte(int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < fakeDAO.GetCurrentCartes(idCampagne).Count)
            {
                Métier.Carte carte = fakeDAO.GetCurrentCartes(idCampagne)[selectedIndex];
                NomCarteTextBlock.Text = carte.Nom;
                DateCreationTextBlock.Text = carte.DateCreation.ToString("dd/MM/yyyy HH:mm:ss");
                DateModificationTextBlock.Text = carte.DateModification.ToString("dd/MM/yyyy HH:mm:ss");

                if (fakeDAO.GetCurrentCartes(idCampagne)[selectedIndex].Visibilite)
                {
                    VisibleCarte.Visibility = Visibility.Visible;
                    VisibleCarteHidden.Visibility = Visibility.Collapsed;
                }
                else
                {
                    VisibleCarte.Visibility = Visibility.Collapsed;
                    VisibleCarteHidden.Visibility = Visibility.Visible;
                }

            }
        }

        /// <summary>
        /// Ouvre les options d'édition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Métier.Carte selectedCarte = (Métier.Carte)CartesListe.SelectedItem; // On récupère la carte selectionnée
            // On affiche les options d'édition
            NomCarteTextBox.Visibility = Visibility.Visible;
            Sauv.Visibility = Visibility.Visible;
            Suppr.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Visible;
            // On cache les options de non édition
            Edit.Visibility = Visibility.Collapsed;
            RejoidCarte.Visibility = Visibility.Collapsed;
            // Uniquement désactivés pour l'aspect visuel
            AjoutCarte.IsEnabled = false;
            Retour.IsEnabled = false;

            CacheCartesListe.Visibility = Visibility.Visible;
            CacheCartesListe.Content = "Vous modifiez la carte : \n" + selectedCarte.Nom;
            NomCarteTextBox.Text = selectedCarte.Nom;
            NomCarteTextBox.Visibility = Visibility.Visible;

            VisibleCarte.IsEnabled = true;
            VisibleCarteHidden.IsEnabled = true;
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
        /// Sauvegarde les changements de la carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sauv_Click(object sender, RoutedEventArgs e)
        {
            Métier.Carte carteEdit = (Métier.Carte)CartesListe.SelectedItem;
            string nouveauNomCarte = NomCarteTextBox.Text;
            bool okCheck = true;

            #region Checks

            string? messageCheckFail = null;

            // Un seul check à la fois, on ne veut pas aggresser l'utilisateur avec des popups en chaine
            if (fakeDAO.CarteTaken(nouveauNomCarte, idUser) == false) { messageCheckFail = "Vous avez déjà une carte portant ce nom"; okCheck = false; }
            else if (string.IsNullOrWhiteSpace(nouveauNomCarte)) { messageCheckFail = "Le nom de votre carte ne peut pas être vide"; okCheck = false; }

            if (!okCheck)
            {
                CustomMessageBox customMessageBox = new CustomMessageBox(messageCheckFail);
                customMessageBox.ShowDialog();
            }

            #endregion

            if (okCheck)
            {
                carteEdit.Nom = nouveauNomCarte;
                fakeDAO.UpdateCarte(carteEdit.ID, carteEdit);

                NomCarteTextBox.Text = "";
                AfficherInformationsCarte(CartesListe.SelectedIndex);
                MettreAJourListBox();
                MasquerElements();
                Edit.Visibility = Visibility.Collapsed;
                RejoidCarte.IsEnabled = false;
            }
        }

        /// <summary>
        /// Supprime la carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerCarte(object sender, RoutedEventArgs e)
        {
            Métier.Carte selection = (Métier.Carte)CartesListe.SelectedItem;
            int idCarte = selection.ID;

            fakeDAO.DeleteCarte(idCarte);

            NomCarteTextBlock.Text = "";
            DateCreationTextBlock.Text = "";
            DateModificationTextBlock.Text = "";
            MettreAJourListBox();
            MasquerElements();
            Edit.Visibility = Visibility.Collapsed;
            RejoidCarte.IsEnabled = false;
        }

        /// <summary>
        /// Change la visibilité de la carte aux joueurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Aaron</author>
        private void OeilChange(object sender, RoutedEventArgs e)
        {
            Métier.Carte carteEdit = (Métier.Carte)CartesListe.SelectedItem;
            carteEdit.Visibilite = false;
            fakeDAO.UpdateCarte(carteEdit.ID, carteEdit);
            AfficherInformationsCarte(CartesListe.SelectedIndex);
        }

        /// <summary>
        /// Change la visibilité de la carte aux joueurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Aaron</author>
        private void OeilChangeHidden(object sender, RoutedEventArgs e)
        {
            Métier.Carte carteEdit = (Métier.Carte)CartesListe.SelectedItem;
            carteEdit.Visibilite = true;
            fakeDAO.UpdateCarte(carteEdit.ID, carteEdit);
            AfficherInformationsCarte(CartesListe.SelectedIndex);
        }

        /// <summary>
        /// Méthode pour masquer les éléments du menu d'édition
        /// </summary>
        /// <author>Aaron</author>
        private void MasquerElements()
        {
            NomCarteTextBox.Visibility = Visibility.Collapsed;
            Sauv.Visibility = Visibility.Collapsed;
            Suppr.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Collapsed;
            AjoutCarte.Visibility = Visibility.Visible;
            RejoidCarte.Visibility = Visibility.Visible;
            CacheCartesListe.Visibility = Visibility.Collapsed;
            CacheCartesListe.Content = "";
            AjoutCarte.IsEnabled = true;
            Retour.IsEnabled = true;
            VisibleCarte.IsEnabled = false;
            VisibleCarteHidden.IsEnabled = false;
        }

        /// <summary>
        /// Mets à jour CartesListe selon les données du DAO 
        /// </summary>
        public void MettreAJourListBox()
        {
            List<Métier.Carte> cartes = fakeDAO.GetCurrentCartes(idCampagne); // On récupère les cartes depuis le fakeDAO

            CartesListe.Items.Clear(); // On efface les éléments existants dans la ListBox

            foreach (Métier.Carte carte in cartes) // On ajoute chaque carte de la liste
            {
                CartesListe.Items.Add(carte);
            }
            CartesListe.DisplayMemberPath = "Nom"; // On affiche le contenu de la propriété 'Nom' des cartes
        }

        #region Creation Carte

        /// <summary>
        /// Ouvre la fenêtre CreationCarte et limite cette action tant que cette dernière n'est pas fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCreationCarte_Click(object sender, RoutedEventArgs e)
        {
            if (creaCarteOpen == false)
            {
                this.IsEnabled = false;
                pagecreationcarte = new CreationCarte(this, idCampagne);
                pagecreationcarte.Closed += CreationCarte_Closed;
                pagecreationcarte.Left = this.Left;
                pagecreationcarte.Top = this.Top;
                pagecreationcarte.Show();
                creaCarteOpen = true;
            }
        }

        /// <summary>
        /// Force le shutdown de l'application quand CreationCarte est la dernière fenêtre à être fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreationCarte_Closed(object sender, EventArgs e)
        {
            creaCarteOpen = false;
            this.IsEnabled = true;
            if (Application.Current.Windows.Count == 1)
            {
                Application.Current.Shutdown();
            }
        }

        #endregion

        #region Sortie Cartes

        /// <summary>
        /// Ouvre le fenêtre Personnages et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirPersonnages_Click(object sender, RoutedEventArgs e)
        {
            Personnages pagecreationperso = new Personnages(idUser, roleUser, idCampagne);
            pagecreationperso.Left = this.Left;
            pagecreationperso.Top = this.Top;
            pagecreationperso.Show();

            CartesWindow.Hide(); // Evite de voir la fenêtre se fermer en retard
            var timer = new System.Timers.Timer(100); // Délai de 100 millisecondes 
            timer.Elapsed += (s, args) =>
            {
                timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    CartesWindow.Close(); // On ferme cette fenêtre en retard pour éviter que le if de ShutdownEnForce ne passe.
                });
            };
            timer.Start();
        }

        /// <summary>
        /// Ouvre le fenêtre Carte et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCarte_Click(object sender, RoutedEventArgs e)
        {
            int idCarte = CartesListe.SelectedIndex + 1;
            Carte carteselect = new Carte(idUser, roleUser, idCarte, idCampagne);
            carteselect.Left = this.Left;
            carteselect.Top = this.Top;
            carteselect.Show();

            CartesWindow.Hide(); // Evite de voir la fenêtre se fermer en retard
            var timer = new System.Timers.Timer(100); // Délai de 100 millisecondes 
            timer.Elapsed += (s, args) =>
            {
                timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    CartesWindow.Close(); // On ferme cette fenêtre en retard pour éviter que le if de ShutdownEnForce ne passe.
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
