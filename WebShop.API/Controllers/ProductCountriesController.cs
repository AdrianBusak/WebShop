using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebShop.API.DTOs;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.ProductCountryRepo;
using WebShop.DAL.Services.ProductCountryServices;

namespace WebShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCountriesController : ControllerBase
    {
        private readonly IProductCountryService _service;
        private readonly IMapper _mapper;

        public ProductCountriesController(IProductCountryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // Dodaj zemlju proizvodu
        [HttpPost]
        public ActionResult AddCountryToProduct(CountryProductDto dto)
        {
            try
            {
                var exists = _service.Get(dto.ProductId, dto.CountryId) != null;

                if (exists)
                    return BadRequest("This product is already linked to that country.");

                var productCountry = _mapper.Map<ProductCountry>(dto);

                _service.Link(productCountry.ProductId, productCountry.CountryId);
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
                var pc = _service.Get(dto.ProductId, dto.CountryId);

                if (pc == null)
                    return NotFound();

                _service.Unlink(pc.ProductId, pc.CountryId);
                return Ok("Unlinked successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }
    }

}
