using System.ComponentModel;

namespace Projet_S1_BRUGERE_CALI
{
    public class Program
    {
        static public void AfficherTableau(string[] tableau) 
        {
            for (int i = 0; i < tableau.Length; i++)
            {

                Console.Write(tableau[i] + " ");
                Console.WriteLine();
            }
        }
        static public void AfficherMatrice(string[,] tableau) 
        {
            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                for (int j = 0; j < tableau.GetLength(1); j++)
                {
                    Console.Write(tableau[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Jeu.Jouer();
        }
    }
}