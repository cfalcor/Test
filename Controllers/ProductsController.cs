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

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
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
