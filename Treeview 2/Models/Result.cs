using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Treeview_2.Models
{
    public class Result
    {
        public int Id { get; set; }
        [Display(Name = "Grade")]
        public string Grade { get; set; }
        [Display(Name = "Achieved Mark(Percentage)")]
        public double MarkPercentage { get; set; }
        [Display(Name="Student")]
        [Required]
        public int StudentId { get; set; }
        [Required]
        [Display(Name="Subject")]
        public int SubjectId { get; set; }
        [Display(Name = "Class")]
        [Required]
        public int ClassId { get; set; }
        [Display(Name = "School")]
        [Required]
        public int SchoolId { get; set; }


        public virtual Subject Subject { get; set; }
        public virtual Student Student{ get; set; }
        public virtual School School { get; set; }
        public virtual SchoolClass Class { get; set; }
    }
}