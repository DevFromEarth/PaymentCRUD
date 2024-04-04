using CompanyManagement.Middlwares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PaymentAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
	x.AddSecurityDefinition("X-API-KEY", new OpenApiSecurityScheme
	{
		Name = "X-API-KEY",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "ApiKeyScheme",
		In = ParameterLocation.Header,
		Description = "ApiKey must appear in header"
	});
	x.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "X-API-KEY"
				},
				In = ParameterLocation.Header
			},
			new string[]{}
		}
	});
});

builder.Services.AddDbContext<PaymentDetailContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()) ;

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
