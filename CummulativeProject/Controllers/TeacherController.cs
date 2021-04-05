using CummulativeProject.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CummulativeProject.Controllers
{
    public class TeacherController : Controller
    {
        private SchoolDbContext schooldb = new SchoolDbContext();
        // GET: Teacher
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
            cmd.CommandText = "Select * from Teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Teacher> TeacherNames = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Teacher teacher = new Teacher();
                teacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"].ToString());
                teacher.Teacherfname = ResultSet["teacherfname"].ToString();
                teacher.Teacherlname = ResultSet["teacherlname"].ToString();
                teacher.Employeenumber = ResultSet["employeenumber"].ToString();
                teacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"].ToString());
                teacher.Salary = Convert.ToDecimal(ResultSet["salary"].ToString());
                //Add the Teacher Name to the List
                TeacherNames.Add(teacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return View(TeacherNames);
        }
        public ActionResult Show(int id)
        {
            MySqlConnection Conn = schooldb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where teacherid=" + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();


            Teacher teacher = new Teacher { };

            //If statement

            if (ResultSet.Read())
            {
                teacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"].ToString());
                teacher.Teacherfname = ResultSet["teacherfname"].ToString();
                teacher.Teacherlname = ResultSet["teacherlname"].ToString();
                teacher.Employeenumber = ResultSet["employeenumber"].ToString();
                teacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"].ToString());
                teacher.Salary = Convert.ToDecimal(ResultSet["salary"].ToString());
                //Add the Teacher Name to the List
            }
            teacher.Classes = getClassesbyteacher(id);
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return View(teacher);
        }

        private List<Class> getClassesbyteacher(int id)
        {
            MySqlConnection Conn = schooldb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where TeacherId = "+ id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Class Names
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

            return ClassNames;
        }

    }
}