namespace G2Maintenance.WebAPI.Models
{
	public class G2RepairHistory
	{
		public int Id { get; set; } //value object
		public int VehicleId { get; set; } //value object
		public DateTime RepairDate { get; set; }
		public string Description { get; set; } = string.Empty;
		public decimal Cost { get; set; }
		public string PerformedBy { get; set; } = string.Empty;
	}
}
