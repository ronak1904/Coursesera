using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Video
    {
        [Key]
        public int videoId { get; set; }

        [StringLength(int.MaxValue)]
        public string path { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string videoName { get; set; }

        public string videoType { get; set; }
        //public int seriesId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }

    }
}