using System;
using System.Collections.Generic;
using System.Linq;
using VinlandSol.Métier;
using Carte = VinlandSol.Métier.Carte;

namespace VinlandSol.BDD
{
    /// <summary>
    /// Classe du DAO
    /// </summary>
    class FakeDAO
    {
        private List<Joueur> joueurs;
        private List<MJ> mjs;
        private List<Campagne> campagnes;
        private List<Personnage> personnages;
        private List<Carte> cartes;

        /// <summary>
        /// Constructeur de la classe 
        /// </summary>
        public FakeDAO()
        {
            joueurs = new List<Joueur>();
            mjs = new List<MJ>();
            campagnes = new List<Campagne>();
            personnages = new List<Personnage>();
            cartes = new List<Carte>();
        }

        /// <summary>
        /// Permet l'ajout d'une campagne
        /// </summary>
        /// <param name="campagne">Une simple campagne</param>
        public void AjouterCampagne(Campagne campagne)
        {
            campagnes.Add(campagne);
        }

        /// <summary>
        /// Permet l'ajout d'un joueur
        /// </summary>
        /// <param name="joueur">Un simple joueur</param>
        public void AjouterJoueur(Joueur joueur)
        {
            joueurs.Add(joueur);
        }

        /// <summary>
        /// Permet l'ajout d'un maître de jeu
        /// </summary>
        /// <param name="mj"></param>
        public void AjouterMJ(MJ mj)
        {
            mjs.Add(mj);
        }

        /// <summary>
        /// Permet l'ajout de personnages
        /// </summary>
        /// <param name="personnage">Un simple personnage</param>
        public void AjouterPersonnage(Personnage personnage)
        {
            personnages.Add(personnage);
        }

        /// <summary>
        /// Permet l'ajout d'une carte 
        /// </summary>
        /// <param name="carte">Une carte</param>
        public void AjouterCarte(Carte carte)
        {
            cartes.Add(carte);
        }

        /// <summary>
        /// Ajoute un joueur à la liste des joueurs, permettant ainsi sa création
        /// </summary>
        /// <param name="nom">Nom d'un joueur</param>
        /// <param name="mdp">Mot de passe d'un joueur</param>
        public void CreateJoueur(string nom, string mdp)
        {
            int id = joueurs.Count + 1;
            Joueur newUser = new Joueur(id, nom, mdp);
            joueurs.Add(newUser);
        }

        /// <summary>
        /// Ajoute un MJ à la liste des MJ's, permettant ainsi sa création
        /// </summary>
        /// <param name="nom">Nom d'un MJ</param>
        /// <param name="mdp">Mot de passe d'un MJ</param>
        public void CreateMJ(string nom, string mdp)
        {
            int id = mjs.Count + 1;
            MJ newMJ = new MJ(id, nom, mdp);
            mjs.Add(newMJ);
        }

        /// <summary>
        /// Ajoute un personnage à la liste des personnages, permettant ainsi sa création
        /// </summary>
        /// <param name="nom">Nom d'un joueur</param>
        /// <param name="mdp">Mot de passe d'un joueur</param>
        /// <param name="campagne">Campagne souhaitée pour un personnage</param>
        public void CreatePersonnage(string nom, Joueur joueur, Campagne campagne)
        {
            int id = personnages.Count + 1;
            Personnage newCharacter = new Personnage(id, nom, joueur, campagne);
            personnages.Add(newCharacter);
        }

        /// <summary>
        /// Ajoute une carte à la liste des cartes, permettant ainsi sa création
        /// </summary>
        /// <param name="hauteur">Hauteur d'une carte</param>
        /// <param name="largeur">Largeur d'une carte</param>
        /// <param name="campagne">Campagne visée</param>
        public void CreateCarte(int hauteur, int largeur, Campagne campagne)
        {
            int id = cartes.Count + 1;
            Carte newMap = new Carte(id, hauteur, largeur, campagne);
            cartes.Add(newMap);
        }

        /// <summary>
        /// Ajoute une campagne à la liste des campagnes, permettant ainsi sa création et son accès
        /// </summary>
        /// <param name="nom">Nom d'une campagne</param>
        public void CreateCampagne(string nom)
        {
            int id = campagnes.Count + 1;
            Campagne newCampagne = new Campagne(id, nom);
            campagnes.Add(newCampagne);
        }

        /// <summary>
        /// Permet la suppression d'un personnage par son identifiant 
        /// </summary>
        /// <param name="id">Identifiant permettant de différencier une personnage</param>
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

        /// <summary>
        /// Permet la suppression d'une carte par son identifiant 
        /// </summary>
        /// <param name="id">Identifiant permettant de différencier une carte</param>
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

        /// <summary>
        /// Permet la suppression d'une campagne par son identifiant 
        /// </summary>
        /// <param name="id">Identifiant permettant de différencier une campagne</param>
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

        /// <summary>
        /// Donne la liste des campagnes 
        /// </summary>
        /// <returns>une liste</returns>
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

        /// <summary>
        /// Permet l'aquisition d'un personnage
        /// </summary>
        /// <param name="id">l'identifiant d'un personnage</param>
        /// <returns>Un personnage</returns>
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

        /// <summary>
        /// Permet l'acquisition d'une carte
        /// </summary>
        /// <param name="id">l'identifiant d'une carte</param>
        /// <returns></returns>
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

        /// <summary>
        /// Retourne la liste des joueurs
        /// </summary>
        /// <returns>Une liste</returns>
        public List<Joueur> GetJoueurs()
        {
            return joueurs;
        }

        /// <summary>
        /// Retourne la liste des MJS
        /// </summary>
        /// <returns>Une liste</returns>
        public List<MJ> GetMJs()
        {
            return mjs;
        }

        /// <summary>
        /// Retourne la liste des personnages
        /// </summary>
        /// <returns>Une liste</returns>
        public List<Personnage> GetPersonnages()
        {
            return personnages;
        }

        /// <summary>
        /// Met à jour la campagne
        /// </summary>
        /// <param name="id">Identifiant d'une campagne</param>
        /// <param name="campagne">Une campagne</param>
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

        /// <summary>
        /// Met à jour une campagne en lui attribuant un nouveau nom
        /// </summary>
        /// <param name="id">Identifiant d'une campagne</param>
        /// <param name="newNom">Nouveau nom</param>
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

        /// <summary>
        /// Mettre à jour les dates dans les campagnes
        /// </summary>
        /// <param name="id">Identifiant d'une campagne</param>
        /// <param name="campagne">une campagne</param>
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

        /// <summary>
        /// Mets à jour un personnage et ses informations
        /// </summary>
        /// <param name="id">identifiant d'un personnage</param>
        /// <param name="personnage">un personnage</param>
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

        /// <summary>
        /// Permet la mise à jour d'une carte 
        /// </summary>
        /// <param name="id">Identifiant d'une carte</param>
        /// <param name="carte">Une carte</param>
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

        /// <summary>
        /// Permet de vérifier si un compte valide est saisi
        /// </summary>
        /// <param name="username">Nom d'utilisateur</param>
        /// <param name="password">Mot de passe</param>
        /// <returns>un accès ou non</returns>
        public bool VerifyUserAccount(string username, string password)
        {
            return (joueurs.Any(account => account.Nom == username && account.Mdp == password) || mjs.Any(account => account.Nom == username && account.Mdp == password));
        }

    }
}
