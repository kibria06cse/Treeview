using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class Student 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SchoolId { get; set; }

        public virtual School School { get; set; }
    }
}