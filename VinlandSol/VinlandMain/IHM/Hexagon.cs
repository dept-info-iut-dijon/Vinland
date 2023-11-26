using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace VinlandSol.IHM
{
    public class Hexagon
    {
        private double x;
        private double y;
        private Terrain associatedTerrain;
        private BitmapImage imageSource;


        public double X { get; set; }
        public double Y { get; set; }
        public string ImagePath { get; set; }
        public Terrain AssociatedTerrain { get; set; }
        public Image ImageControl { get; set; }

        /// <summary>
        /// Chemin vers l'image hexagone
        /// </summary>
        public BitmapImage ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }
        public void UpdateImage()
        {
            imageSource = new BitmapImage(new Uri("pack://application:,,,/VinlandSol;component/IHM/Media/Resources/" + ImagePath));
        }

        public void UpdateTerrain(Terrain newTerrain)
        {
            AssociatedTerrain = newTerrain;
            ImagePath = newTerrain.ImagePath;
        }

        /// <summary>
        /// Créé une instance d'hexagone au coordonnées (du canvas)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Hexagon(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}