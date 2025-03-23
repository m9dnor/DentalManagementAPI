using System.ComponentModel.DataAnnotations;

namespace DentalManagementAPI.Models
{
    public class Treatment
    {
        [Key]
        public int TreatmentID { get; set; }

        [Required]
        [MaxLength(100)]
        public string TreatmentName { get; set; }

        [Required]
        [MaxLength(100)]
        public string TreatmentSpecialist { get; set; }

        [Required]
        public decimal TreatmentPrice { get; set; }



    }
}
