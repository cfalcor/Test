using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductManagmentContext _productManagmentContext;
        private HttpContextAccessor _httpContextAccessor;

        public ProductController(ILogger<ProductController> logger, ProductManagmentContext context, HttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _productManagmentContext = context;
            _httpContextAccessor = httpContextAccessor;
        }
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
