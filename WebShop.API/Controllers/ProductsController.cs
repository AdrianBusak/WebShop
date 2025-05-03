using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebShop.API.DTOs;
using WebShop.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly WebShopContext _context;
        private readonly IMapper _mapper;
        public ProductsController(WebShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<ProductsController>
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            if (_context.Products.Any())
            {
                return NotFound();
            }

            return Ok(_context.Products);

        }

        // GET api/<ProductsController>/5
        [HttpGet("[action]/{id}")]
        public ActionResult<ProductDto> Get(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        // POST api/<ProductsController>
        [HttpPost("[action]")]
        public ActionResult Create([FromBody] ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                _context.Products.Add(product);
                _context.SaveChanges();

                return CreatedAtAction(
                    nameof(Get),
                    new { id = product.Id },
                    productDto
                );
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut("[action]/{id}")]
        public ActionResult Update(int id, [FromBody] ProductDto productDto)
        {
            var product = _context.Products
                .FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _mapper.Map(productDto, product);
            product.Id = id;
            _context.SaveChanges();
            return Ok();
        }


        // DELETE api/<ProductsController>/5
        [HttpDelete("[action]/{id}")]
        public ActionResult<ProductDto> Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);


        }
    }
}
