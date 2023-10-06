﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VinlandMain.IHM;
using static VinlandMain.IHM.Campagnes;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour Personnages.xaml
    /// </summary>
    public partial class Personnages : Window
    {
        Personnage nouveauPersonnage = new Personnage
        {
            NomPersonnage = "",
            DateCreation = DateTime.Now,
            NomUtilisateur = ""
        };
        List<Personnage> personnages = new List<Personnage>();
        public struct Personnage
        {
            public string NomPersonnage { get; set; }
            public string NomUtilisateur { get; set; }
            public DateTime DateCreation { get; set; }
        }
        private AjouterPersonnage? pageajouterPerso;
        private bool ajouterPersoOpen = false;

        public Personnages()
        {
            InitializeComponent();
            Closed += ShutdownEnForce; 
            
            LoadPersonnages();

        }
        public void LoadPersonnages()
        {
           
            string filePath = "personnages.txt";

            
            if (File.Exists(filePath))
            {
               
                string[] lignes = File.ReadAllLines(filePath);

                
                foreach (string ligne in lignes)
                {
                    
                    string[] elements = ligne.Split(',');

                    if (elements.Length == 2)
                    {
                        string idJoueur = elements[0].Trim();
                        string idPersonnage = elements[1].Trim();

                        
                        PersonnagesListe.Items.Add($"{idPersonnage}");
                        personnages.Add(new Personnage
                        {
                            NomUtilisateur = idJoueur,
                            NomPersonnage = idPersonnage,
                            DateCreation = DateTime.Now 
                        });
                    }
                }
            }
        }
        private void PersonnagesListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = PersonnagesListe.SelectedIndex;
            AfficherInformationsPersonnage(selectedIndex);
        }
        private void AfficherInformationsPersonnage(int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < personnages.Count)
            {
                string filePath = "personnages.txt";
                if (File.Exists(filePath))
                {
                    string[] lignes = File.ReadAllLines(filePath);
                    string[] elements = lignes[selectedIndex].Split(',');

                    if (elements.Length == 2)
                    {
                        string idJoueur = elements[0].Trim();
                        string idPersonnage = elements[1].Trim();

                        NomUtilisateurTextBlock.Text = idJoueur;
                        NomPersonnageTextBlock.Text = idPersonnage;
                        DateCreationTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                }
            }
        }

        /// <summary>
        /// Ouvre la fenêtre Campagnes et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCampagnes_Click(object sender, RoutedEventArgs e)
        {
            Campagnes pagecreation = new Campagnes();
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            pagecreation.Show();

            PersonnagesWindow.Hide(); // Evite de voir la fenetre se fermer en retard
            var timer = new System.Timers.Timer(100); // Delai de 100 millisecondes 
            timer.Elapsed += (s, args) =>
            {
                timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    PersonnagesWindow.Close(); // On ferme cette fenêtre en retard pour éviter que le if de ShutdownEnForce ne passe.
                });
            };
            timer.Start();
        }

        /// <summary>
        /// Ouvre la fenêtre Cartes et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCartes_Click(object sender, RoutedEventArgs e)
        {
            Cartes pagecreation = new Cartes();
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            pagecreation.Show();

            PersonnagesWindow.Hide(); // Evite de voir la fenêtre se fermer en retard
            var timer = new System.Timers.Timer(100); // Délai de 100 millisecondes 
            timer.Elapsed += (s, args) =>
            {
                timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    PersonnagesWindow.Close(); // On ferme cette fenêtre en retard pour éviter que le if de ShutdownEnForce ne passe.
                });
            };
            timer.Start();
        }

        /// <summary>
        /// Ouvre la fenêtre AjouterPersonnage et limite cette action tant que cette dernière n'est pas fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirAjouterPersonnage_Click(object sender, RoutedEventArgs e)
        {
            if (ajouterPersoOpen == false)
            {
                pageajouterPerso = new AjouterPersonnage(this);
                pageajouterPerso.Closed += AjouterPerso_Closed;
                pageajouterPerso.Left = this.Left;
                pageajouterPerso.Top = this.Top;
                pageajouterPerso.Show();
                ajouterPersoOpen = true;
            }
        }

        private void SupprimerPersonnage(object sender, RoutedEventArgs e)
        {
            int selectedIndex = PersonnagesListe.SelectedIndex;
            string filePath = "personnages.txt";

            if (selectedIndex >= 0 && selectedIndex < personnages.Count)
            {

                string nomPersonnageASupprimer = personnages[selectedIndex].NomPersonnage;

                PersonnagesListe.Items.RemoveAt(selectedIndex);

                personnages.RemoveAt(selectedIndex);

                File.WriteAllLines(filePath, personnages.Select(p => $"{p.NomUtilisateur}, {p.NomPersonnage}"));

                NomUtilisateurTextBlock.Text = "";
                NomPersonnageTextBlock.Text = "";
                DateCreationTextBlock.Text = "";
            }
        }

        private void CrayonEdit_Click(object sender, RoutedEventArgs e)
        {
            NomPersonnageTextBox.Visibility = Visibility.Visible;
            ValiderButton.Visibility = Visibility.Visible;


        }

        public void ValiderButton_Click(object sender, RoutedEventArgs e)
        {
            string nouveauNomPersonnage = NomPersonnageTextBox.Text;

            int selectedIndex = PersonnagesListe.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < personnages.Count)
            {
                List<Personnage> personnagesClone = personnages.ToList();

                personnagesClone[selectedIndex] = new Personnage
                {
                    NomPersonnage = nouveauNomPersonnage,
                    NomUtilisateur = personnages[selectedIndex].NomUtilisateur,
                    DateCreation = personnages[selectedIndex].DateCreation
                };

                PersonnagesListe.Items[selectedIndex] = nouveauNomPersonnage;

                personnages = personnagesClone;

                NomPersonnageTextBlock.Text = nouveauNomPersonnage;

                string filePath = "personnages.txt";
                File.WriteAllLines(filePath, personnages.Select(p => $"{p.NomUtilisateur}, {p.NomPersonnage}"));
            }

            NomPersonnageTextBox.Visibility = Visibility.Collapsed;
            ValiderButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Force le shutdown de l'application quand AjouterPersonnage est la dernière fenêtre a être fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterPerso_Closed(object sender, EventArgs e)
        {
            ajouterPersoOpen = false;
            if (Application.Current.Windows.Count == 1)
            {
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Force le shutdown de l'application quand cette fenêtre est la dernière a être fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShutdownEnForce(object sender, EventArgs e)
        {
            if (Application.Current.Windows.Count == 1)
            {

                Application.Current.Shutdown();
            }
        }


    }
}
