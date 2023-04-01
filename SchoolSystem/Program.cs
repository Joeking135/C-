using System;
using System.Collections.Concurrent;
using System.Text;
using System.Windows;

//Cursor parking: _________1


namespace SchoolSystem
{
    class Program
    {

        // Add remove student routine. Add student.txt to store student name and gender only (register is seperate).

        static void Main(string[] args)
        {
            List<Student> students = new();

            LoadDatabase(ref students);
            do
            {

                
                students = students.OrderBy(e => e.Name).ToList();

                Menu();
                
                GetUserInput(
                    out int menuSelection,
                    delegate (int input){ return input < 1 || input > 7 ;},
                    "\nInput Operation: ",
                    "That is not a valid operation number. Try again."
                );

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
            char gender;
            Student.GenderType genderType;
            while (!file.EndOfStream)
            {
                name = file.ReadLine();
                gender = file.ReadLine().ToUpper()[0];
                file.ReadLine();

                genderType = GetGenderType(gender); 

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
        
            Console.Write("Input Full Name: "); string name = Console.ReadLine() ?? "Undefined";
            Console.Write("Input Gender (M/F/U): "); char gender = Console.ReadLine().ToUpper()[0];

            Student.GenderType genderType;
            genderType = GetGenderType(gender); 

            students.Add(new(name, genderType));
        }

        static void RemoveStudent(ref List<Student> students){
            
            bool isError = false;
            if (students.Count > 0)
            {
                do
                {
                    DisplayAllStudents(students);

                    GetUserInput(
                        out int removeIndex,
                        delegate (int input) {return input < 0 ; },
                        $"Enter student ID to remove: ",
                        "That is not a valid student ID."
                    );

                    if (removeIndex + 1> students.Count )
                    {
                        Console.WriteLine("That is not a valid Student ID.");
                        isError = true;
                        Console.WriteLine("Hit Enter."); Console.ReadLine();
                    }    
                    else
                    {
                        students.RemoveAt(removeIndex);
                        Console.WriteLine("Student removed from the dtatbase.");

                        isError = false;
                    }
                } while (isError);

    
            }
            else
            {
                Console.WriteLine("There are no students on the database.");
            }                        

        }
        delegate bool FailConditionInt(int input);
        static void GetUserInput(out int input, FailConditionInt failConditionInt, string request, string errorMessage){
            bool isError = false;
            do
            {
                input = -1;
                try
                {
                    Console.Write(request); 
                    input = int.Parse(Console.ReadLine());
                    if (failConditionInt(input))
                    {
                        Console.WriteLine(errorMessage);
                        isError = true;
                    }
                    else
                    {
                        isError = false;
                    }
                }
                catch 
                {
                    Console.WriteLine(errorMessage);
                    isError = true;    
                } 
            } while (isError);

        }
        static Student.GenderType GetGenderType(char gender){
            
            switch (gender)
            {
                case 'M':
                    return Student.GenderType.Male; 
                case 'F':
                    return Student.GenderType.Female; 
                default:
                    return Student.GenderType.Undefined; 
            }
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