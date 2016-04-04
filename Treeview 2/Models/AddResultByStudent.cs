using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class AddResultByStudent
    {
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Result> Results { get; set; }
    }
}