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
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
        public int NombreCartes { get; set; }
        public int NombrePersonnages { get; set; }
        public List<Personnage> Personnages { get; set; }
        public List<Carte> Cartes { get; set; }


        public Campagne(int id, string nom) 
        {
            ID = id;
            Nom = nom;
            DateCreation = DateTime.Now;
            DateModification = DateTime.Now;
            NombreCartes = 0;
            NombrePersonnages = 0;
            Personnages = new List<Personnage>();
            Cartes = new List<Carte>();
        }
    }
}
