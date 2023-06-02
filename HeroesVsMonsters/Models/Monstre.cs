using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Models
{
    public abstract class Monstre : Personnage
    {
        public Monstre(string name, int bonusEndu, int bonusForce) : base(name, bonusEndu, bonusForce)
        {

        }
    }
}
