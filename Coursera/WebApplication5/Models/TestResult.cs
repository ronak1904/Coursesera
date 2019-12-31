using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class TestResult
    {
        [Key]
        public int resId { get; set; }
        public int marks{get;set;}
        public int total { get; set; }
        public virtual Test Test { get; set; }
        public virtual Student Student { get; set; }



    }
}