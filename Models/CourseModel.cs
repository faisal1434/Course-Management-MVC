using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CourseManagement_MVC.Models
{
    public class Course
    {
        public Course() { this.Students = new List<Student>(); }
        public int CourseId { get; set; }
        [Required, StringLength(50), Display(Name = "Course Name")]
        public string CourseName { get; set; }
        [Required, Column(TypeName ="money"), Display(Name ="Course Fee"),DisplayFormat(DataFormatString ="{0:0.00}", ApplyFormatInEditMode =true)]
        public decimal CourseFee { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Start Date"),
         DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        //Navigation
        public virtual ICollection<Student> Students { get; set; }
    }
    public class Student
    {
        public int StudentId { get; set; }
        [Required, StringLength(50), Display(Name = "Student Name")]
        public string StudentName { get; set; }
        [Required, StringLength(20), Display(Name = "Phone Number")]
        public string Phone { get; set; }
        [Required, EmailAddress, StringLength(50), Display(Name = "Email Address")]
        public string Email { get; set; }
        //FK
        [Required, ForeignKey("Course")]
        public int CourseId { get; set; }
        //Navigation
        public virtual Course Course { get; set; }
    }
    public class StudentDbContext : DbContext
    {
        public StudentDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<StudentDbContext>
    {
        protected override void Seed(StudentDbContext context)
        {
            base.Seed(context);
            Course c1 = new Course { CourseName = "C# Crash couse", Duration = 30, StartDate = DateTime.Now.AddDays(30) };
            c1.Students.Add(new Student { StudentName = "Afzal Hossain", Phone = "987006700", Email = "afzal@mail.net" });
            c1.Students.Add(new Student { StudentName = "Rakib Hossain", Phone = "987006700", Email = "rakib@mail.net" });
            context.Courses.Add(c1);
            context.SaveChanges();
        }
    }
}