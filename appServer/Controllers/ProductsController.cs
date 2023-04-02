using appServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public ProductsController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {

            var products= await _dataContext.Products.ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {

            var p = await _dataContext.Products.FirstOrDefaultAsync(i => i.id == id);
            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product entity)
        {

            _dataContext.Products.Add(entity); 
            await _dataContext.SaveChangesAsync();
            var products = await _dataContext.Products.ToListAsync();
            return Ok(products);

        }



    }
}
