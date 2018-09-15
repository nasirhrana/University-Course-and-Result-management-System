using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UCRMS.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("([0-9]+)")]
        public string ContactNo { get; set; }
        [Required]
        public int DesignationId { get; set; }
        [Required]
        public int DeptId { get; set; }
        [Required]
        [Range(0,25,ErrorMessage = "Credit can not be negative")]
        public double CreditToTaken { get; set; }

    }
}