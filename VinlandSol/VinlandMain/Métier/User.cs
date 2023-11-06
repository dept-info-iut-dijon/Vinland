using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.Métier
{
    public class User
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Mdp { get; set; }

        public int? ID_Joueur { get; set; }
        public Joueur Joueur { get; set; }


        public int? ID_MJ { get; set; }
        public MJ MJ { get; set; }

        public User(int id, string nom, string mdp) { }
    }

    public class Joueur
    {
        public int ID { get; set; }
    }

    public class MJ
    {
        public int ID { get; set; }
    }
}
