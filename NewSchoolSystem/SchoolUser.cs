using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSchoolSystem
{

    public enum GenderType{
        Male,
        Female,
        Undefined
    }



    public abstract class SchoolUser: IHuman
    {
        
        public Tuple<string, string> Name {get;  protected set;}

        public GenderType Gender {get; protected set;} 

        public int Age {get; protected set;}

        public DateTime DOB {get; protected set;}


        public virtual void Display()
        {
            Console.WriteLine($"Name: {Name.Item1} {Name.Item2}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"DOB: {DOB.ToShortDateString()}");
        }

         
    }
}
