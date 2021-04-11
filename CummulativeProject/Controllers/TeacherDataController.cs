using CummulativeProject.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CummulativeProject.Controllers
{
   // [Route("api/teacherdata")]
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext schooldb = new SchoolDbContext();
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
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

            //Return the final list of teacher names
            return TeacherNames;
        }
        public Teacher Show(int id)
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

            return teacher;
        }
        /// <summary>
        /// Adds a Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Simon",
        ///	"TeacherLname":"Borer",
        ///	"Employeenumber":"4568",
        ///	"Hiredate":"05/02/2019",
        ///	"Salary":3500
        /// }
        /// </example>
        [HttpPost]
       /// [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = schooldb.AccessDatabase();

            Debug.WriteLine(NewTeacher.Teacherfname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname,@TeacherLname,@Employeenumber, @Hiredate, @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.Teacherfname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.Teacherlname);
            cmd.Parameters.AddWithValue("@Employeenumber", NewTeacher.Employeenumber);
            cmd.Parameters.AddWithValue("@Hiredate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
        /// <summary>
        /// Deletes a Teacher from the connected MySQL Database if the ID of that teacher exists. Does NOT maintain relational integrity. Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = schooldb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }


        private List<Class> getClassesbyteacher(int id)
        {
            MySqlConnection Conn = schooldb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where TeacherId = " + id;

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
