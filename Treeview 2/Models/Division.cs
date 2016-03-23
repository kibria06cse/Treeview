using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class Division 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BaseId { get; set; }

        public virtual Base Base { get; set; }
    }
}