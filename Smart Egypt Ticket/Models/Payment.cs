using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Egypt_Ticket.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Required, MaxLength(50)]
        public string Provider { get; set; } = "Fake";

        [MaxLength(200)]
        public string? ProviderReference { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required, StringLength(3)]
        public string Currency { get; set; } = "EGP";

        [Required, MaxLength(30)]
        public string Status { get; set; } = "Pending";

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAtUtc { get; set; }

        [MaxLength(500)]
        public string? FailureReason { get; set; }
    }
}