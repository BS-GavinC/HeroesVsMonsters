using System.Text;
using HeroesVsMonsters.Jeu;
using HeroesVsMonsters.Models;

Console.OutputEncoding = Encoding.UTF8;
Nain nain = new Nain("Gimli");

Jeu.DemarrerJeu(nain, 50);
