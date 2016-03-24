using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class Village 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name="Union")]
        public int UnionId { get; set; }

        public virtual Union Union { get; set; }
    }
}