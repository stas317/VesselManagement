using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VesselManagement.Domain.Interfaces;
using VesselManagement.Infrastructure.Data;
using VesselManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<VesselContext>(options =>
	options.UseInMemoryDatabase("VesselDb"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("VesselManagement.Application")));

builder.Services.AddScoped<IVesselRepository, VesselRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();