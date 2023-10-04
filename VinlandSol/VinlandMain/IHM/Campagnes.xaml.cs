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
        public Campagnes()
        {
            InitializeComponent();

            // Charger les noms des campagnes depuis le fichier texte existant (s'il y en a un)
            LoadCampagnesDepuisFichier("campagnes.txt");

            // Attachez le gestionnaire d'événement SelectionChanged à la ListBox
            CampagnesListe.SelectionChanged += CampagnesListe_SelectionChanged;

            // Attachez le gestionnaire d'événement SelectionChanged à la ListBox
            CampagnesListe.SelectionChanged += CampagnesListe_SelectionChanged;
        }
        private void NouvelleCampagne_Click(object sender, RoutedEventArgs e)
        {
            // Affiche les TextBox et cache les TextBlock
            NomNouvCamp.Visibility = Visibility.Visible;
            Valider.Visibility = Visibility.Visible;
        }
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            // Récupérez le nom de la nouvelle campagne depuis le TextBox
            string newCampaignName = NomNouvCamp.Text;
            // Vérifiez si la nouvelle campagne existe déjà dans la liste
            if (!campagnes.Any(c => c.Nom == newCampaignName))
            {

                // Mettez à jour le nom de la campagne par défaut avec le nouveau nom
                nouvelleCampagne.Nom = newCampaignName;

                // Affichez le nom de la campagne mise à jour dans le TextBlock NomCampTextBlock
                NomCampTextBlock.Text = newCampaignName;

                // Ajoutez la nouvelle campagne à votre liste de campagnes
                campagnes.Add(nouvelleCampagne);

                // Mettez à jour la ListBox en réaffectant sa source de données
                CampagnesListe.ItemsSource = campagnes.Select(c => c.Nom).ToList();

                // Sélectionnez la nouvelle campagne dans la ListBox (facultatif)
                CampagnesListe.SelectedItem = newCampaignName;

                // Générez un nom de fichier unique basé sur un timestamp
                string fileName = "campagnes_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

                // Enregistrez la liste mise à jour dans un fichier texte
                SaveCampagnesTxt("campagnes.txt");
            }
            else
            {
                // Affichez un message d'erreur si le nom de la campagne existe déjà
                MessageBox.Show("Le nom de la campagne existe déjà.", "Erreur de Nom de Campagne", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Le fichier se save dans -> VinlandSol\VinlandMain\bin\Debug\net6.0-windows
        private void SaveCampagnesTxt(string filePath)
        {
            try
            {
                // Créez ou écrasez le fichier texte avec la liste des noms de campagnes
                System.IO.File.WriteAllLines(filePath, campagnes.Select(c => c.Nom));

                // Affichez un message de succès ou effectuez d'autres actions si nécessaires
                MessageBox.Show("Les noms de campagne ont été enregistrés dans le fichier texte avec succès.", "Enregistrement Réussi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, affichez un message d'erreur ou gérez l'exception de manière appropriée
                MessageBox.Show("Une erreur s'est produite lors de l'enregistrement dans le fichier texte : " + ex.Message, "Erreur d'Enregistrement", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private List<string> LoadCampagnesTxt(string filePath)
        {
            try
            {
                // Lisez toutes les lignes du fichier texte et retournez-les en tant que liste de noms de campagnes
                List<string> campagneNoms = System.IO.File.ReadAllLines(filePath).ToList();
                return campagneNoms;
            }
            catch (Exception ex)
            {
                // En cas d'erreur, affichez un message d'erreur ou gérez l'exception de manière appropriée
                MessageBox.Show("Une erreur s'est produite lors de la lecture du fichier texte : " + ex.Message, "Erreur de Lecture", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<string>(); // Retournez une liste vide en cas d'erreur
            }
        }

        // Utilisation pour charger les noms de campagnes depuis un fichier texte
        private void LoadCampagnesDepuisFichier(string filePath)
        {
            List<string> nomsDeCampagnes = LoadCampagnesTxt(filePath);

            // Mettez à jour la liste des campagnes avec les noms chargés
            campagnes.Clear();
            foreach (string nom in nomsDeCampagnes)
            {
                campagnes.Add(new Campagne
                {
                    Nom = nom,
                    DateCreation = DateTime.Now,
                    DateModification = DateTime.Now,
                    NombreCartes = 0, // Valeur par défaut, vous pouvez la modifier
                    NombrePersonnages = 0 // Valeur par défaut, vous pouvez la modifier
                });
            }
            // Mettez à jour la ListBox avec les noms des campagnes
            CampagnesListe.ItemsSource = campagnes.Select(c => c.Nom).ToList();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // Récupérez la campagne sélectionnée dans la ListBox
            int selectedIndex = CampagnesListe.SelectedIndex;

            RejoidComp.Visibility = Visibility.Collapsed;
            RejoidCompS.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Collapsed;
            EditS.Visibility = Visibility.Visible;
            Sauv.Visibility = Visibility.Visible;

            // Vérifiez si une campagne est sélectionnée
            if (selectedIndex >= 0 && selectedIndex < campagnes.Count)
            {
                // Affichez la campagne sélectionnée dans les TextBox et permettez la modification
                Campagne selectedCampagne = campagnes[selectedIndex];

                NomCampTextBox.Text = selectedCampagne.Nom;

                NomCampTextBox.Visibility = Visibility.Visible;
                NomCampTextBlock.Visibility = Visibility.Collapsed;

                // Assurez-vous de mémoriser l'indice de la campagne en cours d'édition
                indiceCampagneEnEdition = selectedIndex;
            }
        }
        private int indiceCampagneEnEdition = -1; // Variable pour mémoriser l'indice de la campagne en cours d'édition
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
        private void CampagnesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtenez l'index de l'élément sélectionné dans la ListBox
            int selectedIndex = CampagnesListe.SelectedIndex;

            // Appelez la fonction pour mettre à jour les TextBlock avec les informations de la campagne sélectionnée
            AfficherInformationsCampagne(selectedIndex);
        }
        private void AfficherInformationsCampagne(int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < campagnes.Count)
            {
                Campagne selectedCampagne = campagnes[selectedIndex];

                // Mettez à jour les TextBlock avec les informations de la campagne
                NomCampTextBlock.Text = selectedCampagne.Nom;
                DateCreationTextBlock.Text = selectedCampagne.DateCreation.ToString("dd/MM/yyyy HH:mm:ss");
                DateModificationTextBlock.Text = selectedCampagne.DateModification.ToString("dd/MM/yyyy HH:mm:ss");
                NombreCartesTextBlock.Text = selectedCampagne.NombreCartes.ToString();
                NombrePersonnagesTextBlock.Text = selectedCampagne.NombrePersonnages.ToString();
            }
        }
        private void OuvrirPersonnages_Click(object sender, RoutedEventArgs e)
        {
            Personnages pagecreation = new Personnages();
            pagecreation.Show();
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            CampagnesWindow.Close();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (indiceCampagneEnEdition >= 0 && indiceCampagneEnEdition < campagnes.Count)
            {
                // Récupérez le nouveau nom de la campagne depuis le TextBox
                string newCampaignName = NomCampTextBox.Text;

                // Mettez à jour le nom de la campagne dans la liste
                campagnes[indiceCampagneEnEdition] = new Campagne
                {
                    Nom = newCampaignName,
                    DateCreation = campagnes[indiceCampagneEnEdition].DateCreation,
                    DateModification = DateTime.Now, // Vous pouvez mettre à jour la date de modification ici
                    NombreCartes = campagnes[indiceCampagneEnEdition].NombreCartes,
                    NombrePersonnages = campagnes[indiceCampagneEnEdition].NombrePersonnages
                };

                // Mettez à jour la ListBox avec les noms des campagnes
                CampagnesListe.ItemsSource = campagnes.Select(c => c.Nom).ToList();

                // Générez un nom de fichier unique basé sur un timestamp
                string fileName = "campagnes_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

                // Enregistrez la liste mise à jour dans un fichier texte
                SaveCampagnesTxt("campagnes.txt");
            }

            // Réinitialisez la variable d'indice de campagne en édition
            indiceCampagneEnEdition = -1;

            // Cachez le TextBox et affichez le TextBlock
            NomCampTextBox.Visibility = Visibility.Collapsed;
            NomCampTextBlock.Visibility = Visibility.Visible;

            // Cachez le bouton "Valider"
            Valider.Visibility = Visibility.Collapsed;

            // Affichez les boutons "Éditer" et "Sauvegarder"
            Edit.Visibility = Visibility.Visible;
            EditS.Visibility = Visibility.Collapsed;
            Sauv.Visibility = Visibility.Collapsed;
        }
    }
}


