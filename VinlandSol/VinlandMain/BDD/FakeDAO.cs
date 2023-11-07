using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinlandSol.IHM;
using VinlandSol.Métier;
using Carte = VinlandSol.Métier.Carte;

namespace VinlandSol.BDD
{
    class FakeDAO
    {
        private List<Joueur> joueurs;
        private List<MJ> mjs;
        private List<Campagne> campagnes;
        private List<Personnage> personnages;
        private List<Carte> cartes;


        public FakeDAO()
        {
            joueurs = new List<Joueur>();
            mjs = new List<MJ>();
            campagnes = new List<Campagne>();
            personnages = new List<Personnage>();
            cartes = new List<Carte>();
        }

        public void CreateJoueur(string nom, string mdp)
        {
            int id =  joueurs.Count+1;
            Joueur newUser = new Joueur(id, nom, mdp);
            joueurs.Add(newUser);
        }

        public bool VerifyUserAccount(string username, string password)
        {
            return (joueurs.Any(account => account.Nom == username && account.Mdp == password) || mjs.Any(account => account.Nom == username && account.Mdp == password));
        }

        public void AjouterJoueur(Joueur joueur)
        {
            joueurs.Add(joueur);
        }

        public List<Joueur> GetJoueurs()
        {
            return joueurs;
        }

        public void AjouterMJ(string nom, string mdp)
        {
            int id = mjs.Count + 1;
            MJ mj = new MJ(id, nom, mdp);
            mjs.Add(mj);
        }

        public List<MJ> GetMJs()
        {
            return mjs;
        }

        public void AjouterCampagne(Campagne campagne)
        {
            campagnes.Add(campagne);
        }

        public List<Campagne> GetCampagnes()
        {
            return campagnes;
        }

        public void AjouterPersonnage(Personnage personnage)
        {
            personnages.Add(personnage);
        }

        public List<Personnage> GetPersonnages()
        {
            return personnages;
        }
    }
}
