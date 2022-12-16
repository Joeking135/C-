class Program
{
    private struct LoginStruct
    {
        public string MainUsername;
        public string Mainpassword;
    }

    private struct Passwords
    {
        public string Website;
        public string Username;
        public string Password;
    }


    static void Main(string[] args)
    {
        List<Passwords> index = new List<Passwords>();
        List<LoginStruct> logins = new List<LoginStruct>();

        Read(ref index, ref logins);
        bool quit = false;

        int sessionPos = 0;
        Login(logins);


        do
        {
            Console.Clear();
            Console.Write("1. Password manager\n2. Main Login manager\nq. Quit\n\nEnter selection: ");
            char managerChoice = Console.ReadLine()[0];
            switch (managerChoice)
            {
                case '1': //password manager
                    Console.Clear();
                    IndexOutput(index);
                    Console.Write("\n1. Add\n2. Remove\n3. Clear\nq. Quit\n\nEnter Selection:");
                    char passwordManagerChoice = Console.ReadLine().ToLower()[0];

                    switch (passwordManagerChoice)
                    {
                        case '1':
                            Add(ref index);
                            break;
                        case '2':
                            Remove(ref index);
                            break;
                        case '3':
                            Clear(ref index);
                            break;
                        case 'q':
                            break;

                        default:
                            Console.Write("That is not a valid selection. ");
                            break;
                    }
                    break;


                case '2': //Main manager
                    Console.Clear();
                    Console.Write("1. Display Main Logins\n2. Add Main Login\n3. Remove Main Login\nq. Quit\n\nEnter Selection: ");
                    char mainManagerChoice = Console.ReadLine()[0];
                    switch (mainManagerChoice)
                    {
                        case '1':
                            DisplayMains(logins);
                            break;
                        case '2':
                            AddLogin(ref logins);
                            break;
                        case '3':
                            RemoveMain(ref logins);
                            break;
                        case 'q':
                            break;


                        default:
                            Console.Write("That is not a valid selection. ");
                            break;
                    }
                    break;

                case 'q':
                    quit = true;
                    break;

                default:
                    Console.Write("That is not a valid selection. ");
                    break;
            }
            Console.WriteLine("Hit a key."); Console.ReadKey();
            Write(index, logins);
        }
        while (!quit);
    }

    private static void Login(List<LoginStruct> logins)
    {

        int failedAttempts = 0;
        int waitingTime = 150000; 

        while (failedAttempts < 5)
        {
            Console.Clear();
            Console.Write("Input username: ");
            string username = Console.ReadLine();

            if (logins.Any(client => client.MainUsername == username))
            {
                int Pos = logins.FindIndex(client => client.MainUsername == username);
                Console.Write("Enter password: ");
                string passwordAttempt = Console.ReadLine();

                if (logins.ElementAt(Pos).Mainpassword == passwordAttempt)
                {
                    Console.Clear();
                    Console.WriteLine("Login successful. Hit a key"); Console.ReadKey(); return;
                }
                else
                {
                    Console.WriteLine("Incorrect password.");
                    failedAttempts++;
                    
                }
            }
            else
            {
                Console.WriteLine("Username not found");
            }
            if (failedAttempts == 5)
            {
                waitingTime *= 2;

                Console.Clear();
                Console.WriteLine($"You have had too many incorrect attempts. Wait {waitingTime / 60000} minutes");
                for (int i = 0; i < waitingTime / 1000; i++)
                {
                    Console.SetCursorPosition(0, 1);
                    Console.Write($"{i} / {waitingTime / 1000} seconds");
                    Thread.Sleep(1000);
                }

                failedAttempts = 0;
            }


            Console.WriteLine("Hit a key");
            Console.ReadKey();


        }
        

    }


    


    static void Read(ref List<Passwords> index, ref List<LoginStruct> logins)
    {
        StreamReader sr = new StreamReader("Index.txt");
        while (!sr.EndOfStream)
        {
            Passwords Read;
            Read.Website = sr.ReadLine();
            Read.Username = sr.ReadLine();
            Read.Password = sr.ReadLine();
            index.Add(Read);
        }
        sr.Close();

        StreamReader sr2 = new StreamReader("logins.txt");

        while (!sr2.EndOfStream)
        {
            LoginStruct user;
            user.MainUsername = sr2.ReadLine();
            user.Mainpassword = sr2.ReadLine();
            logins.Add(user);
        }
        sr2.Close();

    }
    static void Write(List<Passwords> index, List<LoginStruct> logins)
    {
        StreamWriter sw = new StreamWriter("Index.txt");

        for (int i = 0; i < index.Count; i++)
        {
            sw.WriteLine(index[i].Website);
            sw.WriteLine(index[i].Username);
            sw.WriteLine(index[i].Password);
        }
        sw.Close();

        StreamWriter sw2 = new StreamWriter("logins.txt");
        for (int i = 0; i < logins.Count; i++)
        {
            sw2.WriteLine(logins[i].MainUsername);
            sw2.WriteLine(logins[i].Mainpassword);
        }
        sw2.Close();

    }
    static void IndexOutput(List<Passwords> index)
    {
        index.OrderBy(item => item.Website);
        Console.WriteLine("Index\n-------\n");
        for (int i = 0; i < index.Count; i++)
        {
            Console.WriteLine($"{i}. WEBSITE: {index[i].Website}\tUSERNAME: {index[i].Username}\tPASSWORD: {index[i].Password}");
        }
    }

    static void Add(ref List<Passwords> index)
    {
        Console.Clear();
        Passwords input;

        Console.Write("Enter Website: "); input.Website = Console.ReadLine();
        Console.Write("Enter Username: "); input.Username = Console.ReadLine();
        Console.Write("Enter Password: "); input.Password = Console.ReadLine();
        index.Add(input);
    }

    static void Remove(ref List<Passwords> index)
    {
        while (true)
        {
            Console.Clear();
            IndexOutput(index);
            Console.Write("\nEnter number you want to remove or quit (q): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "q")
            {
                return;
            }

            //else:
            try
            {
                index.RemoveAt(int.Parse(input));
            }
            catch
            {
                Console.WriteLine("That is not a valid selection. Hit a key."); Console.ReadKey();
            }
        }
    }

    static void Clear(ref List<Passwords> index)
    {
        Console.Write("Are you sure you want to clear the index? [y/n]");
        char input = Console.ReadLine().ToLower()[0];

        if (input == 'y')
        {
            index.Clear();
            Console.WriteLine("Index cleared. Hit a key"); Console.ReadKey();
        }
        else
        {
            return;
        }
    }

    static void DisplayMains(List<LoginStruct> logins)
    {

        Console.Clear();
        for (int i = 0; i < logins.Count; i++)
        {
            Console.WriteLine($"{i + 1}. USERNAME: {logins[i].MainUsername}\tPASSWORD: {logins[i].Mainpassword}");
        }
    }

    static void AddLogin(ref List<LoginStruct> logins)
    {
        Console.Clear();
        LoginStruct NewLogin;
        Console.Write("Enter a Username: "); NewLogin.MainUsername = Console.ReadLine();
        Console.Write("Enter a Password: "); NewLogin.Mainpassword = Console.ReadLine();
        Console.Clear();
        if (NewLogin.MainUsername == NewLogin.Mainpassword)
        {
            Console.WriteLine("The username and password must not be the same.");
        }
        else
        {
            logins.Add(NewLogin); Console.Write("Login added. ");
        }

    }

    static void RemoveMain(ref List<LoginStruct> logins)
    {
        while (true)
        {
            if (logins.Count <= 1)
            {
                Console.WriteLine("You only have one login."); return;
            }
            else
            {
                Console.Clear();
                DisplayMains(logins);
                Console.WriteLine("\nEnter the number you would like to remove or c to cancel.");
                string input = Console.ReadLine().ToLower();
                if (input == "c")
                {
                    return;
                }
                else
                {
                    try
                    {
                        int intInput = int.Parse(input);
                        logins.RemoveAt(intInput - 1);
                        Console.Write("Login removed. "); return;

                    }
                    catch
                    {
                        Console.WriteLine("That is not a valid input. Try again."); Thread.Sleep(1000);
                    }
                }
            }
            
        }
        
    }
}
