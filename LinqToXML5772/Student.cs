using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToXML5772
{
    public class Student
    {
        double id;
        string firstName;
        string lastName;
        static double countID = 0;

        public static double CountID
        {
            get { return Student.countID; }
            set { Student.countID = value; }
        }

        public double Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public Student(string fName, string lName)
        //{
        //    firstName = fName;
        //    lastName = lName;

        //}
    }
}
