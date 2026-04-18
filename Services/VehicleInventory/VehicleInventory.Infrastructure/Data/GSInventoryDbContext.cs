using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.LocationAggregate;
using VehicleInventory.Domain.VehicleAggregate;

namespace VehicleInventory.Infrastructure.Data
{
	public class GSInventoryDbContext : DbContext
	{
		public GSInventoryDbContext(DbContextOptions<GSInventoryDbContext> options) : base(options)
		{
		}
		public DbSet<GSVehicle> Vehicles { get; set; }
		public DbSet<GSLocation> Locations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<GSVehicle>(entity =>
			{
				entity.HasKey(e => e.Id);

				// Map value obj
				entity.OwnsOne(e => e.VehicleCode, vc =>
				{
					vc.Property(p => p.Value)
					  .HasColumnName("VehicleCode")
					  .IsRequired()
					  .HasMaxLength(10);
				});
				entity.Property(e => e.LocationId).IsRequired();
				entity.Property(e => e.VehicleType).IsRequired();
				entity.Property(e => e.VehicleStatus).IsRequired();
			});

			modelBuilder.Entity<GSLocation>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

				// Map value obj
				entity.OwnsOne(e => e.Address, a =>
				{
					a.Property(p => p.Street).IsRequired().HasMaxLength(100);
					a.Property(p => p.City).IsRequired().HasMaxLength(50);
					a.Property(p => p.PostalCode).IsRequired().HasMaxLength(10);
				});
			});
		}
	}
}
