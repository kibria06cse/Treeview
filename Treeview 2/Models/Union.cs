using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class Union 
    {
        public int Id { get; set; }
        [Display(Name = "Union/Ward")]
        public string Name { get; set; }
        [Display(Name = "Thana/Upazilla")]
        public int ThanaId { get; set; }

        public virtual Thana Thana { get; set; }
    }
}