using E_Commerce.DataAccess;
using E_commerce.Entities.Models;
using E_commerce.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DataAccess.Implementation
{
	public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
	{
		private readonly ApplicationDbContext _context;
		public OrderDetailRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(OrderDetail Orderdetail)
		{
			_context.OrderDetails.Update(Orderdetail);
		}
	}
}
