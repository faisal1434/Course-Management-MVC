using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseManagement_MVC.ViewModels
{
    public class CourseViewModel
    {
        
        public int CourseId { get; set; }
        [Required, StringLength(50), Display(Name = "Course Name")]
        public string CourseName { get; set; }
        [Display(Name = "Course Fee"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal CourseFee { get; set; }
      
        public int Duration { get; set; }
        [Display(Name = "Start Date"),
         DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Display(Name ="Student Count")]
        public int StudentCount { get; set; }
    }
}