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
using VinlandSol.Métier;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour Carte.xaml
    /// </summary>
    public partial class Carte : Window
    {
        private HexagonManager hexagonManager;
        private MatrixTransform zoomTransform = new MatrixTransform();
        private List<Image> selectedHexagons = new List<Image>();
        private string nom;
        private int largeur;
        private int hauteur;
        private bool isRightMouseDown = false;
        private Point lastMousePosition;
        private int zoomLevel = 0; // + = Dézoom // - = Zoom
        private int zoomLimitDezoom = 5; // zoomLimit doit être plus grand que zoomLevelDezoom
        private int zoomLimitZoom = -5; // zoomLimit doit être plus petit que zoomLevelZoom

        /// <summary>
        /// Carte de base - Dédiée au tests
        /// </summary>
        /// <author>Aaron</author>
        public Carte()
        {
            InitializeComponent();
            HexagonCanvas.RenderTransform = zoomTransform; // zoom map
            hexagonManager = new HexagonManager(HexagonCanvas); // Add les hexagones
            this.nom = "Carte par default";
            this.largeur = 10;
            this.hauteur = 10;

            GenerateHexagonalMap(largeur, hauteur); // Géneration de la map

            HexagonCanvas.MouseRightButtonDown += Canvas_MouseRightButtonDown; // Déplacement de la map avec le clic droit
            HexagonCanvas.MouseRightButtonUp += Canvas_MouseRightButtonUp;
            HexagonCanvas.MouseMove += Canvas_MouseMove;
        }

        /// <summary>
        /// Constructeur de carte - Appelé depuis Cartes.xaml ("vrai constructeur")
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="largeur"></param>
        /// <param name="hauteur"></param>
        /// <author>Aaron</author>
        public Carte(string nom, int largeur, int hauteur)
        {
            InitializeComponent();
            HexagonCanvas.RenderTransform = zoomTransform; // zoom map
            hexagonManager = new HexagonManager(HexagonCanvas); // Add les hexagones
            this.nom = nom;
            this.largeur = largeur;
            this.hauteur = hauteur;  
            
            GenerateHexagonalMap(largeur, hauteur); // Géneration de la map

            HexagonCanvas.MouseRightButtonDown += Canvas_MouseRightButtonDown; // Déplacement de la map avec le clic droit
            HexagonCanvas.MouseRightButtonUp += Canvas_MouseRightButtonUp;
            HexagonCanvas.MouseMove += Canvas_MouseMove;
        }

        /// <summary>
        /// Géneration de la map
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <author>Camille</author>
        private void GenerateHexagonalMap(int width, int height)
        {
            HexagonCanvas.Children.Clear();
            selectedHexagons.Clear();
            hexagonManager.Clear();

            double hexWidth = 70;
            double hexHeight = 70;
            double verticalSpacing = 0;

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    double x = col * (hexWidth * 0.95);
                    double y = row * (hexHeight * 0.70 + verticalSpacing);

                    if (row % 2 == 1)
                    {
                        x += hexWidth * 0.48;
                    }

                    Hexagon hexagon = new Hexagon(x + ((Width/7*5 - hexWidth * largeur) / 2) , y + ((Height - hexHeight * hauteur / 1.923076923 - 260) / 2)); // Le calcul permet de centrer de façon approximative les hexagones dans la fenêtre. 1.923076923 correspond a 500 (la hauteur d'un hexagone) divisé par 260 (la somme de la hauteur de la partie supérieure et de la partie inférieure de ce dernier) )
                    hexagonManager.AddHexagon(hexagon);

                    Image hexagonImage = hexagon.CreateImage();
                    HexagonCanvas.Children.Add(hexagonImage);
                }
            }
        }

        /// <summary>
        /// Méthode permetant de zoomer sur la map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Camille + Aaron (limite)</author>
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point position = e.GetPosition(HexagonCanvas);
            double zoomFactor = 1.1;
            Matrix matrix = zoomTransform.Matrix;
            if (e.Delta > 0) // Zoom
            {
                if (zoomLevel > zoomLimitZoom) // On ne peut pas trop zoomer
                {
                    zoomLevel -= 1;
                    matrix.ScaleAtPrepend(zoomFactor, zoomFactor, position.X, position.Y);
                }
            }
            else // Dézoom
            {   if(zoomLevel < zoomLimitDezoom) // On ne peut pas trop dézoomer
                {
                    zoomLevel += 1;
                    matrix.ScaleAtPrepend(1.0 / zoomFactor, 1.0 / zoomFactor, position.X, position.Y);
                }               
            }
            zoomTransform.Matrix = matrix;
        }

        /// <summary>
        /// Ouvre la fenêtre Cartes et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Aaron</author>
        private void OuvrirCartes_Click(object sender, RoutedEventArgs e)
        {
            Cartes pagecreation = new Cartes();
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            pagecreation.Show();
            CarteWindow.Close();
        }

        /// <summary>
        /// Pression du clic droit détecté (change l'état du booléen en True)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Evan</author>
        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                isRightMouseDown = true;
                lastMousePosition = e.GetPosition(HexagonCanvas);
                HexagonCanvas.CaptureMouse();
            }
        }

        /// <summary>
        /// Relachement du clic droit détecté (change l'état du booléen en False)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Evan</author>
        private void Canvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isRightMouseDown)
            {
                isRightMouseDown = false;
                HexagonCanvas.ReleaseMouseCapture();
            }
        }

        /// <summary>
        /// Déplacement de la map suivant la position de la souris et de ifRightMouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Evan + Aaron (limite)</author>
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRightMouseDown)
            {
                Point positionSourisFenetre = Mouse.GetPosition(this); // Position par rapport a la fenêtre 


                if (positionSourisFenetre.X >= this.Width / 7 && positionSourisFenetre.X <= this.Width / 7 * 6 && positionSourisFenetre.Y >= this.Height / 7 && positionSourisFenetre.Y <= this.Height) // Limiter le drag sur "la zone de la carte" 
                {
                    Point currentPosition = e.GetPosition(HexagonCanvas); // Position par rapport au canvas

                    double deltaX = (currentPosition.X - lastMousePosition.X) * 0.5;
                    double deltaY = (currentPosition.Y - lastMousePosition.Y) * 0.5;

                    Matrix matrix = zoomTransform.Matrix;
                    matrix.Translate(deltaX, deltaY);
                    zoomTransform.Matrix = matrix;

                    lastMousePosition = currentPosition;
                }
            }
        }
    }
}
