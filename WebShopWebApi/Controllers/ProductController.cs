using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.ProductRepo;
using WebShop.DAL.Services.LogServices;
using WebShop.DAL.Services.ProductService;
using WebShopWebApi.DTOs;

namespace WebShopWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        public ProductController(IProductService productService, IMapper mapper, ILogService logService)
        {
            _productService = productService;
            _mapper = mapper;
            _logService = logService;
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

                Log log = new Log()
                {
                    Timestamp = DateTime.Now,
                    Level = "INFO",
                    Message = "Retrieved all products from the database."
                };

                _logService.Add(log);
                return Ok(products);
            }
            catch (Exception)
            {
                Log errorLog = new Log()
                {
                    Timestamp = DateTime.Now,
                    Level = "ERROR",
                    Message = "An error occurred while retrieving products."
                };
                _logService.Add(errorLog);
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

                Log log = new Log()
                {
                    Timestamp = DateTime.Now,
                    Level = "INFO",
                    Message = $"Retrieved products for category ID {categoryId} from the database."
                };
                _logService.Add(log);

                return Ok(products);
            }
            catch (Exception)
            {
                Log errorLog = new Log()
                {
                    Timestamp = DateTime.Now,
                    Level = "ERROR",
                    Message = "An error occurred while retrieving products by category."
                };
                _logService.Add(errorLog);
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

                Log log = new Log()
                {
                    Timestamp = DateTime.Now,
                    Level = "INFO",
                    Message = $"Retrieved product with ID {id} from the database."
                };
                _logService.Add(log);

                return Ok(productDto);
            }
            catch (Exception)
            {
                Log errorLog = new Log()
                {
                    Timestamp = DateTime.Now,
                    Level = "ERROR",
                    Message = $"An error occurred while retrieving product with ID {id}."
                };
                _logService.Add(errorLog);
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

                _logService.Add(new Log
                {
                    Timestamp = DateTime.Now,
                    Level = "INFO",
                    Message = $"Retrieved products in country with ID {countryId}."
                });
                return Ok(products);
            }
            catch (Exception)
            {
                _logService.Add(new Log
                {
                    Timestamp = DateTime.Now,
                    Level = "ERROR",
                    Message = $"An error occurred while retrieving products in country with ID {countryId}."
                });
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
                _logService.Add(new Log
                {
                    Timestamp = DateTime.Now,
                    Level = "INFO",
                    Message = $"Retrieved product details for product with ID {id}."
                });
                return Ok(dto);
            }
            catch (Exception)
            {
                _logService.Add(new Log
                {
                    Timestamp = DateTime.Now,
                    Level = "ERROR",
                    Message = $"An error occurred while retrieving product details for product with ID {id}."
                });
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

                _logService.Add(new Log
                {
                    Timestamp = DateTime.Now,
                    Level = "INFO",
                    Message = $"Created a new product with ID {product.Id}."
                });
                return CreatedAtAction(
                    nameof(Get),
                    new { id = product.Id },
                    productDto
                );
            }
            catch (Exception)
            {
                _logService.Add(new Log
                {
                    Timestamp = DateTime.Now,
                    Level = "ERROR",
                    Message = "An error occurred while creating a new product."
                });
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

                _logService.Add(new Log
                {
                    Timestamp = DateTime.Now,
                    Level = "INFO",
                    Message = $"Updated product with ID {id}."
                });
                return Ok();
            }
            catch (Exception)
            {
                _logService.Add(new Log
                {
                    Timestamp = DateTime.Now,
                    Level = "ERROR",
                    Message = $"An error occurred while updating product with ID {id}."
                });
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
                _logService.Add(new Log
                {
                    Timestamp = DateTime.Now,
                    Level = "INFO",
                    Message = $"Deleted product with ID {id}."
                });
                var productDto = _mapper.Map<ProductResponseDto>(product);

                return Ok(productDto);
            }
            catch (Exception)
            {
                _logService.Add(new Log
                {
                    Timestamp = DateTime.Now,
                    Level = "ERROR",
                    Message = $"An error occurred while deleting product with ID {id}."
                });
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }

        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<ProductResponseDto>> SearchProducts(string query)
        {
            try
            {
                var products = _productService.GetAll()
                    .Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                (p.Description != null && p.Description.Contains(query, StringComparison.OrdinalIgnoreCase)))
                    .Select(x => _mapper.Map<ProductResponseDto>(x))
                    .ToList();
                Log log = new Log()
                {
                    Timestamp = DateTime.Now,
                    Level = "INFO",
                    Message = $"Searched products with query '{query}'."
                };
                _logService.Add(log);
                return Ok(products);
            }
            catch (Exception)
            {
                Log errorLog = new Log()
                {
                    Timestamp = DateTime.Now,
                    Level = "ERROR",
                    Message = "An error occurred while searching for products."
                };
                _logService.Add(errorLog);
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }
    }
}
