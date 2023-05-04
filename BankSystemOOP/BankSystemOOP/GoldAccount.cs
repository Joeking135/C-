using System;
using System.Runtime.Serialization;

namespace BankSystemOOP
{
    [Serializable]
    public class GoldAccount : Account
    {


        public double OverdraftLimit {get; private set;}

        public GoldAccount(long accountNumber, double balance, string name, string address, DateTime dob, double overdraftLimit)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Name = name;
            Address = address;
            DOB = dob;
            OverdraftLimit = overdraftLimit;
        } 

        public override void ViewAccount()
        {
            base.ViewAccount();
            Console.WriteLine($"Overdraft: \t{OverdraftLimit}");
        }

        public override void Withdraw()
        {
            double amount = Program.GetUserInput<double>(input => input < 0 , "Input Withdrawal Amount: ", "That is not a valid amount.");

            if (amount > Balance + OverdraftLimit)
            {
                Console.WriteLine($"Insufficient funds: ({amount.ToString("£0.00")} > £{Math.Abs(Balance).ToString("£0.00")} (Overdraft limit: {OverdraftLimit.ToString("£0.00")}))");
            }
            else
            {
                Balance -= amount;
            }
        }


         


         
    }
}
