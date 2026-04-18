using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.Interfaces;

namespace VehicleInventory.Application.Services
{
	public class GSDeleteVehicle
	{
		private readonly IVehicleRepository _vehicleRepository;

		public GSDeleteVehicle(IVehicleRepository vehicleRepository)
		{
			_vehicleRepository = vehicleRepository;
		}

		public async Task DeleteVehicle(int id)
		{
			var vehicle = await _vehicleRepository.GetById(id);
			// GetVehicleById already checks for null so i dont need it again.

			await _vehicleRepository.Delete(id);
			await _vehicleRepository.SaveChanges();
		}


	}
}
