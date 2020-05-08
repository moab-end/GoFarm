﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GoFarm.DataAccess.Data.Repository.IRepository
{
	public interface IUnitOfWork : IDisposable
	{
		public ICategoryRepository Category { get; }

		public IFrequencyRepository Frequency { get; }

		public IServiceRepository Service { get; }
		public IOrderHeaderRepository OrderHeader { get; }
		public IOrderDetailsRepository OrderDetails { get; }
		void Save();
	}
}
