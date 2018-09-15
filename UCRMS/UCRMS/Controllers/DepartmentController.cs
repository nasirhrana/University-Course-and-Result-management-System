using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCRMS.Manager;
using UCRMS.Models;

namespace UCRMS.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        private DepartmentManager aManager=new DepartmentManager();
        public ActionResult Index()
        {
            List<Department> aList = aManager.GetAllDepartment();
            return View(aList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (aManager.CheckeCode(department.Code))
            {
                ViewBag.msg = "Code is already exist";
            }
            else if (aManager.CheckeName(department.Name))
            {
                ViewBag.msg1 = "Name  is already exist";
            }
            else
            {
                int msg = aManager.SavaDepartment(department);
                if (msg > 0)
                {
                    ViewBag.message = "Save Successfully";
                }
                else
                {
                    ViewBag.message = "failed to Save ";
                }

                return RedirectToAction("Index");
            }
            return View();

        }
        [HttpGet]
        public ActionResult CreateCourse()
        {
            var alist = aManager.GetAllDepartment();
            ViewBag.Department = alist;
            ViewBag.Semester = aManager.GetAllSemester();
            return View();
        }
        [HttpPost]
        public ActionResult CreateCourse(Course course)
        {
            ViewBag.Department = aManager.GetAllDepartment();
            ViewBag.Semester = aManager.GetAllSemester();
            if (aManager.CheckeCourseCode(course.CourseCode))
            {
                ViewBag.msg = "Code is already exist";
            }
            else if (aManager.CheckCourseeName(course.Name))
            {
                ViewBag.msg1 = "Name  is already exist";
            }
            else
            {
                int msg = aManager.SavaCourse(course);
                if (msg > 0)
                {
                    ViewBag.message = "Save Successfully";
                }
                else
                {
                    ViewBag.message = "failed to Save ";
                }

                //return RedirectToAction("Index");
            }

            return View();
        }
	}
}