namespace UnitTestVinland
{
    public class UnitTestDAO
    {
        [Fact]
        public void CreateJoueur_ShouldAddNewUser()
        {
            // Arrange
            var fakeDao = new FakeDAO();

            // Act
            fakeDao.CreateJoueur("TestUser", "TestPassword");

            // Assert
            Assert.Single(fakeDao.GetJoueurs());
        }

        [Fact]
        public void VerifyUserAccount_ShouldReturnTrueForValidUser()
        {
            // Arrange
            var fakeDao = new FakeDAO();
            fakeDao.CreateJoueur("TestUser", "TestPassword");

            // Act
            var isValid = fakeDao.VerifyUserAccount("TestUser", "TestPassword");

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void VerifyUserAccount_ShouldReturnFalseForInvalidUser()
        {
            // Arrange
            var fakeDao = new FakeDAO();

            // Act
            var isValid = fakeDao.VerifyUserAccount("InvalidUser", "InvalidPassword");

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void AjouterJoueur_ShouldAddUserToList()
        {
            // Arrange
            var fakeDao = new FakeDAO();
            var joueur = new Joueur(1, "TestUser", "TestPassword");

            // Act
            fakeDao.AjouterJoueur(joueur);

            // Assert
            Assert.Single(fakeDao.GetJoueurs());
        }

        [Fact]
        public void AjouterMJ_ShouldAddMJToList()
        {
            // Arrange
            var fakeDao = new FakeDAO();

            // Act
            fakeDao.AjouterMJ("TestMJ", "TestPassword");

            // Assert
            Assert.Single(fakeDao.GetMJs());
        }

        [Fact]
        public void AjouterCampagne_ShouldAddCampagneToList()
        {
            // Arrange
            var fakeDao = new FakeDAO();
            var campagne = new Campagne(/* Campagne parameters */);

            // Act
            fakeDao.AjouterCampagne(campagne);

            // Assert
            Assert.Single(fakeDao.GetCampagnes());
        }

        [Fact]
        public void AjouterPersonnage_ShouldAddPersonnageToList()
        {
            // Arrange
            var fakeDao = new FakeDAO();
            var personnage = new Personnage(/* Personnage parameters */);

            // Act
            fakeDao.AjouterPersonnage(personnage);

            // Assert
            Assert.Single(fakeDao.GetPersonnages());
        }
    }
}