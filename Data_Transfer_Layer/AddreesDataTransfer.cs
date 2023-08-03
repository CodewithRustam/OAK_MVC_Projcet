using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Layer
{
    public class AddreesDataTransfer
    {
        public int AddressID { get; set; }
        [Required(ErrorMessage ="Please Enter Address Content")]
        public string AddressContent { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Phone")]
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        [Required(ErrorMessage = "Please Enter Mappath")]
        public string LargeMapPath { get; set; }
        [Required(ErrorMessage = "Please Enter Mappath")]
        public string SmallMapPath { get; set; }

    }
}
