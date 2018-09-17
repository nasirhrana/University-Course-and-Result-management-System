using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCRMS.Gateway;
using UCRMS.Models;

namespace UCRMS.Manager
{
    public class AllocateClassroomManager
    {
        private AllocateClassroomGateway allocateClassroomGateway=new AllocateClassroomGateway();

        public List<ClassRoomNo> GetAllClassroom()
        {
            return allocateClassroomGateway.GetAllClassroom();
        }
         public List<Day> GetAllDay()
         {
             return allocateClassroomGateway.GetAllDay();
         }

        public int Allocate(AllocateClassroom allocate)
        {
            return allocateClassroomGateway.Allocate(allocate);
        }

        public bool IsTimeExist(AllocateClassroom allocate)
        {
            return allocateClassroomGateway.IsTimeExist(allocate);
        }
            
    }
}