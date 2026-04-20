using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Domain.VehicleAggregate;

namespace VehicleInventory.Application.Interfaces
{
	public interface IVehicleRepository
	{
		Task<G2Vehicle> GetById(int id);
		Task<List<G2Vehicle>> GetAll();
		Task Add(G2Vehicle vehicle);
		Task Delete(int id);
		Task SaveChanges();


	}
}
