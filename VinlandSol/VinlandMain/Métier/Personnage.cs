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
    }

}
