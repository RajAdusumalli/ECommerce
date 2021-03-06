using AutoMapper;
using ECommerce.API.Products.Db;
using ECommerce.API.Products.Profiles;
using ECommerce.API.Products.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.API.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProductsAsync()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProductsAsync))
                .Options;
            var context = new ProductsDbContext(options);
            CreateProducts(context);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsService = new ProductsService(context, null, mapper);
            var product = await productsService.GetProductsAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.Products!.Any());
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingValidIdAsync()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingValidIdAsync))
                .Options;
            var context = new ProductsDbContext(options);
            CreateProducts(context);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsService = new ProductsService(context, null, mapper);
            var product = await productsService.GetProductAsync(1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.True(product.Product?.Id == 1);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingInValidIdAsync()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingInValidIdAsync))
                .Options;
            var context = new ProductsDbContext(options);
            CreateProducts(context);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsService = new ProductsService(context, null, mapper);
            var product = await productsService.GetProductAsync(-1);
            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext context)
        {
            for(int i = 1; i < 10; i++)
            {
                context.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Price = (decimal)(i * 3.14)
                });
            }
            context.SaveChanges();
        }
    }
}