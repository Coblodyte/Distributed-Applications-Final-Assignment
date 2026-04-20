using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Exceptions;
using VehicleInventory.Application.Interfaces;


namespace VehicleInventory.Application.Services
{
	public class G2GetVehicleById
	{
		private readonly IVehicleRepository _vehicleRepository;

		public G2GetVehicleById(IVehicleRepository vehicleRepository)
		{
			_vehicleRepository = vehicleRepository;
		}

		public async Task<G2VehicleDto> GetVehicleById(int id)
		{
			var vehicle = await _vehicleRepository.GetById(id);

			if (vehicle == null)
			{
				throw new NotFoundException($"Vehicle with ID {id} not found.");
			}
			return new G2VehicleDto
			{
				Id = vehicle.Id,
				VehicleCode = vehicle.VehicleCode.Value,
				LocationId = vehicle.LocationId,
				VehicleType = vehicle.VehicleType,
				VehicleStatus = vehicle.VehicleStatus
			};
		}
	}
}
