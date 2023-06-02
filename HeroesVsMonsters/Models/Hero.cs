using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Models
{
    public abstract class Hero : Personnage
    {

        public Hero(string name, int bonusEndu, int bonusForce) : base(name ,bonusEndu, bonusForce)
        {
            PVMax = PV;
        }
        private int PVMax { get; set; }

        public void RegenererVie()
        {
            PV = PVMax;
            Console.WriteLine($"{Name} regenere sa vie à {PV} points.");
        }
        public void Loot(Monstre cible)
        {

            foreach(var item in cible.Inventaire)
            {
                Console.WriteLine($"{Name} loot {item.Value} {item.Key} sur {cible.Name}");
                if (Inventaire.ContainsKey(item.Key))
                {
                    Inventaire[item.Key] += item.Value;
                }
                else
                {
                    Inventaire.Add(item.Key, item.Value);
                }
                
            }
            cible.Inventaire.Clear();
        }

    }
}
