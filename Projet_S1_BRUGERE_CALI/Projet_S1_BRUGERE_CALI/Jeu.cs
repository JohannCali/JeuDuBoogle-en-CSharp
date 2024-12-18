using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WordCloudSharp;

namespace Projet_S1_BRUGERE_CALI
{
    public class Jeu
    {
        private Joueur[] joueurs = Array.Empty<Joueur>(); // Initialise à un tableau vide
        private Dictionnaire dico = null!;
        private int nbjoueurs;
        private Dictionary<string, int> dictionnairedesmots;
        public Jeu()
        {
            Fichier FichierLettrestxt = new Fichier("Fichiers_Annexes/Lettres.txt");
            string choixlangue;
            this.dictionnairedesmots = new Dictionary<string, int>();

            do
            {
                Console.WriteLine("Veuillez saisir la langue du dictionnaire utilisé (français ou anglais)");
                choixlangue = Console.ReadLine()?.Trim()?? "";
                if (!(choixlangue.ToLower().Trim() == "français" || choixlangue.ToLower().Trim() == "francais" || choixlangue.ToLower().Trim() == "anglais"))
                {
                    Console.WriteLine("\nLa langue choisie n'est pas disponible !");
                }

            } while (!(choixlangue.ToLower().Trim() == "français" || choixlangue.ToLower().Trim() == "francais" || choixlangue.ToLower().Trim() == "anglais"));

            this.dico = new Dictionnaire(choixlangue);
            Console.WriteLine("\nVous chercherez donc des mots " + dico.Langue);

            int a;
            string j;
            List<string> listej = new List<string>(); // liste vide qui est attribuée à tous les joueurs et qui contiendra les mot qu'ils ont trouvés

            Dé dé = new Dé(FichierLettrestxt.TableauLettres().ToList());
            Plateau plateau;

            int taille; // 

            do
            {
                Console.WriteLine("\nQuelle est la taille d'une colonne de la grille (4 à 8) ?");
                taille = Convert.ToInt32(Console.ReadLine());
                if (taille < 4 || taille > 8)
                {
                    Console.WriteLine("taille invalide !! ");
                }
            } while (!(4 <= taille && taille <= 8));

            do
            {
                Console.WriteLine("\nCombien y a-t-il de joueurs ? (le nombre maximum de joueurs est 5)");
                a = int.Parse(Console.ReadLine()?? "0");
                this.nbjoueurs = a;


                if (a > 5)
                {
                    Console.WriteLine("Le nombre maximum de joueurs est 5 !");
                }
                else
                {
                    this.joueurs = new Joueur[a];

                    for (int i = 0; i < a; i++)
                    {


                        Console.WriteLine($"\nVeuillez entrer le nom du joueur{i + 1}");
                        j = Console.ReadLine()?.Trim() ?? "";
                        j = string.IsNullOrWhiteSpace(j) ? $"Joueur{i + 1}" : j.Trim();

                        plateau = new Plateau(dé.LettreFaceVisible, FichierLettrestxt.TableauLettres().ToList(), dé, taille * taille);

                        plateau.GenererListeGrille(); // Génère la liste
                        plateau.GenererMatrice(); // Génère la matrice

                        Joueur joueur = new Joueur(j, 0, listej, 0, plateau);
                        this.joueurs[i] = joueur;

                        Console.WriteLine("Joueur ajouté !");
                    }
                }
            } while (a > 5);

            Console.WriteLine("\nVoici la liste des joueurs : ");
            for (int i = 0; i < a; i++)
            {
                Console.Write(this.joueurs[i].Nom + " ");
                Console.WriteLine();
            }
        }
        public Joueur[] Joueurs
        {
            get { return this.joueurs; }
        }
        public Dictionnaire Dico
        {
            get { return this.dico; }
        }
        public int NbJoueurs
        {
            get { return this.nbjoueurs; }
            set { this.nbjoueurs = value; }
        }
        public Dictionary<string, int> DictionnaireDesMots
        {
            get { return this.dictionnairedesmots; }
            set { this.dictionnairedesmots = value; }
        }
        public static void Jouer()
        {
            Fichier FichierLettrestxt = new Fichier("Fichiers_Annexes/Lettres.txt");

            Console.WriteLine("                                                    PARAMETRES\n");

            Jeu jeu = new Jeu(); // initialiser le jeu
            Console.WriteLine("\n\nLa partie commencera dans 5 secondes"
                + "\nChaque joueur aura 1 minute pour jouer, amusez-vous bien !");
            Thread.Sleep(5000);
            Console.Clear();
            Joueur gagnant = jeu.joueurs[0];

            for (int i = 0; i < jeu.nbjoueurs; i++) // lancer le tour de chaque joueur
            {
                Console.WriteLine("                                                       BOOGLE\n");

                DateTime fin = DateTime.Now.AddSeconds(5); // lance un compte à rebours en secondes 

                Console.WriteLine("\nC'est au tour de " + jeu.joueurs[i].Nom + "\n");
                Console.WriteLine(jeu.joueurs[i].PlatJoueur);
                string[,] matricefichierlettres = Fichier.RecupererDonneesFichierLettres(FichierLettrestxt);

                Console.WriteLine("Entrez un mot");

                while (DateTime.Now < fin)
                {
                    TimeSpan TempsRestant = fin - DateTime.Now;

                    if (Console.KeyAvailable) // Vérifie s'il y a une saisie
                    {
                        string motsaisi = Console.ReadLine()?.Trim() ?? string.Empty;
                        Console.WriteLine($"\nTemps restant : {TempsRestant.Seconds} secondes");

                        if (jeu.Joueurs[i].PlatJoueur.Test_Plateau(motsaisi, jeu.Joueurs[i].PlatJoueur.Matrice) &&
                            jeu.Dico.VerifPresenceMot(jeu.dico, motsaisi) &&
                            !jeu.joueurs[i].Motstrouvés.Contains(motsaisi) &&
                            motsaisi.Length > 2) // conditions de validité d'un mot
                        {
                            jeu.joueurs[i].Motstrouvés.Add(motsaisi);

                            foreach (char lettre in motsaisi.ToUpper())
                            {
                                for (int k = 0; k < 26; k++) // Cherche le score correspondant à la lettre puis au mot
                                {
                                    if (matricefichierlettres[k, 0] == lettre.ToString())
                                    {
                                        int scoremot = int.Parse(matricefichierlettres[k, 1]);
                                        jeu.joueurs[i].Score += scoremot;
                                        jeu.DictionnaireDesMots[motsaisi] = scoremot;

                                        if (jeu.joueurs[i].Score > gagnant.Score)
                                        {
                                            gagnant = jeu.joueurs[i];
                                        }
                                        break;
                                    }
                                }
                            }

                            Console.WriteLine("Mot valide !");
                            Console.WriteLine("\nEntrez en un autre");
                        }
                        else
                        {
                            Console.WriteLine("Mot non valide ou déjà trouvé !");
                            Console.WriteLine("\nEntrez en un autre");
                        }
                    }
                }

                Console.WriteLine("\nTemps écoulé !\n");
                jeu.joueurs[i].Motstrouvés.Clear();
                Thread.Sleep(1000);

                if (i == jeu.nbjoueurs - 1)
                {
                    Console.WriteLine("\nPARTIE TERMINÉE");
                }
                else
                {
                    Console.WriteLine("\nAppuyez sur n'importe quelle touche pour lancer le tour du joueur suivant");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Thread.Sleep(3000);
            Console.Clear();

            Console.WriteLine("                                                    FIN DU JEU\n");


            Console.WriteLine("Vous voulez savoir qui a gagné ? Alors tenez-vous prêt....");
            Thread.Sleep(2000);
            Console.WriteLine("\nSuspense...");
            Thread.Sleep(2000);

            bool egalite = true; // pour prendre en compte les cas d'égalité

            for (int i = 0; i < jeu.nbjoueurs; i++) 
            {
                if (jeu.Joueurs[i].Score != gagnant.Score)
                {
                    egalite = false; 
                }
            }

            if (egalite)
            {
                Console.WriteLine("\nIl y a égalité, tous les joueurs sont vainqueurs !");
            }
            else
            {
                Console.WriteLine($"\nLe gagnant est {gagnant.Nom} avec un score de {gagnant.Score} !");
            }

            Thread.Sleep(1000);
            Console.WriteLine("\nTABLEAU DES SCORES ");

            for (int i = 0; i < jeu.joueurs.Length; i++)
            {
                Console.WriteLine($"{jeu.joueurs[i].Nom} : {jeu.joueurs[i].Score}"); 
            }

            Thread.Sleep(1000);

            var mots = jeu.DictionnaireDesMots.Keys.ToList(); // generer des listes à partir du dictionnaire pour correspondre à 'Draw'
            var pondération = jeu.DictionnaireDesMots.Values.ToList();
            var wordCloud = new WordCloud(800, 600);
            var bitmap = wordCloud.Draw(mots, pondération, System.Drawing.Color.White);


            // Sauvegarder l'image générée dans le fichier indiqué
            bitmap.Save("wordcloud.png");

            Console.WriteLine("\nTrouvez le fichier wordcloud.png dans le dossier net8.0 pour voir un joli nuage de mots ;)");

            string nouvellepartie;
            Thread.Sleep(1000);

            do
            {
                Console.WriteLine("\nVoulez-vous refaire une partie ? (tapez oui ou non) ");
                nouvellepartie = Console.ReadLine()?.Trim() ?? string.Empty;

                if (nouvellepartie == "oui")
                {
                    Console.Clear();
                    Jouer();
                }

                if (nouvellepartie == "non")
                {
                    Console.WriteLine("\nAlors à bientôt !");
                }
            } while (!(nouvellepartie == "oui" || nouvellepartie == "non"));
        }
    }
}

