using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S1_BRUGERE_CALI
{
    public class Plateau
    {
        private char lettrefacevisible;
        private List<char> liste;          // Liste initiale
        private Dé dé;                   
        private List<char> listegrille;   // Liste des faces visibles générée
        private char[,] matrice;          // Matrice du plateau générée
        private int taille;

        public Plateau(char lettrefacevisible, List<char> liste, Dé dé, int taille) 
        {
            this.lettrefacevisible = lettrefacevisible;
            this.liste = liste;
            this.dé = dé;
            this.listegrille = new List<char>(); // Initialisation d'une liste vide
            this.matrice = new char[0,0]; // Matrice non initialisée par défaut
            this.taille = taille;
        }
        public char Lettrefacevisible 
        { 
            get { return this.lettrefacevisible; } 
        }
        public List<char> Liste 
        { get { return this.liste; } }
        public Dé Dé 
        { 
            get { return this.dé; } 
        }
        public char[,] Matrice 
        { 
            get { return this.matrice; } 
        } 
        // Générer la liste des faces visibles
        public int Taille 
        {  
            get { return this.taille; } 
        }
        public void GenererListeGrille() 
        {
            for (int i = 1; i <= this.taille; i++)
            {
                Dé dé = new Dé(liste);             // Générer un nouveau dé
                listegrille.Add(dé.LettreFaceVisible);     // Ajouter la face visible à la liste
                liste.Remove(dé.LettreFaceVisible);       // Retirer la face de la liste originale
            }
        }       
        // Générer une matrice carrée à partir de la liste
        public void GenererMatrice() 
        {
            int tailleMatrice = (int)Math.Sqrt(listegrille.Count);

            if (tailleMatrice * tailleMatrice != listegrille.Count)
            {
                Console.WriteLine("La liste ne peut pas être convertie en une matrice carrée.");
            }

            matrice = new char[tailleMatrice, tailleMatrice];
            int compteur = 0;

            for (int j = 0; j < tailleMatrice; j++)
            {
                for (int k = 0; k < tailleMatrice; k++)
                {
                    matrice[j, k] = listegrille[compteur];
                    compteur++;
                }
            }
        }
        // Afficher la matrice sous forme de chaîne
        public override string ToString() 
        {
            if (matrice == null)
            {
                return "La matrice n'a pas été générée.";
            }
            string a = "";
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    a += matrice[i, j] + " ";

                }
                a += "\n";

            }
            
            return a;
        }
        public bool Test_Plateau(string mot, char[,] matrice) // vérifie si le mot est éligible par rapport à la configuration du plateau
        {
            mot = mot.ToUpper();   
            int lignes = matrice.GetLength(0);
            int colonnes = matrice.GetLength(1);
            bool[,] visite = new bool[lignes, colonnes]; 

            // Méthode récursive locale pour la recherche
            bool Recherche(int x, int y, int index)
            {
                // Si toutes les lettres ont été trouvées
                if (index == mot.Length)
                {
                    return true;
                }

                // Vérifications des limites et des conditions de validité
                if (x < 0 || x >= lignes || y < 0 || y >= colonnes || visite[x, y] || matrice[x, y] != mot[index])
                {
                    return false;
                }

                // Marquer la case actuelle comme visitée
                visite[x, y] = true;

                // Déplacements dans les 8 directions (haut, bas, gauche, droite, diagonales)
                int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
                int[] dy = { -1, 0, 1, 1, 1, 0, -1, -1 };

                // Parcours des 8 directions
                for (int dir = 0; dir < 8; dir++)
                {
                    if (Recherche(x + dx[dir], y + dy[dir], index + 1))
                    {
                        return true;
                    }
                }

                // Annuler la visite avant de revenir en arrière
                visite[x, y] = false;
                return false;
            }

            // Recherche dans la matrice pour chaque occurrence de la première lettre
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    if (matrice[i, j] == mot[0])                // Début de la recherche pour la première lettre
                    {
                        if (Recherche(i, j, 0))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;                                        // Si le mot n'est pas trouvé
        }
    }
}
