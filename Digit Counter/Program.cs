using System;
using System.Collections.Generic;
using System.Linq;

namespace Digit_Counter
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            bool isError;

            while (true)
            {
                Console.Clear();
                do
                {
                    Console.Write("Enter list of numbers(eg. 1, 2, 3, 4 etc): "); string? input = Console.ReadLine(); //Takes input from user (nullable)
                    isError = ListConvert(ref numbers, input); //Converts string to list, whilst checking validitiy 
                } while (isError); //Repeat if the input is erroneous.

                Console.WriteLine("INDEX");
                GetIndex(numbers);
                Console.WriteLine($"Mode = {GetMode(numbers)} " +
                                  $"\nMedian = {GetMedian(numbers)}" +
                                  $"\nMean = {GetMean(numbers)}"); //Outputs the results.

                Console.ReadKey();

            }            
        }

        static bool ListConvert(ref List<int> Numbers, string input) 
        {
            try
            {
                Numbers = Array.ConvertAll(input.Split(','), int.Parse).ToList(); //Splits the list into an integer array, which is then converted to a list.
                return false;
            }
            catch 
            {
                Console.WriteLine("The string contains invalid characters."); //Error occured.
                return true;
            }
        }

        static void GetIndex(List<int> Numbers)
        {
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine($"{i} - {Numbers.Count(e => e.Equals(i))}"); //Uses LINQ to count each number.
            }
            
        }

        static int GetMode(List<int> Numbers)
        {
            return Numbers.GroupBy(i => i)
                .OrderByDescending(grp => grp.Count())
                .Select(grp => grp.Key)
                .FirstOrDefault();                //Using multiple LINQ expressions to get modal value.
        }

        static double GetMedian(List<int> Numbers)
        {
            Numbers.Sort();
            if (Numbers.Count % 2 != 0)
            {

                return Numbers[Numbers.Count / 2]; //If the list has a definite middle value (count is odd), take that value.
            }
            else
            {
                double Higher = Numbers[Numbers.Count / 2];
                double Lower = Numbers[(Numbers.Count / 2) - 1]; //calculates the average of the 2 middle values.

                return (Lower + Higher ) / 2;
            }
        }

        static double GetMean(List<int> Numbers)
        {
            return Math.Round(Numbers.Average(), 2); //Simple inbuilt function to find mean.
        }

    }
}