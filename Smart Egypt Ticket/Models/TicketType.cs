using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Egypt_Ticket.Models
{
    public class TicketType
    {
        public int Id { get; set; }

        public int MonumentId { get; set; }
        public Monument? Monument { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } // e.g. Adult, Student, Foreigner

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAtUtc { get; set; }
    }
}