
using E_commerce.Entities.Repositories;
using E_Commerce.DataAccess;
using E_Commerce.Entites;
using E_Commerce.Entites.Models;
using E_Commerce.Entites.Utility;
using E_Commerce.Entities.Utility;
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
        private readonly IUnitOfWork _unitofwork;

        public HomeController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public IActionResult Index()
        {

            var products = _unitofwork.Product.GetAll();
            return View(products);
            
        }

        public IActionResult Details(int ProductId)
        {
            ShoppingCart obj = new ShoppingCart()
            {
                ProductId = ProductId,
                Product = _unitofwork.Product.GetFirstorDefault(v => v.Id == ProductId, Includeword: "Category"),
                Count = 1
            };
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;


            ShoppingCart Cartobj = _unitofwork.ShoppingCart.GetFirstorDefault(
                u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);
            if (Cartobj == null)
            {
                _unitofwork.ShoppingCart.Add(shoppingCart);
                _unitofwork.Complete();

            }
            else
            {
                _unitofwork.ShoppingCart.IncreaseCount(Cartobj, shoppingCart.Count);
                _unitofwork.Complete();

            }





            return RedirectToAction("Index");
        }


    }
}



