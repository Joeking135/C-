using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsters
{
    class Monsters
    {
        private List<Monster> _MonsterList = new List<Monster>();

        internal List<Monster> MonsterList
        {
            get { return _MonsterList; }
            set { _MonsterList = value; }
        }

        private Random R = new Random();

        public Monsters(int NoOfMonstersToSpawn, int width, int height)
        {
            for (int i = 0; i < NoOfMonstersToSpawn; i++)
            {
                int x = R.Next(1, width);
                int y = R.Next(1, height);
                while (x == width / 2 && y == height / 2)
                {
                    x = R.Next(1, width);
                    y = R.Next(1, height);
                }

                Monster M = new Monster(x, y, ConsoleColor.Red, 'X', R.Next(1, 4));
                _MonsterList.Add(M);
            }
        }

        public void DisplayMonsters()
        {
            foreach (Monster M in _MonsterList)
            {
                if (!M.Dead)
                {
                    M.display();
                }
            }
        }

        public void Attack(Player P)
        {
            foreach (Monster M in _MonsterList)
            {
                if (!M.Dead)
                {
                    M.Attack(P);
                }
            }
        }

        public int MonstersLeft()
        {
            return MonsterList.Count(e => e.Dead == false);
        }

        public void MoveTowardsPlayer(Player P, int screenW, int screenH)
        {
            foreach (Monster M in _MonsterList)
            {
                if (!M.Dead)
                {
                    M.MoveTowardsPlayer(P, screenW, screenH);
                }
            }
        }

    }
}

