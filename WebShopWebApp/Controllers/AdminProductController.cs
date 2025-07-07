using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.ProductCountryRepo;
using WebShop.DAL.Services.CategoryServices;
using WebShop.DAL.Services.CountryServices;
using WebShop.DAL.Services.ImageServices;
using WebShop.DAL.Services.ProductCountryServices;
using WebShop.DAL.Services.ProductService;
using WebShopWebApp.ViewModels;

namespace WebShopWebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICountryService _countryService;
        private readonly IImageService _imageService;
        private readonly ICategoryService _categoryService;
        private readonly IProductCountryService _productCountryService;
        private readonly IConfiguration _configuration;

        public AdminProductController(
            IMapper mapper,
            IProductService service,
            ICountryService countryService,
            IImageService imageService,
            ICategoryService categoryService,
            IProductCountryService productCountryService,
            IConfiguration configuration
            )
        {
            _mapper = mapper;
            _productService = service;
            _countryService = countryService;
            _imageService = imageService;
            _categoryService = categoryService;
            _productCountryService = productCountryService;
            _configuration = configuration;
        }

        public IActionResult Index(AdminSearchVM searchVm)
        {
            if (searchVm.Page == 0) searchVm.Page = 1;
            if (searchVm.Size == 0) searchVm.Size = 10;

            var query = _productService.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchVm.Q))
            {
                query = query.Where(p => p.Name.Contains(searchVm.Q));
            }

            switch (searchVm.OrderBy?.ToLower())
            {
                case "name":
                    query = query.OrderBy(p => p.Name);
                    break;
                case "price":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "brand":
                    query = query.OrderBy(p => p.Brand);
                    break;
                case "category":
                    query = query.OrderBy(p => p.Category.Name);
                    break;
                default:
                    query = query.OrderBy(p => p.Id);
                    break;
            }

            var totalItems = query.Count();

            query = query.Skip((searchVm.Page - 1) * searchVm.Size).Take(searchVm.Size);

            var productList = query.ToList();
            var productVMs = _mapper.Map<List<ProductResponseVM>>(productList);

            searchVm.Products = productVMs;
            searchVm.LastPage = (int)Math.Ceiling(totalItems / (double)searchVm.Size);
            searchVm.FromPager = Math.Max(1, searchVm.Page - 2);
            searchVm.ToPager = Math.Min(searchVm.LastPage, searchVm.Page + 2);

            return View(searchVm);
        }


        [HttpGet]
        public IActionResult Create()
        {
            InsertCountries();
            InsertCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM productCreate)
        {
            if (!ModelState.IsValid)
            {
                InsertCountries();
                InsertCategories();
                return View(productCreate);
            }
            bool exists = _productService
                                   .GetAll()
                                   .Any(p => p.Name.Trim().ToLower() == productCreate.Name.Trim().ToLower());

            if (exists)
            {
                ModelState.AddModelError("Name", "A product with this name already exists.");
                InsertCountries();
                InsertCategories();
                return View(productCreate);
            }

            var product = _mapper.Map<Product>(productCreate);
            _productService.Create(product);

            await SaveImages(productCreate.UploadedImages, product.Id, product.Images.ToList());

            return RedirectToAction("Index");
        }

        private async Task SaveImages(List<IFormFile> files, int productId, List<Image> existingImages)
        {
            var existingContents = existingImages.Select(img => img.Content).ToHashSet();

            var imageTasks = files.Select(async file =>
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                var imageBytes = ms.ToArray();
                var content = $"data:{file.ContentType};base64,{Convert.ToBase64String(imageBytes)}";

                if (!existingContents.Contains(content))
                {
                    return new Image
                    {
                        Content = content,
                        ProductId = productId
                    };
                }

                return null;
            });

            var images = (await Task.WhenAll(imageTasks)).Where(img => img != null).ToList();

            if (images.Any())
                _imageService.AddRange(images);
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
            productEditVM.Images = product.Images.ToList();
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
            ViewBag.Images = productEditVM.Images;
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
        public async Task<IActionResult> Edit(ProductEditVM editVM)
        {
            if (!ModelState.IsValid)
            {
                PopulateItemTypes(editVM);
                return View(editVM);
            }

            var product = _productService.GetWithDetails(editVM.Id);
            if (product == null)
                return NotFound();

            _mapper.Map(editVM, product);

            var existingCountryIds = product.ProductCountries.Select(pc => pc.CountryId).ToList();

            RemoveUnSelectedCountries(editVM, existingCountryIds);

            _productService.Update(product);

            await SaveImages(editVM.UploadedImages ?? new List<IFormFile>(), product.Id, product.Images.ToList());

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
                        _productCountryService.UnLink(editVM.Id, countryId);
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

        [HttpPost]
        public IActionResult DeleteImage(int id, int productId)
        {
            var image = _imageService.GetById(id);
            if (image == null)
            {
                return NotFound();
            }
            _imageService.Delete(image);
            return Json(new { success = true });
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