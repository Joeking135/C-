﻿using System;
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

        public string Name {get; private set;}
        public bool Bust
        {
            get { return Total  > 21; }
        }

        public int AceCount
        {
            get {return cards.Count(e => e.GetValue() == 11);}
        }

        private int AcesUsed { get; set; }

        public bool HasAcesLeft 
        {
            get {return AcesUsed < AceCount; }
        }

        public int ID {get; set;}

        public int Balance {get; private set;}
        public int CurrentBet{get; private set;}
        public bool Bankrupt{get { return Balance <= 0;}}

        public Player(int id) : base()
        { 
            AcesUsed = 0;
            
            Name = Program.GetUserInput<string>
            (
                input => input == "",
                $"Player {id} name: ",
                "Invalid Name"
            );

            ID = id;
            CurrentBet = 0;
            Balance = 1000;
        }
        public Player(string name) : base() 
        {
            AcesUsed = 0;
            Name = name;
        }

        public void Add(Card card)
        {
            cards.Enqueue(card);
        }
        public Card PeekLast()
        {
            return cards.Last();
        }

        public List<Card> GetCards()
        {
            return cards.ToList();
        } 
        public void UseAce()
        {
            AcesUsed++;
        }

        public void Reset()
        {
            AcesUsed = 0;
            cards.Clear();
            CurrentBet = 0;
        }

        

        public void CheckAces()
        {
            if (Bust && HasAcesLeft)
            {
               UseAce(); 
            }
        }

        public void Bet(int Amount){
            CurrentBet = Amount;
            Balance -= Amount;
        }
        public void Reward(){
            const int scale = 2;
            Balance += CurrentBet * scale;
        }
    }
}
