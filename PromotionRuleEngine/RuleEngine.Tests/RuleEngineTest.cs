using Microsoft.Extensions.DependencyInjection;
using PromotionRuleEngine.Core.Manager;
using PromotionRuleEngine.Core.Manager.Interfaces;
using PromotionRuleEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RuleEngine.Tests
{
    public class RuleEngineTest
    {
        IProductManager productManager;
        IPromotionManager promotionManager;
        ICartManager cartManager;

        public RuleEngineTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IProductManager, ProductManager>()
                .AddScoped<IPromotionManager, PromotionManager>()
                .AddScoped<ICartManager, CartManager>()
                .BuildServiceProvider();

            this.productManager = serviceProvider.GetRequiredService<IProductManager>();
            this.promotionManager = serviceProvider.GetRequiredService<IPromotionManager>();
            this.cartManager = serviceProvider.GetRequiredService<ICartManager>();

            productManager.AddProduct("A", 50);
            productManager.AddProduct("b", 30);
            productManager.AddProduct("C", 20);
            productManager.AddProduct("D", 15);

            Dictionary<Product, int> dictCombo = new Dictionary<Product, int>();
            Dictionary<Product, int> dictA = new Dictionary<Product, int>();
            Dictionary<Product, int> dictB = new Dictionary<Product, int>();

            var products = this.productManager.GetAllProducts();

            dictCombo.Add(products.Where(c => c.Sku == "C").FirstOrDefault(), 1);
            dictCombo.Add(products.Where(c => c.Sku == "D").FirstOrDefault(), 1);
            dictA.Add(products.Where(c => c.Sku == "A").FirstOrDefault(), 3);
            dictB.Add(products.Where(c => c.Sku == "B").FirstOrDefault(), 2);

            promotionManager.AddPromotions(dictCombo, 30);
            promotionManager.AddPromotions(dictA, 130);
            promotionManager.AddPromotions(dictB, 45);
        }

        [Fact]
        public void ProductsTest()
        {
            var products = productManager.GetAllProducts();

            Assert.Equal(4, products.Count());
        }

        [Fact]
        public void ItemsinCartTest()
        {
            var products = productManager.GetAllProducts();

            cartManager.AddCartItems(products.Where(c => c.Sku == "A").FirstOrDefault(), 3);
            cartManager.AddCartItems(products.Where(c => c.Sku == "B").FirstOrDefault(), 5);
            cartManager.AddCartItems(products.Where(c => c.Sku == "C").FirstOrDefault(), 1);
            cartManager.AddCartItems(products.Where(c => c.Sku == "D").FirstOrDefault(), 1);

            var cartItems = cartManager.GetCartItems();

            Assert.Equal(4, cartItems.Count());
            Assert.Equal(10, cartManager.GetTotalCartItems());
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 50)]
        [InlineData(0, 1, 0, 0, 30)]
        [InlineData(0, 0, 1, 0, 20)]
        [InlineData(0, 0, 0, 1, 15)]
        [InlineData(1, 1, 0, 0, 80)]
        [InlineData(1, 1, 1, 0, 100)]
        [InlineData(1, 1, 1, 1, 110)]
        [InlineData(3, 2, 1, 1, 205)]
        [InlineData(5, 5, 1, 0, 370)]
        [InlineData(3, 5, 1, 1, 280)]
        public void VerifyCartValue(object valueA, object valueB, object valueC, object valueD, object expected)
        {
            var products = this.productManager.GetAllProducts();
            cartManager.AddCartItems(products.Where(c => c.Sku == "A").FirstOrDefault(), Convert.ToInt32(valueA));
            cartManager.AddCartItems(products.Where(c => c.Sku == "B").FirstOrDefault(), Convert.ToInt32(valueB));
            cartManager.AddCartItems(products.Where(c => c.Sku == "C").FirstOrDefault(), Convert.ToInt32(valueC));
            cartManager.AddCartItems(products.Where(c => c.Sku == "D").FirstOrDefault(), Convert.ToInt32(valueD));

            var cart = cartManager.Clone();
            var resp = promotionManager.CalculateDiscount(cart);

            Assert.Equal(Convert.ToDecimal(expected), resp);
            Assert.Equal(Convert.ToDecimal(expected), resp);
            Assert.Equal(Convert.ToDecimal(expected), resp);
        }
    }
}
