using System;
using System.Runtime.Serialization;


namespace BankSystemOOP
{
    [Serializable]
    public class Account
    {
        //attributes

        public long AccountNumber { get; protected set; }

        public double Balance { get; protected set; }

        public string Name { get; protected set; }

        public string Address { get; protected set; }

        public DateTime DOB { get; protected set; }

        //methods

        public Account(long accountNumber, double balance, string name, string address, DateTime dob)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Name = name;
            Address = address;
            DOB = dob;

            Console.WriteLine("Account Created.");

        }

        public Account() //DO NOT REMOVE THIS (SERIOUSLY)
        { }


        public virtual void ViewAccount()
        {
            Console.WriteLine("\nAccount Number: {0}", AccountNumber);
            Console.WriteLine("Balance: \t{0}", String.Format("{0:C}", Balance));
            Console.WriteLine("Name: \t\t{0}", Name);
            Console.WriteLine("Address: \t{0}", Address);
            Console.WriteLine("DOB: \t\t{0}", DOB.ToShortDateString());
        }

        public virtual void Withdraw()
        {

            double amount = Program.GetUserInput<double>(input => input < 0 , "Input Withdrawal Amount: ", "That is not a valid amount.");

            if (amount > Balance)
            {
                Console.WriteLine($"Insufficient funds: (£{amount.ToString("£0.00")} > £{Balance.ToString("£0.00")})");
            }
            else
            {
                Balance -= amount;
            }
        }

        public void Deposit()
        {
            double amount = Program.GetUserInput<double>(input => input < 0, "Input Deposit Amount: ", "That is not a valid Amount.");
            Balance += amount;
        }

        //----------------------
    }
}

