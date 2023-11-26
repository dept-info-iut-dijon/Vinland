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

        public int ID { get; set; }
        public string Nom { get; set; }
        public DateTime DateCreation { get; set; } // Date de la création de la campagne
        public DateTime DateModification { get; set; } // Dernière date de modification
        public int Hauteur { get; set; } // Hauteur (en Hexagones) de la carte
        public int Largeur { get; set; } // Largeur (en Hexagones) de la carte
        public int IDCampagne { get; set; } // Id de la Campagne à laquelle la Carte appartient
        public bool Visibilite { get; set; } // La carte est elle visible aux joueurs de la campagne? 
        public List<int> IDHexagons { get; set; } // Les Hexagones de la carte
        public List<int> IDTerrains { get; set; } // Les Terrains de la carte


        #endregion

        #region Constructeur

        public Carte() { }

        public Carte(int id, string nom, int hauteur, int largeur, int idcampagne)
        {
            ID = id;
            Nom = nom;
            DateCreation = DateTime.Now;
            DateModification = DateTime.Now;
            Hauteur = hauteur;
            Largeur = largeur;
            IDCampagne = idcampagne;
            Visibilite = false;
            IDHexagons = new List<int>();
            IDTerrains = new List<int>();
        }

        #endregion
    }
}
