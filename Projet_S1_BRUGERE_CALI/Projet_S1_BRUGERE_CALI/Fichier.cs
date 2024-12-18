using System;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Projet_S1_BRUGERE_CALI
{
    public class Fichier 
    {
        private string chemin; // le chemin attendu est le chemin d'accès du fichier en question 
        public Fichier (string chemin) 
        {
            this.chemin = chemin;
        }
        public string Chemin 
        { 
            get { return chemin; } 
        }
        public static string[,] RecupererDonneesFichierLettres(Fichier f) // renvoie les données de fichierlettre sous forme de matrice 
        {
            string[] lines = File.ReadAllLines(f.chemin); // Lire toutes les lignes du fichier
            string[,] matrice = new string[lines.Length, lines[0].Split(';').Length]; // Créer une matrice pour stocker les données

            for (int i = 0; i < lines.Length; i++)  // Créer une matrice pour stocker les données
            {
                string[] elements = lines[i].Split(';');
                for (int j = 0; j < elements.Length; j++)
                {
                    matrice[i, j] = elements[j];
                }
            }
            return matrice;
        } 
        public char[] TableauLettres() 
        {
            string[] lignes = File.ReadAllLines(this.chemin);          
            int sommeValeursTableauNombres = 0; 

            foreach (string val in lignes)             
            {
                string[] colonnes = val.Split(';');    // on découpe chaque ligne en colonne 
                sommeValeursTableauNombres += Convert.ToInt32(colonnes[2]);  // Calculer la taille totale du tableau final
            }

            char[] tableau = new char[sommeValeursTableauNombres]; // créer un tableau de taille le nombre de lettre de l'alphabet + leur fréquence d'apparition
            int index = 0;
            foreach (string val in lignes)                        
            {
                string[] colonnes = val.Split(';');             // initialisation du tableau colonnes 
                string lettre = colonnes[0];  // lettre = "A"
                char charlettre = Convert.ToChar(lettre);                  //conversion en char 
                int fréquence = Convert.ToInt32(colonnes[2]);

                for (int i = 0; i < fréquence; i++)
                {
                    tableau[index++] = charlettre;
                }

            }
            return tableau;
        }
        public string[] TriBulle(Fichier f) // 5389 itérations sur le fichier test et inutilisable sur le fichier initial 
        {
            string[] line = File.ReadAllLines(f.Chemin);

            // StringSplitOptions.RemoveEmptyEntries pour enlever les termes inutiles dans les cas
            // où il y aurait plusieurs espaces entre les mots du fichier
            string[] trié = line[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // pour compter le nombre de calculs effectués par l'ordinateur, pour pouvoir ensuite 
            // comparer les tris et selectionner le plus performant
            int compteur = 0;

            bool esttrie = false;

            for (int i = 0; i < trié.Length ; i++)
            {
                while(!esttrie)
                {
                    for (int j = 1; j < trié.Length - 1 - i; j++)
                    {
                        esttrie = true;
                        compteur++;

                        /* string.Compare */
                        // < 0 si trié[j] est avant trié[j + 1] dans l'ordre alphabétique.
                        // 0 si les deux string sont égaux.
                        // > 0 si trié[j] est après trié[j + 1] dans l'ordre alphabétique.

                        if (string.Compare(trié[j], trié[j + 1], StringComparison.Ordinal) > 0)
                        {
                            compteur++;
                            string temp = trié[j];
                            trié[j] = trié[j + 1];
                            trié[j + 1] = temp;
                            esttrie = false;
                        }
                    }
                }
            }

            Array.Resize(ref trié, trié.Length + 1); // on augmente la taille du tableau pour y ajouter le compteur
            trié[trié.Length - 1] = compteur.ToString(); // on converti le compteur d'int à string
            return trié;
        }
        public string[] TriCocktail(Fichier f) // 4686 itérations sur le fichier test et inutilisable sur le fichier initial 
        {
            string[] line = File.ReadAllLines(f.Chemin);
            string[] trié = line[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int compteur = 0;

            bool echangé = true; // Indicateur pour savoir si un échange a eu lieu et arrêter le programme si le tableau est trié
            int début = 0; // Début de la zone non triée
            int fin = trié.Length - 1; // Fin de la zone non triée

            while (echangé)
            {
                echangé = false;

                // Parcours de gauche à droite
                for (int j = début; j < fin; j++)
                {
                    if (string.Compare(trié[j], trié[j + 1], StringComparison.Ordinal) > 0)
                    {
                        string temp = trié[j];
                        trié[j] = trié[j + 1];
                        trié[j + 1] = temp;
                        echangé = true;
                        compteur++; 
                    }

                    compteur++;
                }

                if (!echangé)
                    break;

                // Réduction de la zone non triée
                fin--;
                echangé = false;

                // Parcours de droite à gauche
                for (int j = fin; j > début; j--)
                {
                    if (string.Compare(trié[j - 1], trié[j], StringComparison.Ordinal) > 0)
                    {
                        string temp = trié[j - 1];
                        trié[j - 1] = trié[j];
                        trié[j] = temp;
                        echangé = true;
                        compteur++; 
                    }

                    compteur++;
                }

                // Réduction de la zone non triée
                début++;
            }

            Array.Resize(ref trié, trié.Length + 1); // On augmente la taille du tableau pour y ajouter le compteur
            trié[trié.Length - 1] = compteur.ToString(); // On convertit le compteur d'int à string
            return trié;
        }
        public string[] TriFusion(Fichier f) // 448 itérations sur le fichier test et 2053939 sur les fichiers du jeu 
        {
            string[] line = File.ReadAllLines(f.Chemin);
            string[] elements = line[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int compteur = 0; // sert à compter le nombre d'itérations

            // Appeler la fonction de tri fusion
            elements = DiviserPourRegner(elements, ref compteur);

            Array.Resize(ref elements, elements.Length + 1);
            elements[elements.Length - 1] = compteur.ToString();

            return elements;
        } 
        private string[] DiviserPourRegner(string[] array, ref int compteur) 
        {
            if (array.Length <= 1)
            {
                return array;
            }

            // Diviser le tableau en deux moitiés
            int milieu = array.Length / 2;
            string[] gauche = array[..milieu];
            string[] droite = array[milieu..];

            // Trier chaque moitié récursivement
            gauche = DiviserPourRegner(gauche, ref compteur);
            droite = DiviserPourRegner(droite, ref compteur);

            // Fusionner les moitiés triées
            return Fusionner(gauche, droite, ref compteur);
        }
        private string[] Fusionner(string[] gauche, string[] droite, ref int compteur) 
        {
            int i = 0, j = 0, k = 0;
            string[] resultat = new string[gauche.Length + droite.Length];

            while (i < gauche.Length && j < droite.Length)
            {
                compteur++;
                if (string.Compare(gauche[i], droite[j], StringComparison.Ordinal) <= 0)
                {
                    resultat[k++] = gauche[i++];
                }
                else
                {
                    resultat[k++] = droite[j++];
                }
            }

            // Ajouter les éléments restants
            while (i < gauche.Length)
            {
                resultat[k++] = gauche[i++];
            }

            while (j < droite.Length)
            {
                resultat[k++] = droite[j++];
            }

            return resultat;
        }
        public string[] TriSelection(Fichier f)// 4104 itérations sur le fichier test et inutilisable sur le fichier initial 
        {
            string[] line = File.ReadAllLines(f.Chemin);
            string[] trié = line[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int compteur = 0;  

            for (int i = 0; i < trié.Length - 1; i++)
            {
                int indiceMin = i;
                for (int j = i + 1; j < trié.Length; j++)
                {
                    if (string.Compare(trié[j], trié[indiceMin], StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        indiceMin = j;
                        compteur++;
                    }
                    
                    compteur++;
                }

                // Échanger l'élément courant avec le plus petit élément trouvé
                if (indiceMin != i)
                {
                    string temp = trié[i];
                    trié[i] = trié[indiceMin];
                    trié[indiceMin] = temp;
                    compteur++;
                }

                compteur++;
            }

            Array.Resize(ref trié, trié.Length + 1); // on augmente la taille du tableau pour y ajouter le compteur
            trié[trié.Length - 1] = compteur.ToString(); // on converti le compteur d'int à string
            return trié;
        }
        public string[] TriInsertion(Fichier f) // 1819 itérations sur le fichier test et inutilisable sur le fichier initial 
        {
            string[] line = File.ReadAllLines(f.Chemin);
            string[] trié = line[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int compteur = 0;  // pour compter le nombre de calculs effectués par l'ordinateur et pouvoir ensuite comparer les tris

            for (int i = 1; i < trié.Length ; i++)
            {
                string valinser = trié[i];
                int j = i - 1;

                while(j >=0 && string.Compare(trié[j] , valinser) > 0)
                {
                    trié[j + 1] = trié[j];
                    j--;
                    compteur++;
                }

                compteur++;
                trié[j + 1] = valinser;
            }

            Array.Resize(ref trié, trié.Length + 1); // on augmente la taille du tableau pour y ajouter le compteur
            trié[trié.Length - 1] = compteur.ToString(); // on converti le compteur d'int à string
            return trié;
        } 
    }
}
