using G2Maintenance.WebAPI.Models;

namespace G2Maintenance.WebAPI.Services
{
	public class G2FakeG2IRepairHistoryService : G2IRepairHistoryService
	{
		private readonly List<G2RepairHistory> _repairs = new();

		public G2FakeG2IRepairHistoryService()
		{
			_repairs.AddRange(new List<G2RepairHistory>
			{
				new G2RepairHistory
				{
					Id = 1,
					VehicleId = 1,
					RepairDate = DateTime.Now.AddDays(-10),
					Description = "Oil change and filter replacement",
					Cost = 79.99m,
					PerformedBy = "John's Auto Shop"
				},
				new G2RepairHistory
				{
					Id = 2,
					VehicleId = 1,
					RepairDate = DateTime.Now.AddDays(-40),
					Description = "Brake pad replacement",
					Cost = 199.99m,
					PerformedBy = "Speedy Repairs"
				},
				new G2RepairHistory
				{
					Id = 3,
					VehicleId = 1,
					RepairDate = DateTime.Now.AddDays(-90),
					Description = "Tire rotation and balance",
					Cost = 49.99m,
					PerformedBy = "Tire World"
				}
			});

		}
		public G2RepairHistory AddRepair(G2RepairHistory g2Repair)
		{

			g2Repair.Id = _repairs.Count + 1;
			_repairs.Add(g2Repair);
			return g2Repair;
		}

		public List<G2RepairHistory> GetByVehicleId(int vehicleId)
		{
			return _repairs.Where(r => r.VehicleId == vehicleId).ToList();
		}

	}

}
