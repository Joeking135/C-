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
            while (true)
            {
                game.Play();

                Console.WriteLine("\nGame over. Hit a key to play again.");
                Console.ReadKey();
                Console.Clear();
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