using HealthCheckDemo.Data;
using HealthCheckDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthCheckDemo.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(AppDbContext appDbContext, ILogger<ProductsController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index(string name)
        {
            _logger.LogInformation("Test log");
            Product product = new Product { Name = name};
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
            return Ok(product);
        }
    }
}
