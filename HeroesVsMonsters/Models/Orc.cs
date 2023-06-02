using HeroesVsMonsters.Enums;
using HeroesVsMonsters.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Models
{
    public class Orc : Monstre
    {
        public Orc(string name) : base(name, 0, 1)
        {
            
            De de = new De(1, 6);
            Inventaire.Add(Items.Or, de.Lancer());
        }
    }
}
