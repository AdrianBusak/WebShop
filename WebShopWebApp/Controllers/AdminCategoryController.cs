﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Models;
using WebShop.DAL.Services.CategoryServices;
using WebShopWebApp.ViewModels;

namespace WebShopWebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminCategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public AdminCategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            var categoryVM = _mapper.Map<IEnumerable<CategoryVM>>(categories);

            return View(categoryVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                bool exists = _categoryService
                    .GetAll()
                    .Any(c => c.Name.Trim().ToLower() == categoryVM.Name.Trim().ToLower());

                if (exists)
                {
                    ModelState.AddModelError("Name", "A category with this name already exists.");
                    return View(categoryVM);
                }

                var category = _mapper.Map<Category>(categoryVM);
                _categoryService.Create(category);
                return RedirectToAction(nameof(Index));
            }

            return View(categoryVM);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryVM = _mapper.Map<CategoryEditVM>(category);
            return View(categoryVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, CategoryEditVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                var updatedCategory = _mapper.Map<Category>(categoryVM);
                var result = _categoryService.Update(id, updatedCategory);
                if (result == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryVM);
        }

        public IActionResult Delete(int id)
        {
            var result = _categoryService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
