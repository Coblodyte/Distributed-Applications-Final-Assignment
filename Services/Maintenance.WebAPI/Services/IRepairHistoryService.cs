using Maintenance.WebAPI.Models;

namespace Maintenance.WebAPI.Services
{
	public interface IRepairHistoryService
	{
		List<RepairHistory> GetByVehicleId(int vehicleId);

		RepairHistory AddRepair(RepairHistory repair);
	}
}
