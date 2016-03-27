using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class District 
    {
        public int Id { get; set; }
        [Display(Name = "District")]
        public string Name { get; set; }
        [Display(Name = "Division")]
        public int DivisionId { get; set; }


        public virtual Division Division { get; set; }
    }
}