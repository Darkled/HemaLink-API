using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BloodRequestId { get; set; }
        public BloodRequest BloodRequest { get; set; } = null!;
        public int DonorId { get; set; }
        public Donor Donor { get; set; } = null!;
        public required string CancellationToken { get; set; }
        public bool IsCancelled { get; set; } = false;
    }
}
