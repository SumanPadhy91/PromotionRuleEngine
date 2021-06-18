using Microsoft.Extensions.DependencyInjection;
using PromotionRuleEngine.Core.Manager;
using PromotionRuleEngine.Core.Manager.Interfaces;
using System.Linq;
using Xunit;

namespace RuleEngine.Tests
{
    public class RuleEngineTest
    {
        IProductManager productManager;

        public RuleEngineTest()
        {
              var serviceProvider = new ServiceCollection()
                .AddScoped<IProductManager, ProductManager>()
                .BuildServiceProvider();

            this.productManager = serviceProvider.GetRequiredService<IProductManager>();

            productManager.AddProduct("A", 50);
            productManager.AddProduct("b", 30);
            productManager.AddProduct("C", 20);
            productManager.AddProduct("D", 15);
        }

        [Fact]
        public void ProductsTest()
        {
            var products = productManager.GetAllProducts();

            Assert.Equal(4, products.Count());
        }
    }
}
