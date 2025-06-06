using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.API.DTOs;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.ProductRepo;
using WebShop.DAL.Services.ProductService;

namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductResponseDto>> GetAll()
        {
            try
            {
                var products = _productService.GetAll()
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
                var products = _productService.GetByCategoryId(categoryId)
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
                var product = _productService.GetById(id);
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
                var products = _productService.GetInCountry(countryId)
                        .Select(pc => _mapper.Map<ProductResponseDto>(pc))
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
                var product = _productService.GetWithDetails(id);

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
                _productService.Create(product);

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
                var product = _productService.GetById(id);

                if (product == null)
                {
                    return NotFound();
                }

                _mapper.Map(productDto, product);
                _productService.Update(product);

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
                product = _productService.GetById(id);

                if (product == null)
                {
                    return NotFound();
                }

                _productService.Delete(product);
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
