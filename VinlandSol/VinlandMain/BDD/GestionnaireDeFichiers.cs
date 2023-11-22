using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.BDD
{
    public class GestionnaireDeFichiers
    {

        #region Setup Singleton

        private static GestionnaireDeFichiers _instance;

        private GestionnaireDeFichiers()
        { 
            SetupFichiers();
        }

        public static GestionnaireDeFichiers Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GestionnaireDeFichiers();
                }
                return _instance;
            }
        }

        #endregion

        /// <summary>
        /// Répertorie les fichiers à Setup avec l'entête contenant les noms des Propriétés stockées d'une instance
        /// </summary>
        private void SetupFichiers()
        {
            SetupFichier("Joueurs.txt", "Id,Nom,Mdp");
            SetupFichier("Mjs.txt", "Id,Nom,Mdp");
            SetupFichier("Campagnes.txt", "Id,Nom,DateModification");
            SetupFichier("Personnages.txt", "Id,Nom,JoueurId,CampagneId");
            SetupFichier("Carte.txt", "Id,Nom,Hauteur,Largeur,CampagneId");
        }

        /// <summary>
        ///  Créé ls fichier de sauvegarde si il n'existe pas ( ou l'écrase si incorrect )
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="header"></param>
        private void SetupFichier(string filePath, string header)
        {
            if (!File.Exists(filePath) || !File.ReadAllText(filePath).StartsWith(header))
            {
                File.WriteAllText(filePath, header); // Si le fichier n'existe pas ou que l'entete n'est pas présente - Créé ou Ecrase le fichier avec un fichier correct
            }
        }
    }
}
