using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.ImageRepo;
using WebShop.DAL.Services.ImageServices;
using WebShopWebApi.DTOs;

namespace WebShopWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        public ImageController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }

        // GET: api/<ImageController>
        [HttpGet("product/{productId}")]
        public ActionResult<IEnumerable<ImageResponseDto>> GetAll(int productId)
        {
            try
            {
                var images = _imageService.GetAllByProductId(productId)
                    .Select(image => _mapper.Map<ImageResponseDto>(image))
                    .ToList();

                return Ok(images);
            }
            catch (Exception)
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
                var image = _imageService.GetById(id);
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
                _imageService.Create(image);

                return Ok(imageDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while connecting to the database. Please try again later.");
            }
        }

        // PUT api/<ImageController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ImageUpdateDto imageDto)
        {
            try
            {
                var image = _imageService.GetById(id);
                if (image == null)
                {
                    return NotFound();
                }

                _mapper.Map(imageDto, image);

                _imageService.Update(image);

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
                var image = _imageService.GetById(id);
                if (image == null)
                {
                    return NotFound();
                }

                _imageService.Delete(image);

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
