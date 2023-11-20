using System.Collections.Generic;

namespace VinlandSol.Métier
{
    /// <summary>
    /// Un joueur
    /// </summary>
    public class Joueur : IUser
    {
        #region Propriétés

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Mdp { get; set; }
        public List<Personnage> Personnages { get; set; } // La liste des Personnages de ce Joueur

        #endregion

        #region Constructeur

        public Joueur(int id, string nom, string mdp)
        {
            Id = id;
            Nom = nom;
            Mdp = mdp;
            Personnages = new List<Personnage>();
        }

        #endregion
    }
}