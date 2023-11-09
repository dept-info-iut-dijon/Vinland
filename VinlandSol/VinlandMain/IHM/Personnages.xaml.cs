using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VinlandMain.IHM;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour Personnages.xaml
    /// </summary>
    public partial class Personnages : Window
    {
        Personnage nouveauPersonnage = new Personnage
        {
            NomPersonnage = "",
            DateCreation = DateTime.Now,
            NomUtilisateur = ""
        };
        List<Personnage> personnages = new List<Personnage>();

        /// <summary>
        /// Structure d'un personnage
        /// </summary>
        public struct Personnage
        {
            public string NomPersonnage { get; set; }
            public string NomUtilisateur { get; set; }
            public DateTime DateCreation { get; set; }
        }
        private AjouterPersonnage? pageajouterPerso;
        private bool ajouterPersoOpen = false;

        /// <summary>
        /// Constructeur de la fenêtre
        /// </summary>
        public Personnages()
        {
            InitializeComponent();
            Closed += ShutdownEnForce;
            LoadPersonnages();

        }

        /// <summary>
        /// Charge les personnages dans la listbox depuis le txt
        /// </summary>
        public void LoadPersonnages()
        {

            string filePath = "personnages.txt";
            personnages.Clear();
            PersonnagesListe.Items.Clear();

            if (File.Exists(filePath))
            {

                string[] lignes = File.ReadAllLines(filePath);


                foreach (string ligne in lignes)
                {

                    string[] elements = ligne.Split(',');

                    if (elements.Length == 2)
                    {
                        string idJoueur = elements[0].Trim();
                        string idPersonnage = elements[1].Trim();

                        PersonnagesListe.Items.Add($"{idPersonnage}");
                        personnages.Add(new Personnage
                        {
                            NomUtilisateur = idJoueur,
                            NomPersonnage = idPersonnage,
                            DateCreation = DateTime.Now
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Permet d'afficher les informations du personnage selectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonnagesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = PersonnagesListe.SelectedIndex;
            AfficherInformationsPersonnage(selectedIndex);
        }

        /// <summary>
        /// Permet d'afficher les informations du personnage selectionné/modifié
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void AfficherInformationsPersonnage(int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < personnages.Count)
            {
                string filePath = "personnages.txt";
                if (File.Exists(filePath))
                {
                    string[] lignes = File.ReadAllLines(filePath);
                    string[] elements = lignes[selectedIndex].Split(',');

                    if (elements.Length == 2)
                    {
                        string idJoueur = elements[0].Trim();
                        string idPersonnage = elements[1].Trim();

                        NomUtilisateurTextBlock.Text = idJoueur;
                        NomPersonnageTextBlock.Text = idPersonnage;
                        DateCreationTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                }
            }
        }

        /// <summary>
        /// Ouvre la fenêtre Campagnes et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCampagnes_Click(object sender, RoutedEventArgs e)
        {
            Campagnes pagecreation = new Campagnes();
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
            Cartes pagecreation = new Cartes();
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
        /// Ouvre la fenêtre AjouterPersonnage et limite cette action tant que cette dernière n'est pas fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirAjouterPersonnage_Click(object sender, RoutedEventArgs e)
        {
            if (ajouterPersoOpen == false)
            {
                pageajouterPerso = new AjouterPersonnage(this);
                pageajouterPerso.Closed += AjouterPerso_Closed;
                pageajouterPerso.Left = this.Left;
                pageajouterPerso.Top = this.Top;
                pageajouterPerso.Show();
                ajouterPersoOpen = true;
            }
        }

        /// <summary>
        /// Supprime le personnage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerPersonnage(object sender, RoutedEventArgs e)
        {
            int selectedIndex = PersonnagesListe.SelectedIndex;
            string filePath = "personnages.txt";

            if (selectedIndex >= 0 && selectedIndex < personnages.Count)
            {

                string nomPersonnageASupprimer = personnages[selectedIndex].NomPersonnage;

                PersonnagesListe.Items.RemoveAt(selectedIndex);

                personnages.RemoveAt(selectedIndex);

                File.WriteAllLines(filePath, personnages.Select(p => $"{p.NomUtilisateur}, {p.NomPersonnage}"));

                NomUtilisateurTextBlock.Text = "";
                NomPersonnageTextBlock.Text = "";
                DateCreationTextBlock.Text = "";
            }
        }

        /// <summary>
        /// Ouvre les options d'édition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrayonEdit_Click(object sender, RoutedEventArgs e)
        {
            NomPersonnageTextBox.Visibility = Visibility.Visible;
            ValiderButton.Visibility = Visibility.Visible;
            BoutonSuppression.Visibility = Visibility.Visible;
            boutonCrayon2.Visibility = Visibility.Visible;
            boutonCrayon.Visibility = Visibility.Collapsed;

        }
        /// <summary>
        /// Ferme les options d'édition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrayonEditC_Click(object sender, RoutedEventArgs e)
        {
            NomPersonnageTextBox.Visibility = Visibility.Collapsed;
            ValiderButton.Visibility = Visibility.Collapsed;
            BoutonSuppression.Visibility = Visibility.Collapsed;
            boutonCrayon.Visibility = Visibility.Visible;
            boutonCrayon2.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Mets à jour les informations modifiées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ValiderButton_Click(object sender, RoutedEventArgs e)
        {
            string nouvNomPersonnage = NomPersonnageTextBox.Text;

            if (string.IsNullOrWhiteSpace(nouvNomPersonnage))
            {
                MessageBox.Show("Le nom de la campagne ne peut pas être vide.", "Erreur de Nom de Campagne", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int selectedIndex = PersonnagesListe.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < personnages.Count)
            {

                List<Personnage> personnagesClone = personnages.ToList();

                personnagesClone[selectedIndex] = new Personnage
                {
                    NomPersonnage = nouvNomPersonnage,
                    NomUtilisateur = personnages[selectedIndex].NomUtilisateur,
                    DateCreation = personnages[selectedIndex].DateCreation
                };

                PersonnagesListe.Items[selectedIndex] = nouvNomPersonnage;

                personnages = personnagesClone;

                NomPersonnageTextBlock.Text = nouvNomPersonnage;

                string filePath = "personnages.txt";
                File.WriteAllLines(filePath, personnages.Select(p => $"{p.NomUtilisateur}, {p.NomPersonnage}"));
            }
            NomPersonnageTextBox.Text = "";

            NomPersonnageTextBox.Visibility = Visibility.Collapsed;
            ValiderButton.Visibility = Visibility.Collapsed;
            BoutonSuppression.Visibility = Visibility.Collapsed;
            boutonCrayon.Visibility = Visibility.Visible;
            boutonCrayon2.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Force le shutdown de l'application quand AjouterPersonnage est la dernière fenêtre a être fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterPerso_Closed(object sender, EventArgs e)
        {
            ajouterPersoOpen = false;
            if (Application.Current.Windows.Count == 1)
            {
                Application.Current.Shutdown();
            }
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


    }
}
