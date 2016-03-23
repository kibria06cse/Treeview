using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class Union 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ThanaId { get; set; }

        public virtual Thana Thana { get; set; }
    }
}