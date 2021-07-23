using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning.Models
{
    [MetadataType(typeof(StudentMetaData))]

    public partial class Student    
    {
        public string FullName {  get{ return FName + " " + LName; } }
    }
    [MetadataType(typeof(CourseMetaData))]
    public partial class Course
    {
        
    }
    [MetadataType(typeof(EnrollmenrMetaData))]

    public partial class Enrollment
    {

    }
}