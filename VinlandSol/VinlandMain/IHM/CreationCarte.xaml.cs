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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour CreationCarte.xaml
    /// </summary>
    public partial class CreationCarte : Window
    {
        private Cartes cartes;
        public CreationCarte(Cartes cartes)
        {
            InitializeComponent();
            btnCreer.Click += FermerPopupActu_Click;
            this.cartes = cartes;
        }

        private void CreationCarte_Click(object sender, RoutedEventArgs e)
        {
            // Récupérez le contenu des TextBox
            string NomCarte = NameTextBox.Text;
            string nLinge = NLigne.Text;
            string nColonne = NColonne.Text;
            DateTime dateCreation = DateTime.Now;
            DateTime dateModification = DateTime.Now;


            // Vérifiez que les données ne sont pas vides
            if (!string.IsNullOrEmpty(NomCarte) && !string.IsNullOrEmpty(nLinge) && !string.IsNullOrEmpty(nColonne))
            {
                // Créez ou ouvrez un fichier .txt pour sauvegarder les données
                string filePath = "cartes.txt";

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    // Écrivez les données dans le fichier
                    sw.WriteLine($"{NomCarte}, {nLinge}, {nColonne}, {dateCreation}, {dateModification}");
                }
                // Effacez les TextBox après sauvegarde
                NameTextBox.Clear();
                //NLigne.ClearValue();
                //NColonne.ClearValue();

                // Affichez un message de confirmation
                MessageBox.Show("Carte ajouté avec succès !");
            }
            else
            {
                // Affichez un message d'erreur si les données sont vides
                MessageBox.Show("Veuillez remplir toutes les informations avant de créer une carte");
            }

        }

        /// <summary>
        /// Ferme la fenêtre actuelle et actualise la liste des cartes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FermerPopupActu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            cartes.LoadCartes();
        }
    }
}
