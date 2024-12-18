using System;
using System.Collections.Generic;

namespace Projet_S1_BRUGERE_CALI
{
    public class Dé
    {
        private List<char> listedelettrespossibles; 
        private List<char> listelettresfaces;       
        private char lettrefacevisible;

        public Dé(List<char> listedelettrespossibles)
        {
            if (listedelettrespossibles != null && listedelettrespossibles.Count > 0)
            {
                this.listedelettrespossibles = listedelettrespossibles;
            }
            else
            {
                Console.WriteLine("La liste passée en paramètre est vide ou nulle !");
                this.listedelettrespossibles = new List<char>(); // Initialisation par défaut
            }

            Random random = new Random();
            this.listelettresfaces = new List<char>();

            for (int i = 0; i < 6; i++)
            {
                this.listelettresfaces.Add(this.listedelettrespossibles[random.Next(0, this.listedelettrespossibles.Count)]);
            }

            // Définit une face visible par défaut si la liste n'a pas pu être remplie
            this.lettrefacevisible = this.listelettresfaces.Count > 0 
                                    ? this.listelettresfaces[random.Next(0, this.listelettresfaces.Count)] 
                                    : '\0'; // Caractère nul si liste vide
        }
        public List<char> ListeLettresPossibles
        {
            get { return this.listedelettrespossibles; }
        }
        public List<char> ListeLettresFaces
        {
            get { return this.listelettresfaces; }
        }
        public char LettreFaceVisible
        {
            get { return this.lettrefacevisible; }
        }
        public string ListeToString(List<char> liste)
        {
            return string.Join("\n", liste);
        }
        public char Affichage() // pour avoir la lettre en char et non en string 
        {
            return this.lettrefacevisible;
        }
        public override string ToString()
        {
            if (this.listelettresfaces == null || this.listelettresfaces.Count == 0)
            {
                return "Le dé est vide.";
            }
            else
            {
                return $" {this.lettrefacevisible}.";
            }
        }
    }
}
