using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Question
    {
        [Key]
        public int qId { get; set; }
        //public int testId { get; set; }
        public string qText { get; set; }
        public string op1 { get; set; }
        public string op2 { get; set; }
        public string op3 { get; set; }
        public string op4 { get; set; }
        public string ans { get; set; }

        public virtual Test Test { get; set; }
    }
}