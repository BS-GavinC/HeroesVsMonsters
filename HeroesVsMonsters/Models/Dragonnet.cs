using HeroesVsMonsters.Enums;
using HeroesVsMonsters.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Models
{
    public class Dragonnet : Monstre
    {
        public Dragonnet(string name) : base(name, 1, 0)
        {
            
            De de = new De(1, 4);
            Inventaire.Add(Items.Cuir, de.Lancer());
            de = new De(1, 6);
            Inventaire.Add(Items.Or, de.Lancer());
        }
    }
}
