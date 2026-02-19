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
        public required Requester Requester { get; set; }
        public required ICollection<BloodType> BloodTypesNeeded { get; set; }
        public DateTime RequestedOn { get; set; } = DateTime.UtcNow;
        public DateTime? FulfilledOn { get; set; }
        public required int TargetUnits {  get; set; }
        public required int RemainingUnits { get; set; }
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Open;
    }
}
