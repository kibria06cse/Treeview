using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class Treeview
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int? ParentId { get; set; }

        public int HierarchyTypeId { get; set; }
        public int KeyOfThatHierarchy { get; set; }
    
    }
}