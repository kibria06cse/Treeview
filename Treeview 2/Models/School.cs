using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class School 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Village")]
        public int VillageId { get; set; }

        public virtual Village Village { get; set; }
    }
}