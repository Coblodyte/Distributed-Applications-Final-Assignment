using G2Reservations.WebAPI.Models;
using GS_Shared_Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<G2ReservationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient("CustomersApi", client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CustomersApi"]!);
	client.DefaultRequestHeaders.Add("X-From-Gateway", "GS-Gateway-Trusted-Token-111");
});

builder.Services.AddHttpClient("VehicleInventoryApi", client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:VehicleInventoryApi"]!);
	client.DefaultRequestHeaders.Add("X-From-Gateway", "GS-Gateway-Trusted-Token-111");
});

var app = builder.Build();

app.UseMiddleware<GSGlobalExceptionMiddleware>();
app.UseMiddleware<GSGatewayMiddleware>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<G2ReservationDbContext>();
	db.Database.EnsureCreated();
}

app.UseAuthorization();
app.MapControllers();

app.Run();