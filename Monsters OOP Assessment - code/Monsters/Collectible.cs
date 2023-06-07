using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsters
{
    class Collectible : Character
    {
        private Random R = new Random();

        public Collectible(int X, int Y, ConsoleColor C, char I) : base(X, Y, C, I)
        { }

        public void Collected(Player P, int width, int height)
        {
            if (P.PosX == PosX && P.PosY == PosY)
            {
                P.GainHealth(R.Next(3, 7));

                _PosX = R.Next(0, width);
                _PosY = R.Next(0, height);
            }
        }
    }
}
