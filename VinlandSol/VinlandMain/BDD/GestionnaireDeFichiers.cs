using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.BDD
{
    /// <summary>
    /// Singleton responsable de la gestion des fichiers de sauvegarde
    /// </summary>
    public class GestionnaireDeFichiers
    {

        #region Setup Singleton

        private static GestionnaireDeFichiers _instance; // Instance interne du singleton

        /// <summary>
        /// Constructeur de la classe 
        /// </summary>
        private GestionnaireDeFichiers()
        { 
            SetupFichiers();
        }

        /// <summary>
        /// Instance Unique accessible depuis l'extérieur
        /// </summary>
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

        #region Setup Fichiers

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
        ///  Créé le fichier de sauvegarde si il n'existe pas ( ou l'écrase si incorrect )
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

        #endregion

        #region Save

        /// <summary>
        /// Méthode Générique - Sauvegarde la liste donnée dans le fichier 
        /// </summary>
        /// <typeparam name="T">Classe Type (par exemple Joueur)</typeparam>
        /// <param name="liste">liste contenant les instances T</param>
        /// <param name="filePath">chemin vers le fichier correspondant a T</param>
        public void Save<T>(List<T> liste, string filePath)
        {
            foreach (var item in liste) // Pour toutes les instances de T
            {
                string line = GetFormattedLine(item);
                WriteToFile(filePath, line);
            }
        }

        /// <summary>
        /// Transforme les instances en string afin de les mettre dans le fichier
        /// </summary>
        /// <typeparam name="T">Classe Type (par exemple Joueur)</typeparam>
        /// <param name="item">instance de T</param>
        /// <returns></returns>
        private string GetFormattedLine<T>(T item) 
        {
            PropertyInfo[] properties = typeof(T).GetProperties(); // Récupère les properties de l'instance
            return string.Join(",", Array.ConvertAll(properties, p => p.GetValue(item)?.ToString()));
        }

        /// <summary>
        /// Ecris une ligne dans le fichier donné
        /// </summary>
        /// <param name="filePath">le path du fichier</param>
        /// <param name="line">la ligne a écrire</param>
        private void WriteToFile(string filePath, string line)
        {
            using (StreamWriter sw = File.AppendText(filePath)) sw.WriteLine(line);
        }

        #endregion

        #region Load

        /// <summary>
        /// Méthode Générique - Charge les données du fichier dans une liste d'instances T
        /// </summary>
        /// <typeparam name="T">Classe Type (par exemple Joueur)</typeparam>
        /// <param name="filePath">Chemin vers le fichier correspondant à T</param>
        /// <returns>Liste d'instances T</returns>
        public List<T> Load<T>(string filePath)
        {
            List<T> instances = new List<T>();

            if (File.Exists(filePath))
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    T instance = GetInstanceFromLine<T>(line);
                    if (instance != null)
                    {
                        instances.Add(instance);
                    }
                }
            }
            return instances;
        }

        /// <summary>
        /// Transforme une ligne de texte en une instance de la classe T
        /// </summary>
        /// <typeparam name="T">Classe Type (par exemple Joueur)</typeparam>
        /// <param name="line">Ligne de texte</param>
        /// <returns>Instance de la classe T</returns>
        private T GetInstanceFromLine<T>(string line)
        {
            string[] values = line.Split(',');

            if (values.Length == 0)
            {
                return default; // Retourne la valeur par défaut de T si la ligne est vide
            }

            T instance = Activator.CreateInstance<T>();

            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < Math.Min(properties.Length, values.Length); i++)
            {
                Type propertyType = properties[i].PropertyType;

                // Gestion des types spéciaux comme DateTime
                if (propertyType == typeof(DateTime))
                {
                    DateTime parsedDate;
                    if (DateTime.TryParse(values[i], out parsedDate))
                    {
                        properties[i].SetValue(instance, parsedDate);
                    }
                }
                else
                {
                    // Convertit la valeur de string au type de propriété
                    object convertedValue = Convert.ChangeType(values[i], propertyType);
                    properties[i].SetValue(instance, convertedValue);
                }
            }

            return instance;
        }

        #endregion
    }
}
