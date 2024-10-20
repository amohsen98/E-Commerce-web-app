using E_commerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Entities.Repositories
{
	public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
	{
		void Update(OrderDetail orderDetail);
	}
}
