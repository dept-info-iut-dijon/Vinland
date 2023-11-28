using System.Collections.Generic;
using System.Windows;
using VinlandSol.BDD;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour CreationCarte.xaml
    /// </summary>
    public partial class CreationCarte : Window
    {
        private FakeDAO fakeDAO = FakeDAO.Instance;
        private Cartes cartes;
        private int idCampagne;
        public CreationCarte(Cartes cartes, int idCampagne)
        {
            InitializeComponent();
            btnCreer.Click += FermerPopupActu_Click;
            this.cartes = cartes;
            this.idCampagne = idCampagne;
        }

        /// <summary>
        /// Ajoute une carte à la liste des cartes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreationCarte_Click(object sender, RoutedEventArgs e)
        {
            string nomCarte = NameTextBox.Text;
            string nLigne = NLigne.Text;
            string nColonne = NColonne.Text;
            List<Métier.Carte> cartes = fakeDAO.GetCurrentCartes(idCampagne);
            bool okCheck = true;

            #region Checks 

            string? messageCheckFail = null;

            foreach (Métier.Carte carte in cartes)
            {
                if (carte.Nom == nomCarte)
                {
                    messageCheckFail = "Une carte du même nom existe déjà dans cette campagne";
                    okCheck = false;
                }
            }

            if (string.IsNullOrWhiteSpace(nomCarte)) { messageCheckFail = "Le nom de la carte ne peut pas être vide"; okCheck = false; }

            if (!okCheck)
            {
                CustomMessageBox messagebox = new CustomMessageBox(messageCheckFail);
                messagebox.ShowDialog();
            }

            #endregion

            if (okCheck) // Si tout va bien
            {
                fakeDAO.CreateCarte(nomCarte, int.Parse(nLigne), int.Parse(nColonne), idCampagne);
                CustomMessageBox messagebox = new CustomMessageBox("Carte ajoutée avec succès !");
                messagebox.ShowDialog();
                this.Close();
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
            cartes.MettreAJourListBox();
        }
    }
}
