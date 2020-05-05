using GoFarm.DataAccess.Data.Repository.IRepository;
using GoFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoFarm.DataAccess.Data.Repository
{
	public class FrequencyRepository : Repository<Frequency>, IFrequencyRepository
	{
		private readonly ApplicationDbContext _db;

		public FrequencyRepository(ApplicationDbContext db):base(db)
		{
			_db = db;
		}
		public IEnumerable<SelectListItem> GetFrequencyListForDropDown()
		{
			return _db.Frequency.Select(i => new SelectListItem()
			{

				Text = i.Name,
				Value = i.Id.ToString()

			});
		}

		public void Update(Frequency frequency)
		{
			var toBeUpdated = _db.Frequency.FirstOrDefault(s=>s.Id==frequency.Id);

			toBeUpdated.Name = frequency.Name;
			toBeUpdated.FrequencyCounty = frequency.FrequencyCounty;

			_db.SaveChanges();


		}
	}
}
