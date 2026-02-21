namespace Application.Models.Responses
{
    public class DetailedBloodRequestResponseDto
    {
        public int Id { get; set; }
        public int RequesterId { get; set; }
        public List<string>? BloodTypesNeeded { get; set; }
        public DateTime RequestedOn { get; set; }
        public int TargetUnits { get; set; }
        public int RemainingUnits { get; set; }
        public required string Status { get; set; }
    }
}
