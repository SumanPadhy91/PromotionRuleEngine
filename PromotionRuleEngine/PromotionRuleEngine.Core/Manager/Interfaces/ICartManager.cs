using PromotionRuleEngine.Core.Models;
using System.Collections.Generic;

namespace PromotionRuleEngine.Core.Manager.Interfaces
{
    public interface ICartManager
    {
        void AddCartItems(Product product, int quantity);

        Dictionary<Product, int> GetCartItems();

        ICartManager Clone();

        int GetTotalCartItems();

        bool AreProductsAvailableinCartforPromo(Dictionary<Product, int> product);
    }
}
