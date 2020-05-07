using System;
using System.Collections.Generic;
using System.Text;

namespace GoFarm.Models.ViewModels
{
	public class HomeViewModel
	{

		public IEnumerable<Category> CategoryList { get; set; }
		public IEnumerable<Service> ServiceList { get; set; }
	}
}
