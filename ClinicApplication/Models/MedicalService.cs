using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Models
{
    public class MedicalService
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        [Range(0, 100000)]
        public decimal Price { get; set; }
        public Doctor Doctor;
    }
}