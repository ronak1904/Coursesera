using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Login
    {
        [Key]
        public int lid { get; set; }
        public string uType { get; set; }

        public string uName { get; set; }

        [DataType(DataType.Password)]
        public string pwd { get; set; }

    }
}