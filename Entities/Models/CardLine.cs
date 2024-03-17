namespace Entities.Models
{
    public class CardLine 
    {
        public int CardLineId { get; set; }

        public Product Product { get; set; } = new();

        public int Quantity { get; set; }
    }
}