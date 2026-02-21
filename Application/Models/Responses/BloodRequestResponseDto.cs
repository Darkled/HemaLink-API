namespace Application.Models.Responses
{
    public class BloodRequestResponseDto
    {
        public string RequesterName { get; set; } = string.Empty;
        public List<string>? BloodTypesNeeded { get; set; }
        public DateTime RequestedOn { get; set; }
        public int TargetUnits { get; set; }
        public int RemainingUnits { get; set; }
        public required string RequestStatus { get; set; }
    }
}
