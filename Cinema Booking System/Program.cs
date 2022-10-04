using System.Runtime.InteropServices;

namespace Cinema_Booking_System
{
    class Program
    {
        static void Main(string[] args)
        {
            //Copywright of Joe Boothman (I will sue ... probably...).

            bool loop = true;//used to control the loop of entire program
            int horrorTicket = 0, liveTicket = 0, marvelTicket = 0, filthTicket = 0, planeTicket = 0;
            const string ts = "Tickets sold: "; //saves me typing out tickets sold 5 times (petty, I know)


            while (loop)//loops the entire program
            {
                string[] films = { "1. Teenage horror film (15) " + ts + horrorTicket, "2. How I live now (15) " + ts + liveTicket, "3. Another Marvel film (12) " + ts +marvelTicket, "4. Filth (18) "+ts+filthTicket, "5. Planes (U) "+ts+planeTicket };
                bool loop2 = true, oldEnough = true;//loop2 is used for a nested while loop, and oldEnough is used to validate age
                Console.WriteLine("Welcome to Aquinas Multiplex\nWe are presently showing:");
                for (int i = 0; i < films.Length; i++)//prints list (array) of films to the user
                {
                    Console.WriteLine(films[i]);
                }
                try
                {
                    Console.Write("\nEnter the number of the film you wish to see: ");
                    int filmChoice = int.Parse(Console.ReadLine());
                    Console.Write("Enter your age: ");
                    int age = int.Parse(Console.ReadLine());   //takes choice of film and age as integers
                    switch (filmChoice)//determines whether user is old enough and increments tickets sold
                    {
                        case 1 or 2:
                            if (age < 15)
                            {
                                TooYoung(); oldEnough = false;
                            }
                            else if(filmChoice == 1) { horrorTicket++; }
                            else { liveTicket++; }
                            break;
                        case 3:
                            if (age < 12)
                            {
                                TooYoung(); oldEnough = false;
                            }
                            else { marvelTicket++; }
                            break;
                        case 4:
                            if (age < 18)
                            {
                                TooYoung(); oldEnough = false;
                            }
                            else { filthTicket++; }
                            break;
                        case 5:
                            planeTicket++;
                            break;
                        default:
                            NoFilm();break;
                    }
                    if (filmChoice >=1 && filmChoice <= 5 && oldEnough == true) //Validates that all necesary conditions are met before proceeding
                    {
                        while (loop2)//nested loop used for try catch
                        {
                            Console.Write("What date would you like to see the film? Must be within 7 days (xx/xx/xx): ");
                            try
                            {
                                DateTime date = DateTime.Parse(Console.ReadLine()).Date;  //user inputs date
                                
                                if (date < DateTime.Today.AddDays(8) && date >= DateTime.Today) //checks if the date is within one week and not in the past. Note: I also allowed exactly 7 days in the future
                                {
                                    filmChoice--; // allows film integer to be used with array which starts at 0, not 1
                                    Console.WriteLine($"\n--------------------\nAquinas Multiplex\nFilm: {films[filmChoice][3..^20]}\nDate: {date.ToShortDateString()}\n\nEnjoy the film\n-------------------- ");
                                    Console.WriteLine("Press enter to make a new booking: ");Console.ReadLine(); //prints ticket and allows the user to read ticket before restarting
                                    loop2 = false; Console.Clear();//breaks out of nested loop and clears the console for next booking
                                }
                                else
                                {
                                    InvDate();
                                }
                            }
                            catch
                            {
                                InvFormat(); //these catch statements catch any rogue inputs and causes the loop to restart
                            }
                        }
                    }

                }
                catch
                {
                    InvFormat();
                }
            }
        }
        static void TooYoung() //These 4 methods output specific error messages in red text (Inv is short for invalid)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAccess denied - You are too young\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void NoFilm()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAccess denied - No such film\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void InvDate()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAccess denied - Invalid date\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void InvFormat()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThat is not the correct format...Try again: \n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
