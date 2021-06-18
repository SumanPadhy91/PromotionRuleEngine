using PromotionRuleEngine.Core.Models;
using System.Collections.Generic;

namespace PromotionRuleEngine.Core.Manager.Interfaces
{
    public interface IProductManager
    {
        List<Product> GetAllProducts();

        void AddProduct(string id, decimal price);
    }
}
