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
	public class G2CreateVehicle
	{
		private readonly IVehicleRepository _vehicleRepository;

		public G2CreateVehicle(IVehicleRepository vehicleRepository)
		{
			_vehicleRepository = vehicleRepository;
		}

		public async Task CreateVehicle(G2CreateVehicleDto dto)
		{
			var vehicleCode = new G2VehicleCode(dto.VehicleCode);

			var vehicle = new G2Vehicle
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
