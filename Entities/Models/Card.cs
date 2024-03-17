
namespace Entities.Models
{
    public class Card
    {
        public List<CardLine> Lines { get; set; }
        public Card()
        {

            Lines = new List<CardLine>();
        }

        public virtual void AddItem(Product product, int quantity)
        {
            CardLine? line = Lines.Where(l => l.Product.ProductId.Equals(product.ProductId)).FirstOrDefault();

            if (line is null)
            {
                Lines.Add(new CardLine()
                {
                    Product = product
                    ,
                    Quantity = quantity

                }
                );
            }
            else
            {
                line.Quantity += quantity;

            }

        }

        public virtual void RemoveLine(Product product) => Lines.RemoveAll(l => l.Product.ProductId.Equals(product.ProductId));

        public decimal ComputeToTotalValue() => Lines.Sum(e => e.Product.Price * e.Quantity);
        public virtual void Clear() => Lines.Clear();

    }
}