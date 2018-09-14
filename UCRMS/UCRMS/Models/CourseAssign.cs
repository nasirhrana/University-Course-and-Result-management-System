using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UCRMS.Models
{
    public class CourseAssign
    {
        public int Id { get; set; }
        [Required]
        public int DeptId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public double CreditToTaken { get; set; }
        [Required]
        public double RemainingCredit { get; set; }
        [Required]
        public string CourseCodeId { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public double CourseCredit { get; set; }
        public string Status { get; set; }
    }
}