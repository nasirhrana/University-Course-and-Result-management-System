using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UCRMS.Models;

namespace UCRMS.Gateway
{
    public class StudentGateway
    {
        private SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSRDB"].ConnectionString);

        public bool IsStudentEmailExist(string email)
        {
            string query = @"SELECT * FROM [dbo].[Student] WHERE (Email=@Email)";
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

        public int SaveStudent(Student student)
        {
            string query = @"INSERT INTO [dbo].[Student]
           ([Name]
           ,[Email]
           ,[ContactNo]
           ,[Date]
           ,[Address]
           ,[DeptId])
            VALUES ('"+student.Name+"','"+student.Email+"','"+student.ContactNo+"','"+student.Date+"','"+student.Address+"','"+student.DeptId+"')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            con.Close();
            return rowAffect;
        }

    }
}