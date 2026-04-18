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
		Task<GSVehicle> GetById(int id);
		Task<List<GSVehicle>> GetAll();
		Task Add(GSVehicle vehicle);
		Task Delete(int id);
		Task SaveChanges();


	}
}
