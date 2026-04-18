using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.ValueObjects;
using VehicleInventory.Domain.VehicleAggregate;

namespace VehicleInventory.Application.Services
{
	public class GSCreateVehicle
	{
		private readonly IVehicleRepository _vehicleRepository;

		public GSCreateVehicle(IVehicleRepository vehicleRepository)
		{
			_vehicleRepository = vehicleRepository;
		}

		public async Task CreateVehicle(GSCreateVehicleDto dto)
		{
			var vehicleCode = new GSVehicleCode(dto.VehicleCode);

			var vehicle = new GSVehicle
			(
				vehicleCode,
				dto.LocationId,
				dto.VehicleType,
				VehicleStatus.Available // Should be available when created, don't want the client to set it
			);

			await _vehicleRepository.Add(vehicle);
			await _vehicleRepository.SaveChanges();
		}

	}
}
