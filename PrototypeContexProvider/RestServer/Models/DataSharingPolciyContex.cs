﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrototypeContexProvider.src;

namespace RestServer.Models
{
	public class DataSharingPolciyContext : DbContext
	{
		public DbSet<DataSharingPolciy> Polcies { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Filename=./Polcies.db");
		}
	}
}
