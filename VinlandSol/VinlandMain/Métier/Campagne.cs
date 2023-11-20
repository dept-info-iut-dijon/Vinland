using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinlandSol.IHM;

namespace VinlandSol.Métier
{
    /// <summary>
    /// Une Campagne
    /// </summary>
    public class Campagne
    {
        #region Propriétés

        public int ID { get; set; }
        public string Nom { get; set; } // Nom de la campagne
        public DateTime DateCreation { get; set; } // Date de la création de la campagne
        public DateTime DateModification { get; set; } // Dernière date de modification
        public int NombreCartes { get; set; }
        public int NombrePersonnages { get; set; }
        public List<Personnage> Personnages { get; set; } // Les personnages de la campagne
        public List<Carte> Cartes { get; set; } // Les cartes de la campagne
        public MJ MJ { get; set; } // Le MJ qui a créé la campagne

        #endregion

        #region Constructeur

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

        #endregion
    }
}

