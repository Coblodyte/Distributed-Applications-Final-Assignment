using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;
using VehicleInventory.Domain.ValueObjects;

namespace VehicleInventory.Domain.VehicleAggregate
{
	public class GSVehicle
	{
		public int Id { get; private set; }
		public GSVehicleCode VehicleCode { get; private set; }
		public int LocationId { get; private set; }
		public VehicleType VehicleType { get; private set; }
		public VehicleStatus VehicleStatus { get; private set; }

		public GSVehicle(GSVehicleCode vehicleCode, int locationId, VehicleType vehicleType, VehicleStatus vehicleStatus)
		{
			this.VehicleCode = vehicleCode;
			this.LocationId = locationId;
			this.VehicleType = vehicleType;
			this.VehicleStatus = vehicleStatus;
		}

		//for ef core
		private GSVehicle() { }

		public void MarkAvailable()
		{
			if (this.VehicleStatus == VehicleStatus.Reserved)
			{
				throw new GSInvalidVehicleStatusChangeException("Cannot mark a reserved vehicle as available.");
			}
			else
			{
				this.VehicleStatus = VehicleStatus.Available;
			}

		}

		public void MarkRented()
		{
			if (this.VehicleStatus == VehicleStatus.Rented)
			{
				throw new GSInvalidVehicleStatusChangeException("Vehicle is already rented.");
			}

			if (this.VehicleStatus == VehicleStatus.Reserved)
			{
				throw new GSInvalidVehicleStatusChangeException("Cannot mark a reserved vehicle as rented.");
			}

			if (this.VehicleStatus == VehicleStatus.Maintenance)
			{
				throw new GSInvalidVehicleStatusChangeException("Cannot mark a vehicle under maintenance as rented.");
			}

			this.VehicleStatus = VehicleStatus.Rented;

		}

		public void MarkReserved()
		{
			if (this.VehicleStatus == VehicleStatus.Rented)
			{
				throw new GSInvalidVehicleStatusChangeException("Cannot mark a rented vehicle as reserved.");
			}
			if (this.VehicleStatus == VehicleStatus.Maintenance)
			{
				throw new GSInvalidVehicleStatusChangeException("Cannot mark a vehicle under maintenance as reserved.");
			}
			if (this.VehicleStatus == VehicleStatus.Reserved)
			{
				throw new GSInvalidVehicleStatusChangeException("Vehicle is already reserved.");
			}
			this.VehicleStatus = VehicleStatus.Reserved;
		}

		public void MarkServiced()
		{
			if (this.VehicleStatus == VehicleStatus.Rented)
			{
				throw new GSInvalidVehicleStatusChangeException("Cannot mark a rented vehicle as under maintenance.");
			}
			if (this.VehicleStatus == VehicleStatus.Reserved)
			{
				throw new GSInvalidVehicleStatusChangeException("Cannot mark a reserved vehicle as under maintenance.");
			}
			this.VehicleStatus = VehicleStatus.Maintenance;
		}




	}
}
