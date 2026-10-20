using Microsoft.AspNetCore.Identity;

namespace Smart_Egypt_Ticket.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Nationality { get; set; }
        // CreatedAtUtc is used to store the date and time when the user was created in UTC format
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        //is active is used to check if the user is active or not, if the user is not active he can't login to the system
        public bool IsActive { get; set; } = true;
    }
}