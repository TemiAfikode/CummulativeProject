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
        private SchoolDbContext school = new SchoolDbContext();
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            MySqlConnection Conn = school.AccessDatabase();

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
                teacher.TeacherName = ResultSet["teachername"].ToString();
                teacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"].ToString());
                teacher.Salary = Convert.ToDecimal(ResultSet["salary"].ToString());
                //Access Column information by the DB column name as an index
                string TeacherName = ResultSet["teachername"].ToString();
                //Add the Teacher Name to the List
                TeacherNames.Add(teacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return View(TeacherNames);
        }
        public ActionResult Show(int id)
        {
            MySqlConnection Conn = school.AccessDatabase();

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
                teacher.TeacherName = ResultSet["teachername"].ToString();
                teacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"].ToString());
                teacher.Salary = Convert.ToDecimal(ResultSet["salary"].ToString());
                //Access Column information by the DB column name as an index
                string TeacherName = ResultSet["teachername"].ToString();
                //Add the Teacher Name to the List
            }
            teacher.Courses = getCoursesbyteacher(id);
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return View(teacher);
        }

        private List<Course> getCoursesbyteacher(int id)
        {
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Courses where TeacherId = "+ id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Course> CourseNames = new List<Course> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Course course = new Course();
                course.CourseId = Convert.ToInt32(ResultSet["courseid"].ToString());
                course.CourseName = ResultSet["coursename"].ToString();
                course.CourseCode = (ResultSet["coursecode"].ToString());
                course.TeacherId = Convert.ToInt32(ResultSet["teacherid"].ToString());
               
                //Add the Course Name to the List
                CourseNames.Add(course);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();
            return CourseNames;
        }

    }
}