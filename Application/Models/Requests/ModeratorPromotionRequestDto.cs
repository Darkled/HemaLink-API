using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class ModeratorPromotionRequestDto
    {

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
