using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_learning.Models
{
    public class StudentStats
    {
        public int Studentid { get; set; }
        public string Name { get; set; }
        public int NumberOfCourses { get; set; }
        public decimal AverageGrade { get; set; }
        public string Image { get; set; }
    }
}