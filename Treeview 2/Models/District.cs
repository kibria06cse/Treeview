using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class District 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DivisionId { get; set; }


        public virtual Division Division { get; set; }
    }
}