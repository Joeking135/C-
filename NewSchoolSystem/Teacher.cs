using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSchoolSystem
{
    public class Teacher
    {
        public enum RoleType
        {
            Principle,
            Deputy,
            Teacher,
            Assistant
        }

        public RoleType Role {get; private set;}


        
    }
}
