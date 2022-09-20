using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Loops1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter exercise number (1-4): ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    ex1(); break;
                case 2:
                    ex2(); break;
                case 3:
                    ex3(); break;
                case 4:
                    ex4(); break;  
                default:
                    Console.WriteLine("Not 1-4"); break;
            }


            Console.ReadLine();
        }
        static void ex1()
        {
            for (int i = 1; i < 21; i++)
            {
                Console.WriteLine(i);
            }
        }
        static void ex2()
        {
            for(int i = 20; i > 0; i--)
            {
                Console.WriteLine(i);
            }
        }
        static void ex3()
        {
            Console.Write("Enter any word: ");
            string word = Console.ReadLine();
            Console.Write("Enter any number: ");
            int num = int.Parse(Console.ReadLine());
            Console.WriteLine("");

            for (int i = 0; i < num; i++)
            {
                Console.WriteLine(word);
            }
        }
        static void ex4()
        {
            bool loop = true;
            do
            {
                Console.Write("Enter the capital of the UK: ");
                string input = Console.ReadLine().ToUpper();
                if (input == "LONDON")
                {
                    Console.WriteLine("Correct");
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Incorrect. Try again...");
                }
            } while (loop == true);
        }
    }
}
