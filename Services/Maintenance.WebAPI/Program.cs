using GS_Shared_Infrastructure;
using Maintenance.WebAPI.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var usageCounts = new Dictionary<string, int>();
builder.Services.AddSingleton(usageCounts);

builder.Services.AddSingleton<IRepairHistoryService, FakeRepairHistoryService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	// Add API key
	c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
	{
		Description = "API Key needed to access the endpoints. Add X-Api-Key: MY_SECRET_KEY_123",
		Name = "X-Api-Key",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "ApiKeyScheme"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "ApiKey"
				},
				Scheme = "ApiKeyScheme",
				Name = "X-Api-Key",
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});
});



var app = builder.Build();

app.UseMiddleware<GSGlobalExceptionMiddleware>();
app.UseMiddleware<GSGatewayMiddleware>();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}



//app.UseHttpsRedirection();

// Global error handling middleware
app.Use(async (context, next) =>
{
	try
	{
		await next();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex.Message);
		context.Response.StatusCode = 500;
		context.Response.ContentType = "application/json";
		await context.Response.WriteAsJsonAsync(new
		{
			error = "ServerError",
			message = "An unexpected error occurred."
		});
	}
});

// API key validation middleware

const string API_KEY = "MY_SECRET_KEY_123";
app.Use(async (context, next) =>
{
	if (!context.Request.Headers.TryGetValue("X-Api-Key", out var key) ||
	key != API_KEY)
	{
		context.Response.StatusCode = 401;
		await context.Response.WriteAsJsonAsync(new
		{
			error = "Unauthorized",
			message = "Missing or invalid API key."
		});
		return;
	}
	await next();
});


app.UseAuthorization();


app.MapControllers();

app.Run();
