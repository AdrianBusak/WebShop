using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.API.DTOs;
using WebShop.DAL.Models;
using WebShop.DAL.Services.CountryServices;

namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        public CountryController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        // GET: api/<CountriesController>
        [HttpGet]
        public ActionResult<IEnumerable<CountryResponseDto>> GetAll()
        {
            try
            {
                var countries = _countryService.GetAll()
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
                var country = _countryService.GetById(id);

                var countryResponse = _mapper.Map<CountryResponseDto>(country);

                if (countryResponse == null)
                {
                    return NotFound();
                }
                return Ok(countryResponse);
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
                var countries = _countryService.GetForProduct(productId)
                        .Select(pc => _mapper.Map<CountryResponseDto>(pc))
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
                if (_countryService.GetAll().Any(c => c.Name.ToLower() == country.Name.ToLower()))
                {
                    return BadRequest("A genre with the same name already exists.");
                }

                var newCountry = _mapper.Map<Country>(country);

                _countryService.Create(newCountry);

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
                var existingCountry = _countryService.GetById(id);
                if (existingCountry == null)
                {
                    return NotFound();
                }

                _mapper.Map(country, existingCountry);

                _countryService.Update(existingCountry);
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
                var country = _countryService.GetById(id);
                if (country == null)
                {
                    return NotFound();
                }

                _countryService.Delete(country);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }
    }
}
