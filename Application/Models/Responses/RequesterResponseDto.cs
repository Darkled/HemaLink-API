using Domain.Models.Enums;

namespace Application.Models.Responses
{
    public class RequesterResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public AdmissionStatus AdmissionStatus { get; set; }
    }
}
