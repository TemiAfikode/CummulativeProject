using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CummulativeProject.Models
{
    public class Class
    {
        public int ClassId
        {
            get; set;
        }
        public string Classname
        {
            get; set;
        }
        public string Classcode
        {
            get; set;
        }
        public DateTime StartDate
        {
            get; set;
        }
        public DateTime FinishDate
        {
            get; set;
        }
        public int TeacherId
        {
            get; set;
        }
        public List<Class> Classes
        {
            get; set;
        }

    }
}