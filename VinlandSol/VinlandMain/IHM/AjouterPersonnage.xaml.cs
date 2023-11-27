using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using VinlandSol.BDD;
using VinlandSol.Métier;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour AjouterPersonnage.xaml
    /// </summary>
    public partial class AjouterPersonnage : Window
    {
        private FakeDAO fakeDAO = FakeDAO.Instance;
        private Personnages perso;
        private int idCampagne;
        public AjouterPersonnage(Personnages perso,int idCampagne)
        {
            InitializeComponent();
            btnAjouter.Click += FermerPopupActu_Click;
            this.perso = perso;
            this.idCampagne = idCampagne;
        }

        /// <summary>
        /// Ajoute un personnage à la liste des personnages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            string nomJoueur = NomJoueur.Text;
            string personnageNom = PersonnageNom.Text;
            DateTime dateCreation = DateTime.Now;
            List<Joueur> joueurs = fakeDAO.GetJoueurs();
            int idJoueur = -1;
            bool okCheck = true;

            #region Checks

            string? messageCheckFail = null;

            foreach (Joueur joueur in joueurs)
            {
                if (joueur.Nom == nomJoueur) 
                { 
                    idJoueur = joueur.ID; 
                    if(joueur.IDPersonnages.Any(id => id == idCampagne))
                    {
                        messageCheckFail = "Ce joueur a déjà un personnage dans cette campagne";
                        okCheck = false;
                    }
                }
            }
            // Un seul check à la fois, on ne veut pas aggresser l'utilisateur avec des popups en chaine
            if (string.IsNullOrWhiteSpace(personnageNom)) { messageCheckFail = "Le nom du personnage ne peut pas être vide"; okCheck = false; }
            else if (string.IsNullOrWhiteSpace(nomJoueur)) { messageCheckFail = "Le champ nom joueur ne peut pas être vide"; okCheck = false; }
            else if (idJoueur == -1) { messageCheckFail = "Le joueur renseigné est introuvable"; okCheck = false; }
            else if (!fakeDAO.PersonnageTaken(personnageNom, idCampagne)) { messageCheckFail = "Ce nom de Personnage est déjà utilisé dans votre campagne"; okCheck = false; }

                if (!okCheck)
            {
                CustomMessageBox messagebox = new CustomMessageBox(messageCheckFail);
                messagebox.ShowDialog();
            }

            #endregion

            if (okCheck) // Si tout va bien
            {
                fakeDAO.CreatePersonnage(personnageNom, idJoueur, idCampagne);
                CustomMessageBox messagebox = new CustomMessageBox("Personnage ajouté avec succès !");
                messagebox.ShowDialog();
                this.Close();
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
            perso.MettreAJourListBox();
        }
    }
}
