using System;
using System.Collections.Concurrent;
using System.Windows;
using System.ComponentModel;
using System.Text.RegularExpressions;



namespace SchoolSystem
{
    class Program
    {


        static void Main(string[] args)
        {
            List<Student> students = new();

            LoadDatabase(ref students);
            do
            {
                
                students = students.OrderBy(e => e.Name).ToList();

                Menu();
                
                int menuSelection = GetUserInput<int>((input => input < 1 || input > 7), "\nEnter Menu Selection:", "That is not a valid Menu Selection. Try again.");
                switch (menuSelection)
                {
                    case 1:
                        Register(ref students);
                        break;
                    case 2:
                        SaveRegister(ref students);
                        break; 

                    case 3:
                        DisplayAllStudents(students);
                        break;
                    case 4:
                        SearchForStudent(students);
                        break;
                    case 5:
                        AddStudent(ref students);
                        break;
                    case 6:
                        RemoveStudent(ref students);
                        break;
                    case 7:
                        SaveDatabase(students);
                        SaveRegister(ref students);
                        goto end;
                }
                Console.Write("Hit Enter"); Console.ReadLine();
                    
            } while (true);

            end: ;
		}
        
        static void Menu(){
            Console.Clear();
            Console.WriteLine( "1. Register"
            + "\n2. Save and reset Register"
            + "\n3. Display all students" 
            + "\n4. Search for student"
            + "\n5. Add new student"
            + "\n6. Remove student"
            + "\n7. Save and Quit");
        }

        static void Register(ref List<Student> students){
            Console.Clear();
            Console.WriteLine("/ = Present, a = Absent.");

            foreach (Student student in students)
            {
                Console.Write($"{student.Name}: ");
                char input = Console.ReadLine()[0];

                student.Attendance = input == '/' ? Student.Register.Present : Student.Register.Absent; 
            } 

            Console.Write("Register Complete."); 
        }

        static void SaveRegister(ref List<Student> students){
            StreamWriter file = new StreamWriter("Register.txt", true);
            
            file.WriteLine(DateTime.Now.ToString());

            foreach (Student student in students)
            {
                file.WriteLine($"{student.Name}: {student.Attendance}");
                student.Attendance = Student.Register.Absent;
            }

            file.Close();

            Console.WriteLine("Register written to file. "); 
        }


        static void LoadDatabase(ref List<Student> students){
            StreamReader file = new("Students.txt");


            string name;
            Student.GenderType genderType;
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Student.GenderType));
            

            while (!file.EndOfStream)
            {
                name = file.ReadLine();
                genderType = (Student.GenderType)converter.ConvertFromString(file.ReadLine());
                file.ReadLine();

                students.Add(new(name, genderType));

            }
            file.Close();

        }



        static void SaveDatabase(List<Student> students){
            
            StreamWriter file = new("Students.txt");
            foreach (Student student in students)
            {
                file.WriteLine(student.Name);
                file.WriteLine(student.Gender); 
                file.WriteLine();
            }

            file.Close();
        }

        static void DisplayAllStudents(List<Student> students){

            Console.Clear();
            foreach (Student student in students)
            {
                Console.WriteLine($"ID: {students.IndexOf(student)}");
                student.Display();
            }

        }

        static void SearchForStudent(List<Student> students){

            Console.Clear();
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();
            bool found = false;

        
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].Name == name)
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

        static void AddStudent(ref List<Student> students){
        
            


            string name = GetUserInput<string>(
                (input => input == ""),
                "Enter Full Name (eg. FirstName Surname): ",
                "That is not in the correct format."
            );

            Student.GenderType genderType = GetUserInput<Student.GenderType>(
                (input => (int)input < 0 || (int)input > 2),
                "Enter Gender (Male, Female, Undefinded): ",
                "That is not a valid gender. Try again."
            );


            students.Add(new(name, (Student.GenderType)genderType));
        }

        static void RemoveStudent(ref List<Student> students){
            
            bool isError = false;
            if (students.Count > 0)
            {

                do
                {
                    DisplayAllStudents(students);

                    int removeIndex = GetUserInput<int>((input => input < 0), "Enter ID to remove: ", "That is not a valid Student ID");

                    if (removeIndex + 1> students.Count )
                    {
                        Console.WriteLine("That is not a valid Student ID.");
                        isError = true;
                        Console.WriteLine("Hit Enter."); Console.ReadLine();
                    }    
                    else
                    {
                        students.RemoveAt(removeIndex);
                        Console.WriteLine("Student removed from the database.");

                        isError = false;
                    }
                } while (isError);

    
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

        public string Name {get; set;}
        public GenderType Gender {get; set;} 

        public Register Attendance {get; set;} 

        public Student(string name, GenderType gender)
        {
            Name = name;
            Gender = gender;
            Attendance = Register.Absent;
        }

        public void Display()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Current Attendance: {Attendance}\n");
        }
    }
}