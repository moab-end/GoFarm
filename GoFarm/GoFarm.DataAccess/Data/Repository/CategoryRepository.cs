using GoFarm.DataAccess.Data.Repository.IRepository;
using GoFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace GoFarm.DataAccess.Data.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{


		private readonly ApplicationDbContext _db;


		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public IEnumerable<SelectListItem> GetCategoryListForDropDown()
		{
			return _db.Category.Select(i => new SelectListItem()
			{

				Text = i.Name,
				Value = i.Id.ToString()

			});
		}

		public void Update(Category category)
		{
			var toBeUpdated = _db.Category.FirstOrDefault(s => s.Id == category.Id);

			toBeUpdated.Name = category.Name;
			toBeUpdated.DisplayOrder = category.DisplayOrder;

			_db.SaveChanges();
		}
	}
}
