using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRemastered
{
    abstract class Cards
    {
        protected Queue<Card> cards { get; set; }

        public int DeckTotal 
        {
            get { return cards.Sum(e => e.GetValue());}
        }


        public Cards()
        {
            cards = new Queue<Card>();
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

        public void DisplayAll()
        {
            Console.WriteLine(string.Join(", ", cards.Select(e => "<"+e.Values.suit.ToString()[0].ToString()+">" + e.Values.rank.ToString())));
            Console.WriteLine();

        }

        protected static Queue<T> ListToQueue<T>(List<T> list)
        {
            Queue<T> queue = new Queue<T>();
            foreach (T item in list)
            {
                queue.Enqueue(item);
            }
            return queue;
        }

        


    }
}
