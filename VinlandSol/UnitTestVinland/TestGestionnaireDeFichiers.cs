using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinlandSol.BDD;
using VinlandSol.Métier;

namespace UnitTestVinland
{
    /// <summary>
    /// Classe de Test pour le Gestionnaire De Fichiers
    /// </summary>
    /// <author>Aaron</author>
    public class TestGestionnaireDeFichiers
    {
        #region Construct Instance
        private GestionnaireDeFichiers gestionnaireDeFichiers = GestionnaireDeFichiers.Instance;

        #endregion

        #region Setup_Test

        [Fact]
        public void SetupFichier_FichierInexistant()
        {
            #region Init
            var fileName = "Testing.txt"; // Le nom du fichier
            var header = gestionnaireDeFichiers.GetHeader<ClasseDeTest>(); // On récupère le header de notre classe de test
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datafiles", fileName); // On récupère le chemin vers le fichier
            #endregion

            #region Act
            File.Delete(fullPath); // On supprime le fichier 
            gestionnaireDeFichiers.SetupFichier(fileName, header); // On appelle le Setup - Il devrait créer le fichier à l'emplacement indiqué par fullPath
            #endregion

            #region Assert
            Assert.True(File.Exists(fullPath));
            #endregion
        }

        [Fact]
        public void SetupFichier_HeaderIncorrect()
        {
            #region Init
            var fileName = "Testing.txt"; // Le nom du fichier
            var header = gestionnaireDeFichiers.GetHeader<ClasseDeTest>(); // On récupère le header de notre classe de test
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datafiles", fileName); // On récupère le chemin vers le fichier
            #endregion

            #region Act
            File.WriteAllText(fullPath, "HeaderIncorrect"); // On écrit un header incorrect dans le fichier
            gestionnaireDeFichiers.SetupFichier(fileName, header); // On appelle le Setup - Il devrait détecter le header incorrect et recréer le fichier avec un header correct
            #endregion

            #region Assert
            Assert.Equal(header, File.ReadAllText(fullPath).Trim()); // On vérifie que le header du fichier est égal au header correct
            #endregion
        }

        #endregion

        #region Override_Test

        [Fact]
        public void Override_NoList()
        {
            #region Init
            var fileName = "Testing.txt"; // Le nom du fichier
            var header = gestionnaireDeFichiers.GetHeader<ClasseDeTest>(); // On récupère le header de notre classe de test
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datafiles", fileName); // On récupère le chemin vers le fichier
            gestionnaireDeFichiers.SetupFichier(fileName, header); // On appelle le Setup
            List<ClasseDeTestNL> overridenList = new List<ClasseDeTestNL>(); // On crée la liste que l'on va sauvegarder
            overridenList.Add(new ClasseDeTestNL("alice")); // On ajoute une instance alice
            overridenList.Add(new ClasseDeTestNL("bob")); // On ajoute une instance bob
            overridenList.Add(new ClasseDeTestNL("EvE")); // On ajoute une instance eve
            #endregion

            #region Act
            gestionnaireDeFichiers.Override<ClasseDeTestNL>(overridenList, fileName); // La liste de données est sauvegardée
            #endregion

            #region Assert
            string[] lines = File.ReadAllLines(fullPath);
            Assert.Equal("alice,01/01/1970 00:00:00", lines[1].Trim()); // On vérifie que la première ligne contient toutes les données
            Assert.Equal("bob,01/01/1970 00:00:00", lines[2].Trim()); // On vérifie que la deuxième ligne contient toutes les données
            Assert.Equal("EvE,01/01/1970 00:00:00", lines[3].Trim()); // On vérifie que la casse est gardée
            #endregion
        }

        [Fact]
        public void Override_List()
        {
            #region Init
            var fileName = "Testing.txt"; // Le nom du fichier
            var header = gestionnaireDeFichiers.GetHeader<ClasseDeTest>(); // On récupère le header de notre classe de test
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datafiles", fileName); // On récupère le chemin vers le fichier
            gestionnaireDeFichiers.SetupFichier(fileName, header); // On appelle le Setup
            List<ClasseDeTest> overridenList = new List<ClasseDeTest>(); // On crée la liste que l'on va sauvegarder
            ClasseDeTest alice = new ClasseDeTest("alice"); // On créé une instance alice
            alice.ListeI.AddRange(new List<int> { 2, 3, 1 }); // On ajoute une liste avec des int
            alice.ListeS.AddRange(new List<string> { "bob", "talc", "mexicain" }); // On ajoute une liste avec des string
            overridenList.Add(alice); // On ajoute alice à la liste
            #endregion

            #region Act
            gestionnaireDeFichiers.Override<ClasseDeTest>(overridenList, fileName); // La liste de données est sauvegardée
            #endregion

            #region Assert
            string[] lines = File.ReadAllLines(fullPath);
            Assert.Equal("alice,01/01/1970 00:00:00,2|3|1,bob|talc|mexicain", lines[1].Trim()); // On vérifie que la première ligne contient toutes les données
            #endregion
        }

        #endregion
    }

    public class ClasseDeTest
    {
        #region Propriétés

        public string Nom { get; set; } 
        public DateTime Date { get; set; } 
        public List<int> ListeI { get; set; } 
        public List<string> ListeS { get; set; } 

        #endregion

        #region Constructeur

        public ClasseDeTest(string nom)
        {
            Nom = nom;
            Date = DateTime.UnixEpoch;
            ListeI = new List<int>();
            ListeS = new List<string>();
        }

        #endregion
    }

    public class ClasseDeTestNL
    {
        #region Propriétés

        public string Nom { get; set; } 
        public DateTime Date { get; set; }

        #endregion

        #region Constructeur

        public ClasseDeTestNL(string nom)
        {
            Nom = nom;
            Date = DateTime.UnixEpoch; 
        }

        #endregion
    }
}
