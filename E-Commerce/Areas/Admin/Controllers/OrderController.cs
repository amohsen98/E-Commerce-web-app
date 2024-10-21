using E_commerce.DataAccess.Implementation;
using E_commerce.Entities.Models;
using E_commerce.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Entities.ViewModels;
using E_commerce.Entities.Utility;
using Stripe;

namespace E_Commerce.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrderController : Controller
	{
        private IUnitOfWork _unitOfWork;
		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


            public IActionResult Index()
		{

			return View();
		}

		//will be sent over via order.js file using ajax 
		public IActionResult GetData()
		{
			IEnumerable<OrderHeader> orderHeaders;
			orderHeaders = _unitOfWork.OrderHeader.GetAll(Includeword: "ApplicationUser");
			return Json(new { data = orderHeaders }); //will be used in order.js file
		}

		[HttpGet]
		//when you press on site you passing some id so make sure that you are retreiving same id from Db
		public IActionResult Details(int orderid)
		{

			OrderVM orderVM = new OrderVM()
			{

				OrderHeader = _unitOfWork.OrderHeader.GetFirstorDefault(u => u.Id == orderid, Includeword: "ApplicationUser"),
				OrderDetails = _unitOfWork.OrderDetail.GetAll(x => x.OrderHeaderId == orderid, Includeword: "Product")

			};

			return View(orderVM);

			
		}



	}
}
