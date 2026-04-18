using CarRentalCustomers.Models;
using CarRentalPlatform.Models;
using GS_Shared_Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CustomerProfileContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
// Auto create DB/table for Docker SQL Server
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<CustomerProfileContext>();
	db.Database.EnsureCreated();
}

app.UseAuthorization();

app.MapControllers();


app.Run();
