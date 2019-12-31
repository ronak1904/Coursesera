using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class FileDetails
    {
        [Key]
        public int fileId { get; set; }

        [StringLength(int.MaxValue)]
        public string path { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string fileName { get; set; }

        public string fileType { get; set; }
        //public int seriesId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }

    }
}