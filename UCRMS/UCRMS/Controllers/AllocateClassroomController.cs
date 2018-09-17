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
    public class AllocateClassroomController : Controller
    {
        //
        // GET: /AllocateClassroom/
        private DepartmentManager aDepartmentManager=new DepartmentManager();
        private TeacherManager aManager=new TeacherManager();
        private AllocateClassroomManager allocateClassroomManager=new AllocateClassroomManager();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AllocationClassroom()
        {
            ViewBag.Department = aDepartmentManager.GetAllDepartment();
            ViewBag.Room = allocateClassroomManager.GetAllClassroom();
            ViewBag.Day = allocateClassroomManager.GetAllDay();
            return View();
        }
        [HttpPost]
        public ActionResult AllocationClassroom(AllocateClassroom allocate)
        {
            ViewBag.Department = aDepartmentManager.GetAllDepartment();
            ViewBag.Room = allocateClassroomManager.GetAllClassroom();
            ViewBag.Day = allocateClassroomManager.GetAllDay();
            allocate.Status = "Allocated";
            var message = "";
            if (allocateClassroomManager.IsTimeExist(allocate))
            {
                message = "Time Overlaping problem";
            }
            else
            {
                int message1 = allocateClassroomManager.Allocate(allocate);
                if (message1>0)
                {
                    message = "allocated Successfully";
                }
                else
                {
                    message = "failed to allocate";
                }
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByDepartmentId(int deptId)
        {
            List<TeacherCourseCodeViewModel> aList = aManager.GetAllTeacherCourseCode(deptId);
            return Json(aList, JsonRequestBehavior.AllowGet);
        }
	}
}