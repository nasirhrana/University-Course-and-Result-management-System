using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCRMS.Gateway;
using UCRMS.Models;

namespace UCRMS.Manager
{
    public class StudentManager
    {
        private StudentGateway aGateway=new StudentGateway();

        public bool IsStudentEmailExist(string email)
        {
            return aGateway.IsStudentEmailExist(email);
        }

        public int SaveStudent(Student student)
        {
            return aGateway.SaveStudent(student);
        }
    }
}