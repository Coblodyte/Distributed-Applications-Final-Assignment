using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalCustomers.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using CarRentalPlatform.Models;

[ApiController]
[Route("api/[controller]")]
public class CustomersApiController : ControllerBase
{
	private readonly CustomerProfileContext _context;

	public CustomersApiController(CustomerProfileContext context)
	{
		_context = context;
	}

	// GET: api/CustomersApi
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
	{
		return await _context.Customers.ToListAsync();
	}

	// GET: api/CustomersApi/5
	[HttpGet("{id}")]
	public async Task<ActionResult<Customer>> GetCustomer(int id)
	{
		var customer = await _context.Customers.FindAsync(id);

		if (customer == null)
			return NotFound();

		return customer;
	}

	// POST: api/CustomersApi
	[HttpPost]
	public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
	{

		customer.Id = 0;
		_context.Customers.Add(customer);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetCustomer),
			new { id = customer.Id }, customer);
	}

	// PUT: api/CustomersApi/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutCustomer(int id, Customer customer)
	{
		if (id != customer.Id)
			return BadRequest();

		_context.Entry(customer).State = EntityState.Modified;
		await _context.SaveChangesAsync();

		return NoContent();
	}

	// DELETE: api/CustomersApi/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteCustomer(int id)
	{
		var customer = await _context.Customers.FindAsync(id);

		if (customer == null)
			return NotFound();

		_context.Customers.Remove(customer);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}


