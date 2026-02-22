namespace Application.Models.Responses
{
    public class BloodRequestResponseDto
    {
        public int RequestId {get; set;}
        public string RequesterName {get; set; } = string.Empty;
        public List<string>? BloodTypesNeeded {get; set;}
        public DateTime RequestDate {get; set;}
        public int TargetUnits {get; set;}
        public required string Address {get; set;}
        public int RemainingUnits {get; set;}
        public required string RequestStatus {get; set;}
    }
}
