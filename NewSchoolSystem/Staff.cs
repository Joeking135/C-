using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSchoolSystem
{
    public class Staff : SchoolMember
    {
        public enum RoleType
        {
            Principle,
            Deputy,
            Teacher,
            Assistant
        }

        public RoleType Role {get; private set;}

        public Staff(int id, Tuple<string, string> name, GenderType gender, DateTime dob, RoleType role)
        {
            ID = id; 
            Name = name;
            Gender = gender;
            DOB = dob;
            Age = GetAgeFromDOB(dob);
            Role = role;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"Role: {Role}");
        } 
        
    }
}
