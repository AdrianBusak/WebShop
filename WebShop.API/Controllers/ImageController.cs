using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebShop.API.DTOs;
using WebShop.API.Models;

namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly WebShopContext _context;
        private readonly IMapper _mapper;
        public ImageController(WebShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<ImageController>
        [HttpGet("product/{productId}")]
        public ActionResult<IEnumerable<ImageResponseDto>> GetAll(int productId)
        {
            try
            {
                if (!_context.Images.Any())
                {
                    return NotFound();
                }

                var images = _context.Images
                    .Where(x => x.ProductId == productId)
                    .Select(image => _mapper.Map<ImageResponseDto>(image))
                    .ToList();

                return Ok(images);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }

        //GET api/<ImageController>/5
        [HttpGet("{id}")]
        public ActionResult<ImageResponseDto> Get(int id)
        {
            try
            {
                var image = _context.Images.FirstOrDefault(x => x.Id == id);
                if (image == null)
                {
                    return NotFound();
                }
                var mappedImage = _mapper.Map<ImageResponseDto>(image);

                return Ok(mappedImage);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }

        }

        // POST api/<ImageController>
        [HttpPost]
        public ActionResult<ImageCreateDto> Post([FromBody] ImageCreateDto imageDto)
        {
            try
            {
                var image = _mapper.Map<Image>(imageDto);
                _context.Images.Add(image);
                _context.SaveChanges();

                return Ok(imageDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }

        // PUT api/<ImageController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ImageCreateDto imageDto)
        {
            try
            {
                var image = _context.Images.FirstOrDefault(x => x.Id == id);
                if (image == null)
                {
                    return NotFound();
                }

                _mapper.Map(imageDto, image);

                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }

        // DELETE api/<ImageController>/5
        [HttpDelete("{id}")]
        public ActionResult<ImageResponseDto> Delete(int id)
        {
            try
            {
                var image = _context.Images.FirstOrDefault(x => x.Id == id);
                if (image == null)
                {
                    return NotFound();
                }

                _context.Images.Remove(image);
                _context.SaveChanges();

                var imageDto = _mapper.Map<ImageResponseDto>(image);

                return Ok(imageDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }

        }
    }
}
