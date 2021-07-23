using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning.Models
{
    public class RecenrEnrollment:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           var std= (Student) validationContext.ObjectInstance;
            if (std.EnrollmentData == null)
            {
                return new ValidationResult("Data Require ") ;
            }
            else
            {
                var age = DateTime.Today.Year - std.EnrollmentData.Value.Year;
                if(age>3)
                    return new ValidationResult("only recrnt Enrollments are Allowed ");

            }

            return ValidationResult.Success;
        }
    }
}