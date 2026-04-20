using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Application.Exceptions
{
	internal class InvalidVehicleStatusException : Exception
	{
		public InvalidVehicleStatusException(string message) : base(message)
		{
		}
	}
}
