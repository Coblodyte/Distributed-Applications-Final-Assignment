using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.VehicleAggregate;
using VehicleInventory.Infrastructure.Data;

namespace VehicleInventory.Infrastructure.Repositories
{
	public class GSVehicleRepository : IVehicleRepository
	{
		private readonly GSInventoryDbContext _context;

		public GSVehicleRepository(GSInventoryDbContext context)
		{
			_context = context;
		}

		public async Task Add(GSVehicle vehicle)
		{
			await _context.Vehicles.AddAsync(vehicle);
			await SaveChanges();
		}

		public async Task Delete(int id)
		{
			var vehicle = await _context.Vehicles.FindAsync(id);
			if (vehicle != null)
			{
				_context.Vehicles.Remove(vehicle);
				await SaveChanges();
			}
		}

		public async Task<List<GSVehicle>> GetAll()
		{
			return await _context.Vehicles.ToListAsync();
		}

		public async Task<GSVehicle> GetById(int id)
		{
			return await _context.Vehicles.FindAsync(id);
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}
	}
}
