using System.Collections.Generic;

namespace VinlandSol.Métier
{
    /// <summary>
    /// Un joueur
    /// </summary>
    public class Joueur : IUser
    {
        #region Propriétés

        public int ID { get; set; }
        public string Nom { get; set; }
        public string Mdp { get; set; }
        public List<int> IDPersonnages { get; set; } // La liste des id des Personnages de ce Joueur

        #endregion

        #region Constructeur

        public Joueur() { }

        public Joueur(int id, string nom, string mdp)
        {
            ID = id;
            Nom = nom;
            Mdp = mdp;
            IDPersonnages = new List<int>();
        }

        #endregion
    }
}