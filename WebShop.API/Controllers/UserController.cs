using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.API.Models;
using WebShop.API.DTOs;
using WebShop.API.Security;
using AutoMapper;


namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly WebShopContext _context;
        private readonly IMapper _mapper;

        public UserController(IConfiguration configuration, WebShopContext context, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public ActionResult<UserRegisterDto> Register(UserRegisterDto registerDto)
        {
            try
            {
                // Check if there is such a username in the database already
                var trimmedUsername = registerDto.Username.Trim();
                if (_context.Users.Any(x => x.Username.Equals(trimmedUsername)))
                    return BadRequest($"Username {trimmedUsername} already exists");

                // Hash the password
                var b64salt = PasswordHashProvider.GetSalt();
                var b64hash = PasswordHashProvider.GetHash(registerDto.Password, b64salt);

                // Create user from DTO and hashed password
                var user = _mapper.Map<User>(registerDto);
                user.PwdHash = b64hash;
                user.PwdSalt = b64salt;

                // Add user and save changes to database
                _context.Add(user);
                _context.SaveChanges();

                // Update DTO Id to return it to the client
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

                // Try to get a user from database
                var existingUser = _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(x => x.Username == loginDto.Username);

                if (existingUser == null)
                    return Unauthorized(genericLoginFail);

                // Check is password hash matches
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
