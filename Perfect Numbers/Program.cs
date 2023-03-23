using System;
namespace Perfect_Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<long> perfectNumbers = new List<long>(); //Creates a list to store the perfect numbers
            int number = 2; //Number is the number currently being tested.

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
            long sum = 1; // initialize sum to 1 (every number is divisible by 1)
            long limit = (long)Math.Sqrt(number); // only need to check divisors up to the square root of the current number.

            for (long i = 2; i <= limit; i++)
            {
                if (number % i == 0)
                {
                    sum += i; 
                    long otherDivisor = number / i;
                    if (otherDivisor != i)
                    {
                        sum += otherDivisor; 
                    }
                }
            }

            return sum == number; //returns true if the sum of the factors is equal to number (so it is perfect).
        }



    }

    
}