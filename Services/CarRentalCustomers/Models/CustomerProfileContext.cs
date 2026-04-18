using System;
using System.Collections.Generic;
using CarRentalCustomers.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPlatform.Models;

public partial class CustomerProfileContext : DbContext
{
	public CustomerProfileContext()
	{
	}

	public CustomerProfileContext(DbContextOptions<CustomerProfileContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Customer> Customers { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CustomerProfile;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Customer>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC070DC399EC");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
