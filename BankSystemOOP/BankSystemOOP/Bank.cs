using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;

namespace BankSystemOOP
{

    class Bank
    {
        //attributes
        public Hashtable Accounts {get; private set;} 

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

        
    }
}
