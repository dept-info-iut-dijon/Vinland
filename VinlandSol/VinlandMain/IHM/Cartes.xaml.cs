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

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour Cartes.xaml
    /// </summary>
    public partial class Cartes : Window
    {
        Cartee nouvellecarte = new Cartee
        {
            NomCartes = "",
            DateCreation = DateTime.Now,
            DateModification = DateTime.Now
        };
        List<Cartee> cartes = new List<Cartee>();
        public struct Cartee
        {
            public string NomCartes { get; set; }
            public DateTime DateCreation { get; set; }
            public DateTime DateModification { get; set; }
        }
        private CreationCarte? pagecreationcarte;
        private bool creaCarteOpen = false;

        public Cartes()
        {
            InitializeComponent();
            Closed += ShutdownEnForce; // ShutdownEnForce est appelé à la fermeture de cette fenêtre
            LoadCartes();
        }

        public void LoadCartes()
        {
            // Chemin du fichier
            string filePath = "cartes.txt";

            // Assurez-vous que le fichier existe
            if (File.Exists(filePath))
            {
                // Lire toutes les lignes du fichier
                string[] lignes = File.ReadAllLines(filePath);

                // Parcourir chaque ligne pour extraire les donnÃ©es
                foreach (string ligne in lignes)
                {
                    // Divisez la ligne en ID du joueur et Nom du personnage
                    string[] elements = ligne.Split(',');

                    if (elements.Length == 5)
                    {
                        string NomCarte = elements[0].Trim();
                        string nLinge = elements[1].Trim();
                        string nColonne = elements[2].Trim();

                        // Ajoutez les donnÃ©es Ã  la ListBox
                        CartesListe.Items.Add($"{NomCarte}");
                        cartes.Add(new Cartee
                        {
                            NomCartes = NomCarte,
                            DateCreation = DateTime.Now,
                            DateModification = DateTime.Now
                        });
                    }
                }
            }
        }

        private void CartesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = CartesListe.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < cartes.Count)
            {
                string filePath = "cartes.txt";
                if (File.Exists(filePath))
                {
                    string[] lignes = File.ReadAllLines(filePath);
                    string[] elements = lignes[selectedIndex].Split(',');

                    if (elements.Length == 5)
                    {
                        string NomCarte = elements[0].Trim();
                        string nLinge = elements[1].Trim();
                        string nColonne = elements[2].Trim();
                        string dateCreationStr = elements[3].Trim();
                        string dateModificationStr = elements[4].Trim();

                        NomCarteTextBlock.Text = NomCarte;
                        DateCreationTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        DateModificationTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                }
            }
        }

        /// <summary>
        /// Ouvre le fenêtre Personnages et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirPersonnages_Click(object sender, RoutedEventArgs e)
        {
            Personnages pagecreationperso = new Personnages();
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

        private void OeilChange(object sender, RoutedEventArgs e)
        {
           Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeilbarre.png", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Ouvre la fenêtre CreationCarte et limite cette action tant que cette dernière n'est pas fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCreationCarte_Click(object sender, RoutedEventArgs e)
        {     
            if (creaCarteOpen == false)
            {
                pagecreationcarte = new CreationCarte();
                pagecreationcarte.Closed += CreationCarte_Closed;
                pagecreationcarte.Left = this.Left;
                pagecreationcarte.Top = this.Top;
                pagecreationcarte.Show();
                creaCarteOpen = true;
            }     
        }

        /// <summary>
        /// Ouvre le fenêtre Carte et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCarte_Click(object sender, RoutedEventArgs e)
        {
            Carte pagecreation = new Carte("test",15,15);
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            pagecreation.Show();

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
