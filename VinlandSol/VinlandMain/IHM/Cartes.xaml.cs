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
    /// Logique d'interaction pour Cartes.xaml
    /// </summary>
    public partial class Cartes : Window
    {
        private CreationCarte? pagecreationcarte;
        private bool creaCarteOpen = false;

        public Cartes()
        {
            InitializeComponent();
            Closed += ShutdownEnForce; // ShutdownEnForce est appelé à la fermeture de cette fenêtre
        }

        /// <summary>
        /// Ouvre le fenêtre Personnages et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirPersonnages_Click(object sender, RoutedEventArgs e)
        {
            Personnages pagecreationperso = new Personnages();
            pagecreationperso.Show();

            var timer = new System.Timers.Timer(100); // Délai de 100 millisecondes 
            timer.Elapsed += (s, args) =>
            {
                timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    CartesWindow.Close(); // On ferme cette fenêtre en retard pour éviter que le if de ShutdownEnForce ne passe.
                });
            };
            timer.Start();
        }

        private void OeilChange(object sender, RoutedEventArgs e)
        {
           Loeil.Source = new BitmapImage(new Uri("Media/Icones/Oeilbarre.png", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Ouvre la fenêtre CreationCarte et limite cette action tant que cette dernière n'est pas fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirCreationCarte_Click(object sender, RoutedEventArgs e)
        {     
            if (creaCarteOpen == false)
            {
                pagecreationcarte = new CreationCarte();
                pagecreationcarte.Closed += CreationCarte_Closed;
                pagecreationcarte.Show();
                creaCarteOpen = true;
            }     
        }

        /// <summary>
        /// Force le shutdown de l'application quand CreationCarte est la dernière fenêtre à être fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreationCarte_Closed(object sender, EventArgs e)
        {
            creaCarteOpen = false;
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
