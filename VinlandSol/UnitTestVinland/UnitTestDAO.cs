using VinlandMain.IHM;
using VinlandSol.BDD;
using VinlandSol.Métier;

namespace UnitTestVinland
{
    public class UnitTestDAO
    {
        private FakeDAO fakeDAO = FakeDAO.Instance;

        public UnitTestDAO() 
        {
            fakeDAO.ClearLists();
        }

        /// <summary>
        /// Vérifie que la méthode `CreateJoueur` crée un joueur avec les informations spécifiées.
        /// </summary>
        [Fact]
        public void Test_CreateJoueur()
        {
            string nom = "Joueur 1";
            string mdp = "motdepasse";

            fakeDAO.CreateJoueur(nom, mdp);

            Joueur joueur = fakeDAO.GetJoueurs().Last();
            Assert.Equal(nom, joueur.Nom);
            Assert.Equal(mdp, joueur.Mdp);

            fakeDAO.ClearLists(); 
        }

        /// <summary>
        /// Vérifie que la méthode `CreateMJ` crée un MJ avec les informations spécifiées.
        /// </summary>
        [Fact]
        public void Test_CreateMJ()
        {
            string nom = "MJ 1";
            string mdp = "motdepasse";

            fakeDAO.CreateMJ(nom, mdp);

            MJ mj = fakeDAO.GetMJs().Last();
            Assert.Equal(nom, mj.Nom);
            Assert.Equal(mdp, mj.Mdp);

            fakeDAO.ClearLists(); // Remet le FakeDAO à son état d'origine d'avant-test 
        }

        /// <summary>
        /// Vérifie que la méthode `CreatePersonnage` crée un personnage avec les informations spécifiées.
        /// </summary>
        [Fact]
        public void Test_CreatePersonnage()
        {
            string nom = "Personnage 1";
            int idJoueur = 1;
            int idCampagne = 1;

            fakeDAO.CreatePersonnage(nom, idJoueur, idCampagne);

            Personnage personnage = fakeDAO.GetPersonnages().Last();
            Assert.Equal(nom, personnage.Nom);
            Assert.Equal(idJoueur, personnage.IDJoueur);
            Assert.Equal(idCampagne, personnage.IDCampagne);

            fakeDAO.ClearLists(); // Remet le FakeDAO à son état d'origine d'avant-test 
        }

        /// <summary>
        /// Vérifie que la méthode `CreateCarte` crée une carte avec les informations spécifiées.
        /// </summary>
        [Fact]
        public void Test_CreateCarte()
        {
            
            string nom = "Carte 1";
            int hauteur = 100;
            int largeur = 200;
            int idCampagne = 1;
            
            fakeDAO.CreateCarte(nom, hauteur, largeur, idCampagne);
            
            Carte carte = fakeDAO.GetCartes().Last();
            Assert.Equal(nom, carte.Nom);
            Assert.Equal(hauteur, carte.Hauteur);
            Assert.Equal(largeur, carte.Largeur);
            Assert.Equal(idCampagne, carte.IDCampagne);

            fakeDAO.ClearLists(); // Remet le FakeDAO à son état d'origine d'avant-test 
        }

        /// <summary>
        /// Vérifie que la méthode `CreateCampagne` crée une campagne avec le nom spécifié.
        /// </summary>
        [Fact]
        public void Test_CreateCampagne()
        {
            
            string nom = "Campagne Create";

            fakeDAO.CreateCampagne(nom);

            Campagne campagne = fakeDAO.GetCampagnes().Last();
            Assert.Equal(nom, campagne.Nom);

            fakeDAO.ClearLists(); // Remet le FakeDAO à son état d'origine d'avant-test 
        }

        /// <summary>
        /// Vérifie que la méthode `DeleteJoueur` supprime le joueur avec l'identifiant spécifié.
        /// </summary>
        [Fact]
        public void Test_DeleteJoueur()
        {
            
            string nom = "Joueur 1";
            string mdp = "motdepasse";
            fakeDAO.CreateJoueur(nom, mdp); // Initialisation du joueur à supprimer 
            
            fakeDAO.DeleteJoueur(1);

            List<Joueur> joueurs = fakeDAO.GetJoueurs();
            Assert.Empty(joueurs);
        }

        /// <summary>
        /// Vérifie que la méthode `DeleteMJ` supprime le MJ avec l'identifiant spécifié.
        /// </summary>
        [Fact]
        public void Test_DeleteMJ()
        {
            
            string nom = "MJ 1";
            string mdp = "motdepasse";
            fakeDAO.CreateMJ(nom, mdp); // Initialisation du mj à supprimer 
 
            fakeDAO.DeleteMJ(1);

            List<MJ> mjs = fakeDAO.GetMJs();
            Assert.Empty(mjs);
        }

        /// <summary>
        /// Vérifie que la méthode `DeletePersonnage` supprime le personnage avec l'identifiant spécifié.
        /// </summary>
        [Fact]
        public void Test_DeletePersonnage()
        {
            
            string nom = "Personnage 1";
            int idJoueur = 1;
            int idCampagne = 1;
            fakeDAO.CreatePersonnage(nom, idJoueur, idCampagne); // Initialisation du personnage à supprimer 

            fakeDAO.DeletePersonnage(1);

            List<Personnage> personnages = fakeDAO.GetPersonnages();
            Assert.Empty(personnages);
        }

        /// <summary>
        /// Vérifie que la méthode `DeleteCarte` supprime la carte avec l'identifiant spécifié.
        /// </summary>
        [Fact]
        public void Test_DeleteCarte()
        {
            
            string nom = "Carte 1";
            int hauteur = 100;
            int largeur = 200;
            int idCampagne = 1;
            fakeDAO.CreateCarte(nom, hauteur, largeur, idCampagne); // Initialisation de la carte à supprimer 

            fakeDAO.DeleteCarte(1);
            
            List<Carte> cartes = fakeDAO.GetCartes();
            Assert.Empty(cartes);
        }

        /// <summary>
        /// Vérifie que la méthode `DeleteCampagne` supprime la campagne avec l'identifiant spécifié.
        /// </summary>
        [Fact]
        public void Test_DeleteCampagne()
        {
           
            string nom = "Campagne Delete";
            fakeDAO.CreateCampagne(nom);
            
            fakeDAO.DeleteCampagne(1);
            
            List<Campagne> campagnes = fakeDAO.GetCampagnes();
            Assert.Empty(campagnes);
        }

        /// <summary>
        /// Vérifie que la méthode `GetJoueurs` renvoie une liste non vide.
        /// </summary>
        [Fact]
        public void Test_GetJoueurs()
        {
            fakeDAO.CreateJoueur("Joueur 1", "motdepasse");
            fakeDAO.CreateJoueur("Joueur 2", "motdepasse");

            List<Joueur> joueurs = fakeDAO.GetJoueurs();

            Assert.NotNull(joueurs);
            Assert.NotEmpty(joueurs);
            Assert.Equal(2, joueurs.Count);

            fakeDAO.ClearLists();

        }

        /// <summary>
        /// Vérifie que la méthode `GetMJs` renvoie une liste non vide.
        /// </summary>
        [Fact]
        public void Test_GetMJs()
        {
            fakeDAO.CreateMJ("MJ 1", "motdepasse");
            fakeDAO.CreateMJ("MJ 2", "motdepasse");

            List<MJ> mjs = fakeDAO.GetMJs();

            Assert.NotNull(mjs);
            Assert.NotEmpty(mjs);
            Assert.Equal(2, mjs.Count);

            fakeDAO.ClearLists();

        }

        /// <summary>
        /// Vérifie que la méthode `GetCampagnes` renvoie une liste non vide.
        /// </summary>
        [Fact]
        public void Test_GetCampagnes()
        {
            fakeDAO.CreateCampagne("Campagne GetCampagnes1");
            fakeDAO.CreateCampagne("Campagne GetCampagnes2");

            List<Campagne> campagnes = fakeDAO.GetCampagnes();

            Assert.NotNull(campagnes);
            Assert.NotEmpty(campagnes);
            Assert.Equal(2, campagnes.Count);

            fakeDAO.ClearLists();

        }

        /// <summary>
        /// Vérifie que la méthode `GetPersonnages` renvoie une liste non vide.
        /// </summary>
        [Fact]
        public void Test_GetPersonnages()
        {
            fakeDAO.CreateJoueur("Joueur 1", "motdepasse");
            fakeDAO.CreatePersonnage("Personnage 1", 1, 1);
            fakeDAO.CreatePersonnage("Personnage 2", 1, 1);

            List<Joueur> joueurs = fakeDAO.GetJoueurs();
            List<Personnage> personnages = fakeDAO.GetPersonnages();

            Assert.NotNull(personnages);
            Assert.NotEmpty(personnages);
            Assert.Equal(2, personnages.Count);

            fakeDAO.ClearLists();

        }

        /// <summary>
        /// Vérifie que la méthode `GetCartes` renvoie une liste non vide.
        /// </summary>
        [Fact]
        public void Test_GetCartes()
        {
            fakeDAO.CreateCampagne("Campagne GetCartes");
            fakeDAO.CreateCarte("Carte 1", 1, 100, 200);
            fakeDAO.CreateCarte("Carte 2", 1, 100, 200);

            List<Carte> cartes = fakeDAO.GetCartes();
            List<Campagne> campagnes = fakeDAO.GetCampagnes();

            Assert.NotNull(cartes);
            Assert.NotEmpty(cartes);
            Assert.Equal(2, cartes.Count);

            fakeDAO.ClearLists();

        }

        /// <summary>
        /// Vérifie que la méthode `UpdateCampagne()` met à jour correctement une campagne.
        /// </summary>
        [Fact]
        public void Test_UpdateCampagne()
        {
            string nomCampagne = "Campagne Update";
            Campagne campagne = new Campagne(1, nomCampagne);
            fakeDAO.CreateCampagne(nomCampagne);

            fakeDAO.UpdateCampagne(1, new Campagne(1,"Campagne Update2"));

            List<Campagne> campagnes = fakeDAO.GetCampagnes();
            Assert.Equal("Campagne Update2", campagnes[0].Nom);

            fakeDAO.ClearLists();

        }


        /// <summary>
        /// Vérifie que la méthode `UpdateCampagneName()` met à jour correctement le nom d'une campagne.
        /// </summary>
        [Fact]
        public void Test_UpdateCampagneName()
        {
            string nomCampagne = "Campagne UpdateName";
            Campagne campagne = new Campagne(1, nomCampagne);
            fakeDAO.CreateCampagne(nomCampagne);

            fakeDAO.UpdateCampagneName(1, "Campagne UpdateName2");

            List<Campagne> campagnes = fakeDAO.GetCampagnes();
            Assert.Equal("Campagne UpdateName2", campagnes[0].Nom);

            fakeDAO.ClearLists();

        }

        [Fact]
        public void Test_UpdatePersonnage()
        {
            string nomPersonnage = "Personnage 1";
            int idJoueur = 1;
            int idCampagne = 1;
            Personnage personnage = new Personnage(1, nomPersonnage, idJoueur, idCampagne);
            fakeDAO.CreatePersonnage(nomPersonnage, idJoueur, idCampagne);

            personnage.Nom = "Personnage 2";
            fakeDAO.UpdatePersonnage(1, personnage);

            fakeDAO.DeletePersonnage(1);

            List<Personnage> personnages = fakeDAO.GetPersonnages();
            Assert.Equal(0, personnages.Count);

            fakeDAO.ClearLists();
        }

        /// <summary>
        /// Vérifie que la méthode `UpdatePersonnageName()` met à jour correctement le nom du personnage.
        /// </summary>
        [Fact]
        public void Test_UpdatePersonnageName()
        {
            string nomPersonnage = "Personnage 1";
            int idJoueur = 1;
            int idCampagne = 1;
            Personnage personnage = new Personnage(1, nomPersonnage, idJoueur, idCampagne);
            fakeDAO.CreatePersonnage(nomPersonnage, idJoueur, idCampagne);
 
            personnage.Nom = "Personnage 2";
            fakeDAO.UpdatePersonnageName(1, personnage.Nom);

            List<Personnage> personnages = fakeDAO.GetPersonnages();
            Assert.Equal(1, personnages.Count);
            Assert.Equal("Personnage 2", personnages[0].Nom);

            fakeDAO.ClearLists();
        }

        /// <summary>
        /// Vérifie que la méthode `UpdateCarte()` met à jour correctement la carte.
        /// </summary>
        [Fact]
        public void Test_UpdateCarte()
        {
            
            string nomCarte = "Carte 1";
            int idCampagne = 1;
            int hauteur = 1;
            int largeur = 1;
            Carte carte = new Carte(1, nomCarte, idCampagne, 1, 1);
            fakeDAO.CreateCarte(nomCarte,hauteur, largeur, idCampagne);

            carte.Nom = "Carte 2";
            fakeDAO.UpdateCarte(1, carte);

            List<Carte> cartes = fakeDAO.GetCartes();
            Assert.Equal(1, cartes.Count);
            Assert.Equal("Carte 2", cartes[0].Nom);

            fakeDAO.ClearLists();
        }

        /// <summary>
        /// Vérifie que la méthode `UpdateCarteName()` met à jour correctement le nom de la carte.
        /// </summary>
        [Fact]
        public void Test_UpdateCarteName()
        {
            
            string nomCarte = "Carte 1";
            int idCampagne = 1;
            int hauteur = 1;
            int largeur = 1;

            Carte carte = new Carte(1, nomCarte, hauteur, largeur, idCampagne);
            fakeDAO.CreateCarte(nomCarte, hauteur, largeur, idCampagne);

            fakeDAO.UpdateCarteName(1, "Carte 2");

            List<Carte> cartes = fakeDAO.GetCartes();
            Assert.Equal(1, cartes.Count);
            Assert.Equal("Carte 2", cartes[0].Nom);

            fakeDAO.ClearLists();
        }

        /// <summary>
        /// Vérifie que la méthode `UpdateCarteVisibilite()` met à jour correctement la visibilité de la carte.
        /// </summary>
        [Fact]
        public void Test_UpdateCarteVisibilite()
        {
            
            string nomCarte = "Carte 1";
            int idCampagne = 1;
            int hauteur = 1;
            int largeur = 1;

            Carte carte = new Carte(1, nomCarte, hauteur, largeur, idCampagne);
            fakeDAO.CreateCarte(nomCarte, hauteur, largeur, idCampagne);

            fakeDAO.UpdateCarteVisibilite(1, true);

            List<Carte> cartes = fakeDAO.GetCartes();
            Assert.Equal(1, cartes.Count);
            Assert.True(cartes[0].Visibilite);

            fakeDAO.ClearLists();
        }

        [Fact]
        public void Test_VerifyUserAccount()
        {
            fakeDAO.CreateJoueur("joueur1", "mdp1");

            var (id, role) = fakeDAO.VerifyUserAccount("joueur1", "mdp1");

            Assert.Equal(1, id);
            Assert.Equal("Joueur", role);

            fakeDAO.ClearLists();


        }

        [Fact]
        public void Test_VerifyUserAccount_CompteNonTrouve()
        {

            var (id, role) = fakeDAO.VerifyUserAccount("joueurInexistant", "mdpInexistant");

            Assert.Equal(-1, id);
            Assert.Equal("Non trouvé", role);

            fakeDAO.ClearLists();
        }


    }
}