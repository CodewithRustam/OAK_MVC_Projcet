using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data_Transfer_Layer
{
    public class AdsDataTranfer
    {
        public int AdsID { get; set; }
        [Required(ErrorMessage ="Please Enter Name")]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Please Enter Link")]
        public string Link { get; set; }
        [Required(ErrorMessage = "Please Enter ImageSize")]
        public string ImageSize { get; set; }
        [Display(Name ="Ads Image")]
        public HttpPostedFileBase AdsImage { get; set; }
    }
}
