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
        public DateTime RequestedOn { get; set; } = DateTime.UtcNow;
        public DateTime? FulfilledOn { get; set; } = null;
        public int TargetUnits {  get; set; }
        public int RemainingUnits { get; set; }
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Open;
    }
}
