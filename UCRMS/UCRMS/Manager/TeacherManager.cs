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
        public bool IsCourseCodeExist(string code)
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
        public TeacherViewModel GetTeacherCredit(int tchrId)
        {
            return aGateway.GetTeacherCredit(tchrId);
        }
        public CourseNameCreditViewModel GetCourseNameCredit(string courseId)
        {
            return aGateway.GetCourseNameCredit(courseId);
        }
    }
}