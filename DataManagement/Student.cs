using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement

{
    public class Student
    {
        public int StudentId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }

    public class ResultView
    {
        public int GradeId { get; set; } // From Grade Table.
        public string LastName { get; set; } // From Student Table.
        public string FirstName { get; set; } // From Student Table.
        public string SubjectName { get; set; } // From Subject Table.
        public int Percentage { get; set; } // From Grade Table.
    }
}
