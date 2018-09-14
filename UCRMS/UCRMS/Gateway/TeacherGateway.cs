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
        public bool IsCourseCodeExist(string code)
        {
            string query = @"SELECT * FROM CourseAssign WHERE (CourseCodeId=@CourseCodeId)";
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
           ,[CreditTaken]
           ,[RemainingCredit])
            VALUES('" + teacher.Name + "','" + teacher.Address + "','"+teacher.Email+"','"+teacher.ContactNo+"','"+teacher.DesignationId+"'," +
                           "'"+teacher.DeptId+"','"+teacher.CreditToTaken+"','"+teacher.RemainingCredit+"')";
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
           ,[CreditAlreadyTaken]
           ,[ReaminingCredit]
           ,[CourseCodeId]
           ,[CourseName]
           ,[CourseCredit]
           ,[Status])
     VALUES('"+courseAssign.DeptId+"','"+courseAssign.TeacherId+"','"+courseAssign.CreditToTaken+"','"+courseAssign.RemainingCredit+"'," +
                       "'"+courseAssign.CourseCodeId+"','"+courseAssign.CourseName+"','"+courseAssign.CourseCredit+"','"+courseAssign.Status+"')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            con.Close();
            return rowAffect;
        }

        public List<TeacherCourseCodeViewModel> GetAllTeacherCourseCode(int id)
        {
            string query = @"Select t.Id,t.Name,c.CourseCode
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
                department.TeacherName = reader["Name"].ToString();
                department.CourseCode = reader["CourseCode"].ToString();
                aList.Add(department);
            }
            reader.Close();
            con.Close();
            return aList;

        }

        public TeacherViewModel GetTeacherCredit(int tchrId)
        {
            string query = @"select CreditTaken, RemainingCredit 
                             from [dbo].[Teacher]
                              where Id='"+tchrId+"'";
            SqlCommand cmd=new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            TeacherViewModel aModel=new TeacherViewModel();
            if (reader.Read())
            {
                aModel.CreditToTaken =  reader["CreditTaken"].ToString();
                aModel.RemainingCredit = reader["RemainingCredit"].ToString();
            }
            reader.Close();
            con.Close();
            return aModel;
        }
        public CourseNameCreditViewModel GetCourseNameCredit(string courseId)
        {
            string query = @"select CourseName, Credit 
                             from Course
                              where CourseCode='" + courseId + "'";
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
    }
}