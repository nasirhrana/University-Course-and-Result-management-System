using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

        public ActionResult CourseStatics()
        {
            ViewBag.Department = aDepartmentManager.GetAllDepartment();
            return View();
        }
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
            var credit = courseAssign.RemainingCredit - courseAssign.CourseCredit;
            courseAssign.RemainingCredit = credit;
            courseAssign.Status = "Assigned";
            if (credit<0)
            {
                ViewBag.message = "Credit can not  be  more than" + " "+courseAssign.CreditToTaken;
            }
            else
            {
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
            TeacherViewModel aList = new TeacherViewModel();
            if (aManager.IsExistTeacher(tchrId))
            {
                aList.RemainingCredit = aManager.GetRemainingCredit(tchrId);
                aList.CreditToTaken = aManager.GetTeacherCredit(tchrId);
            }
            else
            {
                aList.CreditToTaken = aManager.GetTeacherCredit(tchrId);
                aList.RemainingCredit = aList.CreditToTaken;
            }
            
            return Json(aList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByCourseId(string courseId)
        {
            CourseNameCreditViewModel aList = aManager.GetCourseNameCredit(courseId);
            return Json(aList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseByDepartmentId(int departmentId)
        {
            List<CourseStaticsViewModel> aList = aManager.GetCourseStatics(departmentId);
            return Json(aList, JsonRequestBehavior.AllowGet);
        }

	}
}