
using VinlandSol.BDD;
using VinlandSol.Métier;

namespace UnitTestVinland
{
    public class UnitTestDAO
    {
        private FakeDAO fakeDAO = FakeDAO.Instance;


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

            fakeDAO.DeleteJoueur(joueur.ID); // Remet le FakeDAO à son état d'origine d'avant-test 
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

            fakeDAO.DeleteMJ(mj.ID); // Remet le FakeDAO à son état d'origine d'avant-test 
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

            fakeDAO.DeletePersonnage(personnage.ID); // Remet le FakeDAO à son état d'origine d'avant-test 
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

            fakeDAO.DeleteCarte(carte.Id); // Remet le FakeDAO à son état d'origine d'avant-test 
        }

        /// <summary>
        /// Vérifie que la méthode `CreateCampagne` crée une campagne avec le nom spécifié.
        /// </summary>
        [Fact]
        public void Test_CreateCampagne()
        {
            
            string nom = "Campagne 1";

            fakeDAO.CreateCampagne(nom);

            Campagne campagne = fakeDAO.GetCampagnes().Last();
            Assert.Equal(nom, campagne.Nom);

            fakeDAO.DeleteCampagne(campagne.ID); // Remet le FakeDAO à son état d'origine d'avant-test 
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
           
            string nom = "Campagne 1";
            fakeDAO.CreateCampagne(nom);
            
            fakeDAO.DeleteCampagne(1);
            
            List<Campagne> campagnes = fakeDAO.GetCampagnes();
            Assert.Empty(campagnes);
        }



    }
}