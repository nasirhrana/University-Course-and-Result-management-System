using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCRMS.Gateway;
using UCRMS.Models;

namespace UCRMS.Manager
{
    public class DepartmentManager
    {
        private DepartmentGateway aGateway=new DepartmentGateway();
        public int SavaDepartment(Department department)
        {
            return aGateway.SavaDepartment(department);
        }
        public int SavaCourse(Course course)
        {
            return aGateway.SavaCourse(course);
        }

        public List<Department> GetAllDepartment()
        {
            return aGateway.GetAllDepartment();
        }
        public List<Semester> GetAllSemester()
        {
            return aGateway.GetAllSemester();
        }

        public bool CheckeCode(string code)
        {
            return aGateway.CheckeCode(code);
        }
        public bool CheckeName(string name)
        {
            return aGateway.CheckeName(name);
        }
        public bool CheckeCourseCode(string code)
        {
            return aGateway.CheckeCourseCode(code);
        }
        public bool CheckCourseeName(string name)
        {
            return aGateway.CheckCourseeName(name);
        }
    }
}