using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DiceBox
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            int playCount = 0; //This is used to determine each players go.

            DiceBox player1 = new();
            DiceBox player2 = new(); //initiate objects of each player

            while (true)
            {

                if (playCount % 2 == 0) //Determines whose go it is (alternates after each cycle).
                {
                    Console.WriteLine("Player 1 - press enter to roll the dice."); Console.ReadLine();
                    Console.WriteLine( $"Player 1 rolled a {player1.RollDice()}"); //Rolls the dice, returning an integer value (to display to the user), but this method also updates player 1's boolean array.
                    Console.WriteLine("\nPLAYER 1 SCOREBOARD");
                    player1.DisplayBox(); //Displays player one's boolean array values.

                    
                }
                else //Same as player one but with player two (I couldn't find a way of optimizing this part).
                {
                    Console.WriteLine("Player 2 - press enter to roll the dice."); Console.ReadLine();
                    Console.WriteLine($"Player 2 rolled a {player2.RollDice()}");
                    Console.WriteLine("\nPLAYER 2 SCOREBOARD");
                    player2.DisplayBox();
                }

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
        public bool[] diceValues { get; set; } //Boolean array that stores which values have been rolled.

        public int Lives { get; set; } //Stores the unique player's lives.


        public DiceBox() //Constructor.
        {
            diceValues = new bool[13]; //Initiates the boolean array.
            diceValues[0] = true; 
            diceValues[1] = true; //Positions 0 and 1 are set to true as they cannot be rolled naturally.

            Lives = 3; //Sets the players lives to 3.

        }

        public int RollDice() //This takes both dice values and returns the sum.
        {
            Random rng = new();

            int total = rng.Next(1, 7) + rng.Next(1, 7);

            if (diceValues[total] == true) //If this value has already been rolled, it deducts one from the players lives.
            {
                Lives--;
                Console.WriteLine($"That is a duplicate, lives = {Lives}"); //This outputs in the wrong order but ... oh well.
            }
            diceValues[total] = true; //Sets the value at the location of total to true.
            return total; //returns the outcome of the dice roll (to display to the user).

        }

        public void DisplayBox() //This is a simple for loop which outputs the values in the array from 2-12.
        {

            for (int i = 2; i < diceValues.Length; i++)
            {
                Console.WriteLine($"{i} = {diceValues[i]}");
            }
        }

        public bool CheckWin() //This checks whether the entire array is set to true (and if so, the player has won).
        {
            if (Array.TrueForAll(diceValues, e => e.Equals(true)))
            {
                return true;
            }
            return false;
        }
    }
}