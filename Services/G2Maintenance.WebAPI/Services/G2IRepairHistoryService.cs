using G2Maintenance.WebAPI.Models;

namespace G2Maintenance.WebAPI.Services
{
	public interface G2IRepairHistoryService
	{
		List<G2RepairHistory> GetByVehicleId(int vehicleId);

		G2RepairHistory AddRepair(G2RepairHistory g2Repair);
	}
}
