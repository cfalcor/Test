using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Test;
using Test.Controllers;
using Test.Models;

namespace UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void ProductModel()
        {
            Product product = new() 
            { 
                Name = "",
                Description = "test description",
                FamilyProduct = "test family",
                Price = 10m
            };

            var (isValid, message) = product.IsValid();

            Assert.IsFalse(isValid);
            Assert.AreEqual("Name cannot be empty.", message);

            product.Name = "test product";
            product.Price = -10m;

            (isValid, message) = product.IsValid();

            Assert.IsFalse(isValid);
            Assert.AreEqual("Price cannot be negative.", message);

            product.Name = "";

            (isValid, message) = product.IsValid();

            Assert.IsFalse(isValid);
            Assert.AreEqual("Name cannot be empty.\r\nPrice cannot be negative.", message);

            product.Name = "test product";
            product.Price = 10m;

            (isValid, message) = product.IsValid();

            Assert.IsTrue(isValid);
            Assert.IsEmpty(message);
        }

        [Test]
        public void Views()
        {
            var controller = new HomeController();

            var result = controller.AddProduct() as ViewResult;
            Assert.AreEqual("AddProduct", result.ViewName);

            result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void ProductController()
        {
            // We declare this variables inside the test because we don't want this to effect other scopes. Otherwise this would be on Setup() with global variables.
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var options = new DbContextOptionsBuilder<ProductManagmentContext>()
             .UseInMemoryDatabase(databaseName: "ProductDB")
             .Options;

            var mockProductManagmentContext = new Mock<ProductManagmentContext>(options);
            var mockContextAccessor = new Mock<IHttpContextAccessor>();
            var controller = new ProductsController(mockLogger.Object, mockProductManagmentContext.Object, mockContextAccessor.Object);

            var product = new Product() { Name = "test" };

            var resultOk = controller.Add(product) as OkResult;
            Assert.AreEqual(resultOk.StatusCode, 200);

            product.Name = "";

            var result1 = controller.Add(product) as ObjectResult;
            Assert.AreEqual(result1.StatusCode, 400);
            Assert.AreEqual(result1.Value, "Name cannot be empty.");
        }
    }
}