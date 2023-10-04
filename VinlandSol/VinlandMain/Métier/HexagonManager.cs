using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VinlandSol.Métier
{
    internal class HexagonManager
    {
        private Canvas canvas;
        private List<Hexagon> hexagons = new List<Hexagon>();

        public HexagonManager(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void AddHexagon(Hexagon hexagon)
        {
            hexagons.Add(hexagon);
        }

        public void Clear()
        {
            hexagons.Clear();
        }



    }
}
