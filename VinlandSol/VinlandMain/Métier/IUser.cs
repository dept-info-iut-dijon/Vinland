using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.Métier
{
    /// <summary>
    /// Interface d'un utilisateur
    /// </summary>
    public interface IUser
    {
        #region Propriétés

        int Id { get; set; }
        string Nom { get; set; }
        string Mdp { get; set; }

        #endregion
    }
}
