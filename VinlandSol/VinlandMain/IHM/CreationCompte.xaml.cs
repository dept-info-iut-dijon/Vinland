﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VinlandMain.IHM;
using VinlandSol.BDD;
using VinlandSol.IHM;
using VinlandSol.Métier;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour CreationCompte.xaml
    /// </summary>
    public partial class CreationCompte : Window
    {
        private FakeDAO fakeDAO = FakeDAO.Instance;
        private bool isPasswordVisible = false;

        public CreationCompte()
        {
            InitializeComponent();
            UpdatePasswordVisibility();
        }

        /// <summary>
        /// Ouvre la fenêtre CreationCompte et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnuleCreaCompte_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pagecreation = new MainWindow();
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            pagecreation.Show();
            Creation_Compte.Close();
        }

        /// <summary>
        /// Ajoute le nouveau compte à la liste des comptes et ouvre la fenêtre CreationCompte et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Creer_Click(object sender, RoutedEventArgs e)
        {
            string username = TBNomUtilisateur.Text;
            string password;
            if(!isPasswordVisible) { password = TBMdp.Password; } else {  password = TBVisibleMdp.Text; }
            string? roleSelectionne = ((ComboBoxItem)ChoixRole.SelectedItem)?.Content.ToString();
            bool okCheck = true;

            #region Checks

            string? messageCheckFail = null;
            // Un seul check à la fois, on ne veut pas aggresser l'utilisateur avec des popups en chaine
            if (fakeDAO.UsernameTaken(username) == false) { messageCheckFail = "Nom d'utilisateur déjà pris"; okCheck=false; }
            else if (string.IsNullOrWhiteSpace(username)) { messageCheckFail = "Veuillez remplir le champ Nom d'Utilisateur"; okCheck = false; }
            else if (string.IsNullOrWhiteSpace(password)) { messageCheckFail = "Veuillez remplir le champ Mot de Passe"; okCheck = false; }
            else if (roleSelectionne == null) { messageCheckFail = "Veuillez sélectionner un rôle pour votre compte"; okCheck = false; }

            if (!okCheck) 
            {
                CustomMessageBox messagebox = new CustomMessageBox(messageCheckFail);
                messagebox.ShowDialog();
            } 

            #endregion

            if(okCheck) // Si tout va bien
            {
                if (roleSelectionne == "Maitre du jeu") { fakeDAO.CreateMJ(username, password); }
                else if (roleSelectionne == "Joueur") { fakeDAO.CreateJoueur(username, password); }
                MainWindow pagecreation = new MainWindow();
                pagecreation.Left = this.Left;
                pagecreation.Top = this.Top;
                pagecreation.Show();
                Creation_Compte.Close();
            }
        }

        /// <summary>
        /// Cache ou Révèle le mot de passe en fonction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MdpVisibilityChanged(object sender, RoutedEventArgs e)
        {
            isPasswordVisible = !isPasswordVisible; // On inverse

            UpdatePasswordVisibility();
        }

        /// <summary>
        /// Met à jour la visibilité du mot de passe en fonction de isPasswordVisible
        /// </summary>
        private void UpdatePasswordVisibility()
        {
            if (isPasswordVisible)
            {
                TBVisibleMdp.Text = TBMdp.Password;
                TBMdp.Visibility = Visibility.Collapsed;
                TBVisibleMdp.Visibility = Visibility.Visible;
                Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeilbarre.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                TBMdp.Password = TBVisibleMdp.Text;
                TBVisibleMdp.Visibility = Visibility.Collapsed;
                TBMdp.Visibility = Visibility.Visible;
                Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeil.png", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
