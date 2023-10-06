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
        private Personnages perso;
        public AjouterPersonnage(Personnages perso)
        {
            InitializeComponent();
            btnAjouter.Click += FermerPopupActu_Click;
            this.perso = perso;
        }

        /// <summary>
        /// Ajoute un personnage à la liste des personnages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Récupération du contenu des TextBox
            string idJoueur = IdJoueur.Text;
            string idPersonnage = IdPersonnage.Text;
            DateTime dateCreation = DateTime.Now;


            
            if (!string.IsNullOrEmpty(idJoueur) && !string.IsNullOrEmpty(idPersonnage))
            {
                
                string filePath = "personnages.txt"; // Cela créé un fichier si il n'y en a pas

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine($"{idJoueur}, {idPersonnage}");
                }
                IdJoueur.Clear();
                IdPersonnage.Clear();

                // Affichez un message de confirmation
                MessageBox.Show("Personnage ajouté avec succès !");                
            }
            else
            {
                MessageBox.Show("Veuillez remplir toutes les informations avant de créer un personnage");
            }

        }

        /// <summary>
        /// Ferme la fenêtre actuelle et actualise la liste des persos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FermerPopupActu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            perso.LoadPersonnages();
        }
    }
}
