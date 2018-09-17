using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCRMS.Manager;
using UCRMS.Models;

namespace UCRMS.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        private DepartmentManager aDepartmentManager=new DepartmentManager();
        private StudentManager aManager=new StudentManager();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateStudent()
        {
            ViewBag.Department = aDepartmentManager.GetAllDepartment();
            return View();
        }
        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            ViewBag.Department = aDepartmentManager.GetAllDepartment();
            if (aManager.IsStudentEmailExist(student.Email))
            {
                ViewBag.message = "Email is already exist";
            }
            else
            {
                int message = aManager.SaveStudent(student);
                if (message>0)
                {
                    ViewBag.message = "Save successfully";
                }
                else
                {
                    ViewBag.message = "failed to save";
                }
            }
            return View();
        }
	}
}