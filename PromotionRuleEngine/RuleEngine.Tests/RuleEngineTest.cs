using Microsoft.Extensions.DependencyInjection;
using PromotionRuleEngine.Core.Manager;
using PromotionRuleEngine.Core.Manager.Interfaces;
using PromotionRuleEngine.Core.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RuleEngine.Tests
{
    public class RuleEngineTest
    {
        IProductManager productManager;
        IPromotionManager promotionManager;

        public RuleEngineTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IProductManager, ProductManager>()
                .AddScoped<IPromotionManager, PromotionManager>()
                .BuildServiceProvider();

            this.productManager = serviceProvider.GetRequiredService<IProductManager>();
            this.promotionManager = serviceProvider.GetRequiredService<IPromotionManager>();

            productManager.AddProduct("A", 50);
            productManager.AddProduct("b", 30);
            productManager.AddProduct("C", 20);
            productManager.AddProduct("D", 15);

            Dictionary<Product, int> dict1 = new Dictionary<Product, int>();
            Dictionary<Product, int> dict2 = new Dictionary<Product, int>();
            Dictionary<Product, int> dict3 = new Dictionary<Product, int>();

            var products = this.productManager.GetAllProducts();

            dict1.Add(products.Where(c => c.Sku == "C").FirstOrDefault(), 1);
            dict1.Add(products.Where(c => c.Sku == "D").FirstOrDefault(), 1);
            dict2.Add(products.Where(c => c.Sku == "A").FirstOrDefault(), 3);
            dict3.Add(products.Where(c => c.Sku == "B").FirstOrDefault(), 2);

            promotionManager.AddPromotions(dict1, 30);
            promotionManager.AddPromotions(dict2, 130);
            promotionManager.AddPromotions(dict3, 45);
        }

        [Fact]
        public void ProductsTest()
        {
            var products = productManager.GetAllProducts();

            Assert.Equal(4, products.Count());
        }
    }
}
