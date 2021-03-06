using PromotionRuleEngine.Core.Manager.Interfaces;
using PromotionRuleEngine.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PromotionRuleEngine.Core.Manager
{
    /// <summary>
    /// Manager class for calculating total price after promotions being applied.
    /// </summary>
    public class PromotionManager : IPromotionManager
    {
        private List<Promotion> promotions;

        public PromotionManager()
        {
            this.promotions = new List<Promotion>();
        }

        /// <summary>
        /// Add promotions to the list.
        /// </summary>
        /// <param name="products"></param>
        /// <param name="price"></param>
        public void AddPromotions(Dictionary<Product, int> products, decimal price)
        {
            this.promotions.Add(new Promotion() { PromoProduct = products, PromoPrice = price });
        }

        /// <summary>
        /// Calculate cart amount after applying the promotion.
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public decimal CalculateDiscount(ICartManager cart)
        {
            var counter = 0;
            var totalPromoPrice = 0m;
            var cartItems = cart.GetCartItems();
            foreach (var requiredProduct in this.promotions)
            {
                var promoPrice = 0m;
                var originalPrice = 0m;

                foreach (var item in requiredProduct.PromoProduct)
                {
                    counter++;
                    var requiredAmount = promotions.Where(c => c.PromoProduct.ContainsKey(item.Key)).FirstOrDefault().PromoPrice;

                    cartItems.TryGetValue(item.Key, out var qty);
                    originalPrice += item.Key.Price * qty;

                    if (cartItems.ContainsKey(item.Key) && counter == requiredProduct.PromoProduct.Count())
                    {
                        var lstOfProds = requiredProduct.PromoProduct.Keys.ToList();
                        if (cart.AreProductsAvailableinCartforPromo(requiredProduct.PromoProduct))
                        {
                            promoPrice = (qty / item.Value) * requiredProduct.PromoPrice + (qty % item.Value) * item.Key.Price;
                        }
                    }
                }

                if (promoPrice == 0)
                {
                    promoPrice = originalPrice;
                }
                counter = 0;
                totalPromoPrice += promoPrice;
            }
            return totalPromoPrice;
        }
    }
}
