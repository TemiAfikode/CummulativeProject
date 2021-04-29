using CummulativeProject.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();



            return View(Teachers);
        }
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.Show(id);

            return View(NewTeacher);
        }
        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.Show(id);


            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string Employeenumber, string Hiredate, decimal? Salary)
        {
            //Check if teacher data is missing
            if (TeacherFname == null || TeacherLname == null || Employeenumber== null || Hiredate== null || Salary== null)
            {
                //Redirect to action
                return RedirectToAction("New");
            }
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(Employeenumber);
            Debug.WriteLine(Hiredate);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.Teacherfname = TeacherFname;
            NewTeacher.Teacherlname = TeacherLname;
            NewTeacher.Employeenumber = Employeenumber;
            NewTeacher.HireDate = Convert.ToDateTime(Hiredate);
            NewTeacher.Salary = (Decimal)Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();

        }
        /// <summary>
        /// Routes to a dynamically generated "Author Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Author</param>
        /// <returns>A dynamic "Update Author" webpage which provides the current information of the author and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Author/Update/5</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedAuthor = controller.Show(id);

            return View(SelectedAuthor);
        }

        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.Show(id);

            return View(SelectedTeacher);
        }


        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="EmployeeNumber">The updated employee number of the teacher.</param>
        /// <param name="HireDate">The updated hire date of the teacher.</param>
        /// <param name="Salary">The updated salary of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///"TeacherFname":"Sean",
        ///	"TeacherLname":"Doyle",
        ///	"Employeenumber":"55567",
        ///	"Hiredate":"11/10/2019",
        /// "Salary": 206
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string Employeenumber, DateTime Hiredate, decimal Salary)
        {

            /// Create an if statement, if value entererd is null, redirect to update action
            if (TeacherFname == null || TeacherLname == null || Employeenumber == null || Hiredate == null || Salary == null)
            {
                return RedirectToAction("Update/" + id);
            }
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.Teacherfname = TeacherFname;
            TeacherInfo.Teacherlname = TeacherLname;
            TeacherInfo.Employeenumber = Employeenumber;
            TeacherInfo.HireDate = Hiredate;
            TeacherInfo.Salary = Salary;
            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }



    }
}