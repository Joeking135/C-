using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using System.Security.AccessControl;
using System.Net.Security;
using System.Xml.Schema;

class Program
{
    static void Main(string[] args)
    {
        Random rnd = new Random();
        
        List<string> cardList = new List<string>() { "A", "2", "3", "4", "5", "6", "7", "8", "9", "J", "Q", "K" };
        List<char> suitList = new List<char>() { 's', 'c', 'h', 'd' };
        List<string> hand = new List<string>();
        int total = 0;

        while (true)
        {
            bool hit = true;
            int AceCount = 0;
            string strFull = "";
            while(hit)
            {
                int card = CardGen(ref AceCount, cardList, suitList, rnd, ref strFull);
                total += card;

                hand.Add(strFull);
                OutputHand(hand, total);
                hit = HitorStick();
            } 

            Console.ReadKey();

        }
        

    }
    static int CardGen(ref int AceCount, List<string> cardList, List<char> suitList, Random rnd, ref string strFull)
    {
        string strCard = cardList[rnd.Next(0, 9)];
        char strSuit = suitList[rnd.Next(0, 3)];

        strFull = strCard + strSuit;

        switch (strCard)
        {
            case "A":
                AceCount++;
                return 11;
            case "K" or "Q" or "J":
                return 10;
            default:
                return int.Parse(strCard);
        }

    }

    static void OutputHand(List<string> hand, int total)
    {
        Console.Write("\nYour hand is: ");
        foreach (var item in hand)
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine($"Total = {total}");
    }

    static bool HitorStick()
    {
        while (true)
        {
            Console.Write("Hit (h) or stick (s): ");
            char input = Console.ReadLine().ToLower()[0];

            switch (input)
            {
                case 'h':
                    return true;
                case 's':
                    return false;
                default:
                    Console.WriteLine("Error, please try again."); break;
            }
        }
    }

    
}
