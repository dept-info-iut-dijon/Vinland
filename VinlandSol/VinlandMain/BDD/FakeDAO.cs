using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VinlandSol.Métier;
using Carte = VinlandSol.Métier.Carte;

namespace VinlandSol.BDD
{
    /// <summary>
    /// Classe du DAO - Utilisation de .txt
    /// </summary>
    public class FakeDAO
    {
        #region Setup Singleton

        private GestionnaireDeFichiers _gestionnaireDeFichiers = GestionnaireDeFichiers.Instance;
        private static FakeDAO _instance; // Instance interne du singleton

        /// <summary>
        /// Constructeur de la classe 
        /// </summary>
        private FakeDAO()
        {
            joueurs = _gestionnaireDeFichiers.Load<Joueur>("joueurs.txt");
            mjs = _gestionnaireDeFichiers.Load<MJ>("mjs.txt");
            campagnes = new List<Campagne>();
            personnages = new List<Personnage>();
            cartes = new List<Carte>();
            
        }

        /// <summary>
        /// Instance Unique accessible depuis l'extérieur
        /// </summary>
        public static FakeDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FakeDAO(); // Cette première instance est et devrait toujours être créée dans la fenêtre Vinland.xaml
                }
                return _instance;
            }
        }

        #endregion

        #region Listes

        private List<Joueur> joueurs;
        private List<MJ> mjs;
        private List<Campagne> campagnes;
        private List<Personnage> personnages;
        private List<Carte> cartes;

        #endregion

        #region Méthodes

        #region Création

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

            _gestionnaireDeFichiers.Save(joueurs, "Joueurs.txt");
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

            _gestionnaireDeFichiers.Save(mjs, "Mjs.txt");
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
            Personnage newCharacter = new Personnage(id, nom, joueur.ID, campagne.ID);
            personnages.Add(newCharacter);
        }

        /// <summary>
        /// Ajoute une carte à la liste des cartes, permettant ainsi sa création
        /// </summary>
        /// <param name="hauteur">Hauteur d'une carte</param>
        /// <param name="largeur">Largeur d'une carte</param>
        /// <param name="campagne">Campagne visée</param>
        public void CreateCarte(int hauteur ,string nom ,int largeur ,Campagne campagne)
        {
            int id = cartes.Count + 1;
            Carte newMap = new Carte(id,nom ,hauteur, largeur, campagne.ID);
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

        #endregion

        #region Suppression

        /// <summary>
        /// Permet la suppression d'un Joueur par son identifiant 
        /// </summary>
        /// <param name="id">Identifiant permettant de différencier un Joueur</param>
        public void DeleteJoueur(int id)
        {
            if (joueurs.Count != 0)
            {
                for (int i = 0; i < joueurs.Count; i++)
                {
                    if (joueurs[i].ID == id) joueurs.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Permet la suppression d'un MJ par son identifiant 
        /// </summary>
        /// <param name="id">Identifiant permettant de différencier un MJ</param>
        public void DeleteMJ(int id)
        {
            if (mjs.Count != 0)
            {
                for (int i = 0; i < mjs.Count; i++)
                {
                    if (mjs[i].ID == id) mjs.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Permet la suppression d'un personnage par son identifiant 
        /// </summary>
        /// <param name="id">Identifiant permettant de différencier un personnage</param>
        public void DeletePersonnage(int id)
        {
            if (personnages.Count != 0)
            {
                for (int i = 0; i < personnages.Count; i++)
                {
                    if (personnages[i].ID == id) personnages.RemoveAt(i);
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

        #endregion

        #region Recupération Liste

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
        /// Donne la liste des cartes
        /// </summary>
        /// <returns>une liste</returns>
        public List<Carte> GetCartes()
        {
            return cartes;
        }

        /// <summary>
        /// Donne la liste des campagnes 
        /// </summary>
        /// <returns>une liste</returns>
        public List<Campagne> GetCampagnes()
        {
            return campagnes;
        }

        #endregion

        #region Recupération Instance

        /// <summary>
        /// Donne la joueur demandée
        /// </summary>
        /// <param name="id">l'id du joueur demandée</param>
        /// <returns>Un joueur ou null</returns>
        public Joueur GetJoueur(int id)
        {
            if (joueurs.Count == 0)
            {
                return null;
            }

            Joueur joueur = null;
            for (int i = 0; i < joueurs.Count; i++)
            {
                if (joueurs[i].ID == id) joueur = joueurs[i];
            }
            return joueur;
        }

        /// <summary>
        /// Donne le mj demandée
        /// </summary>
        /// <param name="id">l'id du mj demandée</param>
        /// <returns>Un mj ou null</returns>
        public MJ GetMJ(int id)
        {
            if (mjs.Count == 0)
            {
                return null;
            }

            MJ mj = null;
            for (int i = 0; i < mjs.Count; i++)
            {
                if (mjs[i].ID == id) mj = mjs[i];
            }
            return mj;
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
                if (personnages[i].ID == id) character = personnages[i];
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

        #endregion

        #region Update

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
                    if (personnages[i].ID == personnage.ID) personnages[i] = personnage;
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

        #endregion

        #region Login Check

        /// <summary>
        /// Permet de vérifier si un compte valide est saisi
        /// </summary>
        /// <param name="username">Nom d'utilisateur</param>
        /// <param name="password">Mot de passe</param>
        /// <returns>l'id du user trouvé</returns>
        public (int Id, string Role) VerifyUserAccount(string username, string password)
        {
            var joueur = joueurs.FirstOrDefault(account => account.Nom == username && account.Mdp == password);
            if (joueur != null)
            {
                return (joueur.ID, "Joueur");
            }

            var mj = mjs.FirstOrDefault(account => account.Nom == username && account.Mdp == password);
            if (mj != null)
            {
                return (mj.ID, "MJ");
            }

            return (-1, "Non trouvé");
        }

        /// <summary>
        /// Permet de vérifier si un nom d'utilisateur n'est pas déjà pris
        /// </summary>
        /// <param name="username">Nom d'utilisateur</param>
        /// <returns>Booléen indiquant si le nom est pris ou non</returns>
        public bool UsernameTaken(string username)
        {
            bool disponible = true;
            var joueur = joueurs.FirstOrDefault(account => account.Nom == username);
            if (joueur != null) // Le username est pris par un joueur
            {
                disponible = false;
            }
            var mj = mjs.FirstOrDefault(account => account.Nom == username);
            if (mj != null) // Le username est pris par un mj
            {
                disponible = false;
            }
            return disponible;
        }

        #endregion

        #endregion
    }
}
