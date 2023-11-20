using System.Collections.Generic;

namespace VinlandSol.Métier
{
    /// <summary>
    /// Un user
    /// </summary>
    public class MJ : IUser
    {
        #region Propriétés

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Mdp { get; set; }
        public List<Campagne> Campagnes { get; set; } // La liste des Campagnes dont le MJ est le créateur

        #endregion

        #region Constructeur

        public MJ(int id, string nom, string mdp)
        {
            Id = id;
            Nom = nom;
            Mdp = mdp;
            Campagnes = new List<Campagne>();
        }

        #endregion
    }
}