using System;

namespace Binary_Search
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = {2, 6, 12, 32, 64, 98, 102};

            Console.Write("Input Number: ");
            int input = int.Parse(Console.ReadLine());

            if (BinarySearh(array, input, 0, array.Length))
            {
                Console.WriteLine("Yes") ;
            }
            else
            {
                Console.WriteLine("No");
            }
        }

        static bool BinarySearh(int[] array,int input, int low, int high){

                if (low > high)
                {
                    return false;
                }

                int midPoint = (low + high) / 2;
                if ( array[midPoint] == input)
                {
                    return true; 
                }
                else if (array[midPoint] < input)
                {
                    return BinarySearh(array, input, midPoint + 1, high);
                } 
                else if(array[midPoint] > input)
                {
                    return BinarySearh(array, input, low, midPoint - 1);
                }

            return true;
        }
    }
}