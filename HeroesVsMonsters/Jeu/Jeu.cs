using HeroesVsMonsters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Jeu
{
    public class Jeu
    {


        public static bool[,] Plateau { get; set; } = new bool[15, 15];

        public static Hero ActualHero;

        public static List<Personnage> personnages = new List<Personnage>();

        public static void DemarrerJeu(Hero hero, int nbrMonstre)
        {
            Plateau = new bool[15, 15];
            personnages = new List<Personnage>();
            ActualHero = hero;
            personnages.Add(hero);
            hero.CoordX = 0;
            hero.CoordY = 0;
            Plateau[0, 0] = true;
            peuplerPlateau(nbrMonstre);
            AfficherTableau();
            while (!ActualHero.IsDead && personnages.Count() > 1)
            {
                AfficherTableau();
                if (EnnemiProche() is Monstre m)
                {
                    Combat(ActualHero, m);
                }
                ConsoleKey key = Console.ReadKey().Key;
                Mouvement(key);
                
            }
            if (hero.IsDead)
            {
                GameOver();
                
            }
            else
            {
                Victoire();
            }
        }
        public static void peuplerPlateau(int nbrMonstre)
        {

            for (int i = 0; i < nbrMonstre; i++)
            {
                Random rdn = new Random();
                Monstre monstre;
                switch (rdn.Next(1,4))
                {
                    case 1:
                        monstre = new Loup("BigBadWolf");
                        break;
                    case 2:
                        monstre = new Orc("SauvéWilly");
                        break;
                    default:
                        monstre = new Dragonnet("Norbert");
                        break;
                }

                for (int j = 0; j <= 50; j++)
                {
                    
                    Random rdn1 = new Random();
                    int randomX = rdn1.Next(0, 15);
                    int randomY = rdn1.Next(0, 15);
                    if (LaPlaceEstLibre(randomX, randomY))
                    {
                        
                        monstre.CoordX = randomX;
                        monstre.CoordY = randomY;
                        Plateau[randomX, randomY] = true;
                        personnages.Add(monstre);
                        j = 51;
                    }

                }
            }

        }
        public static void Combat(Hero hero, Monstre monstre)
        {
            while(!hero.IsDead && !monstre.IsDead)
            {
                
                animationCombat(true);
                hero.Frappe(monstre);
                Thread.Sleep(2000);
                if (!monstre.IsDead)
                {
                    animationCombat(false);
                    monstre.Frappe(hero);
                    Thread.Sleep(2000);
                }
                
            }

            if (hero.IsDead)
            {
                
                return;
            }
            Console.Clear();
            Console.WriteLine("🧝   💀");
            Console.WriteLine($"{hero.Name} a terrassé {monstre.Name}.");
            Thread.Sleep(1000);
            hero.Loot(monstre);
            Thread.Sleep(1000);
            hero.RegenererVie();
            Console.ReadKey();
            personnages.Remove(monstre);
            Plateau[monstre.CoordX, monstre.CoordY] = false;

        }
        public static void GameOver()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GAME OVER");
            Console.ResetColor();
        }
        private static bool LaPlaceEstLibre(int x, int y)
        {
            foreach (Personnage p in personnages)
            {
                if (Math.Abs(p.CoordX - x) < 3 && Math.Abs(p.CoordY - y) < 3)
                {
                    return false;
                }
            }
            Console.WriteLine($"Monstre placé en {x}-{y}");
            return true;
        }
        public static void AfficherTableau(int? x = null, int? y = null)
        {
            Console.Clear();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {

                    if (!Plateau[i,j])
                    {
                        if (x == i && y == j)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[X]");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write("[ ]");
                        }
                        
                    }

                    else
                    {
                        foreach (Personnage p in personnages)
                        {
                            if (p.CoordX == i && p.CoordY == j)
                            {

                                if (p is Hero)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write("[H]");
                                    Console.ResetColor();
                                }
                                else if(p is Orc)
                                {
                                    Console.Write("[O]");
                                }
                                else if(p is Dragonnet)
                                {
                                    Console.Write("[D]");
                                }
                                else
                                {
                                    Console.Write("[L]");
                                }

                            }
                        }
                    }

                }
                Console.WriteLine("");
            }
        }

        public static void Mouvement(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Escape:
                    break;
                case ConsoleKey.LeftArrow:
                    Bouger(0, -1);
                    break;
                case ConsoleKey.UpArrow:
                    Bouger(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    Bouger(0, 1);
                    break;
                case ConsoleKey.DownArrow:
                    Bouger(1, 0);
                    break;
                default:
                    break;
            }
        }

        private static void Bouger(int x, int y)
        {
            if (x != 0)
            {
                if (ActualHero.CoordX + x <= 14 && ActualHero.CoordX + x >= 0)
                {
                    Plateau[ActualHero.CoordX, ActualHero.CoordY] = false;
                    Plateau[ActualHero.CoordX + x, ActualHero.CoordY] = true;
                    ActualHero.CoordX += x;
                }
            }
            else{
                if (ActualHero.CoordY + y <= 14 && ActualHero.CoordY + y >= 0)
                {
                    Plateau[ActualHero.CoordX, ActualHero.CoordY] = false;
                    Plateau[ActualHero.CoordX, ActualHero.CoordY + y] = true;
                    ActualHero.CoordY += y;
                }
            }

            
        }

        public static Monstre? EnnemiProche()
        {
            foreach (Personnage p in personnages)
            {
                if (p is Monstre)
                {
                    if ((p.CoordX == ActualHero.CoordX && Math.Abs(p.CoordY - ActualHero.CoordY) == 1) || (p.CoordY == ActualHero.CoordY && Math.Abs(p.CoordX - ActualHero.CoordX) == 1))
                    {
                        return (Monstre)p;
                    }

                }
            }
            return null;
        }

        public static void Victoire()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("YIPPPPPIIIIIIIIIIII");
            Console.ResetColor();
        }

        private static void animationCombat(bool heroAttack)
        {
            Console.Clear();
            if (heroAttack)
            {
                Console.WriteLine("🧝   🧌");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine(" 🧝   🧌");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine("🧝   🧌");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine(" 🧝   🧌");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine("🧝   🧌");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine("🧝🤜💥🧌");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("🧝🤜   🧌");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("🧝🤜    🧌");
                Thread.Sleep(200);
            }
            else
            {
                Console.WriteLine("🧝   🧌");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine(" 🧝   🧌");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine("🧝   🧌");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine(" 🧝   🧌");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine("🧝   🧌");
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine("🧝 🤛🧌");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("🧝 🤛🧌");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("🧝 🤛🧌");
                Thread.Sleep(200);
            }

        }

    }
}
