﻿using System;
using System.ComponentModel;



namespace NewSchoolSystem
{
    internal class Program
    {


        private static string[] mainElements = new string[]{
            "Staff",
            "Student",
            "Save and Quit"
        };

        private static string[] studentMenuElements = new string[]{
            "Display All Students",
            "Lookup Student",
            "Add new Student",
            "Remove Student",
            "Take Attendance",
            "Save and Reset Register",
            "Back"
            };

        private static string[] staffMenuElements = new string[]{
            "Display All Staff",
            "Lookup Staff",
            "Add Staff",
            "Remove Staff",
            "Edit Role",
            "Back"
        };


        public static School school = new();
        static void Main(string[] args)
        {
            int secondarySelection;
            bool quit = false;


            while (!quit)
            {

                int mainMenuSelection = DisplayMenu(mainElements, "MEMBERS");

                switch (mainMenuSelection)
                {
                    case 1:
                        secondarySelection = DisplayMenu(staffMenuElements, "STAFF OPTIONS");
                        switch (secondarySelection)
                        {
                            case 1:
                                school.DisplayAll<Staff>();
                                break;

                            case 2:
                                //lookup staff
                                break;
                            
                            case 3:
                                school.AddMember<Staff>();
                                break;

                            case 4:
                                //Remove staff 
                                break;
                            
                            case 5:
                                //edit role
                                break;
                            
                            default:
                                break;
                        }

                        break;
                    case 2:
                        secondarySelection = DisplayMenu(studentMenuElements, "STUDENT OPTIONS");
                        switch (secondarySelection)
                        {
                            case 1:
                                school.DisplayAll<Student>();
                                break;

                            case 2:
                                //Lookup Student
                                break;

                            case 3:
                                school.AddMember<Student>();
                                break;                            
                            
                            case 4:
                                //Remove Student
                                break;
                            
                            case 5:
                                //Take Attendance
                                break;
                            
                            case 6:
                                //Save and reset register
                                break;
                            default:
                                break;
                        }

                        break;
                    

                    case 3:
                        quit = true;
                        break;

                    
                    default:
                        break;
                }

                Console.Write("\nHit a key."); Console.ReadKey();


            }
            
        }



        private static int DisplayMenu(string[] menuElements, string menuName)
        {

            Console.Clear();
            Console.WriteLine(menuName + "\n" + new string('=', 10));
            for (int i = 0; i < menuElements.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {menuElements[i]}");
            }
            Console.WriteLine();
            return GetUserInput<int>(input => input < 1 || input > menuElements.Length, "Input Selection: ", "Invalid Selection.");
        }

        









        public delegate bool FailCondition<T>(T input);
        public static T GetUserInput<T>(FailCondition<T> failCondition, string request, string errorMessage){
            do
            {
                try
                {
                    Console.Write(request);
                    T output = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(Console.ReadLine());

                    if (failCondition(output))
                    {
                        throw new SystemException(); 
                    }

                    return output;
                }
                catch 
                {
                    Console.WriteLine(errorMessage);
                    continue;
                }
            } while (true);
        }
    }



    
}