using System;


/// <summary>
/// I must admit, I did some maths research before going into this, in order to find a more efficient way of finding the perfect numbers. My first 
/// version did work, but it took a VERY long time, so I discovered mersenne primes to reduce this time.
/// </summary>

class Program
{

    
    static void Main(string[] args)
    {
        int number = 2; //Sets the current number to 2.

        List<long> perfectNumbers = new List<long>(); //This is a list of the perfect numbers, to be displayed later

        while (perfectNumbers.Count < 5)
        {
            long mersennePrime = (long)Math.Pow(2, number) - 1; //Using a formula to calculate the mersenne prime

            if (IsPrime(number) && IsPrime(mersennePrime)) //If both are prime, the number is perfect.
            {
                long perfectNumber = mersennePrime * (long)Math.Pow(2, number - 1); //Using formula to calculate perfect number
                perfectNumbers.Add(perfectNumber); 
            }

            number++; //Iterate the number, in order to search that next.
        }

        foreach (int perfectNumber in perfectNumbers) //This loops through the perfect number list and outputs each perfect number.
        {
            Console.WriteLine(perfectNumber);
        }
        Console.ReadLine();
    }

    static bool IsPrime(long number) //This method simply returns true if the number is prime
    {
        if (number < 2) return false; // 0 and 1 are not prime
        if (number == 2) return true; // 2 is prime

        for (long i = 2; i <= Math.Sqrt(number); i++)//We only need to check numbers up to the square root of the number.
        {
            if (number % i == 0) return false; // If divisible, the numbre is not prime.
        }

        return true; //if the number is not divisible, then it must be prime.

    }
}