using Domain.Models.Enums;

namespace Application.Models.Requests
{
    public class BloodRequestRequestDto
    {
        public DateTime RequestDate { get; set; }
        public ICollection<BloodType>? BloodTypesNeeded { get; set; }
        public int TargetUnits { get; set; }
    }
}
