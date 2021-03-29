using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CummulativeProject.Models
{
    public class Course
    {
        public int CourseId
        {
            get; set;
        }
        public string CourseName
        {
            get; set;
        }
        public string CourseCode
        {
            get; set;
        }
        public int TeacherId
        {
            get; set;
        }
    }
}