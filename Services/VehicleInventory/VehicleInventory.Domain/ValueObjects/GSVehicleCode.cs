using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Domain.ValueObjects
{
	public class GSVehicleCode
	{
		public string Value { get; private set; }

		public GSVehicleCode(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Vehicle Code cannot be empty.");

			if (value.Length > 10)
				throw new ArgumentException("Vehicle Code is too long.");

			Value = value.ToUpper();
		}


		public override bool Equals(object? obj)
		{
			if (obj is GSVehicleCode other)
				return Value == other.Value;
			return false;
		}

		public override int GetHashCode() => Value.GetHashCode();

		public static bool operator ==(GSVehicleCode? left, GSVehicleCode? right)
		{
			if (left is null && right is null) return true;
			if (left is null || right is null) return false;
			return left.Value == right.Value;
		}

		public static bool operator !=(GSVehicleCode? left, GSVehicleCode? right)
		{
			return !(left == right);
		}
	}

}
