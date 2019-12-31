using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Teacher
    {
        [Key]
        public int tId { get; set; }

        [Required]
        public string Fullname { get; set; }

        [DataType(DataType.EmailAddress,ErrorMessage ="Please enter valid email address?")]
        [Required]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DataType(DataType.PhoneNumber,ErrorMessage ="Phone number is invalid")]
        public int mobileno { get; set; }
        public virtual ICollection<FileDetails> FileDetails { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}