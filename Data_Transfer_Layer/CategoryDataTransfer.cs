using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Layer
{
    public class CategoryDataTransfer
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage ="Please enter category name")]
        public string CategoryName { get; set; }
    }
}
