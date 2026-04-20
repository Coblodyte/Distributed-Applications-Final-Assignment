using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Exceptions;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Application.Services
{
	public class G2UpdateVehicleStatus
	{
		private readonly IVehicleRepository _vehicleRepository;

		public G2UpdateVehicleStatus(IVehicleRepository vehicleRepository)
		{
			_vehicleRepository = vehicleRepository;
		}

		public async Task<G2VehicleDto> UpdateVehicleStatus(int id, VehicleStatus newStatus)
		{
			var currentVehicle = await _vehicleRepository.GetById(id);

			if (currentVehicle == null)
			{
				throw new Exception($"Vehicle with id {id} not found.");
			}
			switch (newStatus)
			{
				case VehicleStatus.Available:
					currentVehicle.MarkAvailable();
					break;
				case VehicleStatus.Rented:
					currentVehicle.MarkRented();
					break;
				case VehicleStatus.Reserved:
					currentVehicle.MarkReserved();
					break;
				case VehicleStatus.Maintenance:
					currentVehicle.MarkServiced();
					break;
				default:
					throw new InvalidVehicleStatusException($"Invalid vehicle status: {newStatus}");
			}

			await _vehicleRepository.SaveChanges();

			return new G2VehicleDto
			{
				Id = currentVehicle.Id,
				VehicleCode = currentVehicle.VehicleCode.Value,
				LocationId = currentVehicle.LocationId,
				VehicleType = currentVehicle.VehicleType,
				VehicleStatus = currentVehicle.VehicleStatus
			};



		}

	}
}
