using System.Collections.Generic;

namespace VinlandSol.Métier
{
    /// <summary>
    /// Un user
    /// </summary>
    public class MJ : IUser
    {
        #region Propriétés

        public int ID { get; set; }
        public string Nom { get; set; }
        public string Mdp { get; set; }
        public List<int> IDCampagnes { get; set; } // La liste des id des Campagnes dont le MJ est le créateur

        #endregion

        #region Constructeur

        public MJ() { }

        public MJ(int id, string nom, string mdp)
        {
            ID = id;
            Nom = nom;
            Mdp = mdp;
            IDCampagnes = new List<int>();
        }

        #endregion
    }
}