using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Interfaces;

namespace VehicleInventory.Application.Services
{
	public class G2GetAllVehicles
	{
		private readonly IVehicleRepository _vehicleRepository;

		public G2GetAllVehicles(IVehicleRepository vehicleRepository)
		{
			_vehicleRepository = vehicleRepository;
		}

		public async Task<List<G2VehicleDto>> GetAllVehicles()
		{
			var vehicles = await _vehicleRepository.GetAll();
			return vehicles.Select(vehicle => new G2VehicleDto
			{
				Id = vehicle.Id,
				VehicleCode = vehicle.VehicleCode.Value,
				LocationId = vehicle.LocationId,
				VehicleType = vehicle.VehicleType,
				VehicleStatus = vehicle.VehicleStatus
			}).ToList();
		}

	}
}
