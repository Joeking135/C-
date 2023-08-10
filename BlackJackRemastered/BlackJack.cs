using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRemastered
{
    internal class BlackJack : Cards
    {

        
        private List<Player> players { get; set; }

        private Player dealer {get; set;}


        public BlackJack() : base()
        {
            Console.Clear();
            players = new List<Player>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    cards.Enqueue(new Card(((Card.Suit)i, (Card.Rank)j)));
                }
            }

            dealer = new Player("Dealer");

            int playerCount = Program.GetUserInput<int>
            (
                input => input < 1 || input > 8, "Input Number of Players (max 8): ", "Invalid Player Count."
            );

            for (int i = 0; i < playerCount; i++)
            {
                players.Add(new Player(i + 1)); 
            }
        }


        public void Play()
        {

            Shuffle();
            Console.Clear();

            for (int i = 0; i < 2; i++)
            {
                foreach (Player player in players)
                {
                    player.Add(cards.Dequeue());
                }                


                dealer.Add(cards.Dequeue());


                
            }

            char input;

            foreach (Player player in players)
            {
                player.CheckAces();
                do
                {
                    Console.Clear();
                    Console.WriteLine($"Player {player.ID} ({player.Name})\n" + new string('=', 10) + "\n");               

                    Console.Write($"Dealers card: "); dealer.PeekLast().Display();
                    Console.WriteLine();

                    Console.WriteLine("\nYour Hand:");
                    player.DisplayAll();
                    Console.WriteLine($"\nTotal = {player.Total}");

                    

                    input = Program.GetUserInput<string>(input => !(input.ToUpper() == "H" || input.ToUpper() == "S"), "<H>it or <S>tick: ", "Invalid input").ToUpper()[0];

                    if (input == 'H') //Hit
                    {
                        player.Add(cards.Dequeue());
                        
                        Console.WriteLine();
                    }


                    player.CheckAces();
                    if (player.Bust)
                    {
                        Console.Write($"You got a "); player.PeekLast().Display(); Console.WriteLine();
                        Console.WriteLine("\nYou've gone bust!");
                        Console.WriteLine("Hit Enter.");Console.ReadLine();
                        break;
                        
                    }
                    
                } while (input != 'S');
            }

            List<Player> remainingPlayers = players.Where(e => !e.Bust).ToList();

            if (!remainingPlayers.Any()) //Checks if everyone is bust, in which case dealer wins
            {
                Console.WriteLine($"Everyone is bust! "); 
                Console.WriteLine("Dealer Wins!");
            } 
            else
            {
                while (dealer.Total < 17) //Dealer hit
                {
                    dealer.Add(cards.Dequeue());

                    dealer.CheckAces(); 
                } 
                Console.Clear();
                Console.Write("\nDealers Hand = "); dealer.DisplayAll();
                Console.WriteLine();

                if (dealer.Bust) //If the dealer is bust, anyone still in play wins.
                {
                    Console.WriteLine($"Dealer is Bust! ({dealer.Total})");
                    foreach (Player player in remainingPlayers)
                    {
                        Console.WriteLine($"Player {player.ID} ({player.Name}) wins!"); 
                    }
                }
                else
                {
                    foreach (Player player in remainingPlayers)
                    {
                        Console.WriteLine($"Player {player.ID} = {player.Total}");
                    }

                    Console.WriteLine($"\nDealer Total = {dealer.Total}\n");
                    
                    remainingPlayers = remainingPlayers.Where(e => e.Total > dealer.Total).ToList();

                    if (!remainingPlayers.Any()) //If no one is above the dealer
                    {
                        Console.WriteLine("Dealer Wins!"); 
                    }
                    else
                    {
                        foreach (Player winner in remainingPlayers)
                        {
                            Console.WriteLine($"Player {winner.ID} ({winner.Name}) Wins!"); 
                        }   
                    }
                    
                }

                
            }

            foreach (Player player in players)
            {
                ReturnCards(player); 
                player.Reset();
            }

            ReturnCards(dealer);

        }


        private void ReturnCards(Player player)
        {
            List<Card> playerCards = player.GetCards();

            foreach (Card card in playerCards)
            {
                cards.Enqueue(card);
            }

            player.ClearCards();
            player.Reset();
            
        }
    }
}
