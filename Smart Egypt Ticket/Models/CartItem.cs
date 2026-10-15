namespace Smart_Egypt_Ticket.Models
{
    public class CartItem
    {
        public int TicketTypeId { get; set; }
        public string TicketTypeName { get; set; } = "";
        public string MonumentName { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime VisitDate { get; set; }

        public decimal Total => UnitPrice * Quantity;
    }
}