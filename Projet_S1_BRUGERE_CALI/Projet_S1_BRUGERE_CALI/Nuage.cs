﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S1_BRUGERE_CALI
{
    internal class Nuage
    {
        public static void Run(Dictionary<string, int> dico)
        {
            // Construire le contenu HTML avec JavaScript
            string htmlContent = @"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Nuage de Mots</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f5f5f5;
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        height: 100vh;
                        overflow: hidden;
                    }

                    .word-cloud {
                        position: relative;
                        width: 100%;
                        height: 100%;
                        display: flex;
                        flex-wrap: wrap;
                        justify-content: center;
                        align-items: center;
                    }

                    .word {
                        position: absolute;
                        font-weight: bold;
                        transform: translate(-50%, -50%);
                        white-space: nowrap;
                        margin: 2px; /* Réduire l'espacement entre les mots pour un nuage plus compact */
                        transition: all 0.3s ease; /* Animation de transition */
                    }
                </style>
            </head>
            <body>
                <div class='word-cloud' id='word-cloud'></div>

                <script>
                    // Dictionnaire en JavaScript
                    const wordFrequencies = {";

            // Ajouter les mots et fréquences au script
            foreach (var kvp in dico)
            {
                htmlContent += $"'{kvp.Key}': {kvp.Value}, ";
            }

            // Terminer le script
            htmlContent += @"
            };

            // Fonction pour générer une position aléatoire dans un conteneur pour un mot
            function getRandomPosition(maxX, maxY) {
                const x = Math.floor(Math.random() * maxX);
                const y = Math.floor(Math.random() * maxY);
                return { x, y };
            }

            // Fonction pour générer un nuage de mots sans chevauchement
            function generateWordCloud(words) {
            const container = document.getElementById('word-cloud');
            const containerWidth = container.offsetWidth;
            const containerHeight = container.offsetHeight;

            Object.entries(words).forEach(([word, freq]) => {
                const span = document.createElement('span');
                span.textContent = word;

                // Styles aléatoires pour chaque mot
                span.style.fontSize = (freq * 1.5) + 'px'; // Ajuste la taille en fonction de la fréquence
                span.style.color = hsl(${Math.random() * 360}, 70%, 50%); // Couleur aléatoire

                let position;
                do {
                    position = getRandomPosition(containerWidth, containerHeight);
                } while (isOverlapping(span, position)); // Tant que le mot chevauche un autre mot, continue

                span.style.top = position.y + 'px'; // Position verticale
                span.style.left = position.x + 'px'; // Position horizontale

                span.className = 'word';
                container.appendChild(span);
                });
            }

            // Fonction pour vérifier si un mot chevauche avec un autre
            function isOverlapping(span, position) {
            const spanRect = span.getBoundingClientRect();
            span.style.position = 'absolute';
            span.style.top = position.y + 'px';
            span.style.left = position.x + 'px';

            const otherSpans = document.querySelectorAll('.word');
            for (let other of otherSpans) {
                const otherRect = other.getBoundingClientRect();
                if (!(spanRect.right < otherRect.left ||
                      spanRect.left > otherRect.right ||
                      spanRect.bottom < otherRect.top ||
                      spanRect.top > otherRect.bottom)) {
                    return true; // Chevauchement détecté
                }
            }

            return false;
            }

            // Générer le nuage de mots
            generateWordCloud(wordFrequencies);
                </script>
            </body>
            </html>";

            // Écrire dans un fichier HTML
            File.WriteAllText("nuageDeMots.html", htmlContent);

            Console.WriteLine("Fichier HTML généré avec JavaScript !");
        }
    }
}