using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineRemastered
{
    internal class Machine
    {
        private List<Snack> snacks { get; set; }

        private double Balance {get; set;}

        
        public Machine()
        {
            snacks = new List<Snack>();

            snacks.Add(new Snack("Turkish Dissapointment", 0.8, 5));
            snacks.Add(new Snack("Venus", 1.20, 5));
            snacks.Add(new Snack("Dairy path", 1.15, 5));
            snacks.Add(new Snack("Lancashire Bar", 1.25, 5));
            snacks.Add(new Snack("Mint Metrics", 1.50, 5));

            Balance = 0;
        }

        private List<string> GetSnackList()
        {
            List<string> displayElements = new List<string>(); 

            foreach (Snack snack in snacks)
            {
                displayElements.Add($"{snack.Name.PadRight(25, '.')} ({snack.Price.ToString("£0.00")})\tStock: {snack.Stock}");
            }

            return displayElements;
        }

        private int GetSelection()
        {
            List<string> displayElements = GetSnackList();

            while (true)
            {
                int snackSelecion = Program.GetIntSelection("Currently Selling", displayElements.ToArray()) - 1;   

                if (snacks[snackSelecion].Stock < 1)
                {
                    Console.WriteLine("Sorry, That snack is currently unavailable."); 
                }
                else
                {
                    return snackSelecion;
                }
            }
            

        }

        public void SellSnack()
        {
            int snackSelection = GetSelection();

            Snack snack = snacks[snackSelection];

            double[] possibleCoins = {0, 0.01, 0.02, 0.05, 0.1, 0.2, 0.5, 1, 2};

            while (Balance < snack.Price)
            {
                Console.Clear();
                Console.WriteLine($"{snack.Name}\nPrice: {snack.Price.ToString("£0.00")}\nCurrent Balance: {Balance.ToString("£0.00")}\n");

                double coin = Program.GetUserInput<double>
                (
                    input => !possibleCoins.Contains(input), 
                    "Input Coin as decimal (eg 0.20) (0 to cancel): ",
                    "Invalid Input or Coin"
                ); 

                if (coin == 0)
                {
                    Console.WriteLine("Sale Cancelled."); 
                    return; 
                }

                Balance += coin;
            }

            Console.Clear();
            Balance -= snack.Price;
            snack.ReduceStock();

            if (Balance > 0)
            {
                char refundSelection = Program.GetUserInput<string>
                (
                    input => input.ToLower() != "y" && input.ToLower() != "n",
                    $"Would you like your change ({Balance.ToString("£0.00")}) y/n: ",
                    "Invalid Input (y/n only)"
                ).ToLower()[0];

                if (refundSelection == 'y')
                {
                    Console.WriteLine("Change Dispensed.");
                    Balance = 0; 
                }
            }


            Console.WriteLine("Sale Confirmed, Enjoy.");
        }


        public void AdminPage()
        {
            string[] adminOptions = 
            {
                "Add Snack",
                "Remove Snack",
                "Edit Snack",
                "Clear Balance",
                "Exit"
            };

            int adminSelection;
            do
            {
                Console.Clear();
                adminSelection = Program.GetIntSelection("Admin Options", adminOptions);
                
                Console.Clear();
                switch (adminSelection)
                {

                    case 1:
                        AddSnack();
                        break;
                    case 2:
                        RemoveSnack();
                        break;
                    case 3:
                        EditSnack();
                        break;
                    case 4:
                        Balance = 0;
                        break;
                    case 5:
                        Console.WriteLine("Quitting...");
                        return;
                    default:
                        break;
                } 

                Console.WriteLine("Hit a Key."); Console.ReadKey();
            } while (adminSelection != 5);
            
        }


        private void AddSnack()
        {
            snacks.Add
            (
                new Snack
                (
                    Program.GetUserInput<string>(input => input == "", "Input Name: ", "Invalid Name"),
                    Program.GetUserInput<double>(input => input <= 0, "Input Price: ", "Invalid Price"),
                    Program.GetUserInput<int>(input => input < 0, "Input Stock Count: ", "Invalid Amount")
                )
            );

            Console.WriteLine("Snack Added.");
        }

        private void RemoveSnack()
        {
            if (snacks.Any())
            {
                int removeIndex = Program.GetIntSelection("Remove Snack", GetSnackList().ToArray()) - 1;

                Console.WriteLine($"{snacks[removeIndex].Name} removed.");

                snacks.RemoveAt(removeIndex);   
            }
            else
            {
                Console.WriteLine("There are no snacks to remove.");
            }

            
        }

        private void EditSnack()
        {
            if (snacks.Any())
            {
                int editIndex = Program.GetIntSelection("Edit Snack", GetSnackList().ToArray()) - 1;

                Console.Clear();
                Console.WriteLine($"{snacks[editIndex].Name}\n");

                double newPrice = Program.GetUserInput<double>
                (
                    input => input <= 0, 
                    $"Input new Price (prev. {snacks[editIndex].Price.ToString("£0.00")}): ",
                    "Invalid Price"
                );

                int newStock = Program.GetUserInput<int>
                (
                    input => input < 0,
                    $"Input new Stock Count (prev. {snacks[editIndex].Stock}): ",
                    "Invalid Stock Amount"
                );

                snacks[editIndex].Edit(newPrice, newStock); 
                Console.WriteLine("Edit Confirmed.");
            }
            else
            {
                Console.WriteLine("There are no snacks to edit.");
            }
        } 

        
    }
}
