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
                Nom = "Campagne Future",
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
            // Affiche la TextBox pour entrer le nom de la nouvelle campagne et cache le TextBlock
            NomNouvCamp.Visibility = Visibility.Visible;

            RejoidComp.Visibility = Visibility.Collapsed;
            RejoidCompS.Visibility = Visibility.Visible;

            Edit.Visibility = Visibility.Collapsed;
            EditS.Visibility = Visibility.Visible;

            Sauv.Visibility = Visibility.Visible;
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
                NombreCartesTextBox.Text = selectedCampagne.NombreCartes.ToString();
                NombrePersonnagesTextBox.Text = selectedCampagne.NombrePersonnages.ToString();

                NomCampTextBox.Visibility = Visibility.Collapsed;
                NombreCartesTextBox.Visibility = Visibility.Collapsed;
                NombrePersonnagesTextBox.Visibility = Visibility.Collapsed;
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
            NombreCartesTextBox.Visibility = Visibility.Collapsed;
            NombrePersonnagesTextBox.Visibility = Visibility.Collapsed;
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
                DateCreationTextBlock.Text = selectedCampagne.DateCreation.ToString("dd/MM/yyyy");
                DateModificationTextBlock.Text = selectedCampagne.DateModification.ToString("dd/MM/yyyy");
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


