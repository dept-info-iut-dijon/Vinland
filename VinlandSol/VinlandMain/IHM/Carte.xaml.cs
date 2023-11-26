using System;
using System.Collections.Generic;
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

        private int idUser; // Identifiant de l'utilisateur
        private string roleUser; // Rôle de l'utilisateur
        private int idCampagne; // Identifiant de la campagne
        private Métier.Carte carte; // Instance de la classe Carte du namespace Métier
        private FakeDAO fakeDAO = FakeDAO.Instance; // Instance de FakeDAO pour l'accès aux données fictives
        private string basePath = Directory.GetCurrentDirectory(); // Chemin de base du répertoire courant
        private FileStream output; // Flux de sortie (peut être utilisé pour écrire dans un fichier)
        private MatrixTransform zoomTransform = new MatrixTransform(); // Transformation de zoom utilisant une matrice
        private List<Image> selectedHexagons = new List<Image>(); // Liste des hexagones sélectionnés
        private bool isRightMouseDown = false; // Indique si le bouton droit de la souris est enfoncé
        private Point lastMousePosition; // Dernière position connue de la souris
        private int zoomLevel = 0; // Niveau de zoom (positif pour dézoomer, négatif pour zoomer)
        private int zoomLimitDezoom = 5; // Limite de zoom pour le dézoom
        private int zoomLimitZoom = -5; // Limite de zoom pour le zoom
        private List<Hexagon> hexagons = new List<Hexagon>(); // Liste des hexagones dans la carte
        private List<Terrain> terrains = new List<Terrain>(); // Liste des terrains de la carte

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur de la classe Carte - Appelé depuis Cartes.xaml
        /// </summary>
        /// <param name="idUser"> Identifiant de l'utilisateur identifié </param>
        /// <param name="roleUser"> Role de l'utilisateur </param>
        /// <param name="idCarte"> id de la carte actuelle </param>
        /// <param name="idCampagne"> id de la campagne actuelle </param>
        /// <author> Aaron </author>
        public Carte(int idUser, string roleUser, int idCarte, int idCampagne)
        {     
            InitializeComponent();
            carte = fakeDAO.GetCarte(idCarte); // Récuperation de la carte référencée
            GenerateHexagonalMap(carte.Largeur, carte.Hauteur); // Génération de la map   
            InitializeTerrains(); // Initialise les terrains de la carte
            HexagonCanvas.RenderTransform = zoomTransform; // Zoom map
            NomCarteLabel.Content = carte.Nom; // Affiche le nom de la carte dans l'interface utilisateur
            DimCarteLabel.Content = carte.Largeur + "x" + carte.Hauteur; // Affiche les dimensions de la carte dans l'interface utilisateur

            // Ajoute des gestionnaires d'événements pour le déplacement de la carte avec le clic droit de la souris
            HexagonCanvas.MouseRightButtonDown += Canvas_MouseRightButtonDown; // Déplacement de la map avec le clic droit
            HexagonCanvas.MouseRightButtonUp += Canvas_MouseRightButtonUp;
            HexagonCanvas.MouseMove += Canvas_MouseMove;
        }



        #endregion

        #region Initialisations

        /// <summary>
        /// Initialise l'image hexagon.png dans le projet Net 6.0
        /// </summary>
        /// <author> Camille </author>
        private void InitializeHexagonImage()
        {
            // Vérifie le chemin de destination de l'image hexagon.png
            string hexagonImageDestinationPathCheck = Path.Combine(basePath, "hexagon.png");
            string hexagonImageDestinationPath = Path.Combine(basePath, "hexagon.png");

            // Vérifie si l'image hexagon.png n'existe pas déjà dans le chemin de destination
            if (!File.Exists(hexagonImageDestinationPathCheck))
            {
                // Récupère le chemin de notre hexagon.png dans le projet 
                using (Stream stream = Application.GetResourceStream(new Uri("pack://application:,,,/VinlandSol;component/IHM/Media/Resources/hexagon.png")).Stream)
                {
                    // Copie l'image dans le dossier du projet Net 6.0
                    using (FileStream fileStream = File.Create(hexagonImageDestinationPath))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }
        }

        /// <summary>
        /// Cette fonction initialise les terrains en créant différentes instances de la classe Terrain
        /// les configure avec des images, des noms et des couleurs, puis les ajoute à la liste de terrains
        /// </summary>
        /// <author> Camille </author>
        private void InitializeTerrains()
        {
            // Initialisation des types de terrain avec leurs couleurs
            Terrain vide = new Terrain("vide.png") { Name = "Vide", Color = Colors.White };
            Terrain plaine = new Terrain("plaine.png") { Name = "Plaine", Color = Colors.LightGreen };
            Terrain desert = new Terrain("desert.png") { Name = "Desert", Color = (Color)ColorConverter.ConvertFromString("#f5d9a9") };

            terrains.Add(vide);  // Ajout du terrain "Vide" à la liste de terrains
            terrains.Add(plaine);  // Ajout du terrain "Plaine" à la liste de terrains
            terrains.Add(desert);  // Ajout du terrain "Desert" à la liste de terrains

            ListTerrains.Items.Add(vide.Name);  // Ajout du nom du terrain "Vide" à la liste d'items
            ListTerrains.Items.Add(plaine.Name);  // Ajout du nom du terrain "Plaine" à la liste d'items
            ListTerrains.Items.Add(desert.Name);  // Ajout du nom du terrain "Desert" à la liste d'items
        }


        /// <summary>
        /// Génère une carte hexagonale dans le canvas avec les dimensions spécifiées
        /// </summary>
        /// <param name="width">Largeur de la carte</param>
        /// <param name="height">Hauteur de la carte</param>
        /// <author> Camille </author>
        private void GenerateHexagonalMap(int width, int height)
        {
            HexagonCanvas.Children.Clear(); // Nettoie le canvas des hexagones existants
            selectedHexagons.Clear(); // Efface la liste des hexagones sélectionnés
            hexagons.Clear(); // Efface la liste des hexagones générés précédemment

            double hexWidth = 70;
            double hexHeight = 70;
            double verticalSpacing = 0;

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    double x = col * (hexWidth * 0.95);
                    double y = row * (hexHeight * 0.70 + verticalSpacing);

                    // Ajuste la position des hexagones pour les lignes impaires
                    if (row % 2 == 1)
                    {
                        x += hexWidth * 0.48;
                    }
                    Hexagon hexagon = new Hexagon(x, y);

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

                    // Centrage approximatif des hexagones dans la fenêtre
                    Canvas.SetLeft(imageControl, x + ((Width / 7 * 5 - hexWidth * carte.Largeur) / 2));
                    Canvas.SetTop(imageControl, y + ((Height - hexHeight * carte.Hauteur / 1.923076923 - 260) / 2));

                    imageControl.MouseDown += Hexagon_MouseDown;

                    hexagon.ImageControl = imageControl;
                    hexagons.Add(hexagon);

                    HexagonCanvas.Children.Add(imageControl); // Ajoute l'hexagone au canvas
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
        /// <author> Camille </author>
        private void Hexagon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Parcourt tous les terrains pour mettre à jour les couleurs des hexagones en fonction du terrain
            foreach (Terrain terrain in terrains)
            {
                UpdateHexagonsColor(terrain);
            }

            // Obtient l'image de l'hexagone qui a été cliquée
            Image clickedHexagon = (Image)sender;

            // Trouve l'index de l'hexagone dans la liste
            int index = hexagons.FindIndex(hex => hex.ImageControl == clickedHexagon);

            // Affiche les coordonnées de l'hexagone cliqué dans le label
            typeEmplacementCaseLabel.Content = "Case Vide " + hexagons[index].X + "," + hexagons[index].Y;

            // Vérifie si un terrain est sélectionné dans la liste des terrains
            if (ListTerrains.SelectedIndex != -1 && ListTerrains.Items.Count > 0)
            {
                // Sélectionne l'index du terrain choisi
                int selectedIndex = Math.Min(ListTerrains.SelectedIndex, terrains.Count() - 1);

                // Obtient le terrain sélectionné
                Terrain selectedTerrain = terrains[selectedIndex];

                // Associe le terrain sélectionné à l'hexagone et met à jour son image en conséquence
                hexagons[index].AssociatedTerrain = selectedTerrain;
                hexagons[index].ImageSource = CreateColoredBitmap(hexagons[index].ImagePath, selectedTerrain.Color);

                // Supprime l'image du terrain associé précédemment
                RemoveTerrainImage(selectedTerrain);

                // Crée et enregistre la nouvelle image de l'hexagone avec la couleur du terrain sélectionné
                CreateAndSaveHexagonImage(selectedTerrain);

                // Met à jour l'image de l'hexagone cliqué
                clickedHexagon.Source = hexagons[index].ImageSource;

                // Affiche le type d'emplacement dans le label
                typeEmplacementCaseLabel.Content = "Case " + hexagons[index].AssociatedTerrain.Name + " " + hexagons[index].X + "," + hexagons[index].Y;

                // Force la mise à jour du layout du canvas des hexagones
                HexagonCanvas.UpdateLayout();
            }
        }


        #endregion

        #region Gestion Image Terrains

        /// <summary>
        /// Méthode pour supprimer l'image existante du type de terrain
        /// </summary>
        /// <param name="terrain"> Terrain dont l'image doit être supprimée </param>
        /// <author> Camille </author>

        private void RemoveTerrainImage(Terrain terrain)
        {
            string terrainImagePath = Path.Combine(basePath, terrain.ImagePath); // Chemin complet vers l'image du terrain

            if (File.Exists(terrainImagePath)) // Vérifie si le fichier image existe
            {
                File.Delete(terrainImagePath); // Supprime le fichier image si existant
            }
        }


        /// <summary>
        /// Méthode pour créer et enregistrer la nouvelle image d'hexagone
        /// </summary>
        /// <param name="terrain"> Terrain lu </param>
        /// <author> Camille </author>
        private void CreateAndSaveHexagonImage(Terrain terrain)
        {
            // Charge l'image de base de l'hexagone depuis le chemin
            BitmapImage baseHexagonImage = new BitmapImage(new Uri(Path.Combine(basePath, "hexagon.png")));

            // Crée une image colorée en fonction de la couleur selectionné dans le terrain
            BitmapImage coloredImage = CreateColoredBitmap("hexagon.png", terrain.Color);

            // Chemin pour la nouvelle image d'hexagone
            string newImagePath = Path.Combine(basePath, terrain.ImagePath);

            // Enregistre l'image du terrain avec la couleur selectionné
            SaveTerrainImage(terrain, coloredImage);

            // Enregistre à nouveau l'image du terrain avec la couleur selectionné
            SaveTerrainImage(terrain, coloredImage);
        }

        #endregion

        #region Selection Terrains

        /// <summary>
        /// Gère l'événement de changement de sélection des terrains
        /// Cache les changements de couleur du terrain si le premier est sélectionné ou aucun
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Evan </author>
        public void Terrains_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Vérifie si un terrain est sélectionné et qu'il ne s'agit pas du premier
            if (ListTerrains.SelectedItem != null && ListTerrains.SelectedIndex != 0)
            {
                // Affiche le sélecteur de couleur et le validateur de couleur
                colorPicker.Visibility = Visibility.Visible;
                colorValidator.Visibility = Visibility.Visible;
            }
            else
            {
                // Cache le sélecteur de couleur et le validateur de couleur
                colorPicker.Visibility = Visibility.Hidden;
                colorValidator.Visibility = Visibility.Hidden;
            }
        }


        #endregion

        #region ColorPicker

        /// <summary>
        /// Fonction appelée lorsqu'une nouvelle couleur est sélectionnée dans le sélecteur de couleurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Camille </author>
        private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            // Vérifie si un terrain est sélectionné dans la liste
            if (ListTerrains.SelectedIndex != -1)
            {
                // Récupère le terrain sélectionné
                Terrain selectedTerrain = terrains[ListTerrains.SelectedIndex];

                // Met à jour la couleur du terrain avec la couleur sélectionnée dans le picker, ou blanc si aucune couleur n'est sélectionnée
                selectedTerrain.Color = colorPicker.SelectedColor ?? Colors.White;

                // Met à jour la couleur des hexagones associés à ce terrain
                UpdateHexagonsColor(selectedTerrain);
            }
        }


        /// <summary>
        /// Change la couleur du terrain en fonction de la couleur sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Camille </author>
        private void ChangeColorButton_Click(object sender, RoutedEventArgs e)
        {
            Color selectedColor = colorPicker.SelectedColor ?? Colors.White; // On récupère la couleur du colorPicker

            // Vérifie si un terrain est sélectionné dans la liste
            if (ListTerrains.SelectedIndex >= 0 && ListTerrains.SelectedIndex < terrains.Count())
            {
                Terrain selectedTerrain = terrains[ListTerrains.SelectedIndex];
                selectedTerrain.Color = selectedColor; // On change la couleur du terrain

                // Récupère les hexagones associés au terrain sélectionné
                var associatedHexagons = hexagons.Where(hexagon => hexagon.AssociatedTerrain == selectedTerrain).ToList();

                // Vérifie s'il y a des hexagones associés
                if (associatedHexagons.Count > 0)
                {
                    UpdateHexagonsColor(selectedTerrain); // On met à jour la couleur de l'hexagone en fonction du type de terrain sélectionné
                }
                else
                {
                    // Si aucun hexagone associé, crée une image colorée et sauvegarde le terrain
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
        /// <param name="imagePath">Chemin de notre hexagon.png à créer</param>
        /// <param name="color">Couleur choisie par l'utilisateur</param>
        /// <returns>Image de l'hexagone colorié</returns>
        /// <author> Camille </author>
        private BitmapImage CreateColoredBitmap(string imagePath, Color color)
        {
            // Initialisation de l'image
            InitializeHexagonImage();

            // Chargement de l'image d'origine depuis le chemin spécifié
            BitmapImage originalImage = new BitmapImage(new Uri(Path.Combine(basePath, imagePath)));

            // Obtention des dimensions de l'image
            int width = originalImage.PixelWidth;
            int height = originalImage.PixelHeight;

            // Calcul du nombre d'octets par pixel
            int bytesPerPixel = 4;

            // Création d'un tableau pour stocker les pixels de l'image
            byte[] pixels = new byte[width * height * bytesPerPixel];
            originalImage.CopyPixels(pixels, width * bytesPerPixel, 0);

            // Parcours des pixels un par un
            for (int i = 0; i < pixels.Length; i += bytesPerPixel)
            {
                // Vérification de la couleur pour remplacer les blancs mais en conservant les noirs 
                if (pixels[i + 1] == Colors.White.B && pixels[i + 1] == Colors.White.G && pixels[i + 2] == Colors.White.R)
                {
                    // Remplacement des composantes RGB du pixel par la couleur spécifiée
                    pixels[i] = color.B;
                    pixels[i + 1] = color.G;
                    pixels[i + 2] = color.R;
                }
            }

            // Création d'une nouvelle source d'image bitmap modifiée
            BitmapSource modifiedImage = BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, pixels, width * bytesPerPixel);

            // Création d'une nouvelle image bitmap colorée
            BitmapImage coloredBitmap = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Encodage de l'image modifiée en format PNG
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(modifiedImage));
                encoder.Save(memoryStream);

                // Initialisation de l'image bitmap colorée à partir du flux mémoire encodé
                coloredBitmap.BeginInit();
                coloredBitmap.StreamSource = new MemoryStream(memoryStream.ToArray());
                coloredBitmap.EndInit();
            }

            // Renvoi de l'image bitmap colorée
            return coloredBitmap;
        }



        #endregion

        #region Updates

        /// <summary>
        /// Sauvegarde l'image assignée au terrain
        /// </summary>
        /// <param name="terrain">Le terrain associé</param>
        /// <param name="image">L'image à sauvegarder</param>
        /// <author> Camille </author>
        private void SaveTerrainImage(Terrain terrain, BitmapImage image)
        {
            // Chemin de sortie pour l'image du terrain
            string terrainOutputPath = Path.Combine(basePath, terrain.ImagePath);

            // Création d'un encodeur PNG
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            try
            {
                // Tentative d'ouverture du flux de sortie
                output = new FileStream(terrainOutputPath, FileMode.Create);

                // Sauvegarde de l'image à l'aide de l'encodeur
                encoder.Save(output);
            }
            catch (Exception ex)
            {
                // En cas d'erreur
                CustomMessageBox messageBox = new CustomMessageBox("Une erreur s'est produite lors de la sauvegarde de l'image du terrain : " + ex.Message);
                messageBox.ShowDialog();
            }
            finally
            {
                // Fermeture du flux de sortie
                output?.Close();
            }
        }


        /// <summary>
        /// Met à jour la couleur de l'image de l'hexagone en fonction de son terrain
        /// </summary>
        /// <param name="terrain">Le terrain associé à l'hexagone</param>
        /// <author> Camille </author>
        private void UpdateHexagonsColor(Terrain terrain)
        {
            foreach (var hexagon in hexagons)  // Pour chaque hexagone
            {
                if (hexagon.AssociatedTerrain == terrain)
                {
                    hexagon.ImageSource = CreateColoredBitmap(hexagon.ImagePath, terrain.Color);  // Crée une image colorée en utilisant le chemin de l'image et la couleur du terrain
                    hexagon.ImageControl.Source = hexagon.ImageSource;  // Met à jour la source de l'image du contrôle hexagon
                }
            }
        }


        #endregion 

        #region Zoom Canvas

        /// <summary>
        /// Méthode permettant de gérer le zoom sur la carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Evan + Aaron (limite) </author>
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point position = e.GetPosition(HexagonCanvas); // Obtient la position de la souris par rapport au canvas
            double zoomFactor = 1.1; // Facteur de zoom
            Matrix matrix = zoomTransform.Matrix; // Matrice de transformation actuelle

            if (e.Delta > 0) // Si la molette tourne vers le haut (zoom)
            {
                if (zoomLevel > zoomLimitZoom) // Vérifie si le niveau de zoom est dans les limites autorisées
                {
                    zoomLevel -= 1; // Réduit le niveau de zoom
                    matrix.ScaleAtPrepend(zoomFactor, zoomFactor, position.X, position.Y); // Applique le zoom à la position de la souris
                }
            }
            else // Si la molette tourne vers le bas (dézoom)
            {
                if (zoomLevel < zoomLimitDezoom) // Vérifie si le niveau de dézoom est dans les limites autorisées
                {
                    zoomLevel += 1; // Augmente le niveau de dézoom
                    matrix.ScaleAtPrepend(1.0 / zoomFactor, 1.0 / zoomFactor, position.X, position.Y); // Applique le dézoom à la position de la souris
                }
            }

            zoomTransform.Matrix = matrix; // Met à jour la matrice de transformation
        }


        #endregion

        #region Déplacements Canvas

        /// <summary>
        /// Fonction appelée lorsqu'un clic droit est enfoncé. Modifie l'état du booléen en True
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Evan </author>
        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Vérifie si le bouton droit de la souris est enfoncé
            if (e.RightButton == MouseButtonState.Pressed)
            {
                isRightMouseDown = true; // Change l'état du clic droit en True
                lastMousePosition = e.GetPosition(HexagonCanvas); // Enregistre la dernière position de la souris
                HexagonCanvas.CaptureMouse(); // Capture la souris pour suivre les mouvements
            }
        }

        /// <summary>
        /// Fonction appelée lorsqu'un clic droit est relâché. Modifie l'état du booléen en False
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Evan </author>
        private void Canvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Vérifie si le clic droit était enfoncé précédemment
            if (isRightMouseDown)
            {
                isRightMouseDown = false; // Change l'état du clic droit en False
                HexagonCanvas.ReleaseMouseCapture(); // Libère la capture de la souris
            }
        }


        /// <summary>
        /// Déplacement de la carte en fonction de la position de la souris et de l'état du bouton droit de la souris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Evan + Aaron (limite) </author>
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Vérifie si le bouton droit de la souris est enfoncé
            if (isRightMouseDown)
            {
                Point positionSourisFenetre = Mouse.GetPosition(this); // Position par rapport à la fenêtre

                // Limite le déplacement de la carte à la zone de la carte définie
                if (positionSourisFenetre.X >= this.Width / 7 && positionSourisFenetre.X <= this.Width / 7 * 6 && positionSourisFenetre.Y >= this.Height / 7 && positionSourisFenetre.Y <= this.Height)
                {
                    Point currentPosition = e.GetPosition(HexagonCanvas); // Position par rapport au canvas

                    // Calcule les changements de position de la souris
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
        /// Fonction déclenchée lorsqu'on clique sur le bouton OuvrirCartes
        /// Elle ouvre la fenêtre Cartes tout en fermant la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Aaron </author>
        private void OuvrirCartes_Click(object sender, RoutedEventArgs e)
        {
            Cartes pagecreation = new Cartes(idUser, roleUser, idCampagne); // Crée une nouvelle instance de la classe Cartes avec les paramètres idUser, roleUser et idCampagne
            pagecreation.Left = this.Left; // Assigne la position horizontale de la fenêtre actuelle à la nouvelle fenêtre Cartes
            pagecreation.Top = this.Top; // Assigne la position verticale de la fenêtre actuelle à la nouvelle fenêtre Cartes
            pagecreation.Show(); // Affiche la nouvelle fenêtre Cartes
            CarteWindow.Close(); // Ferme la fenêtre actuelle
        }
        #endregion

    }
}