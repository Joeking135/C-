namespace Perfect_Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<long> perfectNumbers = new List<long>();
            int number = 1;


            Console.WriteLine("The first 5 perfect numbers are...");
            while (perfectNumbers.Count < 5)
            {
                if (IsPerfect(number))
                {
                    perfectNumbers.Add(number);
                }
                number++;
            }


            
            foreach (long item in perfectNumbers)
            {
                Console.WriteLine(item);
            }


        }

        static bool IsPerfect(long number)
        {
            long sum = 0;

            for (int i = 1; i < number; i++)
            {
                if (number % i == 0)
                {
                    sum += i;
                }
            }
            return sum == number;
        }



    }

    
}