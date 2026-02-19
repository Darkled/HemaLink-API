using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public abstract class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required Role Role { get; set; }
        public required string Password { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
