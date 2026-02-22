using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class ModeratorPromotionRequestDto
    {

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = string.Empty;
    }
}
