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


    }
}