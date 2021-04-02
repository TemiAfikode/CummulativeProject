using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CummulativeProject.Models
{
    public class Teacher
    {
        public int TeacherId
        {
            get;set;
        }
        public string Teacherfname
        {
            get;set;
        }
        public string Teacherlname
        {
            get;set;
        }
        public string Employeenumber
        {
            get; set;
        }
        public DateTime HireDate
        {
            get;set;
        }
        public decimal Salary
        {
            get;set;
        }
        public List<Class> Classes
        {
            get;set;
        }

    }
}