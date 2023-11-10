using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AjouterCampagne(Campagne campagne)
        {
            campagnes.Add(campagne);
        }

        public void AjouterJoueur(Joueur joueur)
        {
            joueurs.Add(joueur);
        }

        public void AjouterMJ(MJ mj)
        {
            mjs.Add(mj);
        }

        public void AjouterPersonnage(Personnage personnage)
        {
            personnages.Add(personnage);
        }

        public void AjouterCarte(Carte carte)
        {
            cartes.Add(carte);
        }

        public void CreateJoueur(string nom, string mdp)
        {
            int id = joueurs.Count + 1;
            Joueur newUser = new Joueur(id, nom, mdp);
            joueurs.Add(newUser);
        }

        public void CreateMJ(string nom, string mdp)
        {
            int id = mjs.Count + 1;
            MJ newMJ = new MJ(id, nom, mdp);
            mjs.Add(newMJ);
        }

        public void CreatePersonnage(string nom, Joueur joueur, Campagne campagne)
        {
            int id = personnages.Count + 1;
            Personnage newCharacter = new Personnage(id, nom, joueur, campagne);
            personnages.Add(newCharacter);
        }

        public void CreateCarte(int hauteur, int largeur, Campagne campagne)
        {
            int id = cartes.Count + 1;
            Carte newMap = new Carte(id, hauteur, largeur, campagne);
            cartes.Add(newMap);
        }
        public void CreateCampagne(string nom)
        {
            int id = campagnes.Count + 1;
            Campagne newCampagne = new Campagne(id, nom);
            campagnes.Add(newCampagne);
        }

        public void DeletePersonnage(int id)
        {
            if (personnages.Count != 0)
            {
                for (int i = 0; i < personnages.Count; i++)
                {
                    if (personnages[i].Id == id) personnages.RemoveAt(i);
                }
            }
        }

        public void DeleteCarte(int id)
        {
            if (cartes.Count != 0)
            {
                for (int i = 0; i < cartes.Count; i++)
                {
                    if (cartes[i].Id == id) cartes.RemoveAt(i);
                }
            }
        }

        public void DeleteCampagne(int id)
        {
            if (campagnes.Count != 0)
            {
                for (int i = 0; i < campagnes.Count; i++)
                {
                    if (campagnes[i].ID == id) campagnes.RemoveAt(i);
                }
            }
        }

        public List<Campagne> GetCampagnes()
        {
            return campagnes;
        }

        /// <summary>
        /// Donne la campagne demandée
        /// </summary>
        /// <param name="id">l'id de la campagne demandée</param>
        /// <returns>Une campagne ou null</returns>
        public Campagne GetCampagne(int id)
        {
            if (campagnes.Count == 0)
            {
                return null;
            }

            Campagne campagne = null;
            for (int i = 0; i < campagnes.Count; i++)
            {
                if (campagnes[i].ID == id) campagne = campagnes[i];
            }
            return campagne;
        }

        public Personnage GetPersonnage(int id)
        {
            if (personnages.Count == 0)
            {
                return null;
            }

            Personnage character = null;
            for (int i = 0; i < personnages.Count; i++)
            {
                if (personnages[i].Id == id) character = personnages[i];
            }
            return character;
        }

        public Carte GetCarte(int id)
        {
            if (cartes.Count == 0)
            {
                return null;
            }

            Carte map = null;
            for (int i = 0; i < cartes.Count; i++)
            {
                if (cartes[i].Id == id) map = cartes[i];
            }
            return map;
        }

        public List<Joueur> GetJoueurs()
        {
            return joueurs;
        }

        public List<MJ> GetMJs()
        {
            return mjs;
        }

        public List<Personnage> GetPersonnages()
        {
            return personnages;
        }

        public void UpdateCampagne(int id, Campagne campagne)
        {
            /*
            if (campagnes.Count != 0)
            {
                for (int i = 0; i < campagnes.Count; i++)
                {
                    if (campagnes[i].ID == campagne.ID) campagnes[i] = campagne;
                }
            }
            */
            if (campagnes.Count != 0)
            {
                for (int i = 0; i < campagnes.Count; i++)
                {
                    if (campagnes[i].ID == id)
                    {
                        campagnes[i].Nom = campagne.Nom;
                        campagnes[i].DateModification = DateTime.Now;
                    }
                }
            }
        }
        public void UpdateCampagneName(int id, string newNom)
        {
            if (campagnes.Count != 0)
            {
                for (int i = 0; i < campagnes.Count; i++)
                {
                    if (campagnes[i].ID == id)
                    {
                        campagnes[i].Nom = newNom;
                        campagnes[i].DateModification = DateTime.Now;
                    }
                }
            }
        }

        // Renommez la méthode existante pour refléter sa fonction
        public void UpdateCampagneDetails(int id, Campagne campagne)
        {
            if (campagnes.Count != 0)
            {
                for (int i = 0; i < campagnes.Count; i++)
                {
                    if (campagnes[i].ID == id)
                    {
                        campagnes[i].Nom = campagne.Nom;
                        campagnes[i].DateModification = DateTime.Now;
                    }
                }
            }
        }

        public void UpdatePersonnage(int id, Personnage personnage)
        {
            if (personnages.Count != 0)
            {
                for (int i = 0; i < personnages.Count; i++)
                {
                    if (personnages[i].Id == personnage.Id) personnages[i] = personnage;
                }
            }
        }

        public void UpdateCarte(int id, Carte carte)
        {
            if (cartes.Count != 0)
            {
                for (int i = 0; i < cartes.Count; i++)
                {
                    if (cartes[i].Id == carte.Id) cartes[i] = carte;
                }
            }
        }

        public bool VerifyUserAccount(string username, string password)
        {
            return (joueurs.Any(account => account.Nom == username && account.Mdp == password) || mjs.Any(account => account.Nom == username && account.Mdp == password));
        }

    }
}
