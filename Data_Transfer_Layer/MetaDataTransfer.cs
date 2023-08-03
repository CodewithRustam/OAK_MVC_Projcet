using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Layer
{
    public class MetaDataTransfer
    {
        public int MetaID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage ="Please fill Content Area")]
        public string MetaContent { get; set; }
    }
}
