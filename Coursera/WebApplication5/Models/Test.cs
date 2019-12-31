using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Test
    {
        [Key]
        public int testId { get; set; }
        public string testName { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual Course Course { get; set; }

        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}