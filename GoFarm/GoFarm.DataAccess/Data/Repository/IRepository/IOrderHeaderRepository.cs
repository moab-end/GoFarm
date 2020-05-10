using GoFarm.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace GoFarm.DataAccess.Data.Repository.IRepository
{
	public interface IOrderHeaderRepository : IRepository<OrderHeader>
	{
		public void ChangeOrderStatus(int orderHeaderId, string status);
	}
}
