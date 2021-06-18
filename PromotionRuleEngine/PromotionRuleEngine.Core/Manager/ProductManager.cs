using PromotionRuleEngine.Core.Manager.Interfaces;
using PromotionRuleEngine.Core.Models;
using System.Collections.Generic;

namespace PromotionRuleEngine.Core.Manager
{
    public class ProductManager : IProductManager
    {
        private List<Product> products;

        public ProductManager()
        {
            this.products = new List<Product>();
        }

        public void AddProduct(string id, decimal price)
        {
            this.products.Add(new Product(id.ToUpper(), price));
        }

        public List<Product> GetAllProducts()
        {
            return this.products;
        }
    }
}
