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

        public int ID { get; set; }
        public string Nom { get; set; }
        public int IDJoueur { get; set; } // L'id du Joueur propriétaire de ce Personnage
        public int IDCampagne { get; set; } // L'id de la Campagne dont le Personnage fait partie

        #endregion

        #region Constructeur

        public Personnage() { }

        public Personnage(int id, string nom, int idjoueur, int idcampagne)
        {
            this.ID = id;
            this.Nom = nom;
            this.IDJoueur = idjoueur;
            this.IDCampagne = idcampagne;
        }

        #endregion
    }

}
