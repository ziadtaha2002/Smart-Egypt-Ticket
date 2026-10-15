using System.ComponentModel.DataAnnotations;
namespace Smart_Egypt_Ticket.Models
{
    public class Destination
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string City { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAtUtc { get; set; }

        public ICollection<Monument> Monuments { get; set; } = new List<Monument>();
    }
}