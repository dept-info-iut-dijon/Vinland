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
        public Carte()
        {
            InitializeComponent();
            HexagonCanvas.RenderTransform = zoomTransform; // zoom map
            hexagonManager = new HexagonManager(HexagonCanvas); // Add les hexagones
            GenerateHexagonalMap(10, 10); // Géneration de la map
        }

        /// <summary>
        /// Géneration de la map
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
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

                    Hexagon hexagon = new Hexagon(x, y);
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
        /// 
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point position = e.GetPosition(HexagonCanvas);
            double zoomFactor = 1.1;
            Matrix matrix = zoomTransform.Matrix;

            if (e.Delta > 0)
            {
                matrix.ScaleAtPrepend(zoomFactor, zoomFactor, position.X, position.Y);
            }
            else
            {
                matrix.ScaleAtPrepend(1.0 / zoomFactor, 1.0 / zoomFactor, position.X, position.Y);
            }

            zoomTransform.Matrix = matrix;
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
            CarteWindow.Close();
        }
    }
}
