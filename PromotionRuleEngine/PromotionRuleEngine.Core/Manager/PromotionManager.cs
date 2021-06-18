using PromotionRuleEngine.Core.Manager.Interfaces;
using PromotionRuleEngine.Core.Models;
using System.Collections.Generic;

namespace PromotionRuleEngine.Core.Manager
{
    public class PromotionManager : IPromotionManager
    {
        private List<Promotion> promotions;

        public PromotionManager()
        {
            this.promotions = new List<Promotion>();
        }

        public void AddPromotions(Dictionary<Product, int> products, decimal price)
        {
            this.promotions.Add(new Promotion() { PromoProduct = products, PromoPrice = price });
        }

        public decimal CalculateDiscount(ICartManager cart)
        {
            return 0m;
        }
    }
}
