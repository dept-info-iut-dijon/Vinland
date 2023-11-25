using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using VinlandSol.Métier;

namespace VinlandSol.BDD
{
    /// <summary>
    /// Singleton responsable de la gestion des fichiers de sauvegarde
    /// </summary>
    /// <author>Aaron</author>
    public class GestionnaireDeFichiers
    {

        #region Setup Singleton

        private static GestionnaireDeFichiers _instance; // Instance interne du singleton

        /// <summary>
        /// Constructeur de la classe 
        /// </summary>
        /// <author>Aaron</author>
        private GestionnaireDeFichiers()
        { 
            SetupFichiers();
        }

        /// <summary>
        /// Instance Unique du gestionnaire de fichiers
        /// </summary>
        /// <author>Aaron</author>
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
        /// <author>Aaron</author>
        public void SetupFichiers()
        {
            SetupFichier("Joueurs.txt", GetHeader<Joueur>());
            SetupFichier("Mjs.txt", GetHeader<MJ>());
            SetupFichier("Campagnes.txt", GetHeader<Campagne>());
            SetupFichier("Personnages.txt", GetHeader<Personnage>());
            SetupFichier("Cartes.txt", GetHeader<Carte>());
        }

        /// <summary>
        ///  Créé le fichier de sauvegarde si il n'existe pas ( ou l'écrase si incorrect )
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="header"></param>
        /// <author>Aaron</author>
        private void SetupFichier(string filePath, string header)
        {
            if (!File.Exists(filePath) || !File.ReadAllText(filePath).StartsWith(header))
            {
                File.WriteAllText(filePath, header); // Si le fichier n'existe pas ou que l'entete n'est pas présente - Créé ou Ecrase le fichier avec un fichier correct
                using (StreamWriter sw = File.AppendText(filePath)) sw.WriteLine(); // On passe une ligne pour ne pas écrire la première instance sur l'entête
            }        
        }

        #endregion

        #region Save

        /// <summary>
        /// Méthode Générique - Ecrase le fichier actuel et ajoute dans le fichier les données de la nouvelle liste
        /// </summary>
        /// <typeparam name="T">Classe Type (par exemple Joueur)</typeparam>
        /// <param name="liste">liste contenant les instances T</param>
        /// <param name="filePath">chemin vers le fichier correspondant a T</param>
        /// <author>Aaron</author>
        public void Override<T>(List<T> liste, string filePath)
        {
            File.WriteAllText(filePath, GetHeader<T>()); // Setup de l'override, le fichier est recréé avec le header
            using (StreamWriter sw = File.AppendText(filePath)) sw.WriteLine(); // On passe une ligne pour ne pas écrire la première instance sur l'entête
            foreach (var item in liste) // On réécrit toutes les instances dans le fichier
            {
                string line = GetFormattedLine(item); // On transforme l'item en string
                WriteToFile(filePath, line); // On écrit la ligne dans le fichier
            }
        }

        /// <summary>
        /// Méthode Générique - Transforme les instances en string afin de les mettre dans le fichier
        /// </summary>
        /// <typeparam name="T">Classe Type (par exemple Joueur)</typeparam>
        /// <param name="item">instance de T</param>
        /// <returns>une chaine de caractères correspondant aux propriétés de l'instance de T (séparées par des virgules - les propriétés de type liste voient leurs éléments séparés par '|' )</returns>
        /// <author>Aaron</author>
        private string GetFormattedLine<T>(T item)
        {
            PropertyInfo[] properties = typeof(T).GetProperties(); // Récupère les properties de l'instance

            List<string> values = new List<string>(); // Liste temporaire contenant les données 

            foreach (var property in properties) // Pour chaque propriété de l'instance
            {
                object propertyValue = property.GetValue(item); // On récupère la valeur de la propriété

                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>)) // Si la propriété est une liste
                {
                    var list = (IEnumerable)propertyValue; // On effectue un cast pour que la liste soit considérée comme un IEnumerable et non uniquement un object
                    if (list != null && list.Cast<object>().Any()) // Si la liste n'est pas vide
                    {
                        string listValues = string.Join("|", list.Cast<object>().Select(x => x.ToString())); // Transforme les éléments de la liste en string, séparés par le caractère "|"
                        values.Add(listValues); // On ajoute la liste transformée en string à la liste des string a rassembler
                    }
                    else 
                    {
                        values.Add("vide"); // Ajoute "vide" si la liste est vide
                    }
                }
                else
                {
                    values.Add(propertyValue?.ToString()); // On transforme la valeur en string et on l'ajoute à la liste des string a rassembler
                }
            }
            return string.Join(",", values); // On assemble les strings correspondants aux valeurs de l'instance en un seul et unique string, on sépare chaque string par une virgule
        }

        /// <summary>
        /// Ecris une ligne dans le fichier donné
        /// </summary>
        /// <param name="filePath">le path du fichier</param>
        /// <param name="line">la ligne a écrire</param>
        /// <author>Aaron</author>
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
        /// <author>Aaron</author>
        public List<T> Load<T>(string filePath)
        {
            List<T> instances = new List<T>();

            if (File.Exists(filePath)) // Si le fichier existe
            {
                bool premièreLigneSkip = true; // L'entête doit être ignorée

                foreach (string line in File.ReadLines(filePath)) // On ajoute les instances à partir des lignes du fichier (le nombre d'instances récupérées sera toujours égal au nombre de lignes -1)
                {
                    if(!premièreLigneSkip) // Si l'entête n'a pas encore été ignorée
                    {
                        T instance = GetInstanceFromLine<T>(line); // On récupère une instance depuis une ligne du fichier
                        if (instance != null)
                        {
                            instances.Add(instance); // On ajoute l'instance à la liste des instances
                        }
                    }
                    else
                    {
                        premièreLigneSkip = false;
                    }
                }
            }
            return instances;
        }

        /// <summary>
        /// Méthode Générique - Transforme une ligne de texte en une instance de la classe T
        /// </summary>
        /// <typeparam name="T">Classe Type (par exemple Joueur)</typeparam>
        /// <param name="line">Ligne de texte</param>
        /// <returns>Instance de la classe T</returns>
        /// <author>Aaron</author>
        private T GetInstanceFromLine<T>(string line)
        {
            string[] values = line.Split(','); // Les propriétés sont séparées en une liste de string

            T instance = Activator.CreateInstance<T>(); // On créé une instance de T pour y ajouter les valeurs par la suite

            PropertyInfo[] properties = typeof(T).GetProperties(); // On détermine les types des propriétés en se référant à la classe modele T
            for (int i = 0; i < Math.Min(properties.Length, values.Length); i++) // Pour chaque propriété a intégrer
            {
                Type propertyType = properties[i].PropertyType; // On récupère le type de la Propriété

                if (propertyType == typeof(DateTime)) // Si la propriété est un DateTime
                {
                    DateTime parsedDate;
                    if (DateTime.TryParse(values[i], out parsedDate)) // On effectue un TryParse pour transformer le string en DateTime
                    {
                        properties[i].SetValue(instance, parsedDate); // On ajoute le résultat à la Propriété correspondante
                    }
                }
                else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>)) // Si la propriété est une liste
                {
                    Type elementType = propertyType.GetGenericArguments()[0]; // On récupère le type de valeur de la liste 
                    string[] listValues = values[i].Split('|'); // On sépare les valeurs de la liste en plusieurs strings 

                    if (listValues.Length == 1 && string.Equals(listValues[0], "vide", StringComparison.OrdinalIgnoreCase)) // Si la liste est déclarée comme vide dans le string
                    {
                        properties[i].SetValue(instance, Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType))); // On ajoute une liste vide à la propriété correspondante
                    }
                    else // La liste n'est pas vide
                    {
                        IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType)); // On initialise la liste dans laquelle on va mettre les valeurs
                        foreach (var listValue in listValues) // Pour chaque string de la liste
                        {
                            object convertedValue = Convert.ChangeType(listValue, elementType); // On transforme le string en type correspondant au type d'éléments contenus dans la liste
                            list.Add(convertedValue); // On ajoute la valeur à la liste
                        }

                        properties[i].SetValue(instance, list); // On ajoute le résultat à la Propriété correspondante
                    }
                }
                else // Si la propriété est convertissable depuis Convert
                {
                    object convertedValue = Convert.ChangeType(values[i], propertyType); // On convertit la valeur 
                    properties[i].SetValue(instance, convertedValue); // On ajoute le résultat à la Propriété correspondante
                }
            }
            return instance;
        }

        #endregion

        #region Utilitaires

        /// <summary>
        /// Méthode Générique - Renvoie le header type de la classe donnée
        /// </summary>
        /// <typeparam name="T">Classe Type (par exemple Joueur)</typeparam>
        /// <returns>une chaine de caractères correspondant aux propriétés de T (séparées par des virgules)</returns>
        /// <author>Aaron</author>
        private string GetHeader<T>()
        {
            PropertyInfo[] properties = typeof(T).GetProperties(); // On récupère les propriétés de T
            string header = string.Join(",", properties.Select(p => p.Name)); // On ajoute les noms des propriétés dans un string unique en les séparant par des virgules
            return header;
        }

        #endregion
    }
}
