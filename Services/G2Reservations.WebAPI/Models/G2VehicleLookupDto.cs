namespace G2Reservations.WebAPI.Models
{
	public class G2VehicleLookupDto
	{
		public int Id { get; set; }
		public string VehicleCode { get; set; } = string.Empty;
		public int LocationId { get; set; }
		public string VehicleType { get; set; } = string.Empty;
		public string VehicleStatus { get; set; } = string.Empty;
	}
}
