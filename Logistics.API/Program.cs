<<<<<<< HEAD
using Logistics.API.Middlewares;
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Interfaces;
using Logistics.Infrastructure.Data;
using Logistics.Infrastructure.Repositories;
using Logistics.Infrastructure.Services;
=======
using Logistics.Application.Interfaces;
using Logistics.Infrastructure.Data;
using Logistics.Infrastructure.Repositories;
>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Logistics Management API",
        Version = "v1"
    });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

<<<<<<< HEAD
=======
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

builder.Services.AddAutoMapper(cfg => {
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});

>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Logistics Management API v1");
        options.RoutePrefix = string.Empty;
    });
}

<<<<<<< HEAD
app.UseMiddleware<ExceptionHandlingMiddleware>();

builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IShipmentService, ShipmentService>();
builder.Services.AddScoped<IShipmentStatusHistoryService, ShipmentStatusHistoryService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IEmployeeRoleService, EmployeeRoleService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddAutoMapper(cfg => {
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});



=======
>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();