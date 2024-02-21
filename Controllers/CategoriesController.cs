
using Bookify.Core.Models;
using Bookify.Filters;
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
		[AjaxOnly]
        public IActionResult Create()
        {

            return PartialView("_Form");
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		//cross side request forgery attack
        public IActionResult Create(CategoryFormViewModel model)
        {
			if (!ModelState.IsValid)
				return BadRequest();
			
			var category  = new Category { Name = model.Name };
			_context.Add(category);
			_context.SaveChanges();

            return PartialView("_CategoryRow",category);
        }
		[HttpGet]
        [AjaxOnly]
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
            return PartialView("_Form",viewModel);
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryFormViewModel model)
        {
			if (!ModelState.IsValid)
				return BadRequest();
			
            var category = _context.Categories.Find(model.Id);

            if (category is null)
                return NotFound();
            
			category.Name = model.Name;
			category.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();


            return PartialView("_CategoryRow", category);
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