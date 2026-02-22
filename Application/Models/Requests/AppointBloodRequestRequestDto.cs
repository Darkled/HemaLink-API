using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class AppointBloodRequestRequestDto
    {
        [Required]
        public int BloodRequestId { get; set;}
        [Required]
        public required string DonorName { get; set;}
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public required string DonorEmail { get; set;}
        [Required]
        public required string DonorPhone { get; set;}
    }
}
