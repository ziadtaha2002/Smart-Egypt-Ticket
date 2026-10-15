using System.ComponentModel.DataAnnotations;

namespace Smart_Egypt_Ticket.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int OrderItemId { get; set; }
        public OrderItem? OrderItem { get; set; }

        [Required, MaxLength(40)]
        public string TicketNumber { get; set; } = string.Empty;

        [Required, MaxLength(128)]
        public string ValidationTokenHash { get; set; } = string.Empty;

        public DateTime VisitDate { get; set; }

        [Required, MaxLength(20)]
        public string Status { get; set; } = "Valid";

        public DateTime IssuedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? UsedAtUtc { get; set; }

        public string? UsedByUserId { get; set; }
        public ApplicationUser? UsedByUser { get; set; }
    }
}