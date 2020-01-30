using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClinicApplication.Models
{
    public class CustomValidation  
    {  
        public sealed class CheckCity : ValidationAttribute  
        {  
            public String AllowCity { get; set; }  
            protected override ValidationResult IsValid(object city, ValidationContext validationContext)  
            {  
                string[] myarr = AllowCity.Split(',');  
                if(myarr.Contains(city))  
                {  
                    return ValidationResult.Success;  
                }
                return new ValidationResult("Please choose a valid city.");
            }  
        }  
        public sealed class ContaintCapitalLetter : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)  
            {
                if(Regex.IsMatch((string) value, "[A-Z]+"))  
                {  
                    return ValidationResult.Success;  
                }
                return new ValidationResult("Needed capital letter.");
            }    
        }
    }
}