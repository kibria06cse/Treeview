﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }
        [Display(Name = "School Class")]
        public string Name { get; set; }
        [Display(Name="School")]
        public int SchoolId { get; set; }

        public virtual School School { get; set; }

    }
}