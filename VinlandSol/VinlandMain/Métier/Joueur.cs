﻿namespace VinlandSol.Métier
{
    public class Joueur : IUser
    {
        public int Id { get ; set; }
        public string Nom { get; set; }
        public string Mdp { get; set; }
    
        public Joueur(int id, string nom, string mdp) 
        { 
            Id = id;
            Nom = nom;
            Mdp = mdp;
        }

    }
}