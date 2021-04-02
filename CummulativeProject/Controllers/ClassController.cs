using CummulativeProject.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CummulativeProject.Controllers
{
    public class ClassController : Controller
    {
        private SchoolDbContext schooldb = new SchoolDbContext();
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            MySqlConnection Conn = schooldb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classess";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Class> ClassNames = new List<Class> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Class course = new Class();
                course.ClassId = Convert.ToInt32(ResultSet["classid"].ToString());
                course.Classname = ResultSet["classname"].ToString();
                course.Classcode = ResultSet["classcode"].ToString();
                course.StartDate = Convert.ToDateTime(ResultSet["startdate"].ToString());
                course.FinishDate = Convert.ToDateTime(ResultSet["finishdate"].ToString());
                //Add the Class Name to the List
                ClassNames.Add(course);
            }
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return View(ClassNames);
        }

    }
}