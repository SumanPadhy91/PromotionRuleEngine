using PromotionRuleEngine.Core.Models;
using System.Collections.Generic;

namespace PromotionRuleEngine.Core.Manager.Interfaces
{
    public interface IPromotionEngine
    {
        void AddPromotions(Dictionary<Product, int> products, decimal price);

        decimal CalculateDiscount(ICartManager cart);
    }
}
