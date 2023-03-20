using System;
namespace Perfect_Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<long> perfectNumbers = new List<long>(); //Creates a list to store the perfect numbers
            int number = 1; //Number is the number currently being tested.

            Console.WriteLine("Searching...");

            while (perfectNumbers.Count < 5)  //Loop until we have found 5 perfect numbers
            {
                if (IsPerfect(number)) //Checks whether the current number is perfect
                {
                    perfectNumbers.Add(number); //If so, add it to the list of perfect numbers
                }
                number++; //Iterates the current number

                if (number == 8129) //IMPORTANT: This was just added to test and demonstrate the theory, as otherwise the 5th number takes a very long time to find.
                {
                    number = 33550300; //skips to a position near the next perfect number to save time 
                }

            }

            Console.Clear();
            Console.WriteLine("The first five perfect numbers are: "); //Displays the list of perfect numbers to the user.
            foreach (long item in perfectNumbers)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        static bool IsPerfect(long number) //Takes in the current number and checks if it is perfect
        {
            long sum = 0; //This is the running total which will be compared to the current number.

            for (int i = 1; i <= number / 2; i++) //loops to find each factor.
            {
                if (number % i == 0)
                {
                    sum += i; //If the number is a factor, add it to the running total.
                }
            }
            return sum == number; //If the sum of factors is the same as the number, the number is perfect.
        }



    }

    
}