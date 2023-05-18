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

        public void DisplayAll<T>()
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

                    foreach (Staff staff in users.Values.OfType<Staff>())
                    {
                        Console.WriteLine(new string('-', 20));
                        staff.Display(); 
                        Console.WriteLine(new string ('-', 20));
                    }   
                } 
                else if(typeof(T) == typeof(Student))
                {
                    Console.WriteLine("STUDENTS\n" + new string('=', 20));
                    foreach (Student student in users.Values.OfType<Student>())
                    {
                        Console.WriteLine(new string('-', 20));
                        student.Display(); 
                        Console.WriteLine(new string('-', 20));
                    }
                }
            }

            
        }


        public void AddMember<T>()
        {
            Console.Clear();
            int id = Program.GetUserInput<int>(input => input < 0 || users.ContainsKey(input), "Input ID: ", "Invalid ID.");
            string firstName = Program.GetUserInput<string>(input => input == "", "Input First Name: ", "Invalid First Name.");
            string lastName = Program.GetUserInput<string>(input => input == "", "Input Last Name: ", "Invalid Last Name.");
            GenderType gender = Program.GetUserInput<GenderType>(input => (int)input < 0 || (int)input >= (int)Enum.GetNames(typeof(GenderType)).Length,
                "Input Gender (Male, Female, Undefined): ", "Invalid Gender");

            DateTime dob = Program.GetUserInput<DateTime>(input => input.Date >= DateTime.Today.Date, "Input Date of Birth (xx/xx/xx): ", "Invalid Date of Birth");

            if (typeof(T) == typeof(Student))
            {
                users.Add(id, new Student(id, firstName, lastName, gender, dob));
                Console.WriteLine("Student Added.");
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
                
                users.Add(id, new Staff(id, firstName, lastName, gender, dob, role));
                Console.WriteLine($"{role} Added.");
            }
            else
            {
                Console.WriteLine("COMPILATION ERROR: You have called (AddUser<T>), but the type does not match Student or Teacher. ");
            }
        }
    }
}
