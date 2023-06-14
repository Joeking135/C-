using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRemastered
{
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
            Console.Write($"<{Values.Item1.ToString()[0]}>{Values.Item2}");
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
