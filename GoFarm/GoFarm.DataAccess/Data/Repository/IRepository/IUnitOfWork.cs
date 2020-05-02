using System;
using System.Collections.Generic;
using System.Text;

namespace GoFarm.DataAccess.Data.Repository.IRepository
{
	public interface IUnitOfWork : IDisposable
	{
		public ICategoryRepository Category { get; }

		void Save();
	}
}
