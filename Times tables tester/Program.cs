namespace Times_tables_tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new System.Diagnostics.Stopwatch(); timer.Start();
            bool loop = true;
            Random rnd = new Random();

            Console.WriteLine("Hello. This is a times tables test. You will be asked 10 questions and a score will be taken."); Thread.Sleep(2000);
            Console.Write("\nWhat is your name?  ");
            string name = Console.ReadLine();
            Console.WriteLine($"Ok {name}, ");
            while (loop)
            {
                int score = 0;
                Console.Write("Which times table would you like to be tested on?  ");
                try
                {
                    int table = int.Parse(Console.ReadLine());
                    if (table < 13 && table > 0)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            int num = rnd.Next(1, 13);
                            Console.Write($"What is {table} X {num}?  ");
                            int input = int.Parse(Console.ReadLine());
                            int correct = table * num;

                            if (correct == input)
                            {
                                score++;
                                Console.WriteLine("Correct!"); Thread.Sleep(500);
                            }
                            else
                            {
                                Console.WriteLine($"Incorrect...{table} X {num} = {correct}"); Thread.Sleep(500);
                            }
                        }
                        loop = false;
                        string msg;
                        if (score == 0)
                        {
                            msg = "Oh dear";
                        }
                        else if (score >= 1 && score <= 5)
                        {
                            msg = "Keep practicing";
                        }
                        else if (score >= 6 && score <= 9)
                        {
                            msg = "Nice job";
                        }
                        else
                        {
                            msg = "Incredible";
                        }
                        timer.Stop(); TimeSpan timeTaken = timer.Elapsed;
                        Console.WriteLine($"{msg}! You got {score} out of 10.");
                        Console.Write($"Time taken: {Math.Round(timeTaken.TotalSeconds)} seconds.");


                        Console.ReadLine();

                    }
                    else
                    {
                        Console.WriteLine("That is not between 1 and 12..."); Thread.Sleep(2000);
                    }

                }
                catch
                {
                    Console.WriteLine("That is not an number..."); Thread.Sleep(2000);
                }
            }

        }
    }
}
