using MyAuthenticatedAPI.Models;
using System.Collections.Generic;
using Xunit;

namespace AuthenticatedAPI.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_ShouldHaveId()
        {
            var product = new Product { Id = 1 };
            Assert.Equal(1, product.Id);
        }

        [Fact]
        public void Product_ShouldHavePrice()
        {
            var product = new Product { Price = 9.99m };
            Assert.Equal(9.99m, product.Price);
        }

        [Fact]
        public void Product_ShouldHaveName()
        {
            var product = new Product { Name = "Book" };
            Assert.Equal("Book", product.Name);
        }

        [Fact]
        public void Product_ShouldHaveDescription()
        {
            var product = new Product { Description = "A good read" };
            Assert.Equal("A good read", product.Description);
        }
    }

    public class CategoryTests
    {
        [Fact]
        public void Category_ShouldHaveId()
        {
            var category = new Category { Id = 1 };
            Assert.Equal(1, category.Id);
        }

        [Fact]
        public void Category_ShouldHaveDescription()
        {
            var category = new Category { Description = "Books" };
            Assert.Equal("Books", category.Description);
        }
    }

    public class ShoppingCartTests
    {
        [Fact]
        public void ShoppingCart_ShouldHaveId()
        {
            var cart = new ShoppingCart { Id = 1 };
            Assert.Equal(1, cart.Id);
        }

        [Fact]
        public void ShoppingCart_ShouldHaveUser()
        {
            var cart = new ShoppingCart { User = "user@example.com" };
            Assert.Equal("user@example.com", cart.User);
        }

        [Fact]
        public void ShoppingCart_ShouldHaveProducts()
        {
            var cart = new ShoppingCart { Products = new List<Product>() };
            Assert.NotNull(cart.Products);
        }

        [Fact]
        public void ShoppingCart_ShouldAddProduct()
        {
            var cart = new ShoppingCart { Products = new List<Product>() };
            var product = new Product { Id = 1 };
            cart.Products.Add(product);
            Assert.Contains(product, cart.Products);
        }
    }
}