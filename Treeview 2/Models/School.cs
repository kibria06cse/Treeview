using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class School 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VillageId { get; set; }

        public virtual Village Village { get; set; }
    }
}