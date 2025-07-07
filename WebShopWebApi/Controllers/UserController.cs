using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebShop.DAL.Models;
using WebShop.DAL.Security;
using WebShop.DAL.Services.UserServices;
using WebShopWebApi.DTOs;

namespace WebShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IConfiguration configuration, IMapper mapper, IUserService userService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("[action]")]
        public ActionResult<UserRegisterDto> Register(UserRegisterDto registerDto)
        {
            try
            {
                var trimmedUsername = registerDto.Username.Trim();
                if (_userService.GetAllUsers().Any(x => x.Username.Equals(trimmedUsername)))
                    return BadRequest($"Username {trimmedUsername} already exists");

                var b64salt = PasswordHashProvider.GetSalt();
                var b64hash = PasswordHashProvider.GetHash(registerDto.Password, b64salt);

                var user = _mapper.Map<User>(registerDto);
                user.PwdHash = b64hash;
                user.PwdSalt = b64salt;

                _userService.CreateUser(user);

                registerDto.Id = user.Id;

                return Ok(registerDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult Login(UserLoginDto loginDto)
        {
            try
            {
                var genericLoginFail = "Incorrect username or password";

                var existingUser = _userService.GetAllUsers()
                    .FirstOrDefault(x => x.Username == loginDto.Username);

                if (existingUser == null)
                    return Unauthorized(genericLoginFail);

                var b64hash = PasswordHashProvider.GetHash(loginDto.Password, existingUser.PwdSalt);
                if (b64hash != existingUser.PwdHash)
                    return Unauthorized(genericLoginFail);

                var secureKey = _configuration["JWT:SecureKey"];
                int expiration = _configuration.GetValue<int>("JWT:Expiration");
                var serializedToken = JwtTokenProvider.CreateToken(secureKey, expiration, loginDto.Username, existingUser.Role.Name);

                return Ok(serializedToken);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
