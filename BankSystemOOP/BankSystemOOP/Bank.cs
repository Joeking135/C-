using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace BankSystemOOP
{

    [Serializable] 
    class Bank
    {
        //attributes
        public Hashtable Accounts { get; private set; }

        public Bank()
        {
            Accounts = new Hashtable();
        }
        //methods
        public void AddAccount(Account A)
        {
            // add(key value, account object)
            Accounts.Add(A.AccountNumber, A);

        }

        public void AddAccount(GoldAccount GA) //overloaded method (same name, different parameters)
        {
            Accounts.Add(GA.AccountNumber, GA);
        }

        public T FindAccount<T>()
        {

            long AccountNumber = Program.GetUserInput<long>(input => input < 0, "Enter Account Number: ", "That is not a valid Account Number.");
            T AccountToFind;
            //find this account in the dictionary

            // does the Account exist?
            if (Accounts.ContainsKey(AccountNumber))
            {
                if (typeof(T) == Accounts[AccountNumber].GetType())
                {
                    //yes
                    AccountToFind = (T)Accounts[AccountNumber];
                    return AccountToFind;
                }
                else
                {
                    Console.WriteLine("Wrong Account Type.");
                    return default;
                }


            }
            else
            {
                //no
                Console.WriteLine("No Account Found");
                return default;
            }
        }

        public void DisplayAllAccounts()
        {
            Console.Clear();

            Console.WriteLine("STANDARD ACCOUNTS\n" + new string('=', 20));


            foreach (DictionaryEntry item in Accounts)
            {
                if (Accounts[item.Key].GetType() == typeof(Account))
                {
                    Console.Write(new string('-', 20));
                    ((Account)Accounts[item.Key]).ViewAccount();
                }
            }
            Console.WriteLine(new string('-', 20));


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nGOLD ACCOUNTS\n" + new string('=', 20));
            foreach (DictionaryEntry item in Accounts)
            {
                if (Accounts[item.Key].GetType() == typeof(GoldAccount))
                {
                    Console.Write(new string('-', 20));
                    ((GoldAccount)Accounts[item.Key]).ViewAccount();
                }
            }
            Console.WriteLine(new string('-', 20));

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DisplayTotalBalance()
        {
            double totalBalance = Accounts.Values.OfType<Account>().Sum(account => account.Balance); 

            Console.WriteLine($"Total Balance = {totalBalance.ToString("C0")}");
        }


        

    }
}
