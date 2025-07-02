using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.ProductCountryRepo;
using WebShop.DAL.Services.CategoryServices;
using WebShop.DAL.Services.CountryServices;
using WebShop.DAL.Services.ImageServices;
using WebShop.DAL.Services.ProductCountryServices;
using WebShop.DAL.Services.ProductService;
using WebShop.MVC.Models;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICountryService _countryService;
        private readonly IImageService _imageService;
        private readonly ICategoryService _categoryService;
        private readonly IProductCountryService _productCountryService;

        public AdminProductController(
            IMapper mapper,
            IProductService service,
            ICountryService countryService,
            IImageService imageService,
            ICategoryService categoryService,
            IProductCountryService productCountryService)
        {
            _mapper = mapper;
            _productService = service;
            _countryService = countryService;
            _imageService = imageService;
            _categoryService = categoryService;
            _productCountryService = productCountryService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAll();
            var productViewModels = _mapper.Map<IEnumerable<ProductResponseVM>>(products);

            return View(productViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            InsertCountries();
            InsertCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCreateVM productCreate)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productCreate);
                _productService.Create(product);
                return RedirectToAction("Index");
            }
            return View(productCreate);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetWithDetails(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductEditVM productEditVM = FillEditVM(product);

            return View(productEditVM);
        }

        private ProductEditVM FillEditVM(Product product)
        {
            var productEditVM = _mapper.Map<ProductEditVM>(product);

            productEditVM.SelectedCountriesIds = product.ProductCountries.Select(pc => pc.CountryId).ToList();
            productEditVM.SelectedImagesIds = product.Images.Select(i => i.Id).ToList();
            PopulateItemTypes(productEditVM);
            return productEditVM;
        }

        private void PopulateItemTypes(ProductEditVM productEditVM)
        {
            InsertCountries();
            InsertCategories();
            InsertImages(productEditVM);
        }

        private void InsertCategories()
        {
            ViewBag.Categories = _categoryService
                            .GetAll()
                            .Select(c => new SelectListItem
                            {
                                Value = c.Id.ToString(),
                                Text = c.Name,
                            });
        }

        private void InsertImages(ProductEditVM productEditVM)
        {
            ViewBag.Images = _imageService
                             .GetAllByProductId(productEditVM.Id)
                             .Select(c => new SelectListItem
                             {
                                 Value = c.Id.ToString(),
                                 Text = c.Content,
                             }).ToList();
        }

        private void InsertCountries()
        {
            ViewBag.Countries = _countryService
                            .GetAll()
                            .Select(c => new SelectListItem
                            {
                                Value = c.Id.ToString(),
                                Text = c.Name,
                            }).ToList();
        }

        [HttpPost]
        public IActionResult Edit(ProductEditVM editVM)
        {
            if (!ModelState.IsValid)
            {
                PopulateItemTypes(editVM);
                return View(editVM);
            }

            var product = _productService.GetWithDetails(editVM.Id);
            if (product == null)
                return NotFound();

            // Mapiraj promjene u postojeći entitet
            _mapper.Map(editVM, product);

            // Ručno mapiraj slike (dodavanje/uklanjanje)
            var currentImageIds = product.Images.Select(i => i.Id).ToList();
            var selectedImageIds = editVM.SelectedImagesIds ?? new List<int>();

            // Dodaj nove slike
            foreach (var imageId in selectedImageIds.Except(currentImageIds))
            {
                var image = _imageService.GetById(imageId);
                if (image != null && image.ProductId != product.Id)
                {
                    image.ProductId = product.Id;
                    _imageService.Update(image);
                }
            }

            // Ukloni slike koje više nisu selektirane
            foreach (var imageId in currentImageIds.Except(selectedImageIds))
            {
                var image = _imageService.GetById(imageId);

                if (image != null && image.ProductId == product.Id)
                {
                    image.ProductId = null;
                    _imageService.Update(image);
                }
            }


            var existingCountryIds = product.ProductCountries.Select(pc => pc.CountryId).ToList();

            // Dodaj nove veze makni stare
            RemoveUnSelectedCountries(editVM, existingCountryIds);

            _productService.Update(product);

            return RedirectToAction("Index");
        }

        private void RemoveUnSelectedCountries(ProductEditVM editVM, List<int> existingCountryIds)
        {
            if (editVM.SelectedCountriesIds != null)
            {

                foreach (var countryId in existingCountryIds.Except(editVM.SelectedCountriesIds))
                {
                    try
                    {
                        _productCountryService.Unlink(editVM.Id, countryId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error unlinking product with country {countryId}: {ex.Message}");
                    }
                }

                try
                {
                    foreach (var countryId in editVM.SelectedCountriesIds.Except(existingCountryIds))
                    {
                        try
                        {
                            _productCountryService.Link(editVM.Id, countryId);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error linking country {countryId}: {ex.Message}");
                        }
                    }

                }
                catch (Exception ignore)
                {
                    Console.WriteLine($"Error linking product with country: {ignore.Message}");
                }


            }
        }

        public IActionResult Delete(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productService.Delete(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _productService.GetWithDetails(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDetailsVM = _mapper.Map<ProductDetailsVM>(product);
            return View(productDetailsVM);
        }
    }
}
