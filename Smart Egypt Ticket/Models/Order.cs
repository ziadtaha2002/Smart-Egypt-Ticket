using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Egypt_Ticket.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required, MaxLength(450)]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }

        [Required, MaxLength(30)]
        public string OrderNumber { get; set; } = string.Empty;

        public DateTime OrderDateUtc { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required, MaxLength(30)]
        public string Status { get; set; } = "PendingPayment";

        [Required, StringLength(3)]
        public string Currency { get; set; } = "EGP";

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAtUtc { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}