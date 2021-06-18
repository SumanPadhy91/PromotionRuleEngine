namespace PromotionRuleEngine.Core.Models
{
    /// <summary>
    /// Class for Product in the Storage.
    /// </summary>
    public class Product
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }

        /// <summary>
        /// Initialise Product class with the Product info.
        /// </summary>
        /// <param name="id">SKU Name</param>
        /// <param name="price">cost of SKU</param>
        public Product(string id, decimal price)
        {
            this.Sku = id.ToUpper();
            this.Price = price;
        }
    }
}
