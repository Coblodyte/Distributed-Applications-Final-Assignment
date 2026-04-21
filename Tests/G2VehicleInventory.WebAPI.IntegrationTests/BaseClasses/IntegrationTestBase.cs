using Microsoft.AspNetCore.Mvc.Testing;

namespace G2VehicleInventory.WebAPI.IntegrationTests.BaseClasses
{
	public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
	{
		protected readonly HttpClient Client;

		public IntegrationTestBase(WebApplicationFactory<Program> factory)
		{
			Client = factory.CreateClient();

			// gateway middleware
			Client.DefaultRequestHeaders.Add("X-From-Gateway", "GS-Gateway-Trusted-Token-111");
		}
	}
}