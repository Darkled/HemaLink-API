using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class BloodRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RequesterId { get; set; }
        public Requester? Requester { get; set; }
        public List<BloodType>? BloodTypesNeeded { get; set; }
        public DateTime RequestDate { get; set; }
        public required string Address { get; set; }
        public int TargetUnits {  get; set; }
        public int RemainingUnits { get; set; }
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Open;
        public List<Appointment> Appointments { get; set; } = new();
    }
}
