using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebShop.API.DTOs;
using WebShop.DAL.Models;

namespace WebShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCountriesController : ControllerBase
    {
        private readonly WebShopContext _context;
        private readonly IMapper _mapper;

        public ProductCountriesController(WebShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Dodaj zemlju proizvodu
        [HttpPost]
        public ActionResult AddCountryToProduct(CountryProductDto dto)
        {
            try
            {
                var exists = _context.ProductCountries
                        .Any(pc => pc.ProductId == dto.ProductId && pc.CountryId == dto.CountryId);

                if (exists)
                    return BadRequest("This product is already linked to that country.");

                var productCountry = _mapper.Map<ProductCountry>(dto);

                _context.ProductCountries.Add(productCountry);
                _context.SaveChanges();
                return Ok("Linked successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }

        // Ukloni zemlju iz proizvoda
        [HttpDelete]
        public ActionResult RemoveCountryFromProduct(CountryProductDto dto)
        {
            try
            {
                var pc = _context.ProductCountries
                       .FirstOrDefault(pc => pc.ProductId == dto.ProductId && pc.CountryId == dto.CountryId);

                if (pc == null)
                    return NotFound();

                _context.ProductCountries.Remove(pc);
                _context.SaveChanges();
                return Ok("Unlinked successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }
    }

}
