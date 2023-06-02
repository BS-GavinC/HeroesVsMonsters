using HeroesVsMonsters.Enums;
using HeroesVsMonsters.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Models
{
    public class Loup : Monstre
    {
        public Loup(string name) : base(name, 0,0)
        {
            De de = new De(1, 4);
            Inventaire.Add(Items.Cuir, de.Lancer());
        }
    }
}
