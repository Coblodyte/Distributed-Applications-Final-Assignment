using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Domain.ValueObjects
{
	public class GSAddress
	{
		public string Street { get; private set; }
		public string City { get; private set; }
		public string PostalCode { get; private set; }

		public GSAddress(string street, string city, string postalCode)
		{
			if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street is required.");
			if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.");
			if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException("Postal Code is required.");

			Street = street;
			City = city;
			PostalCode = postalCode;
		}

		public override bool Equals(object? obj)
		{
			if (obj is GSAddress other)
			{
				return Street == other.Street &&
					   City == other.City &&
					   PostalCode == other.PostalCode;
			}
			return false;
		}

		public override int GetHashCode() => HashCode.Combine(Street, City, PostalCode);

		public static bool operator ==(GSAddress? left, GSAddress? right)
		{
			if (left is null && right is null) return true;
			if (left is null || right is null) return false;
			return left.Equals(right);
		}

		public static bool operator !=(GSAddress? left, GSAddress? right) => !(left == right);
	}

}
