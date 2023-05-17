using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSchoolSystem
{
    public class Student : SchoolUser 
    {
        public enum Register
        {
            Present,
            Absent
        }

        public Register attendance {get; private set; }


        public Student(string firstName, string lastName, GenderType gender, DateTime dob)
        {
            Name = Tuple.Create(firstName, lastName);                         
            Gender = gender;
            DOB = dob;
            attendance = Register.Absent;
        }


        public override void Display()
        {
            Console.WriteLine("Status: Student");
            base.Display();
            Console.WriteLine($"Attendance: {attendance}");
        }
    }
}
