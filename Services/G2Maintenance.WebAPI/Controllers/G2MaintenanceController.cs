using G2Maintenance.WebAPI.Models;
using G2Maintenance.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G2Maintenance.WebAPI.Controllers
{
	[Route("api/maintenance")]
	[ApiController]
	public class G2MaintenanceController : ControllerBase
	{
		private readonly G2IRepairHistoryService _service;
		private readonly Dictionary<string, int> _usageCounts;
		public G2MaintenanceController(G2IRepairHistoryService service, Dictionary<string, int> ussageCounts)
		{
			_service = service;
			_usageCounts = ussageCounts;
		}
		[HttpGet("vehicles/{vehicleId}/repairs")]
		public IActionResult GetRepairHistory(int vehicleId)
		{
			var history = _service.GetByVehicleId(vehicleId);
			return Ok(history);
		}

		[HttpPost]
		public IActionResult AddRepair([FromBody] G2RepairHistory g2Repair)
		{
			if (g2Repair.VehicleId <= 0)
			{
				return BadRequest(new
				{
					error = "InvalidParameter",
					message = "VehicleId must be greater than zero."
				});
			}
			if (string.IsNullOrWhiteSpace(g2Repair.Description))
			{
				return BadRequest(new
				{
					error = "InvalidParameter",
					message = "Description must not be empty."
				});
			}
			if (g2Repair.Cost < 0)
			{
				return BadRequest(new
				{
					error = "InvalidParameter",
					message = "Cost cannot be negative."
				});
			}
			var created = _service.AddRepair(g2Repair);
			return CreatedAtAction(
				nameof(GetRepairHistory),
				new { vehicleId = created.VehicleId },
				created
			);
		}

		[HttpGet("usage")]
		public IActionResult Usage()
		{
			var key = Request.Headers["X-Api-Key"].ToString();
			if (!_usageCounts.ContainsKey(key))
				_usageCounts[key] = 0;
			_usageCounts[key]++;
			return Ok(new
			{
				clientId = key,
				callCount = _usageCounts[key]
			});
		}




		[HttpGet("crash")]
		public IActionResult Crash()
		{
			int x = 0;
			int y = 5 / x;
			return Ok();
		}
	}
}
