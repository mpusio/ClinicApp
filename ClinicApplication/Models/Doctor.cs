using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClinicApplication.Models;
using static ClinicApplication.Models.CustomValidation;

namespace ClinicApp.Models
{
    public class Doctor
    {
        [Key] public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter first name.")]
        [MinLength(1)]
        [ContaintCapitalLetter]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings=false, ErrorMessage="Please enter last name.")]
        [MinLength(1)]
        [ContaintCapitalLetter]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings=false, ErrorMessage="Please enter phone number.")]
        [MinLength(1)]
        [RegularExpression("[0-9]{3}-[0-9]{3}-[0-9]{3}", ErrorMessage = "Pattern is : XXX-XXX-XXX")]
        public string Telephone { get; set; }
        public ICollection<MedicalService> MedicalServices { get; set; }
        public ICollection<Office> Offices { get; set; }
    }
}