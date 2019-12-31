using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Student
    {

        [Key]
        public int studentId { get; set; }
		[Required]
		[DataType(DataType.Password)]

		public string password { get; set; }
        public string studentName { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="Enter Valid Email.")]
        public string emailId { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}