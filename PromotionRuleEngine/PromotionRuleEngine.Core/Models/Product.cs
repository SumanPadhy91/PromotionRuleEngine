namespace PromotionRuleEngine.Core.Models
{
    public class Product
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }

        public Product(string id, decimal price)
        {
            this.Sku = id.ToUpper();
            this.Price = price;
        }
    }
}
