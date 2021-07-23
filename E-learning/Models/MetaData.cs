using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning.Models
{
    public class CourseMetaData
    {
        [Required(ErrorMessage = "Please Enter A title")]
        public string Tittel { get; set; }
        [Required(ErrorMessage = "Please Enter A Credits")]
        [Range(2, 6, ErrorMessage = "Please Enter value 2-6")]
        public int Credits { get; set; }
        [Required(ErrorMessage = "Please Enter A Credits")]
        [Range(200, 600, ErrorMessage = "Please Enter value 200-600")]
        public Nullable<decimal> Price { get; set; }
        //[Required(ErrorMessage = "Please Enter A level")]
        //[EnumDataType(typeof(level), ErrorMessage = "Please Enter A level")]
        //public Nullable<level> level { get; set; }
        //[Required(ErrorMessage = "Please Enter A level")]
        //public virtual CourseLevel CourseLevel { get; set; }

    }
    public class StudentMetaData
    {
        [Display(Name ="FristName")]
        public string FName { get; set; }
        [Display(Name = "LastName")]
        public string LName { get; set; }
       [RecenrEnrollment]
        public Nullable<System.DateTime> EnrollmentData { get; set; }
    }
    public class EnrollmenrMetaData
    {
        [Required(ErrorMessage = "Please Enter A Grade")]
        [Range(0, 100, ErrorMessage = "Please Enter value 0-100")]
        public Nullable<decimal> Grade { get; set; }
     
    }
}