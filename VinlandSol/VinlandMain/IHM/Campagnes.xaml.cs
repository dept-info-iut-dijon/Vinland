using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VinlandSol.IHM;

namespace VinlandMain.IHM
{
    /// <summary>
    /// Logique d'interaction pour VisionCampagne.xaml
    /// </summary>
    public partial class Campagnes : Window
    {
        /// <summary>
        /// Campagne de test
        /// </summary>
        Campagne nouvelleCampagne = new Campagne
        {
            Nom = "test",
            DateCreation = DateTime.Now,
            DateModification = DateTime.Now,
            NombreCartes = 0,
            NombrePersonnages = 0
        };

        /// <summary>
        /// Structure d'une campagne
        /// </summary>
        public struct Campagne
        {
            public string Nom { get; set; }
            public DateTime DateCreation { get; set; }
            public DateTime DateModification { get; set; }
            public int NombreCartes { get; set; }
            public int NombrePersonnages { get; set; }
        }
        List<Campagne> campagnes = new List<Campagne>();
        private int indiceCampagneEnEdition = -1;

        /// <summary>
        /// Constructeur de la fenêtre
        /// </summary>
        public Campagnes()
        {
            InitializeComponent();
            LoadCampagnes("campagnes.txt");
            CampagnesListe.SelectionChanged += CampagnesListe_SelectionChanged;
        }
        //Le fichier se save dans -> VinlandSol\VinlandMain\bin\Debug\net6.0-windows

        /// <summary>
        /// Sauvegarde les campagnes
        /// </summary>
        /// <param name="filePath"></param>
        private void SaveCampagnesTxt(string filePath)
        {
            try
            {
                System.IO.File.WriteAllLines(filePath, campagnes.Select(c => c.Nom));

                MessageBox.Show("La sauvegarde s'est réalisée avec succès.", "Enregistrement Réussi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de l'enregistrement dans le fichier texte : " + ex.Message, "Erreur d'Enregistrement", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Récupère les campagnes depuis le fichier campagnes.txt
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Une liste des campagnes</returns>
        private List<string> LoadCampagnesTxt(string filePath)
        {
            try
            {
                List<string> campagneNoms = System.IO.File.ReadAllLines(filePath).ToList();
                return campagneNoms;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la lecture du fichier texte : " + ex.Message, "Erreur de Lecture", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<string>();
            }
        }

        /// <summary>
        /// Crée les campagnes par rapport aux données récupérées par LoadCampagnesTxt
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadCampagnes(string filePath)
        {
            List<string> nomsDeCampagnes = LoadCampagnesTxt(filePath);
            campagnes.Clear();
            foreach (string nom in nomsDeCampagnes)
            {
                campagnes.Add(new Campagne
                {
                    Nom = nom,
                    DateCreation = DateTime.Now,
                    DateModification = DateTime.Now,
                    NombreCartes = 0,
                    NombrePersonnages = 0,
                });
            }
            CampagnesListe.ItemsSource = campagnes.Select(c => c.Nom).ToList();
        }

        /// <summary>
        /// Affiche les informations sur la droite de l'écran par rapport à la campagne sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CampagnesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AfficherInfos(CampagnesListe.SelectedIndex);
        }

        /// <summary>
        /// Permet d'actualiser les informations de la campagne selectionnée/modifiée
        /// </summary>
        /// <param name="index"></param>
        private void AfficherInfos(int index)
        {
            if (index >= 0 && index < campagnes.Count)
            {
                Campagne selectedCampagne = campagnes[index];
                NomCampTextBlock.Text = selectedCampagne.Nom;
                DateCreationTextBlock.Text = selectedCampagne.DateCreation.ToString("dd/MM/yyyy HH:mm:ss");
                DateModificationTextBlock.Text = selectedCampagne.DateModification.ToString("dd/MM/yyyy HH:mm:ss");
                NombreCartesTextBlock.Text = selectedCampagne.NombreCartes.ToString();
                NombrePersonnagesTextBlock.Text = selectedCampagne.NombrePersonnages.ToString();
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
        /// Ajoute la nouvelle campagne dans le .txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            string contenu = NomNouvCamp.Text;
            using (StreamWriter writer = File.AppendText("campagnes.txt"))
            {
                writer.Write(contenu + ",");
            }
            NomCampTextBlock.Text += contenu + Environment.NewLine;

            if (string.IsNullOrWhiteSpace(contenu))
            {
                MessageBox.Show("Le nom de la campagne ne peut pas être vide.", "Erreur de Nom de Campagne", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string newCampaignName = NomNouvCamp.Text;
            if (!campagnes.Any(c => c.Nom == newCampaignName))
            {
                nouvelleCampagne.Nom = newCampaignName;

                NomCampTextBlock.Text = newCampaignName;
                campagnes.Add(nouvelleCampagne);
                CampagnesListe.ItemsSource = campagnes.Select(c => c.Nom).ToList();
                CampagnesListe.SelectedItem = newCampaignName;
                NomNouvCamp.Text = "";
                string fileName = "campagnes_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                SaveCampagnesTxt("campagnes.txt");
            }
            else
            {
                MessageBox.Show("Le nom de la campagne existe déjà.", "Erreur de Nom de Campagne", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            RejoidCompS.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Collapsed;
            EditS.Visibility = Visibility.Visible;
            Sauv.Visibility = Visibility.Visible;
            SupprimerCamp.Visibility = Visibility.Visible;
            NomCampTextBox.Visibility = Visibility.Visible;
            NomCampTextBlock.Visibility = Visibility.Collapsed;

            if (selectedIndex >= 0 && selectedIndex < campagnes.Count)
            {
                Campagne selectedCampagne = campagnes[selectedIndex];
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
            if (indiceCampagneEnEdition >= 0 && indiceCampagneEnEdition < campagnes.Count)
            {
                string newCampaignName = NomCampTextBox.Text;

                if (string.IsNullOrWhiteSpace(newCampaignName))
                {
                    MessageBox.Show("Le nom de la campagne ne peut pas être vide.", "Erreur de Nom de Campagne", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                campagnes[indiceCampagneEnEdition] = new Campagne
                {
                    Nom = newCampaignName,
                    DateCreation = campagnes[indiceCampagneEnEdition].DateCreation,
                    DateModification = DateTime.Now
                };
                CampagnesListe.ItemsSource = campagnes.Select(c => c.Nom).ToList();
                string fileName = "campagnes_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                SaveCampagnesTxt("campagnes.txt");
            }
            AfficherInfos(indiceCampagneEnEdition);
            indiceCampagneEnEdition = -1;
            MasquerElements();
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
            int selection = CampagnesListe.SelectedIndex;

            if (selection >= 0 && selection < campagnes.Count)
            {
                campagnes.RemoveAt(selection);

                CampagnesListe.ItemsSource = campagnes.Select(c => c.Nom).ToList();

                SaveCampagnesTxt("campagnes.txt");

                // Champs qui se rénitialisent 
                NomCampTextBlock.Text = "";
                DateCreationTextBlock.Text = "";
                DateModificationTextBlock.Text = "";
            }
            MasquerElements();
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
    }
}
