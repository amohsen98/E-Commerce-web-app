using E_commerce.Entities.Repositories;
using E_commerce.Entities.ViewModels;
using E_Commerce.Entites.Models;
using E_Commerce.Entities.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; } //instance of shoppingcartVM

        public CartController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                //Get list of carts of corrent user who is logged right now
                 CartsList = _unitOfWork.ShoppingCart.GetAll(u=>u.ApplicationUserId == claim.Value , Includeword : "Product")

            };
            foreach (var item in ShoppingCartVM.CartsList)
            {
				ShoppingCartVM.TotalCarts += (item.Count * int.Parse(item.Product.Price));


            }

            return View(ShoppingCartVM );
        }

		//The plus count affect database (shoppingcart table)
		public IActionResult Plus(int cartid)
		{
			var shoppingcart = _unitOfWork.ShoppingCart.GetFirstorDefault(x => x.Id == cartid);
			_unitOfWork.ShoppingCart.IncreaseCount(shoppingcart, 1);
			_unitOfWork.Complete();
			return RedirectToAction("Index");
		}

		public IActionResult Minus(int cartid)
		{
			var shoppingcart = _unitOfWork.ShoppingCart.GetFirstorDefault(x => x.Id == cartid);

			if (shoppingcart.Count <= 1)
			{
				_unitOfWork.ShoppingCart.Remove(shoppingcart);
				_unitOfWork.Complete();
				return RedirectToAction("Index", "Home");


			}
			else
			{
				_unitOfWork.ShoppingCart.DecreaseCount(shoppingcart, 1);

			}
			_unitOfWork.Complete();
			return RedirectToAction("Index");
		}
		public IActionResult Remove(int cartid) {


			var shoppingcart = _unitOfWork.ShoppingCart.GetFirstorDefault(x => x.Id == cartid);
			_unitOfWork.ShoppingCart.Remove(shoppingcart);
			_unitOfWork.Complete();
			return RedirectToAction("Index");
		}

	}
}
