using G2_Shared_Infrastructure;
using G2Maintenance.Application.Interfaces;
using G2Maintenance.Application.Services;
using G2Maintenance.Infrastructure.Data;
using G2Maintenance.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var usageCounts = new Dictionary<string, int>();
builder.Services.AddSingleton(usageCounts);

builder.Services.AddDbContext<G2MaintenanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<G2IMaintenanceRepository, G2MaintenanceRepository>();
builder.Services.AddScoped<G2AddRepairHistory>();
builder.Services.AddScoped<G2GetRepairHistoryById>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<G2GlobalExceptionMiddleware>();
app.UseMiddleware<G2GatewayMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<G2MaintenanceDbContext>();
    db.Database.Migrate();
}

app.Run();