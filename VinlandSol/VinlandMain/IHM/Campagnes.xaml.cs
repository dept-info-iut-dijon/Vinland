using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VinlandSol.IHM;
using VinlandSol;
using System.Windows.Controls;
using System.Globalization;

namespace VinlandMain.IHM
{
    /// <summary>
    /// Logique d'interaction pour VisionCampagne.xaml
    /// </summary>
    public partial class Campagnes : Window
    {
        // Créez une nouvelle campagne avec des valeurs par défaut
        Campagne nouvelleCampagne = new Campagne
        {
            Nom = "Nouvelle Campagne par défaut",
            DateCreation = DateTime.Now,
            DateModification = DateTime.Now,
            NombreCartes = 0,
            NombrePersonnages = 0
        };
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
        public Campagnes()
        {
            InitializeComponent();
            LoadCampagnesDepuisFichier("campagnes.txt");
            CampagnesListe.SelectionChanged += CampagnesListe_SelectionChanged;
        }
        //Le fichier se save dans -> VinlandSol\VinlandMain\bin\Debug\net6.0-windows
        private void SaveCampagnesTxt(string filePath)
        {
            try
            {
                System.IO.File.WriteAllLines(filePath, campagnes.Select(c => c.Nom));
                MessageBox.Show("Les noms de campagne ont été enregistrés dans le fichier texte avec succès.", "Enregistrement Réussi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de l'enregistrement dans le fichier texte : " + ex.Message, "Erreur d'Enregistrement", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
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
        private void LoadCampagnesDepuisFichier(string filePath)
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
                    NombrePersonnages = 0
                });
            }
            CampagnesListe.ItemsSource = campagnes.Select(c => c.Nom).ToList();
        }
        private void CampagnesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = CampagnesListe.SelectedIndex;
            AfficherInformationsCampagne(selectedIndex);
        }
        private void AfficherInformationsCampagne(int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < campagnes.Count)
            {
                Campagne selectedCampagne = campagnes[selectedIndex];
                NomCampTextBlock.Text = selectedCampagne.Nom;
                DateCreationTextBlock.Text = selectedCampagne.DateCreation.ToString("dd/MM/yyyy HH:mm:ss");
                DateModificationTextBlock.Text = selectedCampagne.DateModification.ToString("dd/MM/yyyy HH:mm:ss");
                NombreCartesTextBlock.Text = selectedCampagne.NombreCartes.ToString();
                NombrePersonnagesTextBlock.Text = selectedCampagne.NombrePersonnages.ToString();
            }
        }
        private void NouvelleCampagne_Click(object sender, RoutedEventArgs e)
        {
            NomNouvCamp.Visibility = Visibility.Visible;
            Valider.Visibility = Visibility.Visible;
        }
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            string newCampaignName = NomNouvCamp.Text;
            if (!campagnes.Any(c => c.Nom == newCampaignName))            {
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
        }        
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = CampagnesListe.SelectedIndex;
            RejoidComp.Visibility = Visibility.Collapsed;
            RejoidCompS.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Collapsed;
            EditS.Visibility = Visibility.Visible;
            Sauv.Visibility = Visibility.Visible;

            if (selectedIndex >= 0 && selectedIndex < campagnes.Count)
            {
                Campagne selectedCampagne = campagnes[selectedIndex];
                NomCampTextBox.Text = selectedCampagne.Nom;
                NomCampTextBox.Visibility = Visibility.Visible;
                NomCampTextBlock.Visibility = Visibility.Collapsed;
                indiceCampagneEnEdition = selectedIndex;
            }
        }
        private void EditS_Click(object sender, RoutedEventArgs e)
        {
            RejoidComp.Visibility = Visibility.Visible;
            RejoidCompS.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Collapsed;
            Sauv.Visibility = Visibility.Collapsed;
            NomCampTextBox.Visibility = Visibility.Collapsed;
            NomCampTextBlock.Visibility = Visibility.Visible;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (indiceCampagneEnEdition >= 0 && indiceCampagneEnEdition < campagnes.Count)
            {
                string newCampaignName = NomCampTextBox.Text;

                campagnes[indiceCampagneEnEdition] = new Campagne
                {
                    Nom = newCampaignName,
                    DateCreation = campagnes[indiceCampagneEnEdition].DateCreation,
                    DateModification = DateTime.Now,
                    NombreCartes = campagnes[indiceCampagneEnEdition].NombreCartes,
                    NombrePersonnages = campagnes[indiceCampagneEnEdition].NombrePersonnages
                };
                CampagnesListe.ItemsSource = campagnes.Select(c => c.Nom).ToList();
                string fileName = "campagnes_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                SaveCampagnesTxt("campagnes.txt");
            }
            indiceCampagneEnEdition = -1;
            NomCampTextBox.Visibility = Visibility.Collapsed;
            NomCampTextBlock.Visibility = Visibility.Visible;            
            Valider.Visibility = Visibility.Collapsed;
            RejoidComp.Visibility = Visibility.Visible;
            RejoidCompS.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Collapsed;
            Sauv.Visibility = Visibility.Collapsed;
        }
        private void OuvrirPersonnages_Click(object sender, RoutedEventArgs e)
        {
            Personnages pagecreation = new Personnages();
            pagecreation.Show();
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            CampagnesWindow.Close();
        }
    }
}
