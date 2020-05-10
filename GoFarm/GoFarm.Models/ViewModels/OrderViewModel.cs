using System;
using System.Collections.Generic;
using System.Text;

namespace GoFarm.Models.ViewModels
{
	public class OrderViewModel
	{
		public OrderHeader OrderHeader { get; set; }

		public IEnumerable<OrderDetails> OrderDetails { get; set; }
	}
}
