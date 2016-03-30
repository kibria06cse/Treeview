using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class AddResultBySubject
    {
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}