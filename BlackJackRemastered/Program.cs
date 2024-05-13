using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;



namespace BlackJackRemastered
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BlackJack game = new BlackJack();
            game.Play();

            while (true)
            {

                Console.WriteLine("\nGame over."); 

                int menuSelection = 2;
                if (game.PlayersLeft)
                {
                    menuSelection = GetUserInput<int>
                    (
                        input => input < 1 || input > 2,
                        "\n1. Play Again\n2. Reset Table\n\nInput: ",
                        "Invalid Input"
                    );   
                }
                else
                {
                    Console.WriteLine("There are no players left. Press enter to restart.");
                    Console.ReadLine();
                }
                

                switch (menuSelection)
                {
                    case 1:
                        break;
                    case 2:
                        game = new BlackJack();
                        break;
                }

                game.Play();
            }
        }


        public delegate bool FailCondition<T>(T input);
        public static T GetUserInput<T>(FailCondition<T> failCondition, string request, string errorMessage)
        {
            while (true)
            {
                try
                {
                    Console.Write(request);

                    T output = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(Console.ReadLine());

                    if (failCondition(output))
                    {
                        throw new SystemException(); 
                    }   

                    return output;
                }
                catch 
                {
                    Console.WriteLine(errorMessage);    
                }
                
            }
        }
    }

    
}