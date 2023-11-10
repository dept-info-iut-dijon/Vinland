using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace VinlandSol.IHM
{
    public class Hexagon
    {
        private double x;
        private double y;
        private BitmapImage imageSource;
        public string ImagePath { get; set; }
        public Terrain AssociatedTerrain { get; set; }
        public Image ImageControl { get; set; }

        /// <summary>
        /// Chemin vers l'image hexagone
        /// </summary>
        public BitmapImage ImageSource
        {
            get { return new BitmapImage(new Uri("pack://application:,,,/VinlandSol;component/IHM/Media/Resources/" + ImagePath)); }
            set { imageSource = value; }
        }

        /// <summary>
        /// Créé une instance d'hexagone au coordonnées 
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