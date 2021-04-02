using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CummulativeProject.Models
{
    public class Student
    {
        public int StudentId
        {
            get; set;
        }
        public string Studentfname
        {
            get; set;
        }
        public string Studentlname
        {
            get; set;
        }
        public string Studentnumber
        {
            get; set;
        }
        public DateTime EnrolDate
        {
            get; set;
        }
        public List<Student> Students
        {
            get; set;
        }

    }
}