using System.ComponentModel.DataAnnotations;
using ClinicApplication.Models;
using static ClinicApplication.Models.CustomValidation;

namespace ClinicApp.Models
{
    public class Office
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        [RegularExpression("[0-9]{2}-[0-9]{3}")]
        public string PostCode { get; set; }
        [CheckCity(AllowCity = "Gdansk,Gdynia,Sopot")]

        public string City { get; set; }
        public string Telephone { get; set; }
        public Doctor Doctor;
    }
}