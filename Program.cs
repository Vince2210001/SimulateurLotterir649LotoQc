/*************************************************************************************************
 *************************************************************************************************
 **                                                                                             **
 ** TITRE : SIMULATEUR DE TIRAGE DU LOTO 6/49 DE LOTO-QUÉBEC                                    **
 ** DESCRIPTION : PROGRAMME QUI PERMET DE SAISIR UN NOMBRE DE PARTICIPATIONS, DE GÉNÉRER DES    **
 **               COMBINAISONS ALÉATOIRES AINSI QU'UNE COMBINAISON GAGNANTE ET DES STATISTIQUES **
 ** FAIT PAR : VINCENT CÔTÉ ;~J                                                                 **
 **       LE : 6 JUILLET 2022                                                                   **
 ** RÉVISÉ PAR :                                                                                **
 **         LE :                                                                                **
 **                                                                                             **
 *************************************************************************************************        
 *************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P24_TP1_649_2210001
{
    internal class Program
    {

        public struct Participation
        {
            public string nomCombinaison;
            public int[] uneCombinaison;
            public int unNumeroComplementaire;
        } // FIN public struct Participation

        public struct CombinaisonGagnante
        {
            public string dateCombinaisonGagnante;
            public int[] uneCombinaisonGagnante;
            public int unNumeroComplementaireGagnant;
        } //FIN public struct CombinaisonGagnante

        public struct Statistique
        {
            public int[] toutesCombinaisonsGagnantes;
            public int tousNumerosComplementairesGagnants;
        } //FIN public struct ParticipationGagnante



        //VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES
        //VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES


        public static Int32 nbreTiragesConsecutifs = 0;
        public static Int32 nbreCombinaisons;
        public static bool valide = false;
        public static bool auRevoir = false;
        public static Random rnd = new Random(DateTime.Now.Millisecond);
        public static Participation[] participations;
        public static CombinaisonGagnante[] combinaisonsgagnantes;
        public static Statistique[] statistiques;
        public static float statNonGagnant, i0_6, i1_6, i2_6, i3_6, i3_6c, i4_6, i4_6c, i5_6, i5_6c, i6_6;

        //VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES
        //VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES VARIABLES

        public static void Titre()
        {
            //METTRE EN FORME L'ÉCRAN
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            //AFFICHER TITRE DU PROGRAMME
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(5, 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ** ** ** ** ** *** ** ** ** ** ** ");
            Console.SetCursorPosition(5, 2);
            Console.WriteLine(" * ** *** SIMULATEUR 6/49 *** ** * ");
            Console.SetCursorPosition(5, 3);
            Console.WriteLine(" ** ** ** ** ** *** ** ** ** ** ** ");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        } //FIN public static void Titre()


        public static Int32 NbreParticipations()
        {
            do
            {
                Console.Clear();
                Titre();
                Console.SetCursorPosition(1, 5);
                Console.Write("Nombre de participations désirées entre 10 et 200 : ");

                //→→→MINIMUM 10 PARTICIPATIONS←←←  →→→MAXIMUM 200 PARTICIPATIONS←←←

                valide = Int32.TryParse(Console.ReadLine(), out nbreCombinaisons)
                            && nbreCombinaisons >= 10 && nbreCombinaisons <= 200;
            }while(!valide);
            
            return nbreCombinaisons;
        } //FIN public static Int32 NbreParticipations()


        static void GenererCombinaisons()
        {
            Array.Resize(ref participations, nbreCombinaisons);

            for (int i = 0; i < nbreCombinaisons; i++)
            {
                Participation participation = new Participation();
                participation.nomCombinaison = ($"Combinaison #{i + 1}");
                participation.uneCombinaison = new int[6];
                int rndInt;
                for (int j = 0; j < participation.uneCombinaison.Length; j++)
                {
                    do
                    {
                        rndInt = (int)rnd.Next(1, 49);
                        valide = Array.IndexOf(participation.uneCombinaison, rndInt) < 0;
                    } while (!valide);
                    participation.uneCombinaison[j] = rndInt;
                }
                participations[i] = participation;

                do
                {
                    rndInt = (int)rnd.Next(1, 49);
                    valide = Array.IndexOf(participation.uneCombinaison, rndInt) < 0;
                } while (!valide);
                participation.unNumeroComplementaire = (rndInt);
                participations[i] = participation;

            }

        } //FINstatic void GenererCombinaisons()


        static void ImprimerParticipations()
        {
            Console.WriteLine(Environment.NewLine);

            foreach (Participation part in participations)
            {
                Console.WriteLine($" {part.nomCombinaison.ToUpper()}");
                
                foreach (int n in part.uneCombinaison)
                {
                    Console.Write($" {n}");
                }
                Console.WriteLine("");

                Console.WriteLine($" Numéro complémentaire : {part.unNumeroComplementaire}");

                Console.WriteLine(Environment.NewLine);
            }

        } //FIN static void ImprimerParticipations()


        static void GenererCombinaisonsGagnante()
        {
            Array.Resize(ref statistiques, nbreTiragesConsecutifs);
            Array.Resize(ref combinaisonsgagnantes, 1);

            for (int i = 0; i < 1; i++)
            {
                CombinaisonGagnante combinaisongagnante = new CombinaisonGagnante();

                Statistique statistique = new Statistique();

                combinaisongagnante.dateCombinaisonGagnante = ($"Combinaison gagnante du tirage #{nbreTiragesConsecutifs} du {DateTime.Today: dddd d MMMM yyyy}");
                combinaisongagnante.uneCombinaisonGagnante = new int[6];

                statistique.toutesCombinaisonsGagnantes = new int[6];

                int rndWinInt;
                for (int j = 0; j < combinaisongagnante.uneCombinaisonGagnante.Length; j++)
                {
                    do
                    {
                        rndWinInt = (int)rnd.Next(1, 49);
                        valide = Array.IndexOf(combinaisongagnante.uneCombinaisonGagnante, rndWinInt) < 0;
                    } while (!valide);
                    combinaisongagnante.uneCombinaisonGagnante[j] = rndWinInt;
                    statistique.toutesCombinaisonsGagnantes[j] = rndWinInt;
                }
                combinaisonsgagnantes[i] = combinaisongagnante;
                statistiques[nbreTiragesConsecutifs - 1] = statistique;

                do
                {
                    rndWinInt = (int)rnd.Next(1, 49);
                    valide = Array.IndexOf(combinaisongagnante.uneCombinaisonGagnante, rndWinInt) < 0;
                } while (!valide);
                combinaisongagnante.unNumeroComplementaireGagnant = (rndWinInt);
                statistique.tousNumerosComplementairesGagnants = (rndWinInt);

                combinaisonsgagnantes[i] = combinaisongagnante;
                statistiques[nbreTiragesConsecutifs - 1] = statistique;

            }
        } //FIN static void GenererCombinaisonsGagnante()

        
        static void ImprimerCombinaisonGagnante()
        {

            foreach (CombinaisonGagnante part in combinaisonsgagnantes)
            {
                //Identification complète du numéro gagnant rédigée dans GenererCombinaisonsGagnante()
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($" {part.dateCombinaisonGagnante.ToUpper()}");
                Console.Write(" ");
                foreach (int n in part.uneCombinaisonGagnante)
                {
                    Console.Write($" {n}");
                }
                Console.WriteLine(" ");

                Console.WriteLine($" Numéro complémentaire Gagnant : {part.unNumeroComplementaireGagnant}");

                Console.WriteLine(Environment.NewLine);
            }

        } //FIN static void ImprimerCombinaisonGagnante()


        static void IdentifierParticipationsGagnantes()
        {
            int localNonGagnant = 0;

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Vos combinaisons Gagnantes : ");

            foreach (CombinaisonGagnante partgngnt in combinaisonsgagnantes)
            { 

                foreach (Participation part in participations)
                {
                    int nbreNumGgnt = 0;
                    bool numComplGgnt = false;
                    foreach (int n in part.uneCombinaison)
                    {
                        
                        if (Array.IndexOf(partgngnt.uneCombinaisonGagnante, n) > -1)
                        {
                            nbreNumGgnt++;
                        }
                    }

                    if (partgngnt.unNumeroComplementaireGagnant == part.unNumeroComplementaire)
                    {
                        numComplGgnt = true;
                    }

                    if (nbreNumGgnt == 0)
                    {
                        statNonGagnant++;
                        localNonGagnant++;
                        i0_6++;
                    }
                    if (nbreNumGgnt == 1)
                    {
                        statNonGagnant++;
                        localNonGagnant++;
                        i1_6++;
                    }
                    if (nbreNumGgnt == 2)
                    {
                        statNonGagnant++;
                        localNonGagnant++;
                        i2_6++;
                    }

                    if (nbreNumGgnt == 3 && numComplGgnt == true)
                    {
                       Console.WriteLine($" {part.nomCombinaison} : {nbreNumGgnt} numéros gagnants avec le numéro complémentaire!");
                        i3_6c++;
                    }

                    if (nbreNumGgnt == 3)
                    {
                        Console.WriteLine($" {part.nomCombinaison} : {nbreNumGgnt} numéros gagnants");
                        i3_6++;
                    }

                    if (nbreNumGgnt == 4 && numComplGgnt == true)
                    {
                        Console.WriteLine($" {part.nomCombinaison} : {nbreNumGgnt} numéros gagnants avec le numéro complémentaire!");
                        i4_6c++;
                    }

                    if (nbreNumGgnt == 4)
                    {
                        Console.WriteLine($" {part.nomCombinaison} : {nbreNumGgnt} numéros gagnants");
                        i4_6++;
                    }
                    
                    if (nbreNumGgnt == 5 && numComplGgnt == true)
                    {
                        Console.WriteLine($" {part.nomCombinaison} : {nbreNumGgnt} numéros gagnants avec le numéro complémentaire!");
                        i5_6c++;
                    }

                    if (nbreNumGgnt == 5)
                    {
                        Console.WriteLine($" {part.nomCombinaison} : {nbreNumGgnt} numéros gagnants");
                        i5_6++;
                    }
                    
                    if (nbreNumGgnt == 6)
                    {
                        Console.WriteLine($" {part.nomCombinaison} : {nbreNumGgnt} numéros gagnants");
                        i6_6++;
                    }

                } //FIN foreach (Participation part in participations)

            } //FIN foreach (CombinaisonGagnante part in participationsgagnantes)


            //MESSAGE MEILLEUR CHANCE LA PROCHAINE FOIS SI AUCUN GAIN...

            if (localNonGagnant == nbreCombinaisons)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(";~J Aucune participation gagnante ;~J");
                Console.WriteLine(";~J Meilleure chance la prochaine fois! ;~J");
                Console.WriteLine(Environment.NewLine);
            }

        } //FIN static void IdentifierParticipationsGagnantes()


        static void GenererStatistiques()
        {

            //NOMBRE DE TIRAGES CONCÉCUTIFS
            //NOMBRE DE TIRAGES CONCÉCUTIFS


            Console.WriteLine(Environment.NewLine);

            if (nbreTiragesConsecutifs < 2)
            {
                Console.WriteLine(" NOMBRE DE TIRAGES CONCÉCUTIFS : ");
                Console.WriteLine(" Premier tirage. ");
                Console.WriteLine(" ");
            }
            else
            {
                Console.WriteLine(" NOMBRE DE TIRAGES CONCÉCUTIFS : ");
                Console.WriteLine($"  {nbreTiragesConsecutifs}e tirage. ");
                Console.WriteLine(" ");
            }


            //NOMBRE D'APPARITIONS DE CHAQUE CHIFFRES (1 À 49) DANS LES COMBINAISONS GAGNANTES
            //NOMBRE D'APPARITIONS DE CHAQUE CHIFFRES (1 À 49) DANS LES COMBINAISONS GAGNANTES


            Console.WriteLine("");
            Console.WriteLine(" Occurences de chaques nombres à l'intérieur des participations gagnantes : ");
            Console.WriteLine("");

            int[] tabstatTtsCombGngnt = new int[nbreTiragesConsecutifs * 6];

            // Le "ii" est le nombre de combinaison où la procédure est rendu.
            int ii = 1;
            foreach (Statistique part in statistiques)
            {
                // Le "compt1" est l'index de la position où le "n" se place.
                int compt1 = ii * 6 - 6;
                foreach (int n in part.toutesCombinaisonsGagnantes)
                {
                    tabstatTtsCombGngnt[compt1] = n;
                    compt1++;
                }
                ii++;
            }
            Console.WriteLine(" ");


            var occurrencesnbre1_49 = tabstatTtsCombGngnt

                .Distinct()
                .Select(i => Tuple.Create(i, tabstatTtsCombGngnt.Count(i2 => i2 == i)))
                .OrderByDescending(pair => pair.Item2);

            foreach (var occurrenceNbre1_49 in occurrencesnbre1_49)
            {
                Console.WriteLine(" En {0} tirage{1}, le {2}{3} est apparu {4} fois.", nbreTiragesConsecutifs,
                    nbreTiragesConsecutifs > 1 ? "s" : "", occurrenceNbre1_49.Item1 < 10 ? " " : "",
                    occurrenceNbre1_49.Item1, occurrenceNbre1_49.Item2);
            }

            //===================================================================
            //===================================================================

            Console.WriteLine("");
            Console.WriteLine(" Impressions de tous les numeros complementaires gagnants : ");
            Console.WriteLine("");
            

            int[] tabstatTsNumComplGngnt = new int[nbreTiragesConsecutifs];


            // Le "compt2" est l'index de la position où le "n" se place
            int compt2 = 0;
            foreach (Statistique part in statistiques)
            {
                tabstatTsNumComplGngnt[compt2] = part.tousNumerosComplementairesGagnants;
                compt2++;
            }                
            
            Console.WriteLine(" ");


            var occurrencesnbre1_49compl = tabstatTsNumComplGngnt

                .Distinct()
                .Select(i => Tuple.Create(i, tabstatTsNumComplGngnt.Count(i2 => i2 == i)))
                .OrderByDescending(pair => pair.Item2);

            foreach (var occurrenceNbre1_49compl in occurrencesnbre1_49compl)
            {
                Console.WriteLine(" En {0} tirage{1}, le {2}{3} est apparu {4} fois.", nbreTiragesConsecutifs,
                    nbreTiragesConsecutifs > 1 ? "s" : "", occurrenceNbre1_49compl.Item1 < 10 ? " " : "",
                    occurrenceNbre1_49compl.Item1, occurrenceNbre1_49compl.Item2);
            }

            //===================================================================
            //===================================================================

            //POURCENTAGE DE PARTICIPATIONS POUR CHAQUE FAMILLE DE RÉSULTATS
            //([0/6, 1/6, 2/6], 3/6, 3/6+C, 4/6, 4/6+C, 5/6, 5/6+C, 6/6)
            //POURCENTAGE DE PARTICIPATIONS POUR CHAQUE FAMILLE DE RÉSULTATS
            //([0/6, 1/6, 2/6], 3/6, 3/6+C, 4/6, 4/6+C, 5/6, 5/6+C, 6/6)


            float somFamilleGain = (i3_6 + i3_6c + i4_6 + i4_6c + i5_6 + i5_6c + i6_6);
            float somToutesFamille = (i0_6 + i1_6 + i2_6 + i3_6 + i3_6c + i4_6 + i4_6c + i5_6 + i5_6c + i6_6);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(" POURCENTAGE DE SORTIE POUR CHAQUE FAMILLE DE RÉSULTATS : ");
            Console.WriteLine(" ");

            if (somToutesFamille == 0)
            {   
                Console.WriteLine($" 3/6 0% sur {nbreTiragesConsecutifs} tirages.");
                Console.WriteLine($" 4/6 0% sur {nbreTiragesConsecutifs} tirages.");
                Console.WriteLine($" 5/6 0% sur {nbreTiragesConsecutifs} tirages.");
                Console.WriteLine($" 6/6 0% sur {nbreTiragesConsecutifs} tirages.");
                Console.WriteLine($" 3/6 & #Compl. 0% sur { nbreTiragesConsecutifs} tirages.");
                Console.WriteLine($" 4/6 & #Compl. 0% sur {nbreTiragesConsecutifs} tirages.");
                Console.WriteLine($" 5/6 & #Compl. 0% sur {nbreTiragesConsecutifs} tirages.");
                Console.WriteLine($" [0/6, 1/6, 2/6] = 0% sur {nbreTiragesConsecutifs} tirages.");
            }
            else
            {
                Console.WriteLine(" Statistiques basées sur {0} tirage{1}.", nbreTiragesConsecutifs, nbreTiragesConsecutifs>1?"s":"");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine(" Familles gagnantes sans le numéro complémentaire : ");
                Console.WriteLine(" ");
                Console.WriteLine(" 3/6 = {0}% des familles gagnantes et {1}% de l'ensemble des familles.", somFamilleGain > 0 ? $"{i3_6 / somFamilleGain * 100f}" : "0", i3_6 / somToutesFamille * 100f);
                Console.WriteLine(" 4/6 = {0}% des familles gagnantes et {1}% de l'ensemble des familles.", somFamilleGain > 0 ? $"{i4_6 / somFamilleGain * 100f}" : "0", i4_6 / somToutesFamille * 100f);
                Console.WriteLine(" 5/6 = {0}% des familles gagnantes et {1}% de l'ensemble des familles.", somFamilleGain > 0 ? $"{i5_6 / somFamilleGain * 100f}" : "0", i5_6 / somToutesFamille * 100f);
                Console.WriteLine(" 6/6 = {0}% des familles gagnantes et {1}% de l'ensemble des familles.", somFamilleGain > 0 ? $"{i6_6 / somFamilleGain * 100f}" : "0", i6_6 / somToutesFamille * 100f);
                Console.WriteLine(" ");
                Console.WriteLine(" Familles gagnantes avec le numéro complémentaire : ");
                Console.WriteLine(" ");
                Console.WriteLine(" 3/6 & #Compl. = {0}% des familles gagnantes et {1}% de l'ensemble des familles.", somFamilleGain == 0 ? "0" : $"{i3_6c / somFamilleGain * 100f}", i3_6c / somToutesFamille * 100f);
                Console.WriteLine(" 4/6 & #Compl. = {0}% des familles gagnantes et {1}% de l'ensemble des familles.", somFamilleGain == 0 ? "0" : $"{i4_6c / somFamilleGain * 100f}", i4_6c / somToutesFamille * 100f);
                Console.WriteLine(" 5/6 & #Compl. = {0}% des familles gagnantes et {1}% de l'ensemble des familles.", somFamilleGain == 0 ? "0" : $"{i5_6c / somFamilleGain * 100f}", i5_6c / somToutesFamille * 100f);
                Console.WriteLine(" ");
                Console.WriteLine($" [0/6, 1/6, 2/6] = {statNonGagnant / somToutesFamille * 100f}% de l'ensemble des familles.");
                Console.WriteLine(" ");
                Console.WriteLine(" Familles non gagnantes ventilées : ");
                Console.WriteLine(" ");
                Console.WriteLine($" 0/6 = {i0_6 / somToutesFamille * 100f}% de l'ensemble des familles.");
                Console.WriteLine($" 1/6 = {i1_6 / somToutesFamille * 100f}% de l'ensemble des familles.");
                Console.WriteLine($" 2/6 = {i2_6 / somToutesFamille * 100f}% de l'ensemble des familles.");
            }
            Console.WriteLine("");
            Console.WriteLine(" Fin des statistiques.");
            Console.WriteLine("");


        } //FIN static void GenererStatistiques()

        public static void MessageJeuAideEtReference()
        {
           
            //LE CHOIX DES COULEURS EST DÉFINI PAR L'ANNONCEUR ;~J 
                               

            //METTRE EN FORME LE FOND DE L'ÉCRAN JAUNE
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Yellow;

            //AFFICHER LES ÉTOILES VERTE

            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(0, 1);
            Console.WriteLine(" ** ** ** ** ** *** ** ** ** ** ** *** ** ** ** ** ** ");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(" ** ** ** ** ** *** ** ** ** ** ** *** ** ** ** ** ** ");

            //AFFICHER LE TEXTE DU MESSAGE VERT FONCÉ
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.SetCursorPosition(0, 3);
            Console.WriteLine("                                                      " +
            "\n                    Le jeu est-il un                  " +
            "\n               problème pour vous ou l’un             " +
            "\n                    de vos proches ?                  " +
            "\n                                                      " +
            "\n                 Nous pouvons vous aider.             " +
            "\n             24 / 7.Gratuit et confidentiel.          " +
            "\n                                                      " +
            "\n                  https://aidejeu.ca/                 " +
            "\n                                                      " +
            "\n           PAR TÉLÉPHONE 24 HEURES / 7 JOURS          " +
            "\n          Montréal et environs:  514 527-0140         " +
            "\n           Partout au Québec: 1 800 461-0140          " +
            "\n                                                      ");

            //AFFICHER LES ÉTOILES VERTE

            Console.ForegroundColor = ConsoleColor.Green;

            if (auRevoir == true)
            {
                Console.WriteLine(" ** ** ** ** ** *** ** ** ** ** ** *** ** ** ** ** ** ");
                Console.WriteLine(" ** ** ** ** ** * *  Au Revoir  * * ** ** ** ** ** ** ");
                Console.WriteLine(" ** ** ** ** ** *** ** ** ** ** ** *** ** ** ** ** ** ");
            }
            else
            {
                if (nbreTiragesConsecutifs > 1)
                {
                    Console.WriteLine(" ** ** ** ** ** *** ** ** ** ** ** *** ** ** ** ** ** ");
                    Console.WriteLine(" ** ** *** ** * Bienvenue à nouveau * ** ** *** ** ** ");
                    Console.WriteLine(" ** ** ** ** ** *** ** ** ** ** ** *** ** ** ** ** ** ");
                }
                else
                {
                    Console.WriteLine(" ** ** ** ** ** *** ** ** ** ** ** *** ** ** ** ** ** ");
                    Console.WriteLine(" ** ** ** ** ** ** * Bienvenue * ** ** ** ** ** ** ** ");
                    Console.WriteLine(" ** ** ** ** ** *** ** ** ** ** ** *** ** ** ** ** ** ");
                }
            }

            
            //INDICATION POUR QUITTER LE MESSAGE EN BLANC

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(5, 21);
            Console.WriteLine(" (Appuyez sur une touche pour quitter ce message.) ");
            Console.ReadKey(true);

        } //FIN public static void MessageJeuAideEtReference()

        static Boolean QuitterOuRecommencer()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Appuyez sur la touche \"J\" pour jouer encore.");
            Console.WriteLine("Appuyez sur la touche de votre choix pour quitter le jeu.");
            Console.WriteLine(";~J");

            if (Console.ReadKey(true).Key == ConsoleKey.J)
            {
                valide = true;
            }
            else
            {
                valide = false;
            }

            return (valide);
        }

        static void Main(string[] args)
        {
            do
            {
                nbreTiragesConsecutifs ++;

                MessageJeuAideEtReference();
                Titre();
                NbreParticipations();
                Console.WriteLine($" Nombre de combinaisons = {nbreCombinaisons}");
                Console.WriteLine(" ");

                GenererCombinaisons();
                ImprimerParticipations();

                Console.WriteLine(";~J");
                Console.WriteLine(" (Appuyez sur une touche pour révéler la combinaison gagnante.) ");
                Console.WriteLine(" ");
                Console.ReadKey(true);

                GenererCombinaisonsGagnante();
                ImprimerCombinaisonGagnante();
                
                Console.WriteLine(" ");
                
                IdentifierParticipationsGagnantes();

                Console.WriteLine(";~J");
                Console.WriteLine(" (Appuyez sur une touche pour afficher les statistiques.) ");
                Console.WriteLine(" ");
                Console.ReadKey(true);

                GenererStatistiques();

                Console.WriteLine(" ;~J");
                Console.WriteLine(" ");

            } while (QuitterOuRecommencer());

            auRevoir = true;
            MessageJeuAideEtReference();

        } //FIN static void Main(string[] args)


    } //FIN internal class Program


} //FIN namespace P24_TP1_649_2210001
