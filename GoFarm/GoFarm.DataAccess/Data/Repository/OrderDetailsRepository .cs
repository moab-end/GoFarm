using GoFarm.DataAccess.Data.Repository.IRepository;
using GoFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoFarm.DataAccess.Data.Repository
{
	public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
	{


		private readonly ApplicationDbContext _db;


		public OrderDetailsRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		
	}
}
