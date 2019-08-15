using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    //Ajudando ferramentas de documentacao (Ex. Swagger)
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IDutchRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        } //constructor


        [HttpGet]
        //Ajudando ferramentas de documentacao
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //Incluindo o retorno como ActionResult<type> eu ajudo ferramentas de documentacao
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts());
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get products {ex.Message}");
                return BadRequest("Failed to get products");
            }
            
            
        }

    }
}
