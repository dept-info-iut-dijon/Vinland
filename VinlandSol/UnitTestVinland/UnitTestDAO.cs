namespace UnitTestVinland
{
    public class UnitTestDAO
    {
        private FakeDAO fakedao;

        public UnitTestDAO()
        {
            fakeDAO = new FakeDAO();
        }

        /// <summary>
        /// V�rifie que la m�thode `CreateJoueur` cr�e un joueur avec les informations sp�cifi�es.
        /// </summary>
        [Fact]
        public void Test_CreateJoueur()
        {
            // Arrange
            string nom = "Joueur 1";
            string mdp = "motdepasse";

            // Act
            fakeDAO.CreateJoueur(nom, mdp);

            // Assert
            List<Joueur> joueurs = fakeDAO.GetJoueurs();
            Assert.Equal(1, joueurs.Count);
            Assert.Equal(nom, joueurs[0].Nom);
            Assert.Equal(mdp, joueurs[0].MotDePasse);
        }

        /// <summary>
        /// V�rifie que la m�thode `CreateMJ` cr�e un MJ avec les informations sp�cifi�es.
        /// </summary>
        [Fact]
        public void Test_CreateMJ()
        {
            // Arrange
            string nom = "MJ 1";
            string mdp = "motdepasse";

            // Act
            fakeDAO.CreateMJ(nom, mdp);

            // Assert
            List<MJ> mjs = fakeDAO.GetMJs();
            Assert.Equal(1, mjs.Count);
            Assert.Equal(nom, mjs[0].Nom);
            Assert.Equal(mdp, mjs[0].MotDePasse);
        }

        /// <summary>
        /// V�rifie que la m�thode `CreatePersonnage` cr�e un personnage avec les informations sp�cifi�es.
        /// </summary>
        [Fact]
        public void Test_CreatePersonnage()
        {
            // Arrange
            string nom = "Personnage 1";
            int idJoueur = 1;
            int idCampagne = 1;

            // Act
            fakeDAO.CreatePersonnage(nom, idJoueur, idCampagne);

            // Assert
            List<Personnage> personnages = fakeDAO.GetPersonnages();
            Assert.Equal(1, personnages.Count);
            Assert.Equal(nom, personnages[0].Nom);
            Assert.Equal(idJoueur, personnages[0].IdJoueur);
            Assert.Equal(idCampagne, personnages[0].IdCampagne);
        }

        /// <summary>
        /// V�rifie que la m�thode `CreateCarte` cr�e une carte avec les informations sp�cifi�es.
        /// </summary>
        [Fact]
        public void Test_CreateCarte()
        {
            // Arrange
            string nom = "Carte 1";
            int hauteur = 100;
            int largeur = 200;
            int idCampagne = 1;

            // Act
            fakeDAO.CreateCarte(nom, hauteur, largeur, idCampagne);

            // Assert
            List<Carte> cartes = fakeDAO.GetCartes();
            Assert.Equal(1, cartes.Count);
            Assert.Equal(nom, cartes[0].Nom);
            Assert.Equal(hauteur, cartes[0].Hauteur);
            Assert.Equal(largeur, cartes[0].Largeur);
            Assert.Equal(idCampagne, cartes[0].IdCampagne);
        }

        /// <summary>
        /// V�rifie que la m�thode `CreateCampagne` cr�e une campagne avec le nom sp�cifi�.
        /// </summary>
        [Fact]
        public void Test_CreateCampagne()
        {
            // Arrange
            string nom = "Campagne 1";

            // Act
            fakeDAO.CreateCampagne(nom);

            // Assert
            List<Campagne> campagnes = fakeDAO.GetCampagnes();
            Assert.Equal(1, campagnes.Count);
            Assert.Equal(nom, campagnes[0].Nom);
        }

        /// <summary>
        /// V�rifie que la m�thode `DeleteJoueur` supprime le joueur avec l'identifiant sp�cifi�.
        /// </summary>
        [Fact]
        public void Test_DeleteJoueur()
        {
            // Arrange
            string nom = "Joueur 1";
            string mdp = "motdepasse";
            fakeDAO.CreateJoueur(nom, mdp);

            // Act
            fakeDAO.DeleteJoueur(1);

            // Assert
            List<Joueur> joueurs = fakeDAO.GetJoueurs();
            Assert.Empty(joueurs);
        }

        /// <summary>
        /// V�rifie que la m�thode `DeleteMJ` supprime le MJ avec l'identifiant sp�cifi�.
        /// </summary>
        [Fact]
        public void Test_DeleteMJ()
        {
            // Arrange
            string nom = "MJ 1";
            string mdp = "motdepasse";
            fakeDAO.CreateMJ(nom, mdp);

            // Act
            fakeDAO.DeleteMJ(1);

            // Assert
            List<MJ> mjs = fakeDAO.GetMJs();
            Assert.Empty(mjs);
        }

        /// <summary>
        /// V�rifie que la m�thode `DeletePersonnage` supprime le personnage avec l'identifiant sp�cifi�.
        /// </summary>
        [Fact]
        public void Test_DeletePersonnage()
        {
            // Arrange
            string nom = "Personnage 1";
            int idJoueur = 1;
            int idCampagne = 1;
            fakeDAO.CreatePersonnage(nom, idJoueur, idCampagne);

            // Act
            fakeDAO.DeletePersonnage(1);

            // Assert
            List<Personnage> personnages = fakeDAO.GetPersonnages();
            Assert.Empty(personnages);
        }

        /// <summary>
        /// V�rifie que la m�thode `DeleteCarte` supprime la carte avec l'identifiant sp�cifi�.
        /// </summary>
        [Fact]
        public void Test_DeleteCarte()
        {
            // Arrange
            string nom = "Carte 1";
            int hauteur = 100;
            int largeur = 200;
            int idCampagne = 1;
            fakeDAO.CreateCarte(nom, hauteur, largeur, idCampagne);

            // Act
            fakeDAO.DeleteCarte(1);

            // Assert
            List<Carte> cartes = fakeDAO.GetCartes();
            Assert.Empty(cartes);
        }

        /// <summary>
        /// V�rifie que la m�thode `DeleteCampagne` supprime la campagne avec l'identifiant sp�cifi�.
        /// </summary>
        [Fact]
        public void Test_DeleteCampagne()
        {
            // Arrange
            string nom = "Campagne 1";
            fakeDAO.CreateCampagne(nom);

            // Act
            fakeDAO.DeleteCampagne(1);

            // Assert
            List<Campagne> campagnes = fakeDAO.GetCampagnes();
            Assert.Empty(campagnes);
        }



    }
}