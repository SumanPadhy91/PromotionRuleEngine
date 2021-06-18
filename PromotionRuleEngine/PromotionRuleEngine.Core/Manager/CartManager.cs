using PromotionRuleEngine.Core.Manager.Interfaces;
using PromotionRuleEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionRuleEngine.Core.Manager
{
    public class CartManager : ICartManager
    {
        private Dictionary<Product, int> cartItems;

        public CartManager()
        {
            this.cartItems = new Dictionary<Product, int>();
        }

        public void AddCartItems(Product product, int quantity)
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

        public Dictionary<Product, int> GetCartItems()
        {
            return this.cartItems;
        }

        public int GetTotalCartItems()
        {
            var count = 0;
            foreach (var item in this.cartItems)
            {
                count += item.Value;
            }
            return count;
        }

        public ICartManager Clone()
        {
            var cart = new CartManager();
            foreach (var item in this.cartItems)
            {
                cart.cartItems.Add(item.Key, item.Value);
            }
            return cart;
        }
    }
}

