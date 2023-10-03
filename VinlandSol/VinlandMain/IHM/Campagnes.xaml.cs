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
            NomCampTextBox.Visibility = Visibility.Hidden;

            // Exemple d'initialisation de la liste de campagnes
            campagnes.Add(new Campagne
            {
                Nom = "Campagne 1",
                DateCreation = DateTime.Now,
                DateModification = DateTime.Now,
                NombreCartes = 10,
                NombrePersonnages = 5
            });

            // Mettez à jour la ListBox avec les noms des campagnes
            CampagnesListe.ItemsSource = campagnes.Select(c => c.Nom).ToList();

        }
        private void NouvelleCampagne_Click(object sender, RoutedEventArgs e)
        {
            // Affiche la TextBox pour entrer le nom de la nouvelle campagne
            NomNouvCamp.Visibility = Visibility.Visible;

            NomCampTextBox.Visibility = Visibility.Visible;
            DateCreationTextBlock.Visibility = Visibility.Visible;
            DateModificationTextBlock.Visibility = Visibility.Visible;
            NombreCartesTextBlock.Visibility = Visibility.Visible;
            NombrePersonnagesTextBlock.Visibility = Visibility.Visible;

            NomCampTextBox.Text = "... ";
            DateCreationTextBlock.Text = "... ";
            DateModificationTextBlock.Text = "... ";
            NombreCartesTextBlock.Text = "... ";
            NombrePersonnagesTextBlock.Text = "... ";
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérez la campagne sélectionnée dans la ListBox
            int selectedIndex = CampagnesListe.SelectedIndex;

            // Vérifiez si une campagne est sélectionnée
            if (selectedIndex >= 0 && selectedIndex < campagnes.Count)
            {
                // Affichez la campagne sélectionnée dans les TextBox et permettez la modification
                Campagne selectedCampagne = campagnes[selectedIndex];
                NomCampTextBox.Text = selectedCampagne.Nom;
                DateCreationTextBlock.Text = selectedCampagne.DateCreation.ToString("dd/MM/yyyy"); ;
                DateModificationTextBlock.Text = selectedCampagne.DateModification.ToString("dd/MM/yyyy"); ;
                NombreCartesTextBlock.Text = selectedCampagne.NombreCartes.ToString();
                NombrePersonnagesTextBlock.Text = selectedCampagne.NombrePersonnages.ToString();

                NomCampTextBox.IsEnabled = true;
                DateCreationTextBlock.IsEnabled = true;
                DateModificationTextBlock.IsEnabled = true;
                NombreCartesTextBlock.IsEnabled = true;
                NombrePersonnagesTextBlock.IsEnabled = true;
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