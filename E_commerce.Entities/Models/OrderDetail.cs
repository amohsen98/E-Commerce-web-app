using E_Commerce.Entites.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Entities.Models
{
	public class OrderDetail
	{
		public int Id { get; set; }

		public int OrderHeaderId { get; set; }
		[ValidateNever]
		//Navigation Property
		public OrderHeader OrderHeader { get; set; }

		public int ProductId { get; set; }
		[ValidateNever]
		public Product Product { get; set; }

		public int Price { get; set; }

		public int Count { get; set; }

	}
}
