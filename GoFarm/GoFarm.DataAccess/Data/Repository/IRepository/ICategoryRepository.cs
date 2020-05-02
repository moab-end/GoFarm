using GoFarm.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;


namespace GoFarm.DataAccess.Data.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		IEnumerable<SelectListItem> GetCategoryListForDropDown();

		void Update(Category category);
	}
}
