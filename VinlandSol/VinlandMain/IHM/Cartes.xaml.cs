using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using VinlandSol.BDD;
using VinlandSol.Métier;
using VinlandMain.IHM;

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
        public Cartes(int idUser, string roleUser, int idcampagne)
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
            if (CartesListe.SelectedItem != null)
            { 
                Edit.Visibility = Visibility.Visible;
                RejoidCarte.IsEnabled = true;
            }
            AfficherInformationsCarte(CartesListe.SelectedIndex);
        }

        /// <summary>
        /// Permet d'afficher les informations de la carte selectionnée
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void AfficherInformationsCarte(int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < fakeDAO.GetCartes().Count)
            {
                Métier.Carte carte = fakeDAO.GetCartes()[selectedIndex];
                NomCarteTextBlock.Text = carte.Nom;
                DateCreationTextBlock.Text = carte.DateCreation.ToString("dd/MM/yyyy HH:mm:ss");
                DateModificationTextBlock.Text = carte.DateModification.ToString("dd/MM/yyyy HH:mm:ss");

                if (fakeDAO.GetCartes()[selectedIndex].Visibilite) 
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
            NomCarteTextBox.Visibility = Visibility.Visible;
            Sauv.Visibility = Visibility.Visible;
            Suppr.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Collapsed;

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
            string nouveauNomCarte = NomCarteTextBox.Text;

            if (string.IsNullOrEmpty(nouveauNomCarte))
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("Le nom de la carte ne peut pas être vide.");
                customMessageBox.ShowDialog();
                return;
            }
            fakeDAO.UpdateCarteName(CartesListe.SelectedIndex + 1, nouveauNomCarte);

            NomCarteTextBox.Text = "";
            AfficherInformationsCarte(CartesListe.SelectedIndex);
            MettreAJourListBox();
            MasquerElements();
            Edit.Visibility = Visibility.Collapsed;
            RejoidCarte.IsEnabled = false;
        }

        /// <summary>
        /// Supprime la carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerCarte(object sender, RoutedEventArgs e)
        {
            Métier.Carte selection = (Métier.Carte)CartesListe.SelectedItem;
            int idCarte = selection.Id;

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
            fakeDAO.UpdateCarteVisibilite(CartesListe.SelectedIndex + 1, false);
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
            fakeDAO.UpdateCarteVisibilite(CartesListe.SelectedIndex + 1, true);
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
            VisibleCarte.Visibility = Visibility.Collapsed;
            VisibleCarteHidden.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Mets à jour CartesListe selon les données du DAO 
        /// </summary>
        public void MettreAJourListBox()
        {
            List<Métier.Carte> cartes = fakeDAO.GetCartes(); // On récupère les cartes depuis le fakeDAO

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
                pagecreationcarte = new CreationCarte(this,idCampagne);
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
            int idCarte = CartesListe.SelectedIndex+1;
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
