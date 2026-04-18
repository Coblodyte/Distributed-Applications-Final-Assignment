using GS_Shared_Infrastructure;
using Microsoft.EntityFrameworkCore;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Application.Services;
using VehicleInventory.Infrastructure.Data;
using VehicleInventory.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<GSInventoryDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IVehicleRepository, GSVehicleRepository>();
builder.Services.AddScoped<GSCreateVehicle>();
builder.Services.AddScoped<GSGetAllVehicles>();
builder.Services.AddScoped<GSGetVehicleById>();
builder.Services.AddScoped<GSUpdateVehicleStatus>();
builder.Services.AddScoped<GSDeleteVehicle>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GSGlobalExceptionMiddleware>();
app.UseMiddleware<GSGatewayMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Apply migrations automatically in Docker
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<GSInventoryDbContext>();
	db.Database.Migrate();
}

app.Run();
