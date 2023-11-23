using System;
using System.IO;
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
            // Récupération du contenu des TextBox
            string idJoueur = IdJoueur.Text;
            string personnageNom = PersonnageNom.Text;
            DateTime dateCreation = DateTime.Now;
            string? messageCheckFail = null;
            int comboCheckFail = 0;
            int idJoueurValide = -1;

            #region Checks

            if (string.IsNullOrWhiteSpace(personnageNom)) { messageCheckFail = "Le nom du personnage ne peut pas être vide"; comboCheckFail++; }
            if (string.IsNullOrWhiteSpace(idJoueur)) { messageCheckFail = "Le champ idJoueur ne peut pas être vide"; comboCheckFail++; }
            if (int.TryParse(idJoueur,out idJoueurValide) == false || idJoueurValide < 0 || idJoueurValide > fakeDAO.GetJoueurs().Count) { messageCheckFail = "L'ID de joueur renseigné n'est pas valide"; comboCheckFail++; }
            if (!fakeDAO.PersonnageTaken(personnageNom, idCampagne)) { messageCheckFail = "Ce nom de Personnage est déjà utilisé dans votre campagne"; comboCheckFail++; }

            if (comboCheckFail >= 2) messageCheckFail = "Les deux champs contiennent des valeurs non valides";
            if (comboCheckFail >= 1)
            {
                CustomMessageBox messagebox = new CustomMessageBox(messageCheckFail);
                messagebox.ShowDialog();
            }

            #endregion

            if (comboCheckFail == 0) // Si tout va bien
            {
                fakeDAO.CreatePersonnage(personnageNom, int.Parse(idJoueur), idCampagne);
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
