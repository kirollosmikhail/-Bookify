﻿
using Bookify.Core.Models;
using Microsoft.AspNetCore.Mvc;


namespace Bookify.Controllers
{
	public class CategoriesController : Controller
	{
		private ApplicationDbContext _context;

		public CategoriesController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			//TODO : use viewModel
			var categories = _context.Categories.AsNoTracking().ToList();
			return View(categories);
		}
		[HttpGet]
        public IActionResult Create()
        {

            return View("Form");
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		//cross side request forgery attack
        public IActionResult Create(CategoryFormViewModel model)
        {
			if (!ModelState.IsValid)
				return View("Form", model);
			
			var category  = new Category { Name = model.Name };
			_context.Add(category);
			_context.SaveChanges();

			TempData["Message"] = "Saved Successfully";


            return RedirectToAction(nameof(Index));
        }
		[HttpGet]
        public IActionResult Edit(int id)
        {
			var category = _context.Categories.Find(id);

			if(category is null)
				return NotFound();
			var viewModel = new CategoryFormViewModel
			{
				Id = id,
				Name = category.Name
            };
            return View("Form",viewModel);
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryFormViewModel model)
        {
			if (!ModelState.IsValid)
				return View(model);
			
            var category = _context.Categories.Find(model.Id);

            if (category is null)
                return NotFound();
            
			category.Name = model.Name;
			category.LastUpdatedOn = DateTime.Now;
            TempData["Message"] = "Saved Successfully";

            _context.SaveChanges();


			return RedirectToAction(nameof(Index));
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ToggleStatus(int id)
		{
			var category = _context.Categories.Find(id);
            if (category is null)
                return NotFound();

			category.IsDeleted = !category.IsDeleted;
			category.LastUpdatedOn = DateTime.Now;
			_context.SaveChanges();

			return Ok(category.LastUpdatedOn.ToString());

        }
    }
}