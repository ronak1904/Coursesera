using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Course
    {
        //scaler properties
        [Key]
        public int courseId { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string Category { get; set; }
        
        //public int seriesId { get; set; }
        
        //public int materialId { get; set; }
        //mapping properties
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<FileDetails> FileDetails { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual Teacher Teacher { get; set; }


    }
}