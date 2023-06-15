using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRemastered
{
    internal class BlackJack : Cards
    {
        public BlackJack() : base()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    cards.Enqueue(new Card(Tuple.Create((Card.Suit)i, (Card.Rank)j)));
                }
            }
        }


        public void Play()
        {
            Shuffle();
            Player dealer = new Player();

            List<Player> players = new List<Player>();

            int playerCount = Program.GetUserInput<int>
            (
                input => input < 1, "Input Number of Players: ", "Invalid Player Count."
            );

            for (int i = 0; i < playerCount; i++)
            {
                players.Add(new Player()); 
                players[i].PlayerID = i + 1;
            }

            for (int i = 0; i < 2; i++)
            {
                foreach (Player player in players)
                {
                    player.Add(cards.Dequeue());
                }                


                dealer.Add(cards.Dequeue());
            }

            char input;

            foreach (Player player in players.Where(e => e.Bust == false))
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine($"Player {player.PlayerID}\n" + new string('=', 10) + "\n");               

                    Console.Write($"Dealers card: "); dealer.PeekLast().Display();
                    Console.WriteLine();

                    Console.WriteLine("\nYour Hand:");
                    player.DisplayAll();
                    Console.WriteLine($"\nTotal = {player.Total}");

                    

                    input = Program.GetUserInput<string>(input => !(input.ToUpper() == "H" || input.ToUpper() == "S"), "<H>it or <S>tick: ", "Invalid input").ToUpper()[0];

                    if (input == 'H') //Hit
                    {
                        player.Add(cards.Dequeue());
                        Console.Write($"You got a "); player.PeekLast().Display();
                        Console.WriteLine();
                    }

                    if (player.Bust)
                    {
                        if (player.HasAcesLeft)
                        {
                            player.UseAce();
                        } 
                        else
                        {
                            Console.WriteLine("You've gone bust!");
                            Console.WriteLine("Hit Enter.");Console.ReadLine();
                            break;
                        }
                        
                    }
                    
                } while (input != 'S');
            }

            if (!players.Any())
            {
                Console.WriteLine($"Everyone is bust! ({dealer.Total})"); 
                return;
            } 
            //Dealer hit

            while (dealer.Total < 17)
            {
                dealer.Add(cards.Dequeue());
            } 

            if (dealer.Bust)
            {
                Console.WriteLine("Dealer is Bust! Everyone Wins.");
                return;
            }

            Console.Clear();

            foreach (Player player in players.Where(e => !e.Bust))
            {
                Console.WriteLine($"Player {player.PlayerID} = {player.Total}");
            }


            Console.WriteLine($"\nDealer Total = {dealer.Total}");


            if (dealer.Total >= players.Where(e => e.Bust == false).Max(e => e.Total))
            {
                dealer.Won = true;
                Console.WriteLine("Dealer Wins!");
                return;
            }
            
            List<Player> winners = new List<Player>();
            winners = players.Where(e => e.Bust == false && e.Total == players.Max(e => e.Total)).ToList();

            for (int i = 0; i < winners.Count; i++)
            {
                Console.WriteLine($"Player {players[i].PlayerID} Wins!"); 
            } 

            foreach (Player player in players)
            {
                ReturnCards(player); 
            }

            ReturnCards(dealer);

        }

        private void ReturnCards(Player player)
        {
            List<Card> playerCards = player.ReturnCards();

            foreach (Card card in playerCards)
            {
                cards.Enqueue(card); 
            }
        }
    }
}
