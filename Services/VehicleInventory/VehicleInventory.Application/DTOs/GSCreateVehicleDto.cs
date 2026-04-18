using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Application.DTOs
{
	public class GSCreateVehicleDto
	{
		public string VehicleCode { get; set; } = string.Empty;
		public int LocationId { get; set; }
		public VehicleType VehicleType { get; set; }
		public VehicleStatus VehicleStatus { get; set; }
	}
}
