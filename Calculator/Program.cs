using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice = "";
            Console.ForegroundColor = ConsoleColor.White;
            while (choice.ToLower() != "q")
            {
                choice = Menu();
                switch (choice[0]) //only check the first charcater of the string
                {
                    case '1' or '2' or '5': 
                        double width = GetDoubleNumber("Enter Width (cm): ");
                        double height = GetDoubleNumber("Enter Height (cm): ");
                        if (choice[0] == '1') { Console.WriteLine("Area is {0}cm2", RectangleArea(width, height)); } //Area of rectangle

                        else if (choice[0] == '2') { Console.WriteLine($"Perimeter is {RectanglePerimeter(width, height)}cm"); }//Perimeter of rectangle

                        else { Console.WriteLine($"Hypotenuse is {Hypotenuse(width, height)} cm"); } //The less lines the better...right?...guys??
                        break;

                    case '3':
                        double radius = GetDoubleNumber("Enter Radius (cm): ");
                        Console.WriteLine($"Area is {CircleArea(radius)} cm^2"); //area of circle
                        break;
                    case '4':
                        radius = GetDoubleNumber("Enter Radius (cm)");
                        Console.WriteLine($"Permimeter is {CirclePerimeter(radius)} cm"); // Perimeter of cirlce
                        break;
                    case '6':
                        char op = GetOperator();                                    //Calculator
                        double num1 = GetDoubleNumber("Enter first number: ");
                        double num2 = GetDoubleNumber("Enter second number: ");
                        double answer = Calculator(op, num1, num2);
                        Console.WriteLine($"{num1} {op} {num2} = {answer}");

                        break;
                    case 'q':
                        Console.WriteLine("\nGoodbye!");

                        break;
                    default:
                        Console.WriteLine("\nInvalid entry\n");
                        break;
                }
                Console.WriteLine("hit a key");
                Console.ReadKey();
                Console.Clear();

            }
        }

        static string Menu()
        {
            //this displays a menu and returns the first charcater of the user choice
            Console.WriteLine("Enter Choice");
            Console.WriteLine("============");
            Console.WriteLine("1. Area of square");
            Console.WriteLine("2. Perimeter of square");
            Console.WriteLine("3. Area of Circle");
            Console.WriteLine("4. Permimeter of Cirlcle");
            Console.WriteLine("5. Hypoteneuse of triangle");
            Console.WriteLine("6. Calculator");
            Console.WriteLine("q. Quit");
            Console.Write("Choice> ");
            string c = Console.ReadLine();
            return c;
        }

        static double GetDoubleNumber(string prompt)
        {
            bool valid = false;
            double NumberIn = 0;
            do
            {
                Console.Write(prompt);
                try
                {
                    NumberIn = double.Parse(Console.ReadLine());
                    valid = true;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not a valid number");
                    Console.ForegroundColor = ConsoleColor.White;
                    valid = false;
                }
            }
            while (!valid);
            return NumberIn;
        }

        static double RectangleArea(double w, double h) //Option 1
        {
            return w * h;
        }

        static double RectanglePerimeter(double w, double h) //Option 2
        {
            return (w*2) + (h*2);
        }

        static double CircleArea(double r)
        {
            return Math.PI * r * r;
        }//Option 3

        static double CirclePerimeter(double r)
        {
            return (r*2) * Math.PI;
        } //Option 4 

        static double Hypotenuse(double w, double h)
        {
            return Math.Sqrt((h * h) + (w * w));
        } //Option 5 

        static char GetOperator()
        {
            Console.Write("Add (+), Subtract (-), Multiply (*) or Divide (/): ");
            char op = Console.ReadLine().ToLower()[0];
            return op;
        }
        
        static double Calculator(char op, double num1, double num2)
        {
            switch (op)
            {
                case '+':
                    return num1 + num2;
                case '-':
                    return num1 - num2;
                case '*':
                    return num1 * num2;
                case '/':
                    return num1 / num2;
                default:
                    Console.WriteLine("Invalid operator..."); return 0;
                
            }
        } //Option 6
    }
}