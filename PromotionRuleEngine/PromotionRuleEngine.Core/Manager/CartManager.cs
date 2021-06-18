using PromotionRuleEngine.Core.Manager.Interfaces;
using PromotionRuleEngine.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PromotionRuleEngine.Core.Manager
{
    /// <summary>
    /// Manager class for Cart items
    /// </summary>
    public class CartManager : ICartManager
    {
        private Dictionary<Product, int> cartItems;

        public CartManager()
        {
            this.cartItems = new Dictionary<Product, int>();
        }

        /// <summary>
        /// Add items to cart
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void AddCartItems(Product product, int quantity)
        {
            if (quantity > 0)
            {
                if (this.cartItems.ContainsKey(product))
                {
                    this.cartItems[product] += quantity;
                }
                else
                {
                    this.cartItems.Add(product, quantity);
                }
            }
        }

        /// <summary>
        /// Get All items in cart 
        /// </summary>
        /// <returns></returns>
        public Dictionary<Product, int> GetCartItems()
        {
            return this.cartItems;
        }

        /// <summary>
        /// Get sum of total cart value without promotions
        /// </summary>
        /// <returns></returns>
        public int GetTotalCartItems()
        {
            var count = 0;
            foreach (var item in this.cartItems)
            {
                count += item.Value;
            }
            return count;
        }

        /// <summary>
        /// Added for Unit testing to return cart object.
        /// </summary>
        /// <returns></returns>
        public ICartManager Clone()
        {
            var cart = new CartManager();
            foreach (var item in this.cartItems)
            {
                cart.cartItems.Add(item.Key, item.Value);
            }
            return cart;
        }

        /// <summary>
        /// Method to check if all products in cart are available in a specific promotion
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public bool AreProductsAvailableinCartforPromo(Dictionary<Product, int> products)
        {
            var cartItemsList = this.GetUniqueItemsfromCart();
            var promoproducts = products.Keys.ToList();

            bool areProductsAvailable = promoproducts.Intersect(cartItemsList).Count() == promoproducts.Count();
            return areProductsAvailable;
        }

        /// <summary>
        /// Fetch unique items from cart
        /// </summary>
        /// <returns></returns>
        public List<Product> GetUniqueItemsfromCart()
        {
            var cartItemsList = new List<Product>();
            foreach(var item in this.cartItems)
            {
                if(item.Value > 0)
                {
                    cartItemsList.Add(item.Key);
                }
            }
            return cartItemsList;
        }
    }
}

