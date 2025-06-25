using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.API.DTOs;
using WebShop.DAL.Models;
using WebShop.DAL.Services.LogServices;

namespace WebShop.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        public LogController(ILogService logService, IMapper mapper)
        {
            _logService = logService;
            _mapper = mapper;
        }

        // GET: api/<LogController>
        [HttpGet]
        public ActionResult<IEnumerable<LogResponseDto>> Get()
        {
            var logs = _logService.GetAll();
            var logResponseDtos = logs.Select(log => _mapper.Map<LogResponseDto>(log)).ToList();
            return Ok(logResponseDtos);

        }

        // GET api/<LogController>/5
        [HttpGet("{n}")]
        public ActionResult<IEnumerable<LogResponseDto>> Get(int n)
        {
            var logs = _logService.Get(n);

            return Ok(logs.Select(log => _mapper.Map<LogResponseDto>(log)).ToList());

        }

        // GET api/<LogController>/count
        [HttpGet("count")]
        public ActionResult<int> GetCount()
        {
            return Ok(_logService.GetCount());
        }

        // POST api/<LogController>
        [HttpPost]
        public ActionResult Post([FromBody] LogCreateDto value)
        {
            if (value == null)
            {
                return BadRequest("Log cannot be null.");
            }
            var log = _mapper.Map<Log>(value);
            _logService.Add(log);
            return CreatedAtAction(nameof(Get), value);
        }


        // DELETE api/<LogController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var log = _logService.Get(id).FirstOrDefault();
                _logService.Delete(id);
                return Ok(log);
            }
            catch (Exception)
            {
                return NotFound($"Log with id {id} not found.");
            }
        }
    }
}
