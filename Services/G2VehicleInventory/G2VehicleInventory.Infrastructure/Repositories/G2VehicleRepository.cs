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
	public class G2VehicleRepository : IVehicleRepository
	{
		private readonly G2InventoryDbContext _context;

		public G2VehicleRepository(G2InventoryDbContext context)
		{
			_context = context;
		}

		public async Task Add(G2Vehicle vehicle)
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

		public async Task<List<G2Vehicle>> GetAll()
		{
			return await _context.Vehicles.ToListAsync();
		}

		public async Task<G2Vehicle> GetById(int id)
		{
			return await _context.Vehicles.FindAsync(id);
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}
	}
}
