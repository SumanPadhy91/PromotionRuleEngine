namespace PromotionRuleEngine.Core.Models
{
    /// <summary>
    /// Class for Promotional proce for a specific product combination or a single product.
    /// </summary>
    public class Promotion : ProductInfo
    {
        public decimal PromoPrice { get; set; }
    }
}
