using E_commerce.DataAccess.Implementation;
using E_commerce.Entities.Models;
using E_commerce.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;

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

		public IActionResult GetData()
		{
			IEnumerable<OrderHeader> orderHeaders;
			orderHeaders = _unitOfWork.OrderHeader.GetAll(Includeword: "ApplicationUser");
			return Json(new { data = orderHeaders });
		}

	}
}
