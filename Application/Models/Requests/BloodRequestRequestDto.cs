using Domain.Models.Enums;

namespace Application.Models.Requests
{
    public class BloodRequestRequestDto
    {
        public ICollection<BloodType>? BloodTypesNeeded { get; set; }
        public int TargetUnits { get; set; }
    }
}
