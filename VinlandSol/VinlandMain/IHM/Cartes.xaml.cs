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
        private IUser user;
        private Campagne campagne;

        Cartee nouvellecarte = new Cartee
        {
            NomCartes = "",
            DateCreation = DateTime.Now,
            DateModification = DateTime.Now,
            NbLignes = 1,
            NbColonnes = 1
        };
        List<Cartee> cartes = new List<Cartee>();
        public struct Cartee
        {
            public string NomCartes { get; set; }
            public DateTime DateCreation { get; set; }
            public DateTime DateModification { get; set; }
            public int NbLignes { get; set; }
            public int NbColonnes { get; set; }
        }
        private CreationCarte? pagecreationcarte;
        private bool creaCarteOpen = false;

        /// <summary>
        /// Constructeur de la page
        /// </summary>
        public Cartes(IUser user, int idcampagne)
        {
            InitializeComponent();
            this.user = user;
            this.campagne = fakeDAO.GetCampagne(idcampagne);
            Closed += ShutdownEnForce; // ShutdownEnForce est appelé à la fermeture de cette fenêtre
            LoadCartes();
        }

        /// <summary>
        /// Afficher les noms des cartes dans la liste
        /// </summary>
        public void LoadCartes()
        {

            string filePath = "cartes.txt";
            cartes.Clear();
            CartesListe.Items.Clear();

            if (File.Exists(filePath))
            {
                string[] lignes = File.ReadAllLines(filePath);

                foreach (string ligne in lignes)
                {
                    string[] elements = ligne.Split(',');

                    if (elements.Length == 5)
                    {
                        string NomCarte = elements[0].Trim();
                        int nLignes = int.Parse(elements[1].Trim());
                        int nColonnes = int.Parse(elements[2].Trim());

                        CartesListe.Items.Add($"{NomCarte}");
                        cartes.Add(new Cartee
                        {
                            NomCartes = NomCarte,
                            DateCreation = DateTime.Now,
                            DateModification = DateTime.Now,
                            NbLignes = nLignes,
                            NbColonnes = nColonnes
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Actualise les informations de la carte selectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Ouvre les options d'édition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            NomCarteTextBox.Visibility = Visibility.Visible;
            Sauv.Visibility = Visibility.Visible;
            Suppr.Visibility = Visibility.Visible;
            EditSBtn.Visibility = Visibility.Visible;
            EditBtn.Visibility = Visibility.Collapsed;

        }

        /// <summary>
        /// Ferme les options d'édition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditS_Click(object sender, RoutedEventArgs e)
        {
            NomCarteTextBox.Visibility = Visibility.Collapsed;
            Sauv.Visibility = Visibility.Collapsed;
            Suppr.Visibility = Visibility.Collapsed;
            EditSBtn.Visibility = Visibility.Collapsed;
            EditBtn.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Sauvegarde les changements de la carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sauv_Click(object sender, RoutedEventArgs e)
        {
            string nouveauNomCarte = NomCarteTextBox.Text;

            int selectedIndex = CartesListe.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < cartes.Count)
            {
                List<Cartee> cartesClone = cartes.ToList();

                cartesClone[selectedIndex] = new Cartee
                {
                    NomCartes = nouveauNomCarte,
                    DateCreation = cartes[selectedIndex].DateCreation,
                    DateModification = cartes[selectedIndex].DateModification,
                    NbLignes = cartes[selectedIndex].NbLignes,
                    NbColonnes = cartes[selectedIndex].NbColonnes
                };

                CartesListe.Items[selectedIndex] = nouveauNomCarte;

                cartes = cartesClone;

                NomCarteTextBlock.Text = nouveauNomCarte;

                string filePath = "cartes.txt";
                File.WriteAllLines(filePath, cartes.Select(p => $"{p.NomCartes}, {p.NbLignes}, {p.NbColonnes}, {p.DateCreation}, {p.DateModification}"));
            }
            NomCarteTextBox.Text = "";
            NomCarteTextBox.Visibility = Visibility.Collapsed;
            Sauv.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Supprime la carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerCarte(object sender, RoutedEventArgs e)
        {
            int selectedIndex = CartesListe.SelectedIndex;
            string filePath = "cartes.txt";

            if (selectedIndex >= 0 && selectedIndex < cartes.Count)
            {

                string nomCarteASupprimer = cartes[selectedIndex].NomCartes;

                CartesListe.Items.RemoveAt(selectedIndex);

                cartes.RemoveAt(selectedIndex);

                File.WriteAllLines(filePath, cartes.Select(p => $"{p.NomCartes}, {p.NbLignes}, {p.NbColonnes}, {p.DateCreation}, {p.DateModification}"));

                NomCarteTextBlock.Text = "";
                DateCreationTextBlock.Text = "";
                DateCreationTextBlock.Text = "";
            }
        }

        /// <summary>
        /// Change la visibilité de la carte aux joueurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OeilChange(object sender, RoutedEventArgs e)
        {
            Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeilbarre.png", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Ouvre le fenêtre Personnages et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirPersonnages_Click(object sender, RoutedEventArgs e)
        {
            Personnages pagecreationperso = new Personnages(user, campagne);
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
        /// Ouvre la fenêtre CreationCarte et limite cette action tant que cette dernière n'est pas fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCreationCarte_Click(object sender, RoutedEventArgs e)
        {
            if (creaCarteOpen == false)
            {
                pagecreationcarte = new CreationCarte(this);
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
            int idCarte = CartesListe.SelectedIndex;           
            Carte carteselect = new Carte(user, idCarte);
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
