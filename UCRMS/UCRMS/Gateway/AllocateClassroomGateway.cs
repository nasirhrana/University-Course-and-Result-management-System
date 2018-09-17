using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UCRMS.Models;

namespace UCRMS.Gateway
{
    public class AllocateClassroomGateway
    {
        private SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSRDB"].ConnectionString);
        public List<ClassRoomNo> GetAllClassroom()
        {
            string query = @"SELECT *
      
                             FROM [dbo].[Room]";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<ClassRoomNo> aList = new List<ClassRoomNo>();
            while (reader.Read())
            {
                ClassRoomNo department = new ClassRoomNo();
                department.Id = (int)reader["Id"];
                department.RoomNo = reader["RoomNo"].ToString();
                aList.Add(department);
            }
            reader.Close();
            con.Close();
            return aList;
        }
        public List<Day> GetAllDay()
        {
            string query = @"SELECT *
      
                             FROM [dbo].[Day]";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Day> aList = new List<Day>();
            while (reader.Read())
            {
                Day department = new Day();
                department.Id = (int)reader["Id"];
                department.Name = reader["Name"].ToString();
                aList.Add(department);
            }
            reader.Close();
            con.Close();
            return aList;
        }

        public int Allocate(AllocateClassroom allocate)
        {
            string query = @"INSERT INTO [dbo].[AllocateClassroom]
           ([DeptId]
           ,[CourseId]
           ,[RoomNoId]
           ,[DayId]
           ,[StartTime]
           ,[EndTime]
           ,[Status])
     VALUES('"+allocate.DeptId+"','"+allocate.CourseId+"','"+allocate.RoomNoId+"','"+allocate.DayId+"','"+allocate.From+"'," +
                           "'"+allocate.To+"','"+allocate.Status+"')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            con.Close();
            return rowAffect;
        }
        public bool IsTimeExist(AllocateClassroom allocate)
        {
            string query = @"SELECT * FROM [dbo].[AllocateClassroom] WHERE (StartTime <= @EndTime AND EndTime>=@StartTime AND DayId=@DayId)";
            SqlCommand cmd = new SqlCommand(query, con);
            bool isExist = false;
            con.Open();
            cmd.Parameters.Clear();
            cmd.Parameters.Add("StartTime", SqlDbType.Char);
            cmd.Parameters["StartTime"].Value = allocate.From;
            cmd.Parameters.Add("EndTime", SqlDbType.Char);
            cmd.Parameters["EndTime"].Value = allocate.To;
            cmd.Parameters.Add("DayId", SqlDbType.Int);
            cmd.Parameters["DayId"].Value = allocate.DayId;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            isExist = reader.HasRows;
            reader.Close();
            con.Close();
            return isExist;
        }
    }
}