using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class Division 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Base")]
        public int BaseId { get; set; }

        public virtual Base Base { get; set; }
    }
}