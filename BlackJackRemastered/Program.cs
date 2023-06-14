using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;



namespace BlackJackRemastered
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cards cards = new Cards();
            Player player = new Player();

            Console.WriteLine(player.Total);
            Console.WriteLine(player.Bust);

        }



        delegate bool FailCondition<T>(T input);
        static T GetUserInput<T>(FailCondition<T> failCondition, string request, string errorMessage)
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
    }


    class Player
    {
        public List<Card> cards {get; private set;}

        public int Total
        {
            get { return cards.Sum(e => e.GetValue());}
        }

        public bool Bust
        {
            get { return Total > 21; }
        }

        public Player()
        {
            cards = new List<Card>();
        }

        public void Add(Card card)
        {
            cards.Add(card);
        }

        public void DisplayCards()
        {

        }

        

        
    }

    class Cards
    {
        private Queue<Card> cards { get; set; }


        public Cards()
        {
            cards = new Queue<Card>(); 


            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 13 ; j++)
                {
                    cards.Enqueue(new Card(Tuple.Create((Card.Suit)i, (Card.Rank)j)));
                } 
            }
        }


        public void PlayBlackJack()
        {
            Player player = new Player();
            Player dealer = new Player();

            for (int i = 0; i < 2; i++)
            {
                player.Add(cards.Dequeue()); 
                dealer.Add(cards.Dequeue());
            }

            while (!player.Bust)
            {

            }

            
        }


        
        

        public void Shuffle()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            List<Card> list = cards.ToList();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                Card value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            cards = ListToQueue<Card>(list);

        }

        private static Queue<T> ListToQueue<T>(List<T> list)
        {
            Queue<T> queue = new Queue<T>();
            foreach (T item in list)
            {
                queue.Enqueue(item);
            }
            return queue;
        }

        
    }






    class Card
    {
        public enum Suit
        {
            Diamonds,
            Hearts,
            Clubs,
            Spades
        }


        public enum Rank
        {
            Ace = 1,
            Two, Three, Four, Five, Six, Seven, Eight, Nine,
            Jack = 10, Queen = 11, King = 12
        }

        public Tuple<Suit, Rank> Values { get; private set; }

        public Card(Tuple<Suit, Rank> values)
        {
            Values = values;
        }

        public void Display()
        {
            Console.WriteLine($"{Values.Item2} ({Values.Item1.ToString()[0]}) ({GetValue()})");
        }

        public int GetValue()
        {
            int value = (int)Values.Item2;

            if (value > 10)
            {
                value = 10; 
            }
            else if (value == 1)
            {
                value = 11;   
            }

            return value;
        }

        
    }
}