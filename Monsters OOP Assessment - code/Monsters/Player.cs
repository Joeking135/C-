using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsters
{
    class Player : Character
    {
        private int _Health;

        public int Health
        {
            get { return _Health; }
        }

        private direction _Dir;

        public direction Dir
        {
            get { return _Dir; }
            set { _Dir = value; }
        }

        public void Zap(Monsters Mons, int screenW, int screenH)
        {
            for (int i = _PosY; i < screenH; i++) // south
            {
                foreach (Monster M in Mons.MonsterList)
                {
                    if (M.PosY == i && M.PosX == _PosX)
                    {
                        M.Kill();
                    }
                }
            }

        }


        public Boolean Dead()
        {
            if (_Health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Player(int StartX, int StartY, ConsoleColor C, Char I, int H) : base(StartX, StartY, C, I)
        {
            _Health = H;
            _Dir = direction.north;
        }


        public void LoseHealth(int attackAmount)
        {
            _Health = _Health - attackAmount;
        }

        public void GainHealth(int gainAmount)
        {
            _Health = _Health + gainAmount;
        }

    }
}
