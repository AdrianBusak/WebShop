using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Models;
using WebShop.DAL.Services;
using WebShop.DAL.Services.CategoryServices;
using WebShopWebApi.DTOs;

namespace WebShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryResponseDto>> GetAll()
        {
            try
            {
                var categories = _service.GetAll();
                var dtos = categories.Select(c => _mapper.Map<CategoryResponseDto>(c));
                return Ok(dtos);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving categories.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryResponseDto> Get(int id)
        {
            try
            {
                var category = _service.GetById(id);
                return category == null ? NotFound() : Ok(_mapper.Map<CategoryResponseDto>(category));
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the category.");
            }
        }

        [HttpPost]
        public ActionResult<CategoryResponseDto> Post([FromBody] CategoryCreateDto dto)
        {
            try
            {
                var entity = _mapper.Map<Category>(dto);
                var created = _service.Create(entity);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, _mapper.Map<CategoryResponseDto>(created));
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the category.");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<CategoryResponseDto> Put(int id, [FromBody] CategoryUpdateDto dto)
        {
            try
            {
                var updated = _service.Update(id, _mapper.Map<Category>(dto));
                return updated == null ? NotFound() : Ok(_mapper.Map<CategoryResponseDto>(updated));
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the category.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<CategoryResponseDto> Delete(int id)
        {
            try
            {
                var deleted = _service.Delete(id);
                return deleted == null ? NotFound() : Ok(_mapper.Map<CategoryResponseDto>(deleted));
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the category.");
            }
        }
    }
}
