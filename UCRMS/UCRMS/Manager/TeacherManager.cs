using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCRMS.Gateway;
using UCRMS.Models;
using UCRMS.ViewModel;

namespace UCRMS.Manager
{
    public class TeacherManager
    {
        private TeacherGateway aGateway=new TeacherGateway();
        public List<Designation> GetAllDesignation()
        {
            return aGateway.GetAllDesignation();
        }

        public bool IsEmailExist(string email)
        {
            return aGateway.IsEmailExist(email);
        }
        public bool IsCourseCodeExist(int code)
        {
            return aGateway.IsCourseCodeExist(code);
        }

        public int SavaTeacher(Teacher teacher)
        {
            return aGateway.SavaTeacher(teacher);
        }
        public int CourseAssign(CourseAssign courseAssign)
        {
            return aGateway.CourseAssign(courseAssign);
        }

        public List<TeacherCourseCodeViewModel> GetAllTeacherCourseCode(int id)
        {
            return aGateway.GetAllTeacherCourseCode(id);
        }
        public string GetTeacherCredit(int tchrId)
        {
            return aGateway.GetTeacherCredit(tchrId);
        }
        public bool IsExistTeacher(int tchrId)
        {
            return aGateway.IsExistTeacher(tchrId);
        }
        public string GetRemainingCredit(int tchrId)
        {
            return aGateway.GetRemainingCredit(tchrId);
        }
        public CourseNameCreditViewModel GetCourseNameCredit(string courseId)
        {
            return aGateway.GetCourseNameCredit(courseId);
        }

        public List<CourseStaticsViewModel> GetCourseStatics(int departmentId)
        {
            return aGateway.GetCourseStatics(departmentId);
        }
    }
}