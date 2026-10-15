using System.ComponentModel.DataAnnotations;

namespace Smart_Egypt_Ticket.Models
{
    public class AuditLog
    {
        public long Id { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [Required, MaxLength(100)]
        public string Action { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string EntityName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string EntityId { get; set; } = string.Empty;

        public string? OldValuesJson { get; set; }

        public string? NewValuesJson { get; set; }

        [MaxLength(45)]
        public string? IpAddress { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}