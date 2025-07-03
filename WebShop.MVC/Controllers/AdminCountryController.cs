using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Models;
using WebShop.DAL.Services.CountryServices;
using WebShop.DAL.Services.ProductCountryServices;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminCountryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICountryService _countryService;
        private readonly IProductCountryService _productCountryService;
        public AdminCountryController(
            IMapper mapper,
            ICountryService countryService,
            IProductCountryService productCountryService)
        {
            _mapper = mapper;
            _countryService = countryService;
            _productCountryService = productCountryService;
        }


        public IActionResult Index()
        {
            var countries = _countryService.GetAll();
            var countryViewModels = _mapper.Map<IEnumerable<CountryVM>>(countries);

            return View(countryViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CountryCreateVM countryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(countryVM);
            }
            var country = _mapper.Map<Country>(countryVM);
            _countryService.Create(country);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var country = _countryService.GetById(id);
            if (country == null)
            {
                return NotFound();
            }
            var countryVM = _mapper.Map<CountryEditVM>(country);
            return View(countryVM);
        }

        [HttpPost]
        public IActionResult Edit(CountryEditVM countryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(countryVM);
            }
            var country = _mapper.Map<Country>(countryVM);
            _countryService.Update(country);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var country = _countryService.GetById(id);
            if (country == null)
            {
                return NotFound();
            }
            _productCountryService.UnLinkAllForCountry(id);

            _countryService.Delete(country);
            return RedirectToAction(nameof(Index));
        }
    }
}
