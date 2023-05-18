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

        public Staff(int id, string firstName, string lastName, GenderType gender, DateTime dob, RoleType role)
        {
            ID = id; 
            Name = Tuple.Create(firstName, lastName);
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
