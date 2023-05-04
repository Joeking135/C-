using System;
using System.Collections.Concurrent;
using System.ComponentModel;



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
                students = students.OrderBy(e => e.Name.Item2).ToList();

                if (students.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("There are no students on the database -> Adding first student: ");
                    AddStudent();
                }


                Menu();

                int menuSelection = GetUserInput<int>((input => input < 1 || input > menuElements.Length), "\nEnter Menu Selection: ", "That is not a valid Menu Selection. Try again.");
                switch (menuSelection)
                {
                    case 1:
                        Register();
                        Console.Write("Register Complete.");
                        break;
                    case 2:
                        SaveRegister();
                        Console.WriteLine("Register written to file. ");
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
                Console.Write("Press Enter: "); Console.ReadLine();

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
            Console.WriteLine("/ = Present, a = Absent. (q to quit).");

            foreach (Student student in students)
            {

                char input = GetUserInput<char>(
                    (input => input != '/' && input != 'a' && input != 'q'),
                    $"{student.Name.Item1} {student.Name.Item2}: ",
                    "That is not a valid character."
                );

                if (input == 'q') {return; }

                student.Attendance = (input == '/') ? Student.Register.Present : Student.Register.Absent;
            }

        }

        static void SaveRegister()
        {
            StreamWriter file = new("Register.txt", true);

            file.WriteLine(DateTime.Now.ToString());

            foreach (Student student in students)
            {
                file.WriteLine($"{student.Name.Item1} {student.Name.Item2}: {student.Attendance}");
                student.Attendance = Student.Register.Absent;
            }

            file.Close();

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
                try
                {
                    firstName = file.ReadLine();
                    lastName = file.ReadLine();
                    genderType = (Student.GenderType)converter.ConvertFromString(file.ReadLine());
                    file.ReadLine();

                    students.Add(new(firstName, lastName, genderType));
                }
                catch (System.Exception)
                {
                    file.Close();
                    return;
                }


            }
            
            file.Close();

        }



        static void SaveDatabase()
        {

            StreamWriter file = new("Students.txt");
            foreach (Student student in students)
            {
                file.WriteLine(student.Name.Item1);
                file.WriteLine(student.Name.Item2);
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
                if (students[i].Name.Item1 == firstName && students[i].Name.Item2 == lastName)
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
                (input => (int)input < 0 || (int)input >= Enum.GetNames(typeof(Student.GenderType)).Length),
                "Enter Gender (Male, Female, Undefined): ",
                "That is not a valid gender. Try again."
            );

            students.Add(new(firstName, lastName, (Student.GenderType)genderType));
        }

        static void RemoveStudent()
        {

            DisplayAllStudents();
            int removeIndex = GetUserInput<int>(
                (input => input < -1 || input >= students.Count),
                "Enter ID to remove (-1 to quit): ",
                "That is not a valid Student ID"
            );

            if (removeIndex == -1) { return; }

            students.RemoveAt(removeIndex);

        }

        delegate bool FailCondition<T>(T input);
        static T GetUserInput<T>(FailCondition<T> failCondition, string request, string errorMessage)
        {
            do
            {
                try
                {
                    Console.Write(request);
                    T output;
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                    output = (T)converter.ConvertFromString(Console.ReadLine());

                    if (failCondition(output))
                    {
                        Console.WriteLine(errorMessage);
                        continue;
                    }

                        return output;
                }
                catch
                {
                    Console.WriteLine(errorMessage);
                    continue;
                }
            }
            while (true);

        }

    }
    class Student
    {
        internal enum Register
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

        public Tuple<string, string> Name {get; set;}
        public GenderType Gender { get; set; }
        public Register Attendance { get; set; }

        public Student(string firstName, string lastName, GenderType gender)
        {
            Name = Tuple.Create(firstName, lastName);
            Gender = gender;
            Attendance = Register.Absent;
        }

        public void Display()
        {
            Console.WriteLine($"Name: {Name.Item1} {Name.Item2}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Current Attendance: {Attendance}\n");
        }

        
    }
}