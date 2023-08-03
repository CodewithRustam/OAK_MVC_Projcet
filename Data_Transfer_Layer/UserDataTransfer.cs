using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data_Transfer_Layer
{
    public class UserDataTransfer
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        public string Email { get; set; }
        public string Imagepath { get; set; }
        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }
        public bool isAdmin { get; set; }
        [Display(Name ="User Image")]
        public HttpPostedFileBase UserImage { get; set; }
    }
}
