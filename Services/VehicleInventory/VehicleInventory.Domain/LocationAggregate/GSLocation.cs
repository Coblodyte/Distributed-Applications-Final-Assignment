using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.ValueObjects;

namespace VehicleInventory.Domain.LocationAggregate
{
	public class GSLocation
	{
		public int Id { get; private set; }
		public string Name { get; private set; }
		public GSAddress Address { get; private set; } // Uses the Value Object

		public GSLocation(string name, GSAddress address)
		{
			Name = name;
			Address = address;
		}

		//for ef core
		private GSLocation() { }
	}
}
