using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsters
{
    class Monster : Character
    {

        private int AttackAmount;
        public Boolean Dead;
        static Random R = new Random();

        public Monster(int X, int Y, ConsoleColor C, Char I, int A) : base(X, Y, C, I)
        {
            AttackAmount = A;
            Dead = false;
        }

        public void Attack(Player P)
        {
            if (PosX == P.PosX && PosY == P.PosY)
            {
                P.LoseHealth(AttackAmount);
            }
        }


        public void Kill()
        {
            Dead = true;
        }

        public void MoveTowardsPlayer(Player P, int screenW, int screenH)
        {
            direction d = direction.none;
            //d = (direction)R.Next(1, 5);

            int relativeX = P.PosX - PosX;
            int relativeY = P.PosY - PosX;

            if (relativeX > relativeY)
            {
                if (relativeX > 0)
                {
                    d = direction.east;
                }
                else if (relativeX < 0)
                {
                    d = direction.west;
                }
                
            }
            else if (relativeY > relativeX)
            {
                if (relativeY > 0)
                {
                    d = direction.south;
                }
                else if (relativeY < 0)
                {
                    d = direction.north; 
                }
            }
            


            move(d, screenW, screenH);
        }
    }
}
