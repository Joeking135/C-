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
            int relativeX = PosX - P.PosX;
            int relativeY = PosY - P.PosY;

            (int xMagnitude, int yMagnitude) = (Math.Abs(relativeX), Math.Abs(relativeY));


            if (xMagnitude == 0 && yMagnitude == 0)
            {
                d = direction.none; 
            }
            else if (xMagnitude > yMagnitude)
            {
                if (relativeX > 0)
                {
                    d = direction.west; 
                } 
                else
                {
                    d = direction.east;
                }
            } 
            else 
            {
                if (relativeY > 0)
                {
                    d = direction.north; 
                } 
                else
                {
                    d = direction.south;
                }
            }
            


            move(d, screenW, screenH);
        }
    }
}
