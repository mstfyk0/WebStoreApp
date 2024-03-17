
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public ICollection<CardLine> Lines {get;set;} =  new List<CardLine>();

        [Required(ErrorMessage = "Name is required.")]
        public String? Name { get; set; }
        public String? Line1  { get; set; }
        public String? Line2  { get; set; }
        public String? Line3  { get; set; }
        public String? City  { get; set; }
        public bool GiftWrap  { get; set; }
        public bool Shipped  { get; set; } 
        public DateTime OrderAt  { get; set; } = DateTime.Now;
    }
}