using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UCRMS.Models;
using UCRMS.ViewModel;

namespace UCRMS.Gateway
{
    public class TeacherGateway
    {
        private SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSRDB"].ConnectionString);
        public List<Designation> GetAllDesignation()
        {
            string query = @"SELECT *
      
                             FROM Designation";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Designation> aList = new List<Designation>();
            while (reader.Read())
            {
                Designation designation = new Designation();
                designation.Id = (int)reader["Id"];
                designation.DesignationName = reader["Name"].ToString();
                aList.Add(designation);
            }
            reader.Close();
            con.Close();
            return aList;
        }

        public bool IsEmailExist(string email)
        {
            string query = @"SELECT * FROM Teacher WHERE (Email=@Email)";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            bool isExist = false;
            cmd.Parameters.AddWithValue("@Email", email);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            isExist = reader.HasRows;
            reader.Close();
            con.Close();
            return isExist;
        }
        public bool IsCourseCodeExist(int code)
        {
            string query = @"SELECT * FROM CourseAssign WHERE (CourseId=@CourseCodeId)";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            bool isExist = false;
            cmd.Parameters.AddWithValue("@CourseCodeId", code);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            isExist = reader.HasRows;
            reader.Close();
            con.Close();
            return isExist;
        }
        public int SavaTeacher(Teacher teacher)
        {
            string query = @"INSERT INTO [dbo].[Teacher]
           ([Name]
           ,[Address]
           ,[Email]
           ,[ContactNo]
           ,[DesignationId]
           ,[DepartmentId]
           ,[CreditTaken])
            VALUES('" + teacher.Name + "','" + teacher.Address + "','"+teacher.Email+"','"+teacher.ContactNo+"','"+teacher.DesignationId+"'," +
                           "'" + teacher.DeptId + "','" + teacher.CreditToTaken + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            con.Close();
            return rowAffect;
        }

        public int CourseAssign(CourseAssign courseAssign)
        {
            string query = @"INSERT INTO [dbo].[CourseAssign]
           ([DeptId]
           ,[TeacherId]
           ,[RemainingCredit]
           ,[CourseId]
           ,[Status])
     VALUES
     ('" + courseAssign.DeptId + "','" + courseAssign.TeacherId + "','" + courseAssign.RemainingCredit + "'," +
                       "'"+courseAssign.CourseCodeId+"','"+courseAssign.Status+"')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            con.Close();
            return rowAffect;
        }

        public List<TeacherCourseCodeViewModel> GetAllTeacherCourseCode(int id)
        {
            string query = @"Select t.Id,t.Name,c.CourseCode, c.Id as CourseId
                            from [dbo].[Teacher] t
                            inner join [dbo].[Department] d on t.DepartmentId=d.Id
                            inner join Course c on d.Id=c.DeptId
                             where d.Id='" +id+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<TeacherCourseCodeViewModel> aList = new List<TeacherCourseCodeViewModel>();
            while (reader.Read())
            {
                TeacherCourseCodeViewModel department = new TeacherCourseCodeViewModel();
                department.TeacherId = (int)reader["Id"];
                department.Id = (int)reader["CourseId"];
                department.TeacherName = reader["Name"].ToString();
                department.CourseCode = reader["CourseCode"].ToString();
                aList.Add(department);
            }
            reader.Close();
            con.Close();
            return aList;

        }

        public string GetTeacherCredit(int tchrId)
        {
            string query = @"select CreditTaken 
                             from [dbo].[Teacher]
                              where Id='"+tchrId+"'";
            SqlCommand cmd=new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            TeacherViewModel aModel=new TeacherViewModel();
            if (reader.Read())
            {
                aModel.CreditToTaken = reader["CreditTaken"].ToString();
            }
            reader.Close();
            con.Close();
            return aModel.CreditToTaken;
        }
        public bool IsExistTeacher(int tchrId)
        {
            string query = @"SELECT * FROM [dbo].[CourseAssign] WHERE (TeacherId=@TeacherId)";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            bool isExist = false;
            cmd.Parameters.AddWithValue("@TeacherId", tchrId);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            isExist = reader.HasRows;
            reader.Close();
            con.Close();
            return isExist;
        }
        public string GetRemainingCredit(int tchrId)
        {
            string query = @"select  MIN(RemainingCredit) as RemainingCredit
                             from [dbo].[CourseAssign]
                              where TeacherId='" + tchrId + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            TeacherViewModel aModel = new TeacherViewModel();

            if (reader.Read())
            {
                aModel.CourseCredit = reader["RemainingCredit"].ToString();
            }
            reader.Close();
            con.Close();
            return aModel.CourseCredit;
        }
        public CourseNameCreditViewModel GetCourseNameCredit(string courseId)
        {
            string query = @"select CourseName, Credit 
                             from Course
                              where Id='" + courseId + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            CourseNameCreditViewModel aModel = new CourseNameCreditViewModel();
            if (reader.Read())
            {
                aModel.Name = reader["CourseName"].ToString();
                aModel.Credit = reader["Credit"].ToString();
            }
            reader.Close();
            con.Close();
            return aModel;
        }
        public List<CourseStaticsViewModel> GetCourseStatics(int departmentId)
        {
            string query = @"Select d.Name,c.CourseCode,c.CourseName,s.Semester
                            from [dbo].[CourseAssign] t
                            inner join [dbo].[Teacher] d on t.TeacherId=d.Id
                            inner join Course c on t.CourseId=c.Id
                            inner join [dbo].[Semester] s on s.Id=c.SemesterId
                             where t.DeptId='" + departmentId + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<CourseStaticsViewModel> aList = new List<CourseStaticsViewModel>();
            while (reader.Read())
            {
                CourseStaticsViewModel department = new CourseStaticsViewModel();
                department.AssignedTeacher = reader["Name"].ToString();
                department.CourseCode = reader["CourseCode"].ToString();
                department.CourseName = reader["CourseName"].ToString();
                department.Semester = reader["Semester"].ToString();
                aList.Add(department);
            }
            reader.Close();
            con.Close();
            return aList;

        }
    }
}