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

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour AjouterPersonnage.xaml
    /// </summary>
    public partial class AjouterPersonnage : Window
    {
        public AjouterPersonnage()
        {
            InitializeComponent();
            btnAjouter.Click += BtnAjouter_Click;
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Récupérez le contenu des TextBox
            string idJoueur = IdJoueur.Text;
            string idPersonnage = IdPersonnage.Text;
            DateTime dateCreation = DateTime.Now;


            // Vérifiez que les données ne sont pas vides
            if (!string.IsNullOrEmpty(idJoueur) && !string.IsNullOrEmpty(idPersonnage))
            {
                // Créez ou ouvrez un fichier .txt pour sauvegarder les données
                string filePath = "personnages.txt";

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    // Écrivez les données dans le fichier
                    sw.WriteLine($"{idJoueur}, {idPersonnage}");
                }
                // Effacez les TextBox après sauvegarde
                IdJoueur.Clear();
                IdPersonnage.Clear();

                // Affichez un message de confirmation
                MessageBox.Show("Personnage ajouté avec succès !");                
            }
            else
            {
                // Affichez un message d'erreur si les données sont vides
                MessageBox.Show("Veuillez remplir toutes les informations avant de créer un personnage");
            }
        }
    }
}
