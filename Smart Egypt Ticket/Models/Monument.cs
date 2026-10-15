using System.ComponentModel.DataAnnotations;
namespace Smart_Egypt_Ticket.Models
{
    public class Monument
    {
        public int Id { get; set; }
        public int DestinationId { get; set; }
        public Destination? Destination { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? OpeningHours { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAtUtc { get; set; }

        public ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
    }
}