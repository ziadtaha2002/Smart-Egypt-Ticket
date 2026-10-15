namespace Smart_Egypt_Ticket.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal GrandTotal => Items.Sum(i => i.Total);
    }
}