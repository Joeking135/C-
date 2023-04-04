using System;
using System.Collections.Concurrent;
using System.Windows;
using System.ComponentModel;
using System.Text.RegularExpressions;



namespace SchoolSystem
{

    class Program
    {

        static List<Student> students = new();

        static string[] menuElements = {
            "1. Register",
            "2. Save and reset Register",
            "3. Display all students",
            "4. Lookup Student",
            "5. Add new student",
            "6. Remove student",
            "7. Save and Quit"
        };

        static void Main(string[] args)
        {
            bool quit;
            LoadDatabase();
            do
            {
                quit = false;
                students = students.OrderBy(e => e.LastName).ToList();

                Menu();

                int menuSelection = GetUserInput<int>((input => input < 1 || input > menuElements.Length), "\nEnter Menu Selection:", "That is not a valid Menu Selection. Try again.");
                switch (menuSelection)
                {
                    case 1:
                        Register();
                        break;
                    case 2:
                        SaveRegister();
                        break;

                    case 3:
                        DisplayAllStudents();
                        break;
                    case 4:
                        SearchForStudent();
                        break;
                    case 5:
                        AddStudent();
                        break;
                    case 6:
                        RemoveStudent();
                        break;
                    case 7:
                        SaveDatabase();
                        SaveRegister();
                        quit = true;
                        break;
                }
                Console.Write("Hit Enter"); Console.ReadLine();

            } while (!quit);

        }

        static void Menu()
        {
            Console.Clear();
            foreach (string menuElement in menuElements)
            {
                Console.WriteLine(menuElement);
            } 
        }

        static void Register()
        {
            Console.Clear();
            Console.WriteLine("/ = Present, a = Absent.");

            foreach (Student student in students)
            {
                Console.Write($"{student.FirstName} {student.LastName}: ");
                char input = Console.ReadLine()[0];

                student.Attendance = input == '/' ? Student.Register.Present : Student.Register.Absent;
            }

            Console.Write("Register Complete.");
        }

        static void SaveRegister()
        {
            StreamWriter file = new StreamWriter("Register.txt", true);

            file.WriteLine(DateTime.Now.ToString());

            foreach (Student student in students)
            {
                file.WriteLine($"{student.FirstName} {student.LastName}: {student.Attendance}");
                student.Attendance = Student.Register.Absent;
            }

            file.Close();

            Console.WriteLine("Register written to file. ");
        }


        static void LoadDatabase()
        {
            StreamReader file = new("Students.txt");


            string firstName;
            string lastName; 
            Student.GenderType genderType;
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Student.GenderType));


            while (!file.EndOfStream)
            {
                firstName = file.ReadLine();
                lastName = file.ReadLine();
                genderType = (Student.GenderType)converter.ConvertFromString(file.ReadLine());
                file.ReadLine();

                students.Add(new(firstName, lastName, genderType));

            }
            file.Close();

        }



        static void SaveDatabase()
        {

            StreamWriter file = new("Students.txt");
            foreach (Student student in students)
            {
                file.WriteLine(student.FirstName);
                file.WriteLine(student.LastName);
                file.WriteLine(student.Gender);
                file.WriteLine();
            }

            file.Close();
        }

        static void DisplayAllStudents()
        {

            Console.Clear();
            foreach (Student student in students)
            {
                Console.WriteLine($"ID: {students.IndexOf(student)}");
                student.Display();
            }

        }

        static void SearchForStudent()
        {

            Console.Clear();

            string firstName = GetUserInput<string>(
                (input => input == ""),
                "Enter first name: ",
                "That is not a valid first name. Try again."
            );

            string lastName = GetUserInput<string>(
                (input => input == ""),
                "Enter last name: ",
                "That is not a valid last name. Try again."
            );

            bool found = false;


            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].FirstName == firstName && students[i].LastName == lastName)
                {
                    Console.WriteLine($"Found: ID = {i}");
                    found = true;
                }

            }

            if (!found)
            {
                Console.Write("\nStudent not found. ");
            }

        }

        static void AddStudent()
        {

            string firstName = GetUserInput<string>(
                (input => input == ""),
                "Enter First Name: ",
                "That is not a valid first name. Try Again."
            );

            string lastName = GetUserInput<string>(
                (input => input == ""),
                "Enter Last Name: ",
                "That is not a valid last name. Try again."
            ); 

            Student.GenderType genderType = GetUserInput<Student.GenderType>(
                (input => (int)input < 0 || (int)input > 2),
                "Enter Gender (Male, Female, Undefinded): ",
                "That is not a valid gender. Try again."
            );

            students.Add(new(firstName, lastName, (Student.GenderType)genderType));
        }

        static void RemoveStudent()
        {

            bool isError = false;
            if (students.Count > 0)
            {
                DisplayAllStudents();
                int removeIndex = GetUserInput<int>(
                    (input => input < 0 || input >= students.Count),
                    "Enter ID to remove: ",
                    "That is not a valid Student ID"
                );

                students.RemoveAt(removeIndex);
            }
            else
            {
                Console.WriteLine("There are no students on the database.");
            }

        }

        delegate bool FailCondition<T>(T input);
        static T GetUserInput<T>(FailCondition<T> failCondition, string request, string errorMessage)
        {
            bool isError = false;

            do
            {
                try
                {
                    Console.Write(request);
                    T output;
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

                    if (converter != null)
                    {
                        output = (T)converter.ConvertFromString(Console.ReadLine());
                        if (failCondition(output))
                        {
                            Console.WriteLine(errorMessage);
                            isError = true;
                            continue;
                        }

                        return output;
                    }
                }
                catch
                {
                    Console.WriteLine(errorMessage);
                    isError = true;
                    continue;
                }
            }
            while (isError);

            return default;
        }

    }
    class Student
    {
        public enum Register
        {
            Present,
            Absent
        }

        internal enum GenderType
        {
            Male,
            Female,
            Undefined
        }

        public string FirstName { get; set; }
        public string LastName {get; set; }
        public GenderType Gender { get; set; }
        public Register Attendance { get; set; }

        public Student(string firstName, string lastName, GenderType gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Attendance = Register.Absent;
        }

        public void Display()
        {
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Current Attendance: {Attendance}\n");
        }
    }
}