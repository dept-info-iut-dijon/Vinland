using System;
using System.Windows;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour CustomMessageBox.xaml
    /// </summary>
    /// <author>Alexis</author>
    public partial class CustomMessageBox : Window
    {
        #region Evenements

        public event EventHandler<bool> YesButtonClicked; // Evenement Oui est Cliqué
        public event EventHandler<bool> NoButtonClicked; // Evenement Non est Cliqué

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise la fenêtre
        /// </summary>
        /// <param name="message">Le message a afficher</param>
        /// <param name="yesno">Indique si la fenêtre contient un choix yes/no</param>
        /// <Author>Alexis + Aaron(choix)</Author>
        public CustomMessageBox(string message, bool yesno = false)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MessageAffiche.Content = message;

            if (yesno) // Si fenêtre à choix
            {
                // On affiche les boutons de la variation à choix
                Non.Visibility = Visibility.Visible;
                Oui.Visibility = Visibility.Visible;
                Ok_Button.Visibility = Visibility.Collapsed;

                Oui.Click += (sender, e) => // Au click de oui
                {
                    YesButtonClicked?.Invoke(this, true); // L'évenement est déclenché et renvoie true
                    this.Close(); // La fenêtre est fermée
                };

                Non.Click += (sender, e) => // Au click de non
                {
                    NoButtonClicked?.Invoke(this, false); // L'évenement est déclenché et renvoie false
                    this.Close(); // La fenêtre est fermée
                };
            }
            else
            {
                // On affiche les boutons de la variation normale
                Non.Visibility = Visibility.Collapsed;
                Oui.Visibility = Visibility.Collapsed;
                Ok_Button.Visibility = Visibility.Visible;

                Ok_Button.Click += (sender, e) => this.Close(); // La fenêtre est fermée au click de Ok
            }
        }

        #endregion
    }
}
