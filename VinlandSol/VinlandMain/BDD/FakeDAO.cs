using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VinlandSol.IHM;
using VinlandSol.Métier;
using Carte = VinlandSol.Métier.Carte;

namespace VinlandSol.BDD
{
    /// <summary>
    /// Classe du DAO - Utilisation de .txt
    /// </summary>
    /// <author>Aaron</author>
    public class FakeDAO
    {
        #region Setup Singleton

        private GestionnaireDeFichiers _gestionnaireDeFichiers = GestionnaireDeFichiers.Instance;
        private static FakeDAO _instance; // Instance interne du singleton

        /// <summary>
        /// Constructeur de la classe 
        /// </summary>
        /// <author>Aaron</author>
        private FakeDAO()
        {
            joueurs = _gestionnaireDeFichiers.Load<Joueur>("joueurs.txt");
            mjs = _gestionnaireDeFichiers.Load<MJ>("mjs.txt");
            campagnes = _gestionnaireDeFichiers.Load<Campagne>("campagnes.txt");
            personnages = _gestionnaireDeFichiers.Load<Personnage>("personnages.txt");
            cartes = _gestionnaireDeFichiers.Load<Carte>("cartes.txt");

        }

        /// <summary>
        /// Instance Unique accessible depuis l'extérieur
        /// </summary>
        /// <author>Aaron</author>
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

        public void ClearLists()
        {
            joueurs.Clear();
            mjs.Clear();
            campagnes.Clear();
            personnages.Clear();
            cartes.Clear();

            _gestionnaireDeFichiers.SetupFichiers();
        }

        #region Création

        /// <summary>
        /// Donne le prochain id disponible par rapport aux valeurs de la liste donnée (on essaye de prendre la valeur la plus petite disponible)
        /// </summary>
        /// <typeparam name="T">classe Type</typeparam>
        /// <param name="list">la liste des T</param>
        /// <param name="getIdFunc">fonction prenant une instance de T et retournant son id</param>
        /// <returns>L'id le plus petit disponible ou -1 </returns>
        private int GetNextAvailableId<T>(List<T> list, Func<T, int> getIdFunc)
        {
            List<int> usedIds = list.Select(getIdFunc).ToList(); // On remplit la liste des id indisponibles

            for (int i = 1; i <= int.MaxValue; i++)
            {
                if (!usedIds.Contains(i))
                {
                    return i;
                }
            }

            return -1; // Aucun id n'est disponible
        }

        /// <summary>
        /// Ajoute un joueur à la liste des joueurs, permettant ainsi sa création
        /// </summary>
        /// <param name="nom">Nom d'un joueur</param>
        /// <param name="mdp">Mot de passe d'un joueur</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void CreateJoueur(string nom, string mdp)
        {
            int id = GetNextAvailableId(joueurs, joueur => joueur.ID);
            Joueur newUser = new Joueur(id, nom, mdp);
            joueurs.Add(newUser);

            _gestionnaireDeFichiers.Override(joueurs, "Joueurs.txt");
        }

        /// <summary>
        /// Ajoute un MJ à la liste des MJ's, permettant ainsi sa création
        /// </summary>
        /// <param name="nom">Nom d'un MJ</param>
        /// <param name="mdp">Mot de passe d'un MJ</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void CreateMJ(string nom, string mdp)
        {
            int id = GetNextAvailableId(mjs, mj => mj.ID);
            MJ newMJ = new MJ(id, nom, mdp);
            mjs.Add(newMJ);

            _gestionnaireDeFichiers.Override(mjs, "Mjs.txt");
        }

        /// <summary>
        /// Ajoute un personnage à la liste des personnages, permettant ainsi sa création
        /// </summary>
        /// <param name="nom">Nom du personnage</param>
        /// <param name="idJoueur">id du joueur propriétaire</param>
        /// <param name="idCampagne">id de la campagne dont le personnage fait partie</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void CreatePersonnage(string nom, int idJoueur, int idCampagne)
        {
            int id = GetNextAvailableId(personnages, personnage => personnage.ID);
            Personnage newCharacter = new Personnage(id, nom, idJoueur, idCampagne);
            personnages.Add(newCharacter);

            for (int i = 0; i < campagnes.Count; i++)
            {
                if (campagnes[i].ID == idCampagne) { campagnes[i].IDPersonnages.Add(id); break; }
            }

            for (int i = 0; i < joueurs.Count; i++ )
            {
                if (joueurs[i].ID == idJoueur) { joueurs[i].IDPersonnages.Add(id); break; }
            }

            _gestionnaireDeFichiers.Override(campagnes, "Campagnes.txt");
            _gestionnaireDeFichiers.Override(joueurs, "Joueurs.txt");
            _gestionnaireDeFichiers.Override(personnages, "Personnages.txt");
        }

        /// <summary>
        /// Ajoute une carte à la liste des cartes, permettant ainsi sa création
        /// </summary>
        /// <param name="hauteur">Hauteur d'une carte</param>
        /// <param name="largeur">Largeur d'une carte</param>
        /// <param name="idCampagne">id de la campagne don fait partie la carte</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void CreateCarte(string nom, int hauteur, int largeur , int idCampagne)
        {
            int id = GetNextAvailableId(cartes, carte => carte.ID);
            Carte newMap = new Carte(id,nom ,hauteur, largeur, idCampagne);
            cartes.Add(newMap);

            _gestionnaireDeFichiers.Override(cartes, "Cartes.txt");
        }

        /// <summary>
        /// Ajoute une campagne à la liste des campagnes, permettant ainsi sa création et son accès
        /// </summary>
        /// <param name="nom">Nom d'une campagne</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void CreateCampagne(string nom, int idMJ)
        {
            int id = GetNextAvailableId(campagnes, campagne => campagne.ID);
            Campagne newCampagne = new Campagne(id, nom, idMJ);
            campagnes.Add(newCampagne);

            for(int i = 0; i < mjs.Count; i++) 
            {
                if (mjs[i].ID == idMJ) { mjs[i].IDCampagnes.Add(id); break; }
            }

            _gestionnaireDeFichiers.Override(campagnes, "Campagnes.txt");
            _gestionnaireDeFichiers.Override(mjs, "Mjs.txt");
        }

        #endregion

        #region Suppression

        /// <summary>
        /// Permet la suppression d'un Joueur par son identifiant 
        /// </summary>
        /// <param name="id">Identifiant permettant de différencier un Joueur</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void DeleteJoueur(int id)
        {
            if (joueurs.Count != 0)
            {
                for (int i = 0; i < joueurs.Count; i++)
                {
                    if (joueurs[i].ID == id) joueurs.RemoveAt(i);
                }
            }

            _gestionnaireDeFichiers.Override(joueurs, "Joueurs.txt");
        }

        /// <summary>
        /// Permet la suppression d'un MJ par son identifiant 
        /// </summary>
        /// <param name="id">Identifiant permettant de différencier un MJ</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void DeleteMJ(int id)
        {
            if (mjs.Count != 0)
            {
                for (int i = 0; i < mjs.Count; i++)
                {
                    if (mjs[i].ID == id) mjs.RemoveAt(i);
                }
            }
            _gestionnaireDeFichiers.Override(mjs, "Mjs.txt");
        }

        /// <summary>
        /// Permet la suppression d'un personnage par son identifiant 
        /// </summary>
        /// <param name="id">Identifiant permettant de différencier un personnage</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void DeletePersonnage(int id)
        {
            Personnage personnageToRemove = personnages.FirstOrDefault(c => c.ID == id); // On cherche le personnage

            if (personnageToRemove != null) // Si on a trouvé
            {
                GetJoueur(personnageToRemove.IDJoueur).IDPersonnages.Remove(id); // On enlève la référence au personnage de la liste les références du joueur
                GetCampagne(personnageToRemove.IDCampagne).IDPersonnages.Remove(id); // On enlève la référence au personnage de la iste des références de la campagne

                personnages.Remove(GetPersonnage(id)); // Le personnage est supprimé
            }
            // On met à jour les fichiers
            _gestionnaireDeFichiers.Override(personnages, "Personnages.txt");
            _gestionnaireDeFichiers.Override(joueurs, "Joueurs.txt");
            _gestionnaireDeFichiers.Override(campagnes, "Campagnes.txt");
        }

        /// <summary>
        /// Permet la suppression d'une carte par son identifiant 
        /// </summary>
        /// <param name="id">Identifiant permettant de différencier une carte</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void DeleteCarte(int id)
        {
            if (cartes.Count != 0)
            {
                for (int i = 0; i < cartes.Count; i++)
                {
                    if (cartes[i].ID == id) cartes.RemoveAt(i);
                }
            }
            _gestionnaireDeFichiers.Override(cartes, "Cartes.txt");
        }

        /// <summary>
        /// Permet la suppression d'une campagne par son identifiant , les références à cette campagne sont aussi supprimées
        /// </summary>
        /// <param name="id">Identifiant permettant de différencier une campagne</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void DeleteCampagne(int id)
        {
            Campagne campagneToRemove = campagnes.FirstOrDefault(c => c.ID == id); // On trouve la campagne à supprimer

            if (campagneToRemove != null)
            {
                MJ mjCreateur = mjs.FirstOrDefault(mj => mj.ID == campagneToRemove.IDMJ); // Le MJ créateur de la campagne est trouvé

                if (mjCreateur != null)
                {
                    mjCreateur.IDCampagnes.Remove(id); // La référence à la campagne est supprimée de la liste du MJ
                }

                List<Personnage> personnagesToRemove = personnages.Where(p => p.IDCampagne == id).ToList(); // Les personnages de la campagne sont trouvés
                foreach (Personnage personnage in personnagesToRemove)
                {
                    foreach (Joueur joueur in joueurs)
                    {
                        joueur.IDPersonnages.RemoveAll(persID => persID == personnage.ID); // La référence au personnage est supprimée de la liste du Joueur
                    }
                    personnages.Remove(personnage); // Le personnage est supprimé
                }

                campagnes.Remove(campagneToRemove); // La campagne est supprimée

                // Mise à jour des fichiers
                _gestionnaireDeFichiers.Override(campagnes, "Campagnes.txt");
                _gestionnaireDeFichiers.Override(joueurs, "Joueurs.txt");
                _gestionnaireDeFichiers.Override(personnages, "Personnages.txt");
                _gestionnaireDeFichiers.Override(mjs, "Mjs.txt");
            }
        }

        #endregion

        #region Recupération Liste

        /// <summary>
        /// Retourne la liste des joueurs
        /// </summary>
        /// <returns>Une liste</returns>
        /// <author>Alexis(setup) + Aaron</author>
        public List<Joueur> GetJoueurs()
        {
            _gestionnaireDeFichiers.Load<Joueur>("joueurs.txt");
            return joueurs;
        }

        /// <summary>
        /// Retourne la liste des MJS
        /// </summary>
        /// <returns>Une liste</returns>
        /// <author>Alexis(setup) + Aaron</author>
        public List<MJ> GetMJs()
        {
            _gestionnaireDeFichiers.Load<MJ>("mjs.txt");
            return mjs;
        }

        /// <summary>
        /// Retourne la liste des campagnes connues de l'utilisateur donné
        /// </summary>
        /// <param name="roleUser">le rôle de l'utilisateur ("MJ" ou "Joueur")</param>
        /// <param name="idUser">l'id de l'utilisateur</param>
        /// <returns>La liste des campagnes</returns>
        /// <Author>Aaron</Author>
        public List<Campagne> GetCurrentCampagnes(string roleUser, int idUser)
        {
            // On récupère les données nécéssaires
            _gestionnaireDeFichiers.Load<Joueur>("joueurs.txt");
            _gestionnaireDeFichiers.Load<Personnage>("personnages.txt");
            _gestionnaireDeFichiers.Load<MJ>("mjs.txt");
            _gestionnaireDeFichiers.Load<Campagne>("campagnes.txt");

            List<Campagne> campagnes = new List<Campagne>(); // On initialise la liste a renvoyer
            if (roleUser == "MJ") // Si l'utilisateur est un mj
            {
                MJ currentMJ = GetMJ(idUser); // On récupère le mj référencé
                for (int i = 0; i < currentMJ.IDCampagnes.Count; i++) // Pour chaque campagne que le mj a dans ses références
                {
                    campagnes.Add(GetCampagne(currentMJ.IDCampagnes[i])); // On ajoute à la liste de renvoi la campagne correpondante à l'id de référence
                }
            }
            else if (roleUser == "Joueur") // Si l'utilisateur est un joueur
            {
                Joueur currentJoueur = GetJoueur(idUser); // On récupère le joueur référencé
                for (int i = 0; i < currentJoueur.IDPersonnages.Count; i++) // Pour chaque personnage que le mj a dans ses références
                {
                    Personnage personnage = GetPersonnage(currentJoueur.IDPersonnages[i]); // On récupère chaque personnage correspondant à l'id de référence
                    campagnes.Add(GetCampagne(personnage.IDCampagne)); // On ajoute à la liste de renvoi la campagne correpondante à l'id de référence contenue dans le personnage
                }
            }
            return campagnes;
        }

        /// <summary>
        /// Retourne la liste des personnages
        /// </summary>
        /// <returns>Une liste</returns>
        /// <author>Alexis(setup) + Aaron</author>
        public List<Personnage> GetCurrentPersonnages(int idCampagne)
        {
            _gestionnaireDeFichiers.Load<Personnage>("personnages.txt");
            _gestionnaireDeFichiers.Load<Campagne>("campagnes.txt");

            List<Personnage> personnages = new List<Personnage>();
            Campagne currentCampagne = GetCampagne(idCampagne);
            for(int i = 0; i < currentCampagne.IDPersonnages.Count; i++ )
            {
                Personnage personnage = GetPersonnage(currentCampagne.IDPersonnages[i]);
                personnages.Add(personnage);
            }

            return personnages;
        }

        /// <summary>
        /// Donne la liste des cartes
        /// </summary>
        /// <returns>une liste</returns>
        /// <author>Alexis(setup) + Aaron</author>
        public List<Carte> GetCartes()
        {
            _gestionnaireDeFichiers.Load<Carte>("cartes.txt");
            return cartes;
        }


        #endregion

        #region Recupération Instance

        /// <summary>
        /// Donne la joueur demandée
        /// </summary>
        /// <param name="id">l'id du joueur demandée</param>
        /// <returns>Un joueur ou null</returns>
        /// <author>Alexis(setup) + Aaron</author>
        public Joueur GetJoueur(int id)
        {
            _gestionnaireDeFichiers.Load<Joueur>("joueurs.txt");
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
        /// <author>Alexis(setup) + Aaron</author>
        public MJ GetMJ(int id)
        {
            _gestionnaireDeFichiers.Load<MJ>("mjs.txt");
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
        /// <author>Alexis(setup) + Aaron</author>
        public Campagne GetCampagne(int id)
        {
            _gestionnaireDeFichiers.Load<Campagne>("campagnes.txt");
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
        /// <author>Alexis(setup) + Aaron</author>
        public Personnage GetPersonnage(int id)
        {
            _gestionnaireDeFichiers.Load<Personnage>("personnages.txt");
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
        /// <returns>Une carte</returns>
        /// <author>Alexis(setup) + Aaron</author>
        public Carte GetCarte(int id)
        {
            _gestionnaireDeFichiers.Load<Carte>("cartes.txt");
            if (cartes.Count == 0)
            {
                return null;
            }

            Carte map = null;
            for (int i = 0; i < cartes.Count; i++)
            {
                if (cartes[i].ID == id) map = cartes[i];
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
        /// <author>Alexis(setup) + Aaron</author>
        public void UpdateCampagne(int id, Campagne campagne)
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
            _gestionnaireDeFichiers.Override(campagnes, "Campagnes.txt");
        }

        /// <summary>
        /// Met à jour une campagne en lui attribuant un nouveau nom
        /// </summary>
        /// <param name="id">Identifiant d'une campagne</param>
        /// <param name="newNom">Nouveau nom</param>
        /// <author>Alexis(setup) + Aaron</author>
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
            _gestionnaireDeFichiers.Override(campagnes, "Campagnes.txt");
        }

        /// <summary>
        /// Mets à jour un personnage et ses informations
        /// </summary>
        /// <param name="id">identifiant d'un personnage</param>
        /// <param name="personnage">un personnage</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void UpdatePersonnage(int id, Personnage personnage)
        {
            if (personnages.Count != 0)
            {
                for (int i = 0; i < personnages.Count; i++)
                {
                    if (personnages[i].ID == personnage.ID) personnages[i] = personnage;
                }
            }
            _gestionnaireDeFichiers.Override(personnages, "Personnages.txt");
        }

        /// <summary>
        /// Mets à jour un personnage et ses informations
        /// </summary>
        /// <param name="id">identifiant d'un personnage</param>
        /// <param name="newNom">le nom du personnage</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void UpdatePersonnageName(int id, string newNom)
        {
            if (personnages.Count != 0)
            {
                for (int i = 0; i < personnages.Count; i++)
                {
                    if (personnages[i].ID == id)
                    {
                        personnages[i].Nom = newNom;
                    }
                }
            }
            _gestionnaireDeFichiers.Override(personnages, "Personnages.txt");
        }

        /// <summary>
        /// Permet la mise à jour d'une carte 
        /// </summary>
        /// <param name="id">Identifiant d'une carte</param>
        /// <param name="carte">Une carte</param>
        /// <author>Alexis(setup) + Aaron</author>
        public void UpdateCarte(int id, Carte carte)
        {
            if (cartes.Count != 0)
            {
                for (int i = 0; i < cartes.Count; i++)
                {
                    if (cartes[i].ID == carte.ID) cartes[i] = carte;
                }
            }
            _gestionnaireDeFichiers.Override(cartes, "Cartes.txt");
        }

        /// <summary>
        /// Mets à jour une carte et ses informations
        /// </summary>
        /// <param name="id">identifiant de la carte</param>
        /// <param name="newNom">le nom de la carte</param>
        /// <author>Aaron</author>
        public void UpdateCarteName(int id, string newNom)
        {
            if (cartes.Count != 0)
            {
                for (int i = 0; i < cartes.Count; i++)
                {
                    if (cartes[i].ID == id)
                    {
                        cartes[i].Nom = newNom;
                        cartes[i].DateModification = DateTime.Now;
                    }
                }
            }
            _gestionnaireDeFichiers.Override(cartes, "Cartes.txt");
        }

        /// <summary>
        /// Mets à jour une carte et ses informations
        /// </summary>
        /// <param name="id">identifiant de la carte</param>
        /// <param name="visible">la visibilité de la carte</param>
        /// <author>Aaron</author>
        public void UpdateCarteVisibilite(int id, bool visible)
        {
            if (cartes.Count != 0)
            {
                for (int i = 0; i < cartes.Count; i++)
                {
                    if (cartes[i].ID == id)
                    {
                        cartes[i].Visibilite = visible;
                        cartes[i].DateModification = DateTime.Now;
                    }
                }
            }
            _gestionnaireDeFichiers.Override(cartes, "Cartes.txt");
        }

        #endregion

        #region Checks

        /// <summary>
        /// Permet de vérifier si un compte valide est saisi
        /// </summary>
        /// <param name="username">Nom d'utilisateur</param>
        /// <param name="password">Mot de passe</param>
        /// <returns>l'id du user trouvé</returns>
        /// <author>Aaron</author>
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
        /// <author>Aaron</author>
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

        /// <summary>
        /// Permet de vérifier si le nom du personnage est déjà utilisé dans la campagne actuelle
        /// </summary>
        /// <param name="username"></param>
        /// <param name="idCampagne"></param>
        /// <returns>Booléen indiquant si le nom est pris ou non</returns>
        /// <author>Aaron</author>
        public bool PersonnageTaken(string username, int idCampagne) 
        {
            bool disponible = true;
            for (int i = 0; i < GetCampagne(idCampagne).IDPersonnages.Count; i++) 
            {
                if (GetPersonnage(GetCampagne(idCampagne).IDPersonnages[i]).Nom == username) { disponible = false; break; }
            }
            return disponible;
        }

        /// <summary>
        /// Permet de vérifier si le nom de la campagne est déjà utilisé pour une des autres campagnes du mj
        /// </summary>
        /// <param name="name"></param>
        /// <param name="idMJ"></param>
        /// <returns>Booléen indiquant si le nom est pris ou non</returns>
        /// <author>Aaron</author>
        public bool CampagneTaken(string name, int idMJ)
        {
            bool disponible = true;
            for (int i = 0; i < GetMJ(idMJ).IDCampagnes.Count; i++)
            {
                if (GetCampagne(GetMJ(idMJ).IDCampagnes[i]).Nom == name) { disponible = false; break; }
            }
            return disponible;
        }

        #endregion

        #endregion
    }
}
