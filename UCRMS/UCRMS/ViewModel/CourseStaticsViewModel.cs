using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCRMS.ViewModel
{
    public class CourseStaticsViewModel
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Semester { get; set; }
        public string AssignedTeacher { get; set; }
    }
}