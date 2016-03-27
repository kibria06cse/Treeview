using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class Student 
    {
        public int Id { get; set; }
        [Display(Name="Student")]
        public string Name { get; set; }
        [Display(Name = "School Class")]
        public int SchoolClassId { get; set; }

        public virtual SchoolClass SchoolClass { get; set; }
    }
}