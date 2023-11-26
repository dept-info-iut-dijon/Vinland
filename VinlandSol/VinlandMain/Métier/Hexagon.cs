using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace VinlandSol.Métier
{
    public class Hexagon
    {
        private Terrain associatedTerrain;
        private BitmapImage imageSource;

        public int ID { get; set; }
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
        public Hexagon(int id, double x, double y)
        {
            ID = id;
            X = x;
            Y = y;
        }
    }
}