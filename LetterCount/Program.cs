using System;
using System.Collections;
using System.Linq;


namespace LetterCount
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Dictionary<char, int> characters = new Dictionary<char, int>(); //Key = character, Value = Count of character;


            Console.Write("Input String: ");
            string? input = Console.ReadLine();

            foreach (char character in input)
            {
                if (characters.ContainsKey(character)) //If the key already exists, increment the count;
                {
                    characters[character]++;
                }
                else //Else add the key to the dictionary;
                {
                    characters.Add(character, 1);
                }
            }

            foreach (var character in characters)
            {
                if (character.Key == ' ') //Replaces space with <> for display purpose; 
                {
                    Console.WriteLine($"<> - {character.Value}");
                }
                else
                {
                    Console.WriteLine($"{character.Key} - {character.Value}");

                }
            }
            
            //Linq expressions to calculate vowel and consonant counts. (Restricts list to eg. vowels, and then totals the count of those vowels);
            int vowelCount = characters.Where(e =>  "AEIOU".Contains(e.Key.ToString(), StringComparison.InvariantCultureIgnoreCase)).Sum(e => e.Value);
            int consCount = characters.Where(e => "BCDFGHJKLMNPQRSTVXZWY".Contains(e.Key.ToString(), StringComparison.InvariantCultureIgnoreCase)).Sum(e => e.Value);

            //Displays vowel and consonant counts;
            Console.WriteLine($"Vowels - {vowelCount.ToString()}");
            Console.WriteLine($"Consonants - {consCount.ToString()}");
            Console.ReadLine();
        }
    }
}