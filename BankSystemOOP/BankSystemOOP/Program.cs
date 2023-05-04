using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace BankSystemOOP
{
    class Program
    {
        // instantiate empty bank object as a global variable
        public static Bank ThisBank = new Bank();

        static void Main(string[] args)
        {

            char option;
            Account ThisAccount;
            GoldAccount ThisGoldAccount;
            do
            {
                Menu();
                option = Console.ReadLine().ToUpper()[0]; //collect option capitalised.
                switch (option)
                {
                    case '1':     //Add account

                        AddAccount<Account>();
                        break;

                    case '2':    //View Account

                        //find the account in the bank
                        ThisAccount = ThisBank.FindAccount<Account>();
                        ThisAccount?.ViewAccount();

                        break;

                    case '3':  //deposit money to account

                        ThisAccount = ThisBank.FindAccount<Account>();
                        ThisAccount?.Deposit();

                        break;

                    case '4':   // withdraw money to account

                        ThisAccount = ThisBank.FindAccount<Account>();
                        ThisAccount?.Withdraw();

                        break;

                    case '5': //Add Gold account

                        AddAccount<GoldAccount>();
                        break;

                    case '6': //View Gold Account balance

                        ThisGoldAccount = ThisBank.FindAccount<GoldAccount>();
                        ThisGoldAccount?.ViewAccount();

                        break;

                    case '7': //deposit money to gold account

                        ThisGoldAccount = ThisBank.FindAccount<GoldAccount>();
                        ThisGoldAccount?.Deposit();

                        break;

                    case '8': // withdraw money from gold account account

                        ThisGoldAccount = ThisBank.FindAccount<GoldAccount>();
                        ThisGoldAccount?.Withdraw();

                        break;

                }

                Console.WriteLine();
                Console.WriteLine("Hit any key to continue");
                Console.ReadKey();
                Console.Clear();

            } while (option != 'Q');

        }

        static void Menu()
        {

            Console.WriteLine("MENU");
            Console.WriteLine("Accounts");
            Console.WriteLine(" 1. Create Account");
            Console.WriteLine(" 2. View Account");
            Console.WriteLine(" 3. Deposit to Account");
            Console.WriteLine(" 4. Withdraw from Account");
            Console.WriteLine("Gold Accounts");
            Console.WriteLine(" 5. Create Account");
            Console.WriteLine(" 6. View Account");
            Console.WriteLine(" 7. Deposit to Account");
            Console.WriteLine(" 8. Withdraw from Account");
            Console.WriteLine();
            Console.WriteLine(" Q. Quit");
            Console.WriteLine();
            Console.Write("Enter choice: ");
        }

        static void AddAccount<T>()
        {
            long accountNumber = GetUserInput<long>(input => input < 0 || ThisBank.Accounts.ContainsKey(input), "Input Account Number: ", "That is not a valid Account Number");
            double balance = GetUserInput<double>(input => input < 0, "Input starting Balance: ", "That is not a valid Balance");
            string name = GetUserInput<string>(input => input == "", "Input Name: ", "That is not a valid Name");
            string address = GetUserInput<string>(input => !Regex.IsMatch(input, "^[A-Za-z]{2}[0-9]{2} [0-9][A-Za-z]{2}$"), "Input Address: ", "That is not a valid Address").ToUpper();
            DateTime dob = GetUserInput<DateTime>(input => input.Date >= DateTime.Now.Date || (int)((DateTime.Now - input).TotalDays / 365.242199) < 18, "Input Date of Birth (xx/xx/xxxx): ", "That is not a valid Date of Birth, or you are too young.");


            if (typeof(T) == typeof(Account))
            {
                Account NewAccount = new Account(accountNumber, balance, name, address, dob); //instantiate an account object
                ThisBank.AddAccount(NewAccount); // add account to bank
            }
            else if (typeof(T) == typeof(GoldAccount))
            {
                double overdraftLimit = GetUserInput<double>(input => input < 0, "Input Overdraft Limit: ", "That is not a valid overdraft limit.");

                GoldAccount NewAccount = new GoldAccount(accountNumber, balance, name, address, dob, overdraftLimit);
                ThisBank.AddAccount(NewAccount); // add account to bank
            }


        }


        public delegate bool FailCondition<T>(T input);
        public static T GetUserInput<T>(FailCondition<T> failCondition, string request, string errorMessage)
        {
            do
            {
                try
                {
                    Console.Write(request);
                    T output = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(Console.ReadLine());

                    if (failCondition(output))
                    {
                        throw new System.Exception();
                    }

                    return output;
                }
                catch
                {
                    Console.WriteLine(errorMessage);
                    continue;
                }

            } while (true);
        }

    }
}
