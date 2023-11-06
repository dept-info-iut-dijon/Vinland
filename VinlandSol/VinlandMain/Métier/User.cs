using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.Métier
{
    public class User
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Mdp { get; set; }
        public Joueur? Joueur { get; set; }
        public MJ? MJ { get; set; }

        public User(int id, string nom, string mdp) 
        {
            Id = id;
            Nom = nom;
            Mdp = mdp;
            Joueur = null; // Joueur = JoueurDAO.GetJoueurById(joueurId); 
            MJ = null; // MJ = MJDAO.GetMJById(mjId); 
        }
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
