using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VinlandSol.BDD;
using VinlandSol.Métier;

namespace VinlandSol.IHM
{
    /// <summary>
    /// Logique d'interaction pour Carte.xaml
    /// </summary>
    public partial class Carte : Window
    {
        #region Attributs

        private int idUser;
        private string roleUser;
        private int idCampagne;
        private Métier.Carte carte;
        private FakeDAO fakeDAO = FakeDAO.Instance;
        private string basePath = Directory.GetCurrentDirectory();
        private FileStream output;
        private MatrixTransform zoomTransform = new MatrixTransform();
        private List<Image> selectedHexagons = new List<Image>();
        private bool isRightMouseDown = false;
        private Point lastMousePosition;
        private int zoomLevel = 0; // + = Dézoom // - = Zoom
        private int zoomLimitDezoom = 5; // zoomLimit doit être plus grand que zoomLevelDezoom
        private int zoomLimitZoom = -5; // zoomLimit doit être plus petit que zoomLevelZoom
        private List<Hexagon> hexagons = new List<Hexagon>();
        private List<Terrain> terrains = new List<Terrain>();

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur de carte - Appelé depuis Cartes.xaml ("vrai constructeur")
        /// </summary>
        /// <param name="user">utilisateur connecté</param>
        /// <param name="carte"></param>
        /// <author>Aaron</author>
        public Carte(int idUser, string roleUser,int idCarte, int idCampagne)
        {
            InitializeComponent();
            carte = fakeDAO.GetCarte(idCarte);
            GenerateHexagonalMap(carte.Largeur, carte.Hauteur); // Géneration de la map
            InitializeTerrains();

            HexagonCanvas.RenderTransform = zoomTransform; // zoom map
            NomCarteLabel.Content = carte.Nom;
            DimCarteLabel.Content = carte.Largeur + "x" + carte.Hauteur;
        
            HexagonCanvas.MouseRightButtonDown += Canvas_MouseRightButtonDown; // Déplacement de la map avec le clic droit
            HexagonCanvas.MouseRightButtonUp += Canvas_MouseRightButtonUp;
            HexagonCanvas.MouseMove += Canvas_MouseMove;
        }

        #endregion

        #region Initialisations

        /// <summary>
        /// Créé les terrains et les mettre dans la list
        /// </summary>
        private void InitializeTerrains()
        {
            Terrain vide = new Terrain { Name = "Vide", ImagePath = "vide.png", Color = Colors.White };
            Terrain plaine = new Terrain { Name = "Plaine", ImagePath = "plaine.png", Color = Colors.LightGreen };
            Terrain desert = new Terrain { Name = "Desert", ImagePath = "desert.png", Color = (Color)ColorConverter.ConvertFromString("#f5d9a9") };

            terrains.Add(vide);
            terrains.Add(plaine);
            terrains.Add(desert);

            ListTerrains.Items.Add(vide.Name);
            ListTerrains.Items.Add(plaine.Name);
            ListTerrains.Items.Add(desert.Name);
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
            hexagons.Clear();

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
                    Hexagon hexagon = new Hexagon(hexagons.Count+1 ,x + ((Width / 7 * 5 - hexWidth * carte.Largeur) / 2), y + ((Height - hexHeight * carte.Hauteur / 1.923076923 - 260) / 2)) ; // Le calcul permet de centrer de façon approximative les hexagones dans la fenêtre. 1.923076923 correspond a 500 (la hauteur d'un hexagone) divisé par 260 (la somme de la hauteur de la partie supérieure et de la partie inférieure de ce dernier) )
                    hexagon.ImageSource = new BitmapImage(new Uri(Path.Combine("pack://application:,,,/VinlandSol;component/IHM/Media/Resources/", "hexagon.png")));
                    hexagon.ImagePath = "hexagon.png";
                    hexagon.X = row+1; // Commence par 1
                    hexagon.Y = col+1; // Commence par 1

                    Image imageControl = new Image
                    {
                        Source = hexagon.ImageSource,
                        Stretch = Stretch.Uniform,
                        Width = hexWidth,
                        Height = hexHeight
                    };

                    Canvas.SetLeft(imageControl, x + ((Width / 7 * 5 - hexWidth * carte.Largeur) / 2));
                    Canvas.SetTop(imageControl, y + ((Height - hexHeight * carte.Hauteur / 1.923076923 - 260) / 2));

                    imageControl.MouseDown += Hexagon_MouseDown;

                    hexagon.ImageControl = imageControl;
                    hexagons.Add(hexagon);

                    HexagonCanvas.Children.Add(imageControl);
                }
            }
        }

        #endregion

        #region Selection Hexagones

        /// <summary>
        /// Modifie l'image de l'hexagone en fonction du terrain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hexagon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach(Terrain terrain in terrains)
            {
                UpdateHexagonsColor(terrain);
            }

            Image clickedHexagon = (Image)sender;

            int index = hexagons.FindIndex(hex => hex.ImageControl == clickedHexagon);
            typeEmplacementCaseLabel.Content = "Case Vide " + hexagons[index].X + "," + hexagons[index].Y;

            if (ListTerrains.SelectedIndex != -1 && ListTerrains.Items.Count > 0)
            {
                int selectedIndex = Math.Min(ListTerrains.SelectedIndex, terrains.Count() - 1);
                Terrain selectedTerrain = terrains[selectedIndex];

                hexagons[index].AssociatedTerrain = selectedTerrain;
                hexagons[index].ImageSource = CreateColoredBitmap(hexagons[index].ImagePath, selectedTerrain.Color);

                // Ajoutez ces lignes pour déboguer
                Debug.WriteLine($"Terrain changed: {hexagons[index].AssociatedTerrain.Name} at ({hexagons[index].X},{hexagons[index].Y})");
                Debug.WriteLine($"New ImageSource: {hexagons[index].ImageSource}");

                clickedHexagon.Source = hexagons[index].ImageSource;
                typeEmplacementCaseLabel.Content = "Case " + hexagons[index].AssociatedTerrain.Name + " " + hexagons[index].X + "," + hexagons[index].Y;
                SaveTerrainImage(selectedTerrain, hexagons[index].ImageSource);
            }
        }

        #endregion

        #region Selection Terrains

        /// <summary>
        /// Cache les changements de couleur de Terrain si le premier est sélectionné ou aucun
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Terrains_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListTerrains.SelectedItem != null && ListTerrains.SelectedIndex != 0)
            {
                colorPicker.Visibility = Visibility.Visible;
                colorValidator.Visibility = Visibility.Visible;
            }
            else
            {
                colorPicker.Visibility = Visibility.Hidden;
                colorValidator.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region ColorPicker

        /// <summary>
        /// Récupere la couleur selectionée du picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (ListTerrains.SelectedIndex != -1)
            {
                Terrain selectedTerrain = terrains[ListTerrains.SelectedIndex];
                selectedTerrain.Color = colorPicker.SelectedColor ?? Colors.White;
                UpdateHexagonsColor(selectedTerrain);
            }
        }

        /// <summary>
        /// Change la couleur du terrain en fonction de la couleur selectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeColorButton_Click(object sender, RoutedEventArgs e)
        {
            Color selectedColor = colorPicker.SelectedColor ?? Colors.White;

            if (ListTerrains.SelectedIndex >= 0 && ListTerrains.SelectedIndex < terrains.Count())
            {
                Terrain selectedTerrain = terrains[ListTerrains.SelectedIndex];
                selectedTerrain.Color = selectedColor;

                var associatedHexagons = hexagons.Where(hexagon => hexagon.AssociatedTerrain == selectedTerrain).ToList();

                if (associatedHexagons.Count > 0)
                {
                    UpdateHexagonsColor(selectedTerrain);
                }
                else
                {
                    BitmapImage coloredImage = CreateColoredBitmap("hexagon.png", selectedTerrain.Color);
                    SaveTerrainImage(selectedTerrain, coloredImage);
                }
            }
        }

        #endregion

        #region Creation Image

        /// <summary>
        /// Créé une image en fonction de la couleur donnée
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private BitmapImage CreateColoredBitmap(string imagePath, Color color)
        {
            BitmapImage originalImage = new BitmapImage(new Uri(Path.Combine(basePath, imagePath)));

            int width = originalImage.PixelWidth;
            int height = originalImage.PixelHeight;

            int bytesPerPixel = 4;
            byte[] pixels = new byte[width * height * bytesPerPixel];
            originalImage.CopyPixels(pixels, width * bytesPerPixel, 0);

            Color referenceColor = GetReferenceColor(pixels, width, height);

            for (int i = 0; i < pixels.Length; i += bytesPerPixel)
            {
                if (IsReferenceColor(pixels, i, referenceColor))
                {
                    pixels[i] = color.B;
                    pixels[i + 1] = color.G;
                    pixels[i + 2] = color.R;
                }
            }

            BitmapSource modifiedImage = BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, pixels, width * bytesPerPixel);

            BitmapImage coloredBitmap = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(modifiedImage));
                encoder.Save(memoryStream);
                coloredBitmap.BeginInit();
                coloredBitmap.StreamSource = new MemoryStream(memoryStream.ToArray());
                coloredBitmap.EndInit();
            }
            return coloredBitmap;
        }

        /// <summary>
        /// Récupére la réference de la couleur
        /// </summary>
        /// <param name="pixels"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Color GetReferenceColor(byte[] pixels, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = (y * width + x) * 4; // 4 bytes per pixel (B, G, R, A)

                    if (pixels[index] == 255 && pixels[index + 1] == 255 && pixels[index + 2] == 255)
                    {
                        return Color.FromRgb(pixels[index + 2], pixels[index + 1], pixels[index]);
                    }
                }
            }
            return Colors.White;
        }

        /// <summary>
        /// Vérifie si la couleur de référence est égale au pixel donné 
        /// </summary>
        /// <param name="pixels"></param>
        /// <param name="index"></param>
        /// <param name="referenceColor"></param>
        /// <returns></returns>
        private bool IsReferenceColor(byte[] pixels, int index, Color referenceColor)
        {
            return pixels[index] == referenceColor.B && pixels[index + 1] == referenceColor.G && pixels[index + 2] == referenceColor.R;
        }

        #endregion

        #region Updates

        /// <summary>
        /// Sauvergarde l'image assignée au terrain
        /// </summary>
        /// <param name="terrain"></param>
        /// <param name="image"></param>
        private void SaveTerrainImage(Terrain terrain, BitmapImage image)
        {
            string terrainOutputPath = Path.Combine(basePath, terrain.ImagePath);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            try
            {
                output = new FileStream(terrainOutputPath, FileMode.Create);
                encoder.Save(output);
            }
            catch (Exception ex)
            {
                CustomMessageBox messageBox = new CustomMessageBox("An error occurred while saving the terrain image: " + ex.Message);
                messageBox.ShowDialog();
            }
            finally
            {
                output?.Close();
            }
        }

        /// <summary>
        /// Change l'image de l'hexagone par rapport a son terrain
        /// </summary>
        /// <param name="terrain"></param>
        private void UpdateHexagonsColor(Terrain terrain)
        {
            foreach (var hexagon in hexagons)
            {
                if (hexagon.AssociatedTerrain == terrain)
                {
                    hexagon.ImageSource = CreateColoredBitmap(hexagon.ImagePath, terrain.Color);
                    hexagon.ImageControl.Source = hexagon.ImageSource;
                }
            }
        }

        #endregion 

        #region Zoom Canvas

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
            {
                if (zoomLevel < zoomLimitDezoom) // On ne peut pas trop dézoomer
                {
                    zoomLevel += 1;
                    matrix.ScaleAtPrepend(1.0 / zoomFactor, 1.0 / zoomFactor, position.X, position.Y);
                }
            }
            zoomTransform.Matrix = matrix;
        }

        #endregion 

        #region Déplacements Canvas

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

        #endregion

        #region Sortie Carte

        /// <summary>
        /// Ouvre la fenêtre Cartes et ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Aaron</author>
        private void OuvrirCartes_Click(object sender, RoutedEventArgs e)
        {
            Cartes pagecreation = new Cartes(idUser, roleUser, idCampagne);
            pagecreation.Left = this.Left;
            pagecreation.Top = this.Top;
            pagecreation.Show();
            CarteWindow.Close();
        }

        #endregion

    }
}