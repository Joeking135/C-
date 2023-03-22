using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace EscapeTheMaze
{
    class Program
    {
        //arrays to hold maze
        static int mazewidth = 15;
        static int mazeheight = 15;
        // maze data a 1 holds  wall
        static int[,] maze = new int[mazeheight + 1, mazewidth + 1];
        static int turtleX = 1; // starting position of turtle in maze
        static int turtleY = 1;

        public const string FileName = "MazeSave.txt";


        enum orientation
        {
            north,
            east,
            south,
            west
        }

        static orientation turtleOrientation = orientation.south;


        static void Main(string[] args)
        {
            //set up simulation window
            Console.Title = "MAZEGAME";
            Console.SetWindowSize(75, 40);
            Console.SetWindowPosition(0, 0);
            bool quit = false;

            string UserChoice = GetMenuChoice();
            if (UserChoice == "1")
            {
                //design Maze
                Introduction();
                Console.Clear();
                planMaze();

            }
            if (UserChoice == "2")
            //pre loaded maze from file
            {
                LoadMaze();

            }
            if (UserChoice == "3")
            // random maze
            {
                GenerateMaze();
            }


            if (GetMenuChoice2() == "1")
            {
                DrawMaze();
                //manual escape instructions
                while (quit == false)
                {
                    string instructions = GetInstructions();

                    //Get Instructions
                    // Fx (e.g. F8 move forward 8 steps - you can only move forward up to 9 steps)
                    // L   (turn the turtle 90 degrees left)
                    // R   (turn the turtle 90 degress right)

                    // so a example would be F8LF4R

                    MoveTurtle(ref quit, instructions);

                    DrawMaze();

                }
                Console.SetCursorPosition(32, 20);
                Console.WriteLine("Game over");
                Console.ReadKey();
            }
            else
            {
                // turtle automaticaly escapes
                DrawMaze();
                ComputerEscape();
            }

        }


        //---------------------------------------------------------------------------------------------------

        static void Introduction()
        {
            //this is the introduction screen
            Console.WriteLine("BUILD MAZE");
            Console.WriteLine();
            Console.WriteLine("To set up your starting world use the following keys..");
            Console.WriteLine("Arrow keys  - move around the screen");
            Console.WriteLine("Space Bar   - places a cell in that location");
            Console.WriteLine("Escape key  - To end setup");
            Console.WriteLine();
            Console.WriteLine("Hit any key to continue");
            Console.ReadKey();
            Console.Clear();
        }



        //-------------------------------------------------------------------------------------------------

        static void planMaze()
        {
            // this routine sets up your starting world
            Boolean setupcomplete = false;
            DrawMaze();
            int cursorx = 1; //holds x position of cursor
            int cursory = 1; // holds y position of cursor
            Console.SetCursorPosition(cursorx, cursory);
            while (!setupcomplete)
            {
                //has a key been pressed?
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey();
                    switch (cki.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (cursory > 1)
                            {
                                cursory = cursory - 1;
                            }
                            break;

                        case ConsoleKey.DownArrow:
                            if (cursory < mazeheight)
                            {
                                cursory = cursory + 1;
                            }
                            break;

                        case ConsoleKey.LeftArrow:
                            if (cursorx > 1)
                            {
                                cursorx = cursorx - 1;
                            }
                            break;

                        case ConsoleKey.RightArrow:
                            if (cursorx < mazewidth)
                            {
                                cursorx = cursorx + 1;
                            }
                            break;

                        case ConsoleKey.Spacebar:
                            // place a cell in the array at this position
                            // remember screen is x, y
                            //but arrays are row, column (y,x)!!
                            maze[cursory, cursorx] = (maze[cursory, cursorx] + 1) % 2;
                            maze[1, 1] = 0; //start cant have a wall
                            maze[mazeheight, mazeheight] = 0; //exit cant have a wall
                            break;

                        case ConsoleKey.Escape:
                            // Escape key ends setup and starts the simulation
                            setupcomplete = true;
                            break;
                    }
                    DrawMaze();
                    Console.SetCursorPosition(cursorx, cursory);
                }

            }
            // finished while loop - so must have pressed escape key
            Console.SetCursorPosition(0, 18);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Press Any key to now start the challenge");
            Console.ReadKey();
        }


        //-----------------------------------------------------------------------------------------------------

        static void DrawMaze()
        {
            // this routine will display the currentgeneration array on the console
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < mazewidth + 1; i++)
            {
                Console.Write(i % 10);
            }

            for (int i = 0; i < mazeheight + 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(i % 10);
                Console.SetCursorPosition(mazewidth + 1, i);
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, mazeheight + 1);
            for (int i = 0; i < mazewidth + 1; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(mazewidth, mazeheight);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;

            for (int r = 1; r < mazeheight + 1; r++)
            {
                for (int c = 1; c < mazewidth + 1; c++)
                {
                    if (maze[r, c] == 1)
                    {
                        Console.SetCursorPosition(c, r);
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                } //inner FOR loop   
            } //outer FOR looop
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(20, 5);
            Console.Write("N");
            Console.SetCursorPosition(18, 6);
            Console.Write("W + E");
            Console.SetCursorPosition(20, 7);
            Console.Write("S");
            Console.ForegroundColor = ConsoleColor.Green;
            if (turtleX > 0 && turtleY > 0)
            {
                Console.SetCursorPosition(turtleX, turtleY);
            }

            Console.Write("O");
        }


        //-----------------------------------------------------------------------------------------------------


        static string GetInstructions()
        {
            string instructions;
            Console.SetCursorPosition(0, 18);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter Your movement instructions to escape from the maze");
            Console.WriteLine("Your goal is to reach the yellow escape door without hitting any obstacles");
            Console.WriteLine("");
            Console.WriteLine("e.g. F5RF4L = Move forward 5 turn right, Move forward 4 turn left.");
            Console.WriteLine("Turtle is pointing SOUTH to start");

            bool validInput = false;

            do
            {
                Console.Write(">");
                instructions = Console.ReadLine().ToUpper();

                if (!System.Text.RegularExpressions.Regex.IsMatch(instructions, "^((F[0-9])|R|L)+$"))
                {
                    Console.WriteLine("Invalid Instructions");
                }
                else
                {
                    validInput = true;
                }
            } while (!validInput);

            return instructions;
        }

        //-----------------------------------------------------------------------------------------------------

        static void MoveTurtle(ref bool quit, string instructions)
        {
            

            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i] == 'F')
                {
                    int steps = int.Parse(instructions[i + 1].ToString());

                    switch (turtleOrientation)
                    {


                        case orientation.north:
                            turtleY -= steps;
                            break;
                        case orientation.east:
                            turtleX += steps;
                            break;
                        case orientation.south:
                            turtleY += steps;
                            break;
                        case orientation.west:
                            turtleX -= steps;
                            break;
                        default:
                            break;
                    }

                }
                else if (instructions[i] == 'L')
                {
                    turtleOrientation = (turtleOrientation == orientation.north) ? (orientation)3 : (orientation)(((int)turtleOrientation - 1) % 4);
                }
                else if (instructions[i] == 'R')
                {
                    turtleOrientation = (turtleOrientation == orientation.west) ? (orientation)0 : (orientation)(((int)turtleOrientation + 1) % 4);
                }

                quit = Collision();



            }






        }

        //-----------------------------------------------------------------------------------------------------


        static void LoadMaze()
        {
            
            StreamReader file = new(FileName);
            int lineNumber = 0;

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();

                for (int i = 0; i < line.Length; i++)
                {
                    maze[lineNumber, i] = int.Parse(line[i].ToString());
                }
                lineNumber++;
            }

            file.Close();

        }


        static void GenerateMaze()
        {
            // this routine will place random walls onto the grid

        }

        static bool Collision()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            if (turtleX > 15 || turtleY > 15)
            {
                DrawMaze();
                Console.SetCursorPosition(32, 20);
                Console.WriteLine("You died!");
                Console.ReadKey();
                return true;
            }

            if (maze[turtleY, turtleX] == 1 || (turtleX < 1 || turtleY < 1))
            {
                DrawMaze();
                Console.SetCursorPosition(32, 20);
                Console.WriteLine("You died!");
                Console.ReadKey();
                return true;

            }
            return false;
        }

        static void ComputerEscape()
        {
            //https://en.wikipedia.org/wiki/A*_search_algorithm#:~:text=A*%20is%20an%20informed%20search,shortest%20time%2C%20etc.).
            // https://www.geeksforgeeks.org/a-search-algorithm/

            // attempt an A* algorithm for finding a route out of the maze

        }

        static bool Escaped()
        {
            if (turtleX == 15 && turtleY == 15)
            {
                DrawMaze();
                Console.SetCursorPosition(32, 20);
                Console.WriteLine("You escaped!");
                Console.ReadKey();
                return true;
            }
            return false;
        }

        static string GetMenuChoice()
        {
            string response;
            do
            {
                Console.Clear();
                Console.WriteLine("MENU");
                Console.WriteLine("1. Plan your own maze");
                Console.WriteLine("2. Load a maze from file");
                Console.WriteLine("3. Generate a random maze");
                Console.Write("Choice :>");
                response = Console.ReadLine();
            }
            while (!"123".Contains(response));
            return response;

        }
        static string GetMenuChoice2()
        {
            string response;
            do
            {
                Console.Clear();
                Console.WriteLine("MENU");
                Console.WriteLine("1. Enter Escape Instructions");
                Console.WriteLine("2. Computer Escape");
                Console.Write("Choice :>");
                response = Console.ReadLine();
            }
            while (response != "1" && response != "2");
            return response;

        }
    }
}
