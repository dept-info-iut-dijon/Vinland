using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinlandSol.IHM;

namespace VinlandSol.Métier
{
    public class Campagne
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public List<Personnage> Personnages { get; set; }
        public List<Carte> Cartes { get; set; }

        public Campagne(int id, string nom) 
        {
            ID = id;
            Nom = nom;
            Personnages = new List<Personnage>();
            Cartes = new List<Carte>();
        }
    }
}
