using System;
using System.ComponentModel.DataAnnotations;

namespace DentalManagementAPI.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        [Required]
        [MaxLength(100)]
        public string PatientName { get; set; }

        public DateTime DOB { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
