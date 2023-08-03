using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Layer
{
    public class VideoDataTransfer
    {
        public int VideoID { get; set; }
        public string VideoPath { get; set; }
        [Required(ErrorMessage ="Please enter original video path")]
        public string OriginalVideoPath { get; set; }
        [Required(ErrorMessage ="Please enter Title")]
        public string Title { get; set; }
        public DateTime? AddDate { get; set; }
    }
}
