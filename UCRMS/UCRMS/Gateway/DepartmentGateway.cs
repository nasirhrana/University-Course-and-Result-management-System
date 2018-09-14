using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UCRMS.Models;

namespace UCRMS.Gateway
{
    public class DepartmentGateway
    {
        private SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSRDB"].ConnectionString);
        public int SavaDepartment(Department department)
        {
            string query = @"INSERT INTO [dbo].[Department]
           ([DeptCode]
           ,[DeptName])
     VALUES('"+department.Code+"','"+department.Name+"')";
            SqlCommand cmd=new SqlCommand(query,con);
            con.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            con.Close();
            return rowAffect;
        }
        public int SavaCourse(Course course)
        {
            string query = @"INSERT INTO [dbo].[Course]
           ([CourseCode]
           ,[CourseName]
           ,[Credit]
           ,[Description]
           ,[DeptId]
           ,[SemesterId])
     VALUES('"+course.CourseCode+"','"+course.Name+"','"+course.Credit+"','"+course.Description+"','"+course.DeptId+"','"+course.SemesterId+"')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            con.Close();
            return rowAffect;
        }

        public List<Department> GetAllDepartment()
        {
            string query = @"SELECT *
      
                             FROM [dbo].[Department]";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Department> aList=new List<Department>();
            while (reader.Read())
            {
                Department department=new Department();
                department.Id = (int) reader["Id"];
                department.Code = reader["DeptCode"].ToString();
                department.Name = reader["DeptName"].ToString();
                aList.Add(department);
            }
            reader.Close();
            con.Close();
            return aList;
        }
        public List<Semester> GetAllSemester()
        {
            string query = @"SELECT *
      
                             FROM Semester";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Semester> aList = new List<Semester>();
            while (reader.Read())
            {
                Semester semester = new Semester();
                semester.Id = (int)reader["Id"];
                semester.Name = reader["Semester"].ToString();
                aList.Add(semester);
            }
            reader.Close();
            con.Close();
            return aList;
        }

        public bool CheckeCode(string code)
        {
            string query = @"SELECT * FROM [dbo].[Department] WHERE (DeptCode=@DeptCode)";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            bool isExist = false;
            cmd.Parameters.AddWithValue("@DeptCode", code);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            isExist = reader.HasRows;
            reader.Close();
            con.Close();
            return isExist;
        }
        public bool CheckeName(string name)
        {
            string query = @"SELECT * FROM [dbo].[Department] WHERE (DeptName=@DeptName)";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            bool isExist = false;
            cmd.Parameters.AddWithValue("@DeptName", name);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            isExist = reader.HasRows;
            reader.Close();
            con.Close();
            return isExist;
        }
        public bool CheckeCourseCode(string code)
        {
            string query = @"SELECT * FROM [dbo].[Course] WHERE (CourseCode=@CourseCode)";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            bool isExist = false;
            cmd.Parameters.AddWithValue("@CourseCode", code);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            isExist = reader.HasRows;
            reader.Close();
            con.Close();
            return isExist;
        }
        public bool CheckCourseeName(string name)
        {
            string query = @"SELECT * FROM [dbo].[Course] WHERE (CourseName=@CourseName)";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            bool isExist = false;
            cmd.Parameters.AddWithValue("@CourseName", name);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            isExist = reader.HasRows;
            reader.Close();
            con.Close();
            return isExist;
        }
    }
}