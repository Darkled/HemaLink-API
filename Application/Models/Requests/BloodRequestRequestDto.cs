using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class BloodRequestRequestDto
    {
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public required string Address { get; set; }
        [Required]
        public ICollection<BloodType>? BloodTypesNeeded { get; set; }
        [Required]
        public int TargetUnits { get; set; }
    }
}
