using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsters
{
    public enum direction
    {
        none,
        north,
        south,
        east,
        west
    }

    class Program
    {
        const int SCREENWIDTH = 80;
        const int SCREENHEIGHT = 40;

        static void Main(string[] args)
        {

            int MonsterSpeed = 500;
            int SurviveTime = 0;
            int waitTime = 0;
            Player P = new Player(SCREENWIDTH / 2, SCREENHEIGHT / 2, ConsoleColor.Yellow, '@', 20);
            Monsters Mons = new Monsters(20, SCREENWIDTH, SCREENHEIGHT);
            Collectible C = new Collectible(5, 5, ConsoleColor.Green, '$');
            Console.CursorVisible = false;
            Console.SetWindowSize(SCREENWIDTH, SCREENHEIGHT);


            
            //game loop
            while (!P.Dead() && Mons.MonstersLeft() > 0)
            {
                Console.SetCursorPosition(1, 0);
                Console.WriteLine("Health = {0}, Survived Time = {1}, Monsters left = {2}", P.Health, SurviveTime / 1000, Mons.MonstersLeft());
                P.display();
                Mons.DisplayMonsters();
                Mons.Attack(P);
                C.display();
                Console.SetCursorPosition(0, 0);
                C.Collected(P, SCREENWIDTH, SCREENHEIGHT);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo K = Console.ReadKey(true);
                    switch (K.Key)
                    {
                        case ConsoleKey.W:
                            P.Dir = direction.north;
                            P.move(direction.north, SCREENWIDTH, SCREENHEIGHT);
                            break;
                        case ConsoleKey.S:
                            P.Dir = direction.south;
                            P.move(direction.south, SCREENWIDTH, SCREENHEIGHT);
                            break;
                        case ConsoleKey.A:
                            P.Dir = direction.west;
                            P.move(direction.west, SCREENWIDTH, SCREENHEIGHT);
                            break;
                        case ConsoleKey.D:
                            P.Dir = direction.east;
                            P.move(direction.east, SCREENWIDTH, SCREENHEIGHT);
                            break;
                        case ConsoleKey.Spacebar:
                            P.Zap(Mons, SCREENWIDTH, SCREENHEIGHT);
                            break;
                        default:
                            break;
                    }
                }
                System.Threading.Thread.Sleep(100);
                UpdateTime(ref waitTime, ref SurviveTime, ref MonsterSpeed, Mons, P);

            }
            Console.Clear();
            Console.WriteLine("Game Over");
            if (P.Health > 0)
            {
                Console.WriteLine("YOU WIN!");
            }
            else
            {
                Console.WriteLine("YOU LOSE!");
            }
            Console.ReadLine();
        }

        public static void UpdateTime(ref int waitTime, ref int SurviveTime, ref int MonsterSpeed, Monsters Mons, Player P)
        {
            waitTime += 100;
            SurviveTime += 100;

            if (waitTime > MonsterSpeed)
            {
                Mons.MoveTowardsPlayer(P, SCREENWIDTH, SCREENHEIGHT);
                waitTime = 0;
            }
        }
    }
}
