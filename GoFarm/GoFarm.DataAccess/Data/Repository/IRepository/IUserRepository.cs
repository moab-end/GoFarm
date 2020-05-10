using GoFarm.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace GoFarm.DataAccess.Data.Repository.IRepository
{
	public interface IUserRepository : IRepository<ApplicationUser>
	{
		void LockUser(string userId);

		void UnlockUser(string userId);
	}
}
