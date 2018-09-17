using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UCRMS.Models
{
    public class AllocateClassroom
    {
        public int Id { get; set; }
        [Required]
        public int DeptId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int RoomNoId { get; set; }
        [Required]
        public int DayId { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }

        public string Status { get; set; }

    }
}