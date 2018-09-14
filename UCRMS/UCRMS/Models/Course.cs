using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace UCRMS.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30,MinimumLength=5, ErrorMessage = "Code must be atleast 5 characters")]
        public string CourseCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.5, 5.00,ErrorMessage = "Credit must be between 0.5 to 5.00")]
        public double Credit { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int DeptId { get; set; }
        [Required]
        public int SemesterId { get; set; }
    }
}