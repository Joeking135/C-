using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRemastered
{
    class Player : Cards
    {
        public int Total
        {
            get { return cards.Sum(e => e.GetValue()) - (AcesUsed * 10); }
        }

        public bool Won {get; set;}

        public bool Bust
        {
            get { return Total  > 21; }
        }

        public int AceCount
        {
            get{ return cards.Count(e => e.GetValue() == 11);}
        }

        private int AcesUsed { get; set; }

        public bool HasAcesLeft 
        {
            get {return AcesUsed < AceCount; }
        }

        public int PlayerID {get; set;}

        public Player() : base()
        { 
            AcesUsed = 0;
            Won = false;
        }

        public void Add(Card card)
        {
            cards.Enqueue(card);
        }
        public Card PeekLast()
        {
            return cards.Last();
        }

        public List<Card> ReturnCards()
        {
            return cards.ToList();
        } 
        public void UseAce()
        {
            AcesUsed++;
        }




    }
}
