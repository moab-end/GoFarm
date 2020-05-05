﻿using System;
using System.Collections.Generic;
using System.Text;
using GoFarm.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoFarm.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options){

			
		}
		public DbSet<Category> Category { get; set; }

		public DbSet<Frequency> Frequency { get; set; }

		public DbSet<Service> Service { get; set; }
	}
}
