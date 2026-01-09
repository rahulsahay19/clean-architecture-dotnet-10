using Discount.API.Services;
using Discount.Application.Handlers;
using Discount.Core.Repositories;
using Discount.Infrastructure.Repositories;
using Discount.Infrastructure.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Mediatr
var assemblies = new Assembly[]
    {
        Assembly.GetExecutingAssembly(), typeof(CreateDiscountHandler).Assembly
    };
builder.Services.AddMediatR(cfg=> cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();

//Database Settings 
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

var app = builder.Build();

//Migrate the database
app.MigrateDatabase();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
});


app.Run();
