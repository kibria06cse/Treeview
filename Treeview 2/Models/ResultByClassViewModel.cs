using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class ResultByClassViewModel
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SchoolId { get; set; }
        public List<Result> ResultWithStudent { get; set; }
    }
}