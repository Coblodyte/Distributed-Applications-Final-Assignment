using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Domain.Exceptions
{
	public class GSInvalidVehicleStatusChangeException : Exception
	{
		public GSInvalidVehicleStatusChangeException(string message) : base(message)
		{
		}
	}
}
