using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.API.DTOs;
using WebShop.API.Models;

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
        [HttpGet]
        public ActionResult<IEnumerable<ProductResponseDto>> GetAll()
        {
            try
            {
                var products = _context.Products
                    .Select(x => _mapper.Map<ProductResponseDto>(x))
                    .ToList();

                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }

        }
        // GET: api/<ProductsController>
        [HttpGet("product/{categoryId}")]
        public ActionResult<IEnumerable<ProductResponseDto>> GetAll(int categoryId)
        {
            try
            {
                var products = _context.Products
                    .Where(x => x.CategoryId == categoryId)
                    .Select(x => _mapper.Map<ProductResponseDto>(x))
                    .ToList();

                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }

        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ActionResult<ProductResponseDto> Get(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                var productDto = _mapper.Map<ProductResponseDto>(product);

                return Ok(productDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }

        [HttpGet("in-country/{countryId}")]
        public ActionResult<List<ProductResponseDto>> GetProductsInCountry(int countryId)
        {
            try
            {
                var products = _context.ProductCountries
                        .Where(pc => pc.CountryId == countryId)
                        .Include(pc => pc.Product)
                        .Select(pc => _mapper.Map<ProductResponseDto>(pc.Product))
                        .ToList();

                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }

        [HttpGet("{id}/details")]
        public ActionResult<ProductDetailDto> GetProductDetails(int id)
        {
            try
            {
                var product = _context.Products
                        .Include(p => p.Category)
                        .Include(p => p.Images)
                        .FirstOrDefault(p => p.Id == id);

                if (product == null)
                    return NotFound();

                var dto = _mapper.Map<ProductDetailDto>(product);
                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }


        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult Post([FromBody] ProductCreateDto productDto)
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
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductUpdateDto productDto)
        {
            try
            {
                var product = _context.Products
                       .FirstOrDefault(x => x.Id == id);

                if (product == null)
                {
                    return NotFound();
                }

                _mapper.Map(productDto, product);

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");

            }
        }


        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public ActionResult<ProductResponseDto> Delete(int id)
        {
            var product = new Product();

            try
            {
                product = _context.Products.FirstOrDefault(x => x.Id == id);
             
                if (product == null)
                {
                    return NotFound();
                }

                var images = _context.Images.Where(img => img.ProductId == id).ToList();
                _context.Images.RemoveRange(images);

                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }

                var productDto = _mapper.Map<ProductResponseDto>(product);

                return Ok(productDto);
        }
    }
}
