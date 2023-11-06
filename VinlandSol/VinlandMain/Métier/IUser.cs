using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.Métier
{
    public interface IUser
    {
        int Id { get; set; }
        string Nom { get; set; }
        string Mdp { get; set; }
    }
}
