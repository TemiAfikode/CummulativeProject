using CummulativeProject.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CummulativeProject.Controllers
{
    public class StudentController : Controller
    {
        private SchoolDbContext schooldb = new SchoolDbContext();
        // GET: Student
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
            cmd.CommandText = "Select * from Students";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Student Names
            List<Student> StudentNames = new List<Student> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Student student = new Student();
                student.StudentId = Convert.ToInt32(ResultSet["studentid"].ToString());
                student.Studentfname = ResultSet["studentfname"].ToString();
                student.Studentlname = ResultSet["studentlname"].ToString();
                student.Studentnumber = ResultSet["studentnumber"].ToString();
                student.EnrolDate = Convert.ToDateTime(ResultSet["hiredate"].ToString());

                //Access Column information by the DB column name as an index
                string StudentName = ResultSet["studentfname"] + " " + ResultSet["studentlname"];
                //Add the Student Name to the List
                StudentNames.Add(student);
            }
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return View(StudentNames);
        }

    }
}