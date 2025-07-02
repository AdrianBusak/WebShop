using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebShop.DAL.Services.UserServices;
using TapNGoMVC.ViewModels;
using WebShop.DAL.Security;
using WebShop.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Login(UserLoginVM loginVM)
        {
            try
            {
                var genericLoginFail = "Incorrect username or password";

                var existingUser = _userService.GetAllUsers()
                    .FirstOrDefault(x => x.Username == loginVM.Username);

                if (existingUser == null)
                {
                    ModelState.AddModelError("", genericLoginFail);
                    return View();
                }

                var b64hash = PasswordHashProvider.GetHash(loginVM.Password, existingUser.PwdSalt);
                if (b64hash != existingUser.PwdHash)
                {
                    ModelState.AddModelError("", genericLoginFail);
                    return View();
                }

                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, loginVM.Username),
                    new Claim("FirstName", existingUser.FirstName),
                    new Claim("LastName", existingUser.LastName),
                    new Claim(ClaimTypes.Role, existingUser.Role.Name)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties();

                Task.Run(async () =>
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties)
                ).GetAwaiter().GetResult();

                if (loginVM.ReturnUrl != null)
                    return LocalRedirect(loginVM.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: UserController/Login
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            UserLoginVM loginVM = new UserLoginVM
            {
                ReturnUrl = returnUrl
            };

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // GET: UserController/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: UserController/Register
        [HttpPost]
        public IActionResult Register(UserRegisterVM userVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (userVM.AddAdmin)
                {
                    var correctAdminPassword = _configuration["AdminPassword"];
                    if (userVM.PasswordAdmin != correctAdminPassword)
                    {
                        ModelState.AddModelError("", "Invalid admin password");
                        return View();
                    }

                    userVM.RoleId = 1; // admin
                }
                else
                {
                    userVM.RoleId = 2; // user
                }

                var trimmedUsername = userVM.Username.Trim();
                if (_userService.GetAllUsers().Any(x => x.Username.Equals(trimmedUsername)))
                {
                    ModelState.AddModelError("", "Username already exists");
                    return View();
                }
                var b64salt = PasswordHashProvider.GetSalt();
                var b64hash = PasswordHashProvider.GetHash(userVM.Password, b64salt);

                var user = _mapper.Map<User>(userVM);
                user.PwdHash = b64hash;
                user.PwdSalt = b64salt;

                _userService.CreateUser(user);

                return RedirectToAction("RegisterConfirmation", "User", userVM);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult RegisterConfirmation(UserRegisterVM userVM)
        {
            return View(userVM);
        }

        [Authorize]
        public IActionResult ProfileDetails()
        {
            var username = HttpContext.User.Identity.Name;

            var userDb = _userService.GetUserByUsername(username);
            if (userDb != null)
            {
                var userVm = _mapper.Map<UserProfileVM>(userDb);
                return View(userVm);
            }
            return RedirectToAction("Index","AdminProduct");
        }

        [Authorize]
        public IActionResult ProfileEdit(int id)
        {
            var userDb = _userService.GetUser(id);
            var userVm = _mapper.Map<UserEditProfileVM>(userDb);

            return View(userVm);
        }

        [Authorize]
        [HttpPut]
        public IActionResult ProfileEdit([FromBody]UserEditProfileVM userVm)
        {
            if (!ModelState.IsValid)
            {
                return View(userVm);
            }
            var userDb = _userService.GetUserByUsername(userVm.Username);
            if (userDb == null)
            {
                return RedirectToAction("ProfileDetails");
            }

            _mapper.Map(userVm, userDb);

            userDb.PwdSalt = PasswordHashProvider.GetSalt();
            userDb.PwdHash = PasswordHashProvider.GetHash(userVm.Password, userDb.PwdSalt);
            userDb.RoleId = userDb.RoleId;

            _userService.UpdateUser(userDb);
            return RedirectToAction("ProfileDetails");
        }

    }
}
