using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data_Transfer_Layer
{
    public class SocialMediaDataTransfer
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Please Enter your Name")]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Please Enter Link")]
        public string Link { get; set; }
        [Display(Name ="Image")]
        public HttpPostedFileBase SocialImage { get; set; }

    }
}
