using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VinlandSol.Métier
{
    internal class Hexagon
    {
        private double x;
        private double y;

        public Hexagon(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Image CreateImage()
        {
            Image hexagonImage = new Image();
            hexagonImage.Source = new BitmapImage(new Uri("pack://application:,,,/VinlandSol;component/IHM/Media/Resources/hexagon.png"));
            hexagonImage.Width = 70; // Hexagon width
            hexagonImage.Height = 70; // Hexagon height
            hexagonImage.MouseDown += HexagonImage_MouseLeftButtonDown;
            Canvas.SetLeft(hexagonImage, x);
            Canvas.SetTop(hexagonImage, y);

            return hexagonImage;
        }

        // Handle MouseLeftButtonDown event for hexagon

        private void HexagonImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Handle click event for the hexagon here
        }
    }
}
