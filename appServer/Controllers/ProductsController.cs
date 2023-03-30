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
            //return CreatedAtAction(nameof(GetProduct), new { id = entity.id }, entity);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpddateProduct(int id,Product entity)
        {

            if (id != entity.id)
            {
                return BadRequest();
            }

            var product= await _dataContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }        


            product.ProductName = entity.ProductName;
            product.Stock = entity.Stock;
            product.Price = entity.Price;
            product.CurrencyType   = entity.CurrencyType;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteProduct(int id)
        {

            var product = await _dataContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }



    }
}
