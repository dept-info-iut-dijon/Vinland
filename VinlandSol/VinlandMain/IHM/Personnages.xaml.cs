using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VinlandMain.IHM;
using static VinlandMain.IHM.Campagnes;

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
        public struct Personnage
        {
            public string NomPersonnage { get; set; }
            public string NomUtilisateur { get; set; }
            public DateTime DateCreation { get; set; }
        }
        private AjouterPersonnage? pageajouterPerso;
        private bool ajouterPersoOpen = false;

        public Personnages()
        {
            InitializeComponent();
            Closed += ShutdownEnForce; // ShutdownEnForce est appelé à la fermeture de cette fenêtre
            // Appelez la méthode pour charger les personnages depuis le fichier
            LoadPersonnages();
        }
        private void LoadPersonnages()
        {
            // Chemin du fichier
            string filePath = "personnages.txt";

            // Assurez-vous que le fichier existe
            if (File.Exists(filePath))
            {
                // Lire toutes les lignes du fichier
                string[] lignes = File.ReadAllLines(filePath);

                // Parcourir chaque ligne pour extraire les donnÃ©es
                foreach (string ligne in lignes)
                {
                    // Diviser la ligne en ID du joueur et Nom du personnage
                    string[] elements = ligne.Split(',');

                    if (elements.Length == 2)
                    {
                        string idJoueur = elements[0].Trim();
                        string idPersonnage = elements[1].Trim();

                        // Ajouter les données à la ListBox
                        PersonnagesListe.Items.Add($"{idPersonnage}");
                        personnages.Add(new Personnage
                        {
                            NomUtilisateur = idJoueur,
                            NomPersonnage = idPersonnage,
                            DateCreation = DateTime.Now // Vous pouvez mettre à jour la date de création ici si nécessaire
                        });
                    }
                }
            }
        }
        private void PersonnagesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = PersonnagesListe.SelectedIndex;
            AfficherInformationsPersonnage(selectedIndex);
        }
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
                pageajouterPerso = new AjouterPersonnage();
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


    }
}
