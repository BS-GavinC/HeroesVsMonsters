using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Tools
{
    public class De
    {
        public De(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; }

        public int Max { get; }


        public int Lancer()
        {
            Random rdn = new Random();
            return rdn.Next(Min, Max + 1);
        }


        public int Meilleurs(int nbrDe, int nbrSelection)
        {
            if (nbrSelection > nbrDe)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("La Fonction 'Meilleurs' de De à une valeur nbrSelection supérieur à nbrDe.");
                Console.ResetColor();
                return 0;
            }

            List<int> listeJets = new List<int>();

            for (int i = 0; i < nbrDe; i++)
            {
                listeJets.Add(Lancer());
            }

            listeJets.Sort();
            listeJets.Reverse();

            int somme = 0;

            for (int i = 0; i < nbrSelection; i++)
            {
                somme += listeJets[i];
            }

            return somme;
        }

    }
}
