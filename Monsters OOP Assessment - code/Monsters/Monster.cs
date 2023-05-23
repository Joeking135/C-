using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsters
{
    class Monster :  Character
    {

        private int AttackAmount;
        public Boolean Dead;
        static Random R = new Random();

        public Monster(int X, int Y, ConsoleColor C, Char I, int A) :  base (X,Y,C,I)
        {
            AttackAmount = A;
            Dead=false;
        }

        public void Attack(Player P)
        {

        }


        public void Kill()
        {
            Dead=true;
        }

        public void MoveTowardsPlayer(Player P, int screenW, int screenH)
        {
            direction d=direction.none;
            d = (direction)R.Next(1, 5);
            move(d,screenW, screenH);
        }
    }
}
