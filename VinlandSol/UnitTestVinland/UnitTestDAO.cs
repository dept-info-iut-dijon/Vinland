
using VinlandMain.IHM;
using VinlandSol.BDD;
using VinlandSol.M�tier;

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
        /// V�rifie que la m�thode `CreateJoueur` cr�e un joueur avec les informations sp�cifi�es.
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
        /// V�rifie que la m�thode `CreateMJ` cr�e un MJ avec les informations sp�cifi�es.
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

            fakeDAO.ClearLists(); // Remet le FakeDAO � son �tat d'origine d'avant-test 
        }

        /// <summary>
        /// V�rifie que la m�thode `CreatePersonnage` cr�e un personnage avec les informations sp�cifi�es.
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

            fakeDAO.ClearLists(); // Remet le FakeDAO � son �tat d'origine d'avant-test 
        }

        /// <summary>
        /// V�rifie que la m�thode `CreateCarte` cr�e une carte avec les informations sp�cifi�es.
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

            fakeDAO.ClearLists(); // Remet le FakeDAO � son �tat d'origine d'avant-test 
        }

        /// <summary>
        /// V�rifie que la m�thode `CreateCampagne` cr�e une campagne avec le nom sp�cifi�.
        /// </summary>
        [Fact]
        public void Test_CreateCampagne()
        {
            
            string nom = "Campagne Create";

            fakeDAO.CreateCampagne(nom);

            Campagne campagne = fakeDAO.GetCampagnes().Last();
            Assert.Equal(nom, campagne.Nom);

            fakeDAO.ClearLists(); // Remet le FakeDAO � son �tat d'origine d'avant-test 
        }

        /// <summary>
        /// V�rifie que la m�thode `DeleteJoueur` supprime le joueur avec l'identifiant sp�cifi�.
        /// </summary>
        [Fact]
        public void Test_DeleteJoueur()
        {
            
            string nom = "Joueur 1";
            string mdp = "motdepasse";
            fakeDAO.CreateJoueur(nom, mdp); // Initialisation du joueur � supprimer 
            
            fakeDAO.DeleteJoueur(1);

            List<Joueur> joueurs = fakeDAO.GetJoueurs();
            Assert.Empty(joueurs);
        }

        /// <summary>
        /// V�rifie que la m�thode `DeleteMJ` supprime le MJ avec l'identifiant sp�cifi�.
        /// </summary>
        [Fact]
        public void Test_DeleteMJ()
        {
            
            string nom = "MJ 1";
            string mdp = "motdepasse";
            fakeDAO.CreateMJ(nom, mdp); // Initialisation du mj � supprimer 
 
            fakeDAO.DeleteMJ(1);

            List<MJ> mjs = fakeDAO.GetMJs();
            Assert.Empty(mjs);
        }

        /// <summary>
        /// V�rifie que la m�thode `DeletePersonnage` supprime le personnage avec l'identifiant sp�cifi�.
        /// </summary>
        [Fact]
        public void Test_DeletePersonnage()
        {
            
            string nom = "Personnage 1";
            int idJoueur = 1;
            int idCampagne = 1;
            fakeDAO.CreatePersonnage(nom, idJoueur, idCampagne); // Initialisation du personnage � supprimer 

            fakeDAO.DeletePersonnage(1);

            List<Personnage> personnages = fakeDAO.GetPersonnages();
            Assert.Empty(personnages);
        }

        /// <summary>
        /// V�rifie que la m�thode `DeleteCarte` supprime la carte avec l'identifiant sp�cifi�.
        /// </summary>
        [Fact]
        public void Test_DeleteCarte()
        {
            
            string nom = "Carte 1";
            int hauteur = 100;
            int largeur = 200;
            int idCampagne = 1;
            fakeDAO.CreateCarte(nom, hauteur, largeur, idCampagne); // Initialisation de la carte � supprimer 

            fakeDAO.DeleteCarte(1);
            
            List<Carte> cartes = fakeDAO.GetCartes();
            Assert.Empty(cartes);
        }

        /// <summary>
        /// V�rifie que la m�thode `DeleteCampagne` supprime la campagne avec l'identifiant sp�cifi�.
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
        /// V�rifie que la m�thode `GetJoueurs` renvoie une liste non vide.
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
        /// V�rifie que la m�thode `GetMJs` renvoie une liste non vide.
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
        /// V�rifie que la m�thode `GetCampagnes` renvoie une liste non vide.
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
        /// V�rifie que la m�thode `GetPersonnages` renvoie une liste non vide.
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
        /// V�rifie que la m�thode `GetCartes` renvoie une liste non vide.
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
        /// V�rifie que la m�thode `UpdateCampagne()` met � jour correctement une campagne.
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
        /// V�rifie que la m�thode `UpdateCampagneName()` met � jour correctement le nom d'une campagne.
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

    }
}