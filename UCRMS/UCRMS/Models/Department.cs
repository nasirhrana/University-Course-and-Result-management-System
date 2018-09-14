using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Antlr.Runtime.Tree;

namespace UCRMS.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "field must be between 2 to 7 characters characters")]
        public string Name { get; set; }
    }
}