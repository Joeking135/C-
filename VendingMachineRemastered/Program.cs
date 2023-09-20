using System;
using System.ComponentModel;



namespace VendingMachineRemastered
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string adminPassword = "1234";

            Machine machine = new Machine();
            string[] mainMenuElements = 
            {
                "Buy Snack",
                "Admin Options"
            }; 

            while (true)
            {
                int mainMenuSelection = GetIntSelection("Main Menu", mainMenuElements);   

                switch (mainMenuSelection)
                {
                    case 1:
                        machine.SellSnack();
                        break;
                    case 2:
                        if (AdminPasswordEntry(adminPassword))
                        {
                            machine.AdminPage(); 
                        } 
                        else
                        {
                            Console.WriteLine("Incorrect Password.");
                        }
                        break;

                    default:
                        break;
                }

                Console.WriteLine("Hit Enter."); Console.ReadLine();
            }
        }


        public static void DisplayMenu(string title, string[] elements)
        {
            Console.WriteLine(title + "\n" + new string('=', 20) + "\n");

            for (int i = 0; i < elements.Length; i++)
            {
                Console.WriteLine($"{i+1}. {elements[i]}") ;
            }

            Console.WriteLine("\n");
        }

        public static int GetIntSelection(string title, string[] elements)
        {
            Console.Clear();
            DisplayMenu(title, elements);
            return GetUserInput<int>(input => input < 1 || input > elements.Length, "Input Selection No: ", "Invalid Selection");
        }

        public delegate bool failCondition<T>(T input);
        public static T GetUserInput<T>(failCondition<T> failCondition, string request, string errorMessage)
        {
            while (true)
            {
                try
                {
                    Console.Write(request);

                    T output = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(Console.ReadLine());

                    if (failCondition(output))
                    {
                        throw new SystemException();
                    }   

                    return output; 
                }
                catch 
                {
                    Console.WriteLine(errorMessage); 
                }
                
            }
        }

        public static bool AdminPasswordEntry(string password)
        {
            string input = Program.GetUserInput<string>
            (
                input => input == "",
                "Input Admin Password: ",
                "Invalid Input"
            );

            return input == password; 
        }

    }
}