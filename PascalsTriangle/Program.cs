namespace RecursiveTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input row count: "); int rowCount = int.Parse(Console.ReadLine());

            OutputTriangle(rowCount);
            Console.ReadLine();
        }

        static int Factorial(int number) //recursive function to find the factorial of a number
        {
            if (number == 0)
            {
                return 1;
            }

            return number * Factorial(number - 1);
        }

        static int NumberAtPosition(int row, int column) //Finds the number at any given position in Pascal's triangle using the choose equation.
        {
            if (column <= row)
            {
                return Factorial(row) / (Factorial(column) * Factorial(row - column));
            }
            return 0;
        }

        static void OutputTriangle(int rowCount)
        {


            for (int i = 0; i < rowCount; i++) //Where i is the current row.
            {
                Console.Write(new string(' ', rowCount - i));
                for (int j = 0; j <= i; j++)
                {
                    string number = NumberAtPosition(i, j).ToString();
                    Console.Write($"{number.PadLeft(3 - number.Length)}  ");
                }
                Console.WriteLine();
            }


        }

    }
}