using GoFarm.DataAccess.Data.Repository.IRepository;
using GoFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoFarm.DataAccess.Data.Repository
{
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{


		private readonly ApplicationDbContext _db;


		public OrderHeaderRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		
	}
}
