using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCRMS.Manager;
using UCRMS.Models;
using UCRMS.ViewModel;

namespace UCRMS.Controllers
{
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher/
        private TeacherManager aManager=new TeacherManager();
        private DepartmentManager aDepartmentManager=new DepartmentManager();
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Designation = aManager.GetAllDesignation();
            ViewBag.Department = aDepartmentManager.GetAllDepartment();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            teacher.RemainingCredit = teacher.CreditToTaken;

            ViewBag.Designation = aManager.GetAllDesignation();
            ViewBag.Department = aDepartmentManager.GetAllDepartment();
            if (aManager.IsEmailExist(teacher.Email))
            {
                ViewBag.msg = "Email is exist";
            }
            else
            {
                int msg = aManager.SavaTeacher(teacher);
                if (msg > 0)
                {
                    ViewBag.message = "Save Successfully";
                }
                else
                {
                    ViewBag.message = "failed to Save ";
                }

            }
            return View();
        }
        [HttpGet]
        public ActionResult CourseAssign()
        {
            ViewBag.Department = aDepartmentManager.GetAllDepartment();
            return View();
        }
        [HttpPost]
        public ActionResult CourseAssign(CourseAssign courseAssign)
        {
            ViewBag.Department = aDepartmentManager.GetAllDepartment();
            courseAssign.Status = "Assigned";
            if (aManager.IsCourseCodeExist(courseAssign.CourseCodeId))
            {
                ViewBag.message = "Course already assigner";
            }
            else
            {
                int msg = aManager.CourseAssign(courseAssign);
                if (msg > 0)
                {
                    ViewBag.message = "Save Successfully";
                }
                else
                {
                    ViewBag.message = "failed to Save ";
                }
            }
            return View();
        }

        public JsonResult GetByDepartmentId(int deptId)
        {
            List<TeacherCourseCodeViewModel> aList = aManager.GetAllTeacherCourseCode(deptId);
            return Json(aList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByTeacherId(int tchrId)
        {
            TeacherViewModel aList = aManager.GetTeacherCredit(tchrId);
            return Json(aList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByCourseId(string courseId)
        {
            CourseNameCreditViewModel aList = aManager.GetCourseNameCredit(courseId);
            return Json(aList, JsonRequestBehavior.AllowGet);
        }

	}
}