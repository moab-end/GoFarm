using GoFarm.DataAccess.Data.Repository.IRepository;
using GoFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoFarm.DataAccess.Data.Repository
{
	public class ServiceRepository : Repository<Service>, IServiceRepository
	{


		private readonly ApplicationDbContext _db;


		public ServiceRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		
		

		public void Update(Service service)
		{
			var toBeUpdated = _db.Service.FirstOrDefault(s => s.Id == service.Id);

			toBeUpdated.Name = service.Name;
			toBeUpdated.Price = service.Price;
			toBeUpdated.LongDesc = service.LongDesc;
			toBeUpdated.ImageUrl = service.ImageUrl;
			toBeUpdated.FrequencyId = service.FrequencyId;
			toBeUpdated.CategoryId = service.CategoryId;

			_db.SaveChanges();
		}

		
	}
}
