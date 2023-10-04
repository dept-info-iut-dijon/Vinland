using System;
using System.Collections.Generic;
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
using VinlandMain.IHM;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour Personnages.xaml
    /// </summary>
    public partial class Personnages : Window
    {
        private AjouterPersonnage? pageajouterPerso;
        private bool ajouterPersoOpen = false;

        public Personnages()
        {
            InitializeComponent();
            Closed += ShutdownEnForce; // ShutdownEnForce est appelé à la fermeture de cette fenêtre
        }

        /// <summary>
        /// Ouvre la fenêtre Campagnes et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCampagnes_Click(object sender, RoutedEventArgs e)
        {
            Campagnes pagecreation = new Campagnes();
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
        /// Ouvre la fenêtre Cartes et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCartes_Click(object sender, RoutedEventArgs e)
        {
            Cartes pagecreation = new Cartes();
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
                pageajouterPerso = new AjouterPersonnage();
                pageajouterPerso.Closed += AjouterPerso_Closed;
                pageajouterPerso.Show();
                ajouterPersoOpen = true;
            }
        }

        /// <summary>
        /// Force le shutdown de l'application quand AjouterPersonnage est la dernière fenêtre à être fermée
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
