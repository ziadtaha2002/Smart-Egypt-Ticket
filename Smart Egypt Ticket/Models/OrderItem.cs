using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Egypt_Ticket.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int TicketTypeId { get; set; }
        public TicketType? TicketType { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime VisitDate { get; set; }

        [Required, MaxLength(100)]
        public string TicketTypeName { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string MonumentName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LineTotal { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
