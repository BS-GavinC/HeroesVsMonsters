using HeroesVsMonsters.Enums;
using HeroesVsMonsters.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Models
{
    public abstract class Personnage
    {

        public Personnage(string name, int bonusEndu, int bonusForce)
        {
            Name = name;
            De de = new De(1, 6);
            _end = de.Meilleurs(4, 3);
            BonusEnd = bonusEndu;
            BonusFor = bonusForce;
            _for = de.Meilleurs(4, 3);
            PV = End + Modificateur.BonusMalus(End);
            Console.WriteLine(this.ToString());
        }

        public int CoordX { get; set; }
        public int CoordY { get; set; }

        public string Name { get; set; }

        private int _end;
        public int End 
        { get 
            {
                return _end + BonusEnd;
            } 
        }

        public int BonusEnd { get; set; }

        private int _for;
        public int For 
        {
            get
            {
                return _for + BonusFor;
            }
        }

        public int BonusFor { get; set; }

        private int _pv;
        public int PV 
        { get 
            {
                return _pv;
            } 
            set 
            {
                if (value <= 0)
                {
                    _pv = 0;
                }
                else
                {
                    _pv = value;
                }
            } 
        }

        public bool IsDead 
        { get
            {
                return PV <= 0;
            } 
        }

        public Dictionary<Items, int> Inventaire { get; set; } = new Dictionary<Items, int>();

        public void Frappe(Personnage cible)
        {
            De de = new De(1, 4);
            int dommages = de.Lancer() + Modificateur.BonusMalus(For);
            cible.PV -= dommages;

            Console.WriteLine($"{Name} attaque {cible.Name} et lui inflige {dommages} Pv.");
            Console.WriteLine($"{cible.Name} n'a plus que {cible.PV} Pv.");
        }

        public override string ToString()
        {
            return $"{Name} - Endu : {End}, Force : {For}, Pv : {PV}";
        }

    }
}
