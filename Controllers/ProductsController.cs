using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.Models;

namespace Test.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductManagmentContext _productManagmentContext;
        private IHttpContextAccessor _httpContextAccessor;

        public ProductsController(ILogger<ProductsController> logger, ProductManagmentContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _productManagmentContext = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get all existing products
        /// </summary>
        /// <returns>List of Product</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /List
        ///
        /// <response code="500">An error occurred listing products.</response>

        [HttpGet]
        public object List()
        {
            // Data to identify session and user. At this moment we only have IP
            string IP = (_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()) ?? "N/D";

            _logger.LogInformation($"[List-{IP}] Getting prodcuts...");
            var productsList = new List<Product>();

            try
            {
                productsList = _productManagmentContext.Products.ToList();
                if (productsList.Count == 0)
                    _logger.LogDebug($"[List-{IP}] No products found.");


                return productsList;
            }
            catch (System.Exception e)
            {
                var error = $"An error occurred listing products: {e.Message}.";

                _logger.LogError($"[List-{IP}] Unable to get products. Error: {error}");

                return StatusCode(500, productsList);
            }
        }

        /// <summary>
        /// Adds new Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// POST /AddProduct
        ///{
        ///   "name": "string",
        ///   "description": "string",
        ///   "price": 0.00,
        ///   "familyProduct": "string"
        /// },
        ///
        /// </remarks>
        /// <response code="200">OK if product added. </response>
        /// <response code="400">OK if product is not valid. </response>
        /// <response code="500">OK if error occurred adding product. </response>
        [HttpPost]
        public ActionResult Add(Product product)
        {
            string IP = (_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()) ?? "N/D";

            _logger.LogInformation($"[AddProduct-{IP}] Adding product {JsonConvert.SerializeObject(product)}...");

            // Forcing id to 0 because we don't know what we have. To ensure integrity.
            product.Id = 0;

            // Model validation to ensure the integrity of the object. In porducts as DevExpress we can use decarators to do this task automaically.
            var (valid, message) = product.IsValid();
            if (!valid)
                return StatusCode(400, message);

            _productManagmentContext.Add(product);

            try
            {
                _productManagmentContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                var error = $"An error occurred adding product: {e.Message}.";

                _logger.LogError($"[AddProduct-{IP}] {JsonConvert.SerializeObject(product)} Error: {error}");

                return StatusCode(500, error);
            }

            return Ok();
        }

        /// <summary>
        /// Test method.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Typical Hello string</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /Dummy
        /// {
        ///     "name": "Carles"
        /// }
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Dummy(string name)
        {
            return Ok($"Hello {name}!");
        }
    }
}
