using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

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

        
        public  IActionResult Details(int ProductId)
        {




            ShoppingCart obj = new()
            {
                ProductId = ProductId,
                Product = _context.products
                                         .Include(p => p.Category)
                                         .FirstOrDefault(p => p.Id == ProductId),
                Count = 1
            };

            return View(obj);
        }

        // Open cart page and proceed to payment
        // Should authorize to make sure that the user is registered on site
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            // Do not set shoppingCart.Id or shoppingCart.Product.Id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var Claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = Claim.Value;

            // Add the shopping cart to the context asynchronously
            _context.ShoppingCarts.Add(shoppingCart); // Use AddAsync
             _context.SaveChanges(); // Use SaveChangesAsync

            return RedirectToAction("Index");
        }

    }
}
