using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServiceAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ApplicationContext context;
        public ProductController(ApplicationContext db)
        {
            context = db;
        }

        // GET All Entity from DATABASE
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await context.Product.ToListAsync();
        }

        // GET api/name
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByName(string name)
        {
            var products = await context.Product.Where(x => x.Name == name).ToListAsync();
            if (products == null)
                return NotFound();
            return new ObjectResult(products);
        }

        // GET api/id
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
            Product product = await context.Product.FirstOrDefaultAsync(x => x.ID == id);
            if (product == null)
                return NotFound();
            return new ObjectResult(product);
        }

        // POST api/(product)
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            if (product == null)
                return BadRequest();
            context.Product.Add(product);
            context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Put(Product product)
        {
            if (product == null)
                return BadRequest();
            if (!context.Product.Any(x => x.ID == product.ID))
                return NotFound();
            context.Product.Update(product);
            context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(Guid id)
        {
            Product product = await context.Product.FirstOrDefaultAsync(x => x.ID == id);
            if (product == null)
                return NotFound();
            context.Product.Remove(product);
            context.SaveChangesAsync();
            return Ok(product);
        }
    }
}
