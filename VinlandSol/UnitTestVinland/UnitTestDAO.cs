using VinlandMain.IHM;
using VinlandSol.BDD;
using VinlandSol.IHM;
using VinlandSol.Métier;
using Carte = VinlandSol.Métier.Carte;


namespace UnitTestVinland
{
    /// <summary>
    /// Classe de Test pour le Fake DAO
    /// </summary>
    /// <author>Alexis Paris</author>
    public class UnitTestDAO
    {
        #region Construct Instance
        private FakeDAO fakeDAO = FakeDAO.Instance;

        #endregion

        #region Create_Test 
        /// <summary>
        /// Vérifie que la méthode `CreateJoueur` crée un joueur avec les informations spécifiées.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_CreateJoueur()
        {

            #region Init
            string nom = "monNom";
            string motDePasse = "monMotDePasse";
            string nom2 = "monNom2";
            string motDePasse2 = "monMotDePasse2";
            #endregion

            #region Act
            fakeDAO.ClearLists(); // On s'assure que les fichiers sont bien vides 

            fakeDAO.CreateJoueur(nom, motDePasse);
            Joueur joueur = fakeDAO.GetJoueur(1);
            fakeDAO.CreateJoueur(nom2, motDePasse2);
            Joueur joueur2 = fakeDAO.GetJoueur(2);

            fakeDAO.ClearLists(); // Remet le FakeDAO à son état d'origine d'avant-test 
            #endregion

            #region Assert
            Assert.Equal(nom, joueur.Nom);
            Assert.Equal(motDePasse, joueur.Mdp);
            Assert.Equal(nom2, joueur2.Nom);
            Assert.Equal(motDePasse2, joueur2.Mdp);
            #endregion

        }

        /// <summary>
        /// Vérifie que la méthode `CreateMJ` crée un MJ avec les informations spécifiées.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_CreateMJ()
        {
            #region Init
            string nomMJ = "monNom";
            string MdpMJ = "monMotDePasse";
            string nomMJ2 = "monNom2";
            string MdpMJ2 = "monMotDePasse2";
            #endregion

            #region Act
            fakeDAO.ClearLists(); // On s'assure que les fichiers sont bien vides 

            fakeDAO.CreateMJ(nomMJ, MdpMJ);
            MJ mj = fakeDAO.GetMJ(1);
            fakeDAO.CreateMJ(nomMJ2, MdpMJ2);
            MJ mj2 = fakeDAO.GetMJ(2);

            fakeDAO.ClearLists(); // Remet le FakeDAO à son état d'origine d'avant-test 
            #endregion

            #region Assert
            Assert.Equal(nomMJ, mj.Nom);
            Assert.Equal(MdpMJ, mj.Mdp);
            Assert.Equal(nomMJ2 , mj2.Nom);
            Assert.Equal(MdpMJ2, mj2.Mdp);
            #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `CreateCampagne` crée une campagne avec le nom spécifié.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_CreateCampagne()
        {
            #region Init
            string nomMJ = "MJ";
            string MdpMJ = "Mdp";
            string nomCampagne = "nomCampagne";
            string nomCampagne2 = "nomCampagne2";
            #endregion

            #region Act
            fakeDAO.ClearLists(); // On s'assure que les fichiers sont bien vides 

            fakeDAO.CreateMJ(nomMJ, MdpMJ);
            MJ mj = fakeDAO.GetMJ(1);

            fakeDAO.CreateCampagne(nomCampagne, mj.ID);
            Campagne campagne = fakeDAO.GetCampagne(1);

            fakeDAO.CreateCampagne(nomCampagne2, mj.ID);
            Campagne campagne2 = fakeDAO.GetCampagne(2);

            fakeDAO.ClearLists(); // Remet le FakeDAO à son état d'origine d'avant-test 
            #endregion

            #region Assert
            Assert.Equal(nomCampagne, campagne.Nom);
            Assert.Equal(nomCampagne2, campagne2.Nom);
            Assert.Contains<int>(campagne.ID, mj.IDCampagnes);
            Assert.Contains<int>(campagne2.ID, mj.IDCampagnes);
            #endregion
        }


        /// <summary>
        /// Vérifie que la méthode `CreatePersonnage` crée un personnage avec les informations spécifiées.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_CreatePersonnage()
        {
            #region Init
            string nomMJ = "nomMJ";
            string mdpMJ = "mdpMJ";
            string nomJoueur = "nomJoueur";
            string mdpJoueur = "mdpJoueur";
            string nomCampagne = "nomCampagne";
            string nomPersonnage = "nomPersonnage";
            string nomPersonnage2 = "nomPersonnage2";
            #endregion

            #region Act
            fakeDAO.ClearLists();

            fakeDAO.CreateMJ(nomMJ, mdpMJ);
            MJ mj = fakeDAO.GetMJ(1);

            fakeDAO.CreateJoueur(nomJoueur, mdpJoueur);
            Joueur joueur = fakeDAO.GetJoueur(1);

            fakeDAO.CreateCampagne(nomCampagne, mj.ID);
            Campagne campagne = fakeDAO.GetCampagne(1);

            fakeDAO.CreatePersonnage(nomPersonnage, joueur.ID, campagne.ID);
            Personnage personnage = fakeDAO.GetPersonnage(1);

            fakeDAO.CreatePersonnage(nomPersonnage2, joueur.ID, campagne.ID);
            Personnage personnage2 = fakeDAO.GetPersonnage(2);

            fakeDAO.ClearLists();
            #endregion

            #region Assert
            Assert.Equal(nomPersonnage, personnage.Nom);
            Assert.Equal(nomPersonnage2, personnage2.Nom);
            Assert.Contains<int>(personnage.ID, joueur.IDPersonnages);
            Assert.Contains<int>(personnage2.ID, joueur.IDPersonnages);
            Assert.Contains<int>(personnage.ID, campagne.IDPersonnages);
            Assert.Contains<int>(personnage2.ID, campagne.IDPersonnages);
            #endregion

        }

        /// <summary>
        /// Vérifie que la méthode `CreateCarte` crée une carte avec les informations spécifiées.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_CreateCarte()
        {
            #region Init
            string nomMJ = "nomMJ";
            string mdpMJ = "mdpMJ";
            string nomCampagne = "nomCampagne";
            string nomCarte = "nomCarte";
            string nomCarte2 = "nomCarte2";
            int largeur = 3;
            int hauteur = 5;
            #endregion

            #region Act
            fakeDAO.ClearLists();

            fakeDAO.CreateMJ(nomMJ, mdpMJ);
            MJ mj = fakeDAO.GetMJ(1);

            fakeDAO.CreateCampagne(nomCampagne, mj.ID);
            Campagne campagne = fakeDAO.GetCampagne(1);

            fakeDAO.CreateCarte(nomCarte, hauteur, largeur, campagne.ID);
            Carte carte = fakeDAO.GetCarte(1);

            fakeDAO.CreateCarte(nomCarte2, hauteur, largeur, campagne.ID);
            Carte carte2 = fakeDAO.GetCarte(2);

            fakeDAO.ClearLists();

            #endregion

            #region Assert
            Assert.Equal(nomCarte, carte.Nom);
            Assert.Equal(nomCarte2, carte2.Nom);
            Assert.Contains<int>(carte.ID, campagne.IDCartes);
            Assert.Contains<int>(carte2.ID, campagne.IDCartes);
            
            #endregion
        }


        #endregion

        #region Delete_Test

        /// <summary>
        /// Vérifie que la méthode `DeleteJoueur` supprime le joueur avec l'identifiant spécifié.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_DeleteJoueur()
        {
            #region Init
            string nom = "monNom";
            string motDePasse = "monMotDePasse";
            string nom2 = "monNom2";
            string motDePasse2 = "monMotDePasse2";

            fakeDAO.ClearLists();

            fakeDAO.CreateJoueur(nom, motDePasse);
            fakeDAO.CreateJoueur(nom2, motDePasse2);
            #endregion

            #region Act
            int idJoueur1 = fakeDAO.GetJoueur(1).ID;
            int idJoueur2 = fakeDAO.GetJoueur(2).ID;

            fakeDAO.DeleteJoueur(idJoueur1);

            Joueur isNull = fakeDAO.GetJoueur(idJoueur1); // Joueur nul
            Joueur isNotNull = fakeDAO.GetJoueur(idJoueur2); // Joueur non nul

            fakeDAO.ClearLists();
            #endregion

            #region Assert

            Assert.Null(isNull); // Assert que le joueur 1 a été supprimé
            
            Assert.NotNull(isNotNull);// Assert que le joueur 2 est toujours présent
            #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `DeleteMJ` supprime le MJ avec l'identifiant spécifié.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_DeleteMJ()
        {
            #region Init
            string nom = "monNom";
            string motDePasse = "monMotDePasse";
            string nom2 = "monNom2";
            string motDePasse2 = "monMotDePasse2";

            fakeDAO.ClearLists();

            fakeDAO.CreateMJ(nom, motDePasse);
            fakeDAO.CreateMJ(nom2, motDePasse2);
            #endregion

            #region Act
            int idMJ1 = fakeDAO.GetMJ(1).ID;
            int idMJ2 = fakeDAO.GetMJ(2).ID;

            fakeDAO.DeleteMJ(idMJ1);

            MJ isNull = fakeDAO.GetMJ(idMJ1); // Maître du jeu nul
            MJ isNotNull = fakeDAO.GetMJ(idMJ2); // Maître du jeu non nul

            fakeDAO.ClearLists();
            #endregion

            #region Assert
            Assert.Null(isNull); // Assert que le MJ 1 a été supprimé

            Assert.NotNull(isNotNull);// Assert que le MJ 2 est toujours présent
            #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `DeleteCampagne` supprime la campagne avec l'identifiant spécifié.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_DeleteCampagne()
        {
            #region Init
            string nomMJ = "MJ";
            string MdpMJ = "Mdp";
            string nomCampagne = "nomCampagne";
            string nomCampagne2 = "nomCampagne2";

            fakeDAO.ClearLists(); // On s'assure que les fichiers sont bien vides 

            fakeDAO.CreateMJ(nomMJ, MdpMJ);
            MJ mj = fakeDAO.GetMJ(1);

            fakeDAO.CreateCampagne(nomCampagne, mj.ID);
            Campagne campagne = fakeDAO.GetCampagne(1);

            fakeDAO.CreateCampagne(nomCampagne2, mj.ID);
            Campagne campagne2 = fakeDAO.GetCampagne(2);
            #endregion

            #region Act
            int idCampagne = fakeDAO.GetCampagne(1).ID;
            int idCampagne2 = fakeDAO.GetCampagne(2).ID;

            fakeDAO.DeleteCampagne(idCampagne);

            Campagne isNull = fakeDAO.GetCampagne(idCampagne); // Campagne nulle
            Campagne isNotNull = fakeDAO.GetCampagne(idCampagne2); // Campagne non nulle

            fakeDAO.ClearLists();
            #endregion

            #region Assert
            Assert.Null(isNull); // Assert que la 1ère campagne a été supprimée

            Assert.NotNull(isNotNull);// Assert que la seconde campagne est toujours présente
            #endregion

        }

        /// <summary>
        /// Vérifie que la méthode `DeletePersonnage` supprime le personnage avec l'identifiant spécifié.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_DeletePersonnage()
        {
            #region Init
            string nomMJ = "nomMJ";
            string mdpMJ = "mdpMJ";
            string nomJoueur = "nomJoueur";
            string mdpJoueur = "mdpJoueur";
            string nomCampagne = "nomCampagne";
            string nomPersonnage = "nomPersonnage";
            string nomPersonnage2 = "nomPersonnage2";

            fakeDAO.ClearLists();

            fakeDAO.CreateMJ(nomMJ, mdpMJ);
            MJ mj = fakeDAO.GetMJ(1);

            fakeDAO.CreateJoueur(nomJoueur, mdpJoueur);
            Joueur joueur = fakeDAO.GetJoueur(1);

            fakeDAO.CreateCampagne(nomCampagne, mj.ID);
            Campagne campagne = fakeDAO.GetCampagne(1);

            fakeDAO.CreatePersonnage(nomPersonnage, joueur.ID, campagne.ID);
            Personnage personnage = fakeDAO.GetPersonnage(1);

            fakeDAO.CreatePersonnage(nomPersonnage2, joueur.ID, campagne.ID);
            Personnage personnage2 = fakeDAO.GetPersonnage(2);

            #endregion

            #region Act
            int idPersonnage = fakeDAO.GetPersonnage(1).ID;
            int idPersonnage2 = fakeDAO.GetPersonnage(2).ID;

            fakeDAO.DeletePersonnage(idPersonnage);

            Personnage isNull = fakeDAO.GetPersonnage(idPersonnage); // Campagne nulle
            Personnage isNotNull = fakeDAO.GetPersonnage(idPersonnage2); // Campagne non nulle

            fakeDAO.ClearLists();
            #endregion

            #region Assert
            Assert.Null(isNull); // Assert que le 1er personnage a été supprimé
            Assert.NotNull(isNotNull);// Assert que le 2ème personnage est toujours présent
            #endregion

        }

        /// <summary>
        /// Vérifie que la méthode `DeleteCarte` supprime la carte avec l'identifiant spécifié.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_DeleteCarte()
        {
            #region Init
            string nomMJ = "nomMJ";
            string mdpMJ = "mdpMJ";
            string nomCampagne = "nomCampagne";
            string nomCarte = "nomCarte";
            string nomCarte2 = "nomCarte2";
            int largeur = 3;
            int hauteur = 5;

            fakeDAO.ClearLists();

            fakeDAO.CreateMJ(nomMJ, mdpMJ);
            MJ mj = fakeDAO.GetMJ(1);

            fakeDAO.CreateCampagne(nomCampagne, mj.ID);
            Campagne campagne = fakeDAO.GetCampagne(1);

            fakeDAO.CreateCarte(nomCarte, hauteur, largeur, campagne.ID);
            Carte carte = fakeDAO.GetCarte(1);

            fakeDAO.CreateCarte(nomCarte2, hauteur, largeur, campagne.ID);
            Carte carte2 = fakeDAO.GetCarte(2);
            #endregion

            #region Act
            int idCarte = fakeDAO.GetCarte(1).ID;
            int idCarte2 = fakeDAO.GetCarte(2).ID;

            fakeDAO.DeleteCarte(idCarte);

            Carte isNull = fakeDAO.GetCarte(idCarte); // Campagne nulle
            Carte isNotNull = fakeDAO.GetCarte(idCarte2); // Campagne non nulle

            fakeDAO.ClearLists();
            #endregion

            #region Assert
            Assert.Null(isNull); // Assert que la 1ère carte a été supprimé

            Assert.NotNull(isNotNull);// Assert que la seconde carte est toujours présente

            #endregion

        }

       

        #endregion

        #region Get_Tests

        /// <summary>
        /// Vérifie que la méthode `GetJoueurs` renvoie une liste non vide.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_GetJoueurs()
        {

             #region Init
             string nom = "monNom";
             string motDePasse = "monMotDePasse";

             fakeDAO.ClearLists();

             fakeDAO.CreateJoueur(nom, motDePasse);
             #endregion

             #region Act
             int idJoueur = fakeDAO.GetJoueur(1).ID;

             Joueur joueurTrouve = fakeDAO.GetJoueur(idJoueur);
             Joueur joueurInexistant = fakeDAO.GetJoueur(100);

             fakeDAO.ClearLists();

             #endregion

             #region Assert
             Assert.NotNull(joueurTrouve); // Assert que le joueur a été trouvé

             // Assert que les informations du joueur sont correctes
             Assert.Equal(nom, joueurTrouve.Nom);
             Assert.Equal(motDePasse, joueurTrouve.Mdp);
   
             Assert.Null(joueurInexistant); //Assert que le joueur inexistant n'a pas été trouvé
             
             #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `GetMJs` renvoie une liste non vide.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_GetMJs()
        {
            #region Init
            string nom = "monNom";
            string motDePasse = "monMotDePasse";

            fakeDAO.ClearLists();

            fakeDAO.CreateMJ(nom, motDePasse);
            #endregion

            #region Act
            int idMJ = fakeDAO.GetMJ(1).ID;

            MJ mjTrouve = fakeDAO.GetMJ(idMJ);
            MJ mjInexistant = fakeDAO.GetMJ(100);

            fakeDAO.ClearLists();
            #endregion

            #region Assert
            Assert.NotNull(mjTrouve); // Assert que le MJ a été trouvé

            // Assert que les informations du MJ sont correctes
            Assert.Equal(nom, mjTrouve.Nom);
            Assert.Equal(motDePasse, mjTrouve.Mdp);

            Assert.Null(mjInexistant); //Assert que le MJ inexistant n'a pas été trouvé
            #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `GetCampagnes` renvoie une liste non vide.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_GetCampagnes()
        {
            #region Init
            string nomMJ = "MJ";
            string MdpMJ = "Mdp";
            string nomCampagne = "nomCampagne";

            fakeDAO.ClearLists(); // On s'assure que les fichiers sont bien vides 

            fakeDAO.CreateMJ(nomMJ, MdpMJ);
            MJ mj = fakeDAO.GetMJ(1);

            fakeDAO.CreateCampagne(nomCampagne, mj.ID);
            #endregion

            #region Act
            int idCampagne = fakeDAO.GetCampagne(1).ID;

            Campagne campTrouve = fakeDAO.GetCampagne(idCampagne); // Campagne prévue existante
            Campagne campInexistante = fakeDAO.GetCampagne(100); // Campagne prévue inexistante

            fakeDAO.ClearLists();
            #endregion

            #region Assert
            Assert.NotNull(campTrouve); // Assert que la campagne a été trouvé

            // Assert que les informations de la campagne sont correctes
            Assert.Equal(nomCampagne, campTrouve.Nom);
            Assert.Equal(1, campTrouve.IDMJ);

            Assert.Null(campInexistante); //Assert que la campagne n'a pas été trouvé à l'id 100
            #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `GetPersonnages` renvoie une liste non vide.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_GetPersonnages()
        {
            #region Init
            string nomMJ = "nomMJ";
            string mdpMJ = "mdpMJ";
            string nomJoueur = "nomJoueur";
            string mdpJoueur = "mdpJoueur";
            string nomCampagne = "nomCampagne";
            string nomPersonnage = "nomPersonnage";
            
            fakeDAO.ClearLists();

            fakeDAO.CreateMJ(nomMJ, mdpMJ);
            MJ mj = fakeDAO.GetMJ(1);

            fakeDAO.CreateJoueur(nomJoueur, mdpJoueur);
            Joueur joueur = fakeDAO.GetJoueur(1);

            fakeDAO.CreateCampagne(nomCampagne, mj.ID);
            Campagne campagne = fakeDAO.GetCampagne(1);

            fakeDAO.CreatePersonnage(nomPersonnage, joueur.ID, campagne.ID);
            #endregion

            #region Act
            int idPersonnage = fakeDAO.GetPersonnage(1).ID;

            Personnage persoTrouve = fakeDAO.GetPersonnage(idPersonnage); // Personnage prévu existant
            Personnage persoInexistante = fakeDAO.GetPersonnage(100); // Personnage prévu inexistant

            fakeDAO.ClearLists();
            #endregion

            #region Assert
            Assert.NotNull(persoTrouve); // Assert que le personnage a été trouvé

            // Assert que les informations du personnage sont correctes
            Assert.Equal(nomPersonnage, persoTrouve.Nom);
            Assert.Equal(1, persoTrouve.IDJoueur);
            Assert.Equal(1, persoTrouve.IDCampagne);

            Assert.Null(persoInexistante); //Assert que le personnage n'a pas été trouvé à l'id 100

            #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `GetCartes` renvoie une liste non vide.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_GetCartes()
        {
            #region Init
            string nomMJ = "nomMJ";
            string mdpMJ = "mdpMJ";
            string nomCampagne = "nomCampagne";
            string nomCarte = "nomCarte";
            int largeur = 3;
            int hauteur = 5;

            fakeDAO.ClearLists();

            fakeDAO.CreateMJ(nomMJ, mdpMJ);
            MJ mj = fakeDAO.GetMJ(1);

            fakeDAO.CreateCampagne(nomCampagne, mj.ID);
            Campagne campagne = fakeDAO.GetCampagne(1);

            fakeDAO.CreateCarte(nomCarte, hauteur, largeur, campagne.ID);
            #endregion

            #region Act
            int idCarte = fakeDAO.GetCarte(1).ID;

            Carte carteTrouve = fakeDAO.GetCarte(idCarte); // Personnage prévu existant
            Carte carteInexistante = fakeDAO.GetCarte(100); // Personnage prévu inexistant

            fakeDAO.ClearLists();
            #endregion

            #region Assert
            Assert.NotNull(carteTrouve); // Assert que la carte a été trouvé

            // Assert que les informations de la carte sont correctes
            Assert.Equal(nomCarte, carteTrouve.Nom);
            Assert.Equal(1, carteTrouve.ID);
            Assert.Equal(1, carteTrouve.IDCampagne);
            Assert.Equal(5, carteTrouve.Hauteur);
            Assert.Equal(3, carteTrouve.Largeur);

            Assert.Null(carteInexistante); //Assert que la carte n'a pas été trouvé à l'id 100
            #endregion
        }
        #endregion

        #region Updates_Test
        /// <summary>
        /// Vérifie que la méthode `UpdateCampagne()` met à jour correctement une campagne.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_UpdateCampagne()
        {
            #region Init

            #endregion

            #region Act

            #endregion

            #region Assert

            #endregion
        }


        /// <summary>
        /// Vérifie que la méthode `UpdateCampagneName()` met à jour correctement le nom d'une campagne.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_UpdateCampagneName()
        {
            #region Init

            #endregion

            #region Act

            #endregion

            #region Assert

            #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `UpdatePersonnage()` met à jour correctement le nom d'un personnage
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_UpdatePersonnage()
        {
            #region Init

            #endregion

            #region Act

            #endregion

            #region Assert

            #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `UpdatePersonnageName()` met à jour correctement le nom du personnage.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_UpdatePersonnageName()
        {
            #region Init

            #endregion

            #region Act

            #endregion

            #region Assert

            #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `UpdateCarte()` met à jour correctement la carte.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_UpdateCarte()
        {
            #region Init

            #endregion

            #region Act

            #endregion

            #region Assert

            #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `UpdateCarteName()` met à jour correctement le nom de la carte.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_UpdateCarteName()
        {
            #region Init

            #endregion

            #region Act

            #endregion

            #region Assert

            #endregion
        }

        /// <summary>
        /// Vérifie que la méthode `UpdateCarteVisibilite()` met à jour correctement la visibilité de la carte.
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_UpdateCarteVisibilite()
        {
            #region Init

            #endregion

            #region Act

            #endregion

            #region Assert

            #endregion
        }
        #endregion

        #region Verify_UserAccount
        /// <summary>
        /// Teste la méthode "VerifyUserAccount"
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_VerifyUserAccount()
        {
            #region Init

            #endregion

            #region Act

            #endregion

            #region Assert

            #endregion
        }

        /// <summary>
        /// Teste la même méthode si le compte n'est pas trouvé
        /// </summary>
        /// <author>Alexis Paris</author>
        [Fact]
        public void Test_VerifyUserAccount_CompteNonTrouve()
        {
            #region Init

            #endregion

            #region Act

            #endregion

            #region Assert

            #endregion
        }

        #endregion
    }
}

