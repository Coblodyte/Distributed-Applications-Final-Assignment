using Maintenance.WebAPI.Models;

namespace Maintenance.WebAPI.Services
{
	public class FakeRepairHistoryService : IRepairHistoryService
	{
		private readonly List<RepairHistory> _repairs = new();

		public FakeRepairHistoryService()
		{
			_repairs.AddRange(new List<RepairHistory>
			{
				new RepairHistory
				{
					Id = 1,
					VehicleId = 1,
					RepairDate = DateTime.Now.AddDays(-10),
					Description = "Oil change and filter replacement",
					Cost = 79.99m,
					PerformedBy = "John's Auto Shop"
				},
				new RepairHistory
				{
					Id = 2,
					VehicleId = 1,
					RepairDate = DateTime.Now.AddDays(-40),
					Description = "Brake pad replacement",
					Cost = 199.99m,
					PerformedBy = "Speedy Repairs"
				},
				new RepairHistory
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
		public RepairHistory AddRepair(RepairHistory repair)
		{

			repair.Id = _repairs.Count + 1;
			_repairs.Add(repair);
			return repair;
		}

		public List<RepairHistory> GetByVehicleId(int vehicleId)
		{
			return _repairs.Where(r => r.VehicleId == vehicleId).ToList();
		}

	}

}
