using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.Métier
{
    /// <summary>
    /// Un personnage
    /// </summary>
    public class Personnage
    {
        #region Propriétés 

        public int Id { get; set; }
        public string Nom { get; set; }
        public Joueur Joueur { get; set; } // Le Joueur propriétaire de ce Personnage
        public Campagne Campagne { get; set; } // La Campagne dont le Personnage fait partie

        #endregion

        #region Constructeur

        public Personnage(int id, string nom, Joueur joueur, Campagne campagne)
        {
            this.Id = id;
            this.Nom = nom;
            this.Joueur = joueur;
            this.Campagne = campagne;
        }

        #endregion
    }

}
