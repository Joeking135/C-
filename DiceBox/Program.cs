using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DiceBox
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            int playCount = 0; //This is used to determine each players go.
            int player;

            DiceBox player1 = new();
            DiceBox player2 = new(); //initiate objects of each player

            while (true)
            {
                player = (playCount % 2) + 1;

                Console.WriteLine($"Player {player} - press enter to roll the dice."); Console.ReadLine();
                Console.Write($"Player {player} rolled a ");

                if (player == 1) //Determines whose go it is (alternates after each cycle).
                {
                    Console.WriteLine(player1.RollDice()); //Rolls the dice, returning an integer value (to display to the user), but this method also updates player 1's boolean array.
                    Console.WriteLine($"PLAYER {player} SCORESHEET \n");
                    player1.DisplayBox(); //Displays player one's boolean array values.
                }
                else //Same as player one but with player two 
                {
                    Console.WriteLine(player2.RollDice());
                    Console.WriteLine($"PLAYER {player} SCORESHEET \n"); //Unsure on how to prevent this repeated code
                    player2.DisplayBox();
                }

                if (player1.Duplicate || player2.Duplicate)
                {
                    Console.WriteLine($"\n PLAYER {player} ROLLED A DUPLICATE"); 
                    player1.Duplicate = false; player2.Duplicate = false; //Yuck... 
                    
                }

                Console.WriteLine($"\nLives: Player 1 = {player1.Lives}\n       Player 2 = {player2.Lives}\n"); //outputs the lives of each player.


                if (player1.CheckWin() || player2.Lives <= 0) //Checks whether each player has rolled all of the possible integers, or if the other player has run out of lives.
                {
                    Console.WriteLine("Player 1 wins the game.");
                    break;
                }
                else if (player2.CheckWin() || player1.Lives <= 0 )
                {
                    Console.WriteLine("Player 2 wins the game.");
                    break;
                }

                Console.WriteLine("Hit a key"); Console.ReadKey(); Console.Clear();
                playCount++; 

            }

        }
    }

    public class DiceBox 
    {
        public bool[] DiceValues { get; set; } //Boolean array that stores which values have been rolled.

        public int Lives { get; set; } //Stores the unique player's lives.

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
                Console.ForegroundColor = ConsoleColor.Red;
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