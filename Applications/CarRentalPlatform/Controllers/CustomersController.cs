using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarRentalPlatform.Models;

namespace CarRentalPlatform.Controllers
{
	public class CustomersController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public CustomersController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}


		private HttpClient GetClient() => _httpClientFactory.CreateClient("ApiGateway");

		// GET: Customers
		public async Task<IActionResult> Index()
		{
			var client = GetClient();
			var customers = await client.GetFromJsonAsync<List<Customer>>("api/CustomersApi");
			return View(customers ?? new List<Customer>());
		}

		// GET: Customers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return NotFound();

			var client = GetClient();
			var customer = await client.GetFromJsonAsync<Customer>($"api/CustomersApi/{id}");

			if (customer == null) return NotFound();

			return View(customer);
		}

		// GET: Customers/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Customers/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Phone,Email")] Customer customer)
		{
			if (ModelState.IsValid)
			{
				var client = GetClient();
				var response = await client.PostAsJsonAsync("api/CustomersApi", customer);

				if (response.IsSuccessStatusCode)
					return RedirectToAction(nameof(Index));
			}
			return View(customer);
		}

		// GET: Customers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();

			var client = GetClient();
			var customer = await client.GetFromJsonAsync<Customer>($"api/CustomersApi/{id}");

			if (customer == null) return NotFound();

			return View(customer);
		}

		// POST: Customers/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Phone,Email")] Customer customer)
		{
			if (id != customer.Id) return NotFound();

			if (ModelState.IsValid)
			{
				var client = GetClient();
				var response = await client.PutAsJsonAsync($"api/CustomersApi/{id}", customer);

				if (response.IsSuccessStatusCode)
					return RedirectToAction(nameof(Index));
			}
			return View(customer);
		}

		// GET: Customers/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();

			var client = GetClient();
			var customer = await client.GetFromJsonAsync<Customer>($"api/CustomersApi/{id}");

			if (customer == null) return NotFound();

			return View(customer);
		}

		// POST: Customers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var client = GetClient();
			await client.DeleteAsync($"api/CustomersApi/{id}");
			return RedirectToAction(nameof(Index));
		}
	}
}