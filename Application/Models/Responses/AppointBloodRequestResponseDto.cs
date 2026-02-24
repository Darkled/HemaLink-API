namespace Application.Models.Responses
{
    public class AppointBloodRequestResponseDto
    {
        public required string BloodRequesterName { get; set; }
        public required string BloodRequestAdress { get; set; }
        public required DateTime RequestDate { get; set; }
        public required string DonorName { get; set; }
        public required string DonorEmail { get; set; }
        public required string DonorPhone { get; set; }
        public bool? IsCancelled { get; set; }
    }
}
