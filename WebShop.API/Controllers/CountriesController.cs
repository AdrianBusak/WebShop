using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.API.DTOs;
using WebShop.API.Models;

namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly WebShopContext _context;
        private readonly IMapper _mapper;
        public CountriesController(WebShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<CountriesController>
        [HttpGet]
        public ActionResult<IEnumerable<CountryResponseDto>> Get()
        {
            try
            {
                var countries = _context.Countries
                    .Select(x => _mapper.Map<CountryResponseDto>(x))
                    .ToList();

                return Ok(countries);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }

        // GET api/<CountriesController>/5
        [HttpGet("{id}")]
        public ActionResult<CountryResponseDto> Get(int id)
        {
            try
            {
                var country = _context.Countries
                    .Where(x => x.Id == id)
                    .Select(x => _mapper.Map<CountryResponseDto>(x))
                    .FirstOrDefault();

                if (country == null)
                {
                    return NotFound();
                }
                return Ok(country);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }

        [HttpGet("for-product/{productId}")]
        public ActionResult<List<CountryResponseDto>> GetCountriesForProduct(int productId)
        {
            try
            {
                var countries = _context.ProductCountries
                        .Where(pc => pc.ProductId == productId)
                        .Include(pc => pc.Country)
                        .Select(pc => _mapper.Map<CountryResponseDto>(pc.Country))
                        .ToList();

                return Ok(countries);
            }
            catch (Exception)
            {
                return StatusCode(500, "message");
            }
        }


        // POST api/<CountriesController>
        [HttpPost]
        public ActionResult Post([FromBody] CountryCreateDto country)
        {
            try
            {
                if (_context.Countries.Any(c => c.Name.ToLower() == country.Name.ToLower()))
                {
                    return BadRequest("A genre with the same name already exists.");
                }

                var newCountry = _mapper.Map<Country>(country);

                _context.Countries.Add(newCountry);
                _context.SaveChanges();

                return CreatedAtAction(nameof(Get), new { id = newCountry.Id }, newCountry);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }

        // PUT api/<CountriesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CountryUpdateDto country)
        {

            try
            {
                var existingCountry = _context.Countries.FirstOrDefault(c => c.Id == id);
                if (existingCountry == null)
                {
                    return NotFound();
                }

                _mapper.Map(country, existingCountry);

                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }

        }

        // DELETE api/<CountriesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var country = _context.Countries.FirstOrDefault(c => c.Id == id);
                if (country == null)
                {
                    return NotFound();
                }

                _context.Countries.Remove(country);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }
    }
}
