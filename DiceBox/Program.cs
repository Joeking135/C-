using System;

namespace DiceBox
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            int playCount = 0; //This is used to determine each players go.
            int playerIdentifier;
            bool gameOver;

            DiceBox player1 = new();
            DiceBox player2 = new(); //initiate objects of each player

            do
            {
                playerIdentifier = (playCount % 2) + 1; //This just identifies the player number.

                if (playerIdentifier == 1) //Determines which player is playing.
                {
                    gameOver = Turn(player1, playerIdentifier); 
                }
                else
                {
                    gameOver = Turn(player2, playerIdentifier);
                }

                Console.WriteLine("Hit a key"); Console.ReadKey(); Console.Clear();
                playCount++; 

            }while (!gameOver); // determines whether the game has finished.



        }

        private static bool Turn(DiceBox currentPlayer, int playerIdentifier) //This takes in the currentPlayer and identifier parameter and uses it to make each player move.
        {
            Console.WriteLine($"Player {playerIdentifier} - press enter to roll the dice."); 
            Console.ReadLine();
            Console.WriteLine($"Player {playerIdentifier} rolled a {currentPlayer.RollDice()}"); //User rolls the dice. (duplicate also handled)

            if (currentPlayer.Duplicate) //If the player has rolled a duplicate.
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nTHAT IS A DUPLICATE");
                Console.ForegroundColor = ConsoleColor.White ;
            }


            Console.WriteLine($"\nPLAYER {playerIdentifier} SCOREBOARD"); //Outputs scoreboard.
            currentPlayer.DisplayBox();

            Console.WriteLine($"\nPlayer {playerIdentifier} lives = {currentPlayer.Lives}\n"); //Displays current players lives.

            if (currentPlayer.Lives <= 0) // If the player has no lives left, they lose (opposition win)
            {
                Console.WriteLine($"Player {playerIdentifier} Loses.");
                return true;
            }
            
            if (currentPlayer.CheckWin()) //If the player manages to score all of the combinations they win.
            {
                Console.WriteLine($"Player {playerIdentifier} Wins.");
                return true;
            }
            return false; //else the game continues.
            
        }
    }

    public class DiceBox 
    {
        public bool[] DiceValues { get; set; } //Boolean array that stores which values have been rolled.

        public int Lives { get; set; } //Stores the unique players lives.

        public bool Duplicate { get; set; } //Boolean to represent if the player has rolled a duplicate


        public DiceBox() //Constructor.
        {
            DiceValues = new bool[13]; //Initiates the boolean array.
            DiceValues[0] = true; 
            DiceValues[1] = true; //Positions 0 and 1 are set to true as they cannot be rolled naturally.

            Lives = 3; //Sets the players lives to 3.
            Duplicate = false;

        }

        public int RollDice() //This takes both dice values and returns the sum.
        {
            Random rng = new();


            int total = rng.Next(1, 7) + rng.Next(1, 7);

            if (DiceValues[total] == true) //If this value has already been rolled, it deducts one from the players lives.
            {
                Lives--;
                Duplicate = true; //Used to output text later.
            }
            else
            {
                Duplicate = false;
                DiceValues[total] = true; //Sets the value at the location of total to true.
            }
            
            return total; //returns the outcome of the dice roll (to display to the user).

        }

        public void DisplayBox() //This is a simple for loop which outputs the values in the array from 2-12.
        {

            for (int i = 2; i < DiceValues.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red; //The colours are changed to make them easier to read (provided you are not red-green colourblind).
                if (DiceValues[i] == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine($"{i} = {DiceValues[i]}");
            }

            Console.ForegroundColor = ConsoleColor.White;

        }

        public bool CheckWin() //This checks whether the entire array is set to true (and if so, the player has won).
        {
            if (Array.TrueForAll(DiceValues, e => e.Equals(true)))
            {
                return true;
            }
            return false;
        }
    }
}