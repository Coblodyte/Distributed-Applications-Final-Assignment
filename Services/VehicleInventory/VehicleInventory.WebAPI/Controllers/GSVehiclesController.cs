using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Services;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GSVehiclesController : ControllerBase
	{
		private readonly GSGetAllVehicles _getAllVehiclesService;
		private readonly GSGetVehicleById _getVehicleByIdService;
		private readonly GSUpdateVehicleStatus _updateVehicleStatusService;
		private readonly GSCreateVehicle _CreateVehicleService;
		private readonly GSDeleteVehicle _DeleteVehicleService;

		public GSVehiclesController(

			GSGetAllVehicles getAllVehiclesService,
			GSGetVehicleById getVehicleByIdService,
			GSUpdateVehicleStatus updateVehicleStatusService,
			GSCreateVehicle createVehicleService,
			GSDeleteVehicle deleteVehicleService)
		{
			_getAllVehiclesService = getAllVehiclesService;
			_getVehicleByIdService = getVehicleByIdService;
			_updateVehicleStatusService = updateVehicleStatusService;
			_CreateVehicleService = createVehicleService;
			_DeleteVehicleService = deleteVehicleService;
		}

		[HttpPost]
		public async Task<ActionResult<GSVehicleDto>> CreateVehicle([FromBody] GSCreateVehicleDto dto)
		{
			await _CreateVehicleService.CreateVehicle(dto);
			return Ok(dto);
		}

		[HttpGet]
		public async Task<ActionResult<List<GSVehicleDto>>> GetAllVehicles()
		{
			var vehicles = await _getAllVehiclesService.GetAllVehicles();
			return Ok(vehicles);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GSVehicleDto>> GetVehicleById(int id)
		{
			var vehicle = await _getVehicleByIdService.GetVehicleById(id);
			return Ok(vehicle);
		}

		[HttpPut("{id}/status")]
		public async Task<ActionResult<GSVehicleDto>> UpdateVehicleStatus(int id, [FromBody] VehicleStatus newStatus)
		{
			var updatedVehicle = await _updateVehicleStatusService.UpdateVehicleStatus(id, newStatus);
			return Ok(updatedVehicle);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteVehicle(int id)
		{
			await _DeleteVehicleService.DeleteVehicle(id);
			return NoContent();
		}




	}
}
