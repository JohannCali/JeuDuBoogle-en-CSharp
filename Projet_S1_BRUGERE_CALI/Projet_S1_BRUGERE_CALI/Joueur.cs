using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S1_BRUGERE_CALI
{
    public class Joueur
    {
        private string nom;
        private int score;
        private List<string> motstrouvés;  // liste car on ne sait pas à l'avance le nombre de mots
        private int occurences;
        private Plateau platjoueur;
        public Joueur(string nom, int score, List<string> motstrouvés, int occurences, Plateau platjoueur) 
        {
            this.nom = nom;
            this.score = score;
            this.motstrouvés = motstrouvés;
            this.occurences = occurences;
            this.platjoueur = platjoueur;
        }
        public Plateau PlatJoueur
        {
            get { return this.platjoueur; }  
            set { this.platjoueur = value;}
        }
        public string Nom 
        { 
            get { return this.nom; } 
        }
        public int Score 
        { 
            get { return this.score; } 
            set {  this.score = value; }
        }
        public List <string> Motstrouvés 
        { 
            get { return this.motstrouvés; } 
        }     
        public int Occurences 
        { 
            get { return this.occurences; } 
        }
        public bool Contain(string mot) // vérifie si le mot à déja été trouvé par le joueur 
        {
            bool motpastrouve = true;
            mot = mot.ToUpper().Trim();
            foreach (string val in this.motstrouvés)
            {
                if (val == mot)
                {
                    motpastrouve = false;
                }
            }
            return motpastrouve;
        }
        public void Add_Mot(string mot) // ajoute le mot dans la liste des mots trouvés par le joueur 
        {
            motstrouvés.Add(mot);

            foreach (string val in motstrouvés)    // compte le nombre de fois ou le mot à été trouvé et augmente le nombre d'occurence  
            {
                if (val == mot)
                { 
                    this.occurences++; 
                }
            }
        }
        public string AffichageMotstrouvés(List <string> motstrouvés)  
        {
            string mots = ""; 
            foreach (string val in motstrouvés)    
            { 
                mots += val+" ";}
            return mots;
        }
        public override string ToString()
        {
            return "\n****************DESCRIPTION JOUEUR****************" + "\nNOM : " + this.nom + "\nScore : " + this.score  
                + "\nMOTS TROUVES : " + AffichageMotstrouvés(this.motstrouvés)+ "\n\n************************************************** \n"; 
        }
    }
}