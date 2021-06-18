using PromotionRuleEngine.Core.Manager.Interfaces;
using PromotionRuleEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool AreProductsAvailableinCartforPromo(Dictionary<Product, int> products)
        {
            var areProductsAvailable = false;
            var cartItemsList = this.GetUniqueItemsfromCart();
            var promoproducts = products.Keys.ToList();

            areProductsAvailable = promoproducts.Intersect(cartItemsList).Count() == promoproducts.Count();

            //foreach (var prod in products)
            //{
            //    if (cartItemsList.Any(c => c.Sku == prod.Key.Sku))
            //    {
            //        areProductsAvailable = true;
            //    }
            //}

            return areProductsAvailable;
        }

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

