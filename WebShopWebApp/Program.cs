using Microsoft.EntityFrameworkCore;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.CartRepo;
using WebShop.DAL.Repositories.CategoryRepo;
using WebShop.DAL.Repositories.CountryRepo;
using WebShop.DAL.Repositories.ImageRepo;
using WebShop.DAL.Repositories.LogRepo;
using WebShop.DAL.Repositories.ProductCountryRepo;
using WebShop.DAL.Repositories.ProductRepo;
using WebShop.DAL.Repositories.UserRepo;
using WebShop.DAL.Services.CartServices;
using WebShop.DAL.Services.CategoryServices;
using WebShop.DAL.Services.CountryServices;
using WebShop.DAL.Services.ImageServices;
using WebShop.DAL.Services.LogServices;
using WebShop.DAL.Services.ProductCountryServices;
using WebShop.DAL.Services.ProductService;
using WebShop.DAL.Services.UserServices;
using WebShopWebApp.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IProductCountryRepository, ProductCountryRepository>();
builder.Services.AddScoped<IProductCountryService, ProductCountryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<ILogRepository, LogRepository>();


builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthentication()
  .AddCookie(options =>
  {
      options.LoginPath = "/User/Login";
      options.LogoutPath = "/User/Logout";
      options.AccessDeniedPath = "/User/Forbidden";
      options.SlidingExpiration = true;
      options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
  });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
