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

            campagnes.Add(new Campagne
            {
                Nom = "Campagne medieval",
                DateCreation = DateTime.Now,
                DateModification = DateTime.Now,
                NombreCartes = 10,
                NombrePersonnages = 5
            });
            campagnes.Add(new Campagne
            {
                Nom = "Campagne Futuriste",
                DateCreation = DateTime.Now,
                DateModification = DateTime.Now,
                NombreCartes = 5,
                NombrePersonnages = 15
            });
            // Mettez à jour la ListBox avec les noms des campagnes
            CampagnesListe.ItemsSource = campagnes.Select(c => c.Nom).ToList();
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
            CampagnesWindow.Close();
        }
    }
}


