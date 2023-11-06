using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.Métier
{
    public class Personnage
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public int ID_Joueur { get; set; }
        public int ID_Campagne { get; set; }

        public Personnage(int id, string nom, int id_joueur, int id_campagne)
        {
            this.ID = id;
            this.Nom = nom;
            this.ID_Joueur = id_joueur;
            this.ID_Campagne = id_campagne;            
        }

    }

}
