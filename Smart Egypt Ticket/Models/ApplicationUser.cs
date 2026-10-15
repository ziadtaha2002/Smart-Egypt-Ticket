using Microsoft.AspNetCore.Identity;

namespace Smart_Egypt_Ticket.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Nationality { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}