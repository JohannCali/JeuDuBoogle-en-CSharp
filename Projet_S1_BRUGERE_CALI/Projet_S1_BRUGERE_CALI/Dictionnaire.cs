using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_S1_BRUGERE_CALI
{
    public class Dictionnaire
    {
        private string[] mots;
        private string langue;
        public Dictionnaire(string langue) 
        {
            Fichier DicoFrTrié = new Fichier("Fichiers_Annexes/DicoFrTrié.txt");
            Fichier DicoAngTrié = new Fichier("Fichiers_Annexes/DicoAngTrié.txt");
            this.langue = langue;
            this.mots = [];

            if (langue.ToLower().Trim() == "anglais") 
            {
                this.langue = "Anglais";
                this.mots = File.ReadAllLines(DicoAngTrié.Chemin);
            }
            
            if (langue.ToLower().Trim() == "français" || langue.ToLower().Trim() == "francais")
            {
                this.langue = "Français";
                this.mots = File.ReadAllLines(DicoFrTrié.Chemin);
            }
        }
        public string[] Mots
        { 
            get { return mots; } 
            set { mots = value; }
        }
        public string Langue
        {
            get { return this.langue; }
        }
        
        //public bool VerifPresenceMot(Dictionnaire dico, string motcible) // pas assez optimisé car trop simple, ne prend pas assez en compte le trie
        //{
        //    bool estpresent=false;

        //    foreach(string mot in dico.mots)
        //    {
        //        if(string.Compare(motcible, mot) < 0)
        //        {
        //            break;
        //        }

        //        if (string.Compare(motcible, mot) == 0)
        //        { 
        //            estpresent = true;
        //            break;
        //        }
        //    }
        //    return estpresent;
        //}
        public bool VerifPresenceMot(Dictionnaire dico, string mot) // par dichotomie
        {
            bool estpresent = false;
            int a = 0;
            int b = dico.Mots.Length - 1;
            mot = mot.ToUpper().Trim();
            while (a <= b)
            {
                int m = (a + b) / 2;
                if (string.Compare(dico.Mots[m], mot) == 0)
                {
                    estpresent = true;
                    break;
                }
                if (string.Compare(dico.Mots[m], mot) < 0)
                {
                    a = m + 1;
                }
                else
                {
                    b = m - 1;
                }
            }
            return estpresent;
        }
    }
}