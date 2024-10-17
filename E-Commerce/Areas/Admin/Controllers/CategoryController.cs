using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            var Categories = _context.categories.ToList();
            return View(Categories);
        }
        [HttpGet]
        //this action is in form of get because it generates new page
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken] //Protects from Cross Side Frgery Attacks
        //we need to get info from user
        //Controller is the link between model and the view
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.categories.Add(category);
                _context.SaveChanges();
                TempData["Create"] = "Data Has Been Deleted Succesfully";

                return RedirectToAction("Index");
            }
            return View(category); //what was written remains

        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null | Id == 0)
            {
                NotFound();
            }


            Category? categoryIndb = _context.categories.Find(Id);
            return View(categoryIndb);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.categories.Update(category);
                _context.SaveChanges();
                TempData["Update"] = "Data Has Been Updated Succesfully";

                return RedirectToAction("Index");
            }

            return View(category);

        }
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null | Id == 0)
            {
                return NotFound();
            }


            Category? categoryIndb = _context.categories.Find(Id);
            return View(categoryIndb);

        }


        [HttpPost]
        public IActionResult DeleteCategory(int? Id) //Different name because same parameters 
        {


            // Find the category in the database by Id
            var categoryIndb = _context.categories.Find(Id);

            // Check if the category was not found
            if (categoryIndb == null)
            {
                return NotFound(); // Return NotFound if category is not in the database
            }

            // Remove the found category from the database
            _context.categories.Remove(categoryIndb);

            // Save changes to persist the removal
            _context.SaveChanges();
            TempData["Delete"] = "Data Has Been Deleted Succesfully";

            // Redirect to the Index action after successful deletion
            return RedirectToAction("Index");
        }


    }
}
