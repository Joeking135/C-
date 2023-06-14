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
            Player player = new Player();
            Player dealer = new Player();

            for (int i = 0; i < 2; i++)
            {
                player.Add(cards.Dequeue());
                dealer.Add(cards.Dequeue());
            }

            char input;

            do
            {
                Console.Clear();

                Console.WriteLine($"Dealers card: {dealer.PeekLast()}");

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
                        return;   
                    }
                    
                }
                
            } while (input != 'S');

            //Dealer hit

            while (dealer.Total < 17)
            {
                dealer.Add(cards.Dequeue());
            } 

            Console.Clear();
            Console.WriteLine($"Your Total = {player.Total}");
            Console.WriteLine($"Dealers Total = {dealer.Total}");


            //evaluate winnings (only need to change positive positions)

            if (dealer.Bust)
            {
                player.Won = true; 
                Console.WriteLine($"Dealer has gone bust!");
            }
            else if (player.Total > dealer.Total)
            {
                player.Won = true; 
            }


            Console.WriteLine(player.Won ? "You win!": "You lose!");




            



        }
    }
}
