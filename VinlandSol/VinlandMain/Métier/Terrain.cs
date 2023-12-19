using System.Windows.Media;

namespace VinlandSol.Métier
{
    /// <summary>
    /// Terrain
    /// </summary>
    public class Terrain
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public Color Color { get; set; }

        public Terrain() { }
        public Terrain(int id, string imagePath)
        {
            ImagePath = imagePath;
        }
    }
}