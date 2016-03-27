using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class Thana 
    {
        public int Id { get; set; }
        [Display(Name = "Thana/Upazila")]
        public string Name { get; set; }
        [Display(Name = "District")]
        public int DistrictId { get; set; }

        public virtual District District { get; set; }
    }
}