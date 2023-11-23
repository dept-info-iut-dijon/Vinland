using System;
using System.Collections;
using System.Collections.Generic;
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
        /// Instance Unique accessible depuis l'extérieur
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
        private void SetupFichiers()
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
        /// Méthode Générique - Sauvegarde la liste donnée dans le fichier 
        /// </summary>
        /// <typeparam name="T">Classe Type (par exemple Joueur)</typeparam>
        /// <param name="liste">liste contenant les instances T</param>
        /// <param name="filePath">chemin vers le fichier correspondant a T</param>
        /// <author>Aaron</author>
        public void Save<T>(List<T> liste, string filePath)
        {
            foreach (var item in liste) // Pour toutes les instances de T
            {
                string line = GetFormattedLine(item);
                WriteToFile(filePath, line);
            }
        }

        public void Override<T>(List<T> liste, string filePath)
        {
            File.WriteAllText(filePath, GetHeader<T>()); // Setup de l'override, le fichier est clear sauf son header
            using (StreamWriter sw = File.AppendText(filePath)) sw.WriteLine(); // On passe une ligne pour ne pas écrire la première instance sur l'entête
            foreach (var item in liste) // Pour toutes les instances de T
            {
                string line = GetFormattedLine(item);
                WriteToFile(filePath, line);
            }
        }

        /// <summary>
        /// Renvoie le header type de la classe donnée
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GetHeader<T>()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            string header = string.Join(",", properties.Select(p => p.Name));
            return header;
        }

        /// <summary>
        /// Transforme les instances en string afin de les mettre dans le fichier
        /// </summary>
        /// <typeparam name="T">Classe Type (par exemple Joueur)</typeparam>
        /// <param name="item">instance de T</param>
        /// <returns></returns>
        /// <author>Aaron</author>
        private string GetFormattedLine<T>(T item)
        {
            PropertyInfo[] properties = typeof(T).GetProperties(); // Récupère les properties de l'instance

            List<string> values = new List<string>(); // Liste temporaire contenant les données 

            foreach (var property in properties) 
            {
                object propertyValue = property.GetValue(item);

                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>)) // Si la propriété est une liste
                {
                    var list = (IEnumerable)propertyValue;
                    if (list != null && list.Cast<object>().Any())
                    {
                        string listValues = string.Join("|", list.Cast<object>().Select(x => x.ToString())); // Transforme les éléments de la liste en string, séparés par le caractère "|"
                        values.Add(listValues);
                    }
                    else
                    {
                        values.Add("vide"); // Ajoute "vide" si la liste est vide
                    }
                }
                else
                {
                    values.Add(propertyValue?.ToString());
                }
            }
            return string.Join(",", values);
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

            if (File.Exists(filePath))
            {
                bool premièreLigneSkip = true;

                foreach (string line in File.ReadLines(filePath))
                {
                    if(!premièreLigneSkip)
                    {
                        T instance = GetInstanceFromLine<T>(line);
                        if (instance != null)
                        {
                            instances.Add(instance);
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
        /// Transforme une ligne de texte en une instance de la classe T
        /// </summary>
        /// <typeparam name="T">Classe Type (par exemple Joueur)</typeparam>
        /// <param name="line">Ligne de texte</param>
        /// <returns>Instance de la classe T</returns>
        /// <author>Aaron</author>
        private T GetInstanceFromLine<T>(string line)
        {
            string[] values = line.Split(',');

            T instance = Activator.CreateInstance<T>();

            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < Math.Min(properties.Length, values.Length); i++)
            {
                Type propertyType = properties[i].PropertyType;

                if (propertyType == typeof(DateTime))
                {
                    DateTime parsedDate;
                    if (DateTime.TryParse(values[i], out parsedDate))
                    {
                        properties[i].SetValue(instance, parsedDate);
                    }
                }
                else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    Type elementType = propertyType.GetGenericArguments()[0];
                    string[] listValues = values[i].Split(',');

                    if (listValues.Length == 1 && string.Equals(listValues[0], "vide", StringComparison.OrdinalIgnoreCase))
                    {
                        properties[i].SetValue(instance, Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType)));
                    }
                    else
                    {
                        IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
                        foreach (var listValue in listValues)
                        {
                            if (!string.IsNullOrWhiteSpace(listValue))
                            {
                                object convertedValue = Convert.ChangeType(listValue, elementType);
                                list.Add(convertedValue);
                            }
                            else
                            {
                                list.Add(default);
                            }
                        }

                        properties[i].SetValue(instance, list);
                    }
                }
                else
                {
                    object convertedValue = Convert.ChangeType(values[i], propertyType);
                    properties[i].SetValue(instance, convertedValue);
                }
            }

            return instance;
        }
        #endregion
    }
}
