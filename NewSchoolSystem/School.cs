using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NewSchoolSystem
{
    public class School
    {
        private Hashtable users {get; set;} 

        
        public School()
        {
            users = new Hashtable();
        }

        public void DisplayAll<T>() where T : SchoolMember
        {

            if (users.Values.OfType<T>().Count() == 0)
            {
                Console.WriteLine("There are no Members to Display.");
            }
            else
            {
                Console.Clear();

                if (typeof(T) == typeof(Staff))
                {
                    Console.WriteLine("STAFF\n" + new string('=', 20));
                } 
                else if(typeof(T) == typeof(Student))
                {
                    Console.WriteLine("STUDENTS\n" + new string('=', 20));
                }
                       
                foreach (T user in users.Values.OfType<T>())
                {
                    Console.WriteLine(new string('-', 20));
                    user.Display(); 
                    Console.WriteLine(new string ('-', 20));
                }
            }

            
        }

        public void LookupMember<T>() where T :SchoolMember
        {
            List<string> lookupOptions = new List<string> (){
                "ID",
                "Name",
                "Gender",
                "Age"
            };


            int lookupOption = Program.DisplayMenu(lookupOptions.ToArray(), "LOOKUP OPTIONS");

            List<T> filteredList = new();

            switch (lookupOption)
            {

                case 1:

                    int id = Program.GetUserInput<int>(input => input < 0, "Input ID to Search: ", "Invalid ID");

                    filteredList = users.Values.OfType<T>().Where(e => e.ID == id).ToList();
                    break;
                        
                case 2:
                    Tuple<string, string> name = GetNameTuple(); 

                    filteredList = users.Values.OfType<T>().Where(e => e.Name.Item1 == name.Item1 && e.Name.Item2 == name.Item2).ToList();
                    break;
                
                case 3:
                    GenderType gender = Program.GetUserInput<GenderType>
                    (
                        input => (int)input < 0 || (int)input >= Enum.GetNames(typeof(GenderType)).Count(),
                        "Input Gender (Male, Female, Undefined): ",
                        "Invalid Gender."
                    );

                    filteredList = users.Values.OfType<T>().Where(e => e.Gender == gender).ToList();
                    break;

                case 4:
                    int age = Program.GetUserInput<int>(input => input < 0, "Input Age: ", "Invalid Age.");
                    filteredList = users.Values.OfType<T>().Where(e => e.Age == age).ToList();
                    break;
                
                                    

            }

            filteredList.OrderBy(e => e.ID);

            Console.Clear();
            Console.WriteLine("FILTERED RESULTS" + "\n" + new string('=', 20));
            foreach (T user in filteredList)
            {
                Console.WriteLine(new string('-', 10));
                user.Display();
                Console.WriteLine(new string('-', 10));
            } 

        }

        private static Tuple<string, string> GetNameTuple()
        {
            string firstName = Program.GetUserInput<string>(input => input == "", "Input First Name: ", "Invalid First Name.");
            string lastName = Program.GetUserInput<string>(input => input == "", "Input Last Name: ", "Invalid Last Name.");

            return Tuple.Create(firstName, lastName);
 
        }


        public void AddMember<T>() where T : SchoolMember
        {
            Console.Clear();
            int id = Program.GetUserInput<int>(input => input < 0 || users.ContainsKey(input), "Input ID: ", "Invalid ID.");

            Tuple<string, string> name = GetNameTuple();

            GenderType gender = Program.GetUserInput<GenderType>(input => (int)input < 0 || (int)input >= (int)Enum.GetNames(typeof(GenderType)).Length,
                "Input Gender (Male, Female, Undefined): ", "Invalid Gender");

            DateTime dob = Program.GetUserInput<DateTime>(input => input.Date >= DateTime.Today.Date, "Input Date of Birth (xx/xx/xx): ", "Invalid Date of Birth");

            if (typeof(T) == typeof(Student))
            {
                users.Add(id, new Student(id, name, gender, dob));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Student Added.");
            }
            else if (typeof(T) == typeof(Staff))
            {
                string[] roles = Enum.GetNames(typeof(Staff.RoleType));

                Console.Write("\n");
                for (int i = 0; i < roles.Length; i++)
                {
                    Console.WriteLine($"{i}. {roles[i]}"); 
                } 


                Staff.RoleType role = Program.GetUserInput<Staff.RoleType>(
                    input => (int)input < 0 || (int)input > (int)Enum.GetNames(typeof(Staff.RoleType)).Length,
                    "\nInput Role: ",
                    "Invalid Role");

                users.Add(id, new Staff(id, name, gender, dob, role));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[+] {role} Added.");
            }

            Console.ForegroundColor = ConsoleColor.White;
         
            
        }
    
        public void RemoveMember() 
        {
            while (true)
            {
                int removeIndex = Program.GetUserInput<int>(input => input < -1 , "Input ID to Remove (-1 to cancel): ", "Invalid ID");

                if (removeIndex == -1)
                {
                    Console.WriteLine("Returning to Menu...");
                    return; 
                }
                else if (!users.ContainsKey(removeIndex))
                {
                    Console.WriteLine("There is no member using that ID.");
                    continue;
                }

                users.Remove(removeIndex);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[-] Member Removed.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
   
            }
            
        }

        public T GetMember<T>() where T : SchoolMember
        {
            int id = Program.GetUserInput<int>
            (
                input => input < 0 || !users.ContainsKey(input),
                "Input ID: ",
                "Invalid ID."
            );

            return (T)users[id];
        }


    }
}
