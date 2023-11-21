using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.Métier
{
    /// <summary>
    /// Une carte
    /// </summary>
    public class Carte
    {
        #region Propriétés

        public int Id { get; set; }
        public string Nom { get; set; }
        public int Hauteur { get; set; } // Hauteur (en Hexagones) de la carte
        public int Largeur { get; set; } // Largeur (en Hexagones) de la carte
        public Campagne Campagne { get; set; } // Campagne à laquelle la Carte appartient

        #endregion

        #region Constructeur

        public Carte(int id, string nom, int hauteur, int largeur, Campagne campagne)
        {
            Id = id;
            Nom = nom;
            Hauteur = hauteur;
            Largeur = largeur;
            Campagne = campagne;
        }

        #endregion
    }
}
