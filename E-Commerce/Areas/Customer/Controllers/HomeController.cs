using E_Commerce.Data;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;

        }
        //Index: home page or default view
        public IActionResult Index()
        {
            var products = _context.products.ToList();
            return View(products);


  
        }

        public IActionResult Details(int Id)
        {
            ShoppingCart shoppingCart = new ShoppingCart()
            {

                Product = _context.products
                                  .Include(p => p.Category)
                                  .FirstOrDefault(p => p.Id == Id),
                Count = 1


            };

            
            
            return View(shoppingCart);



        }
    }
}
