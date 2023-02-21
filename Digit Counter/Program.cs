using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Digit_Counter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter string: ");

            Digits digits = new(Console.ReadLine()); //Takes input string and uses class Digits

            Console.WriteLine("\nINDEX");
            digits.Display(); 

            Console.WriteLine($"\nMode = {digits.Mode()}\nMedian = {digits.Median()}\nMean = {digits.Mean()}"); //Ouputs the extra features using methods within the Digits class

            Console.ReadLine();

        }


        public class Digits
        {
            public List<int> Numbers { get; private set; } 

            public Digits(string inputString) //Constructor
            {
                Numbers = Array.ConvertAll(inputString.Split(','), int.Parse).ToList(); //Splits the string into an integer array, which is then converted a list.
            }

            public void Display() 
            {
                for (int i = 1; i <= 9; i++)
                {
                    Console.WriteLine($"{i} - {Numbers.Count(e => e.Equals(i))}"); //Uses LINQ to count each number.
                }
            }

            public int Mode()
            {
                return Numbers.GroupBy(i => i)          //Groups by identical numbers
                .OrderByDescending(grp => grp.Count())  //Orders the groups in descending order by size
                .Select(grp => grp.Key)                 //Takes the number from the biggest (first) group
                .FirstOrDefault();
            }

            public double Median()
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

                    return Math.Round((Lower + Higher) / 2, 2);
                }
            }

            public double Mean()
            {
                return Math.Round(Numbers.Average(), 2); //Simple inbuilt function to find mean.
            }


        }

    }
}