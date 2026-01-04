using Basket.Application.Handlers;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Basket.Infrastructure.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Add application services
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//Add swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register mediatr

var assemblies = new Assembly[]
    {
        Assembly.GetExecutingAssembly(),
        typeof(CreateShoppingCartHandler).Assembly
    };
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

//Options pattern
builder.Services.Configure<CacheSettings>(
    builder.Configuration.GetSection("CacheSettings"));

//Redis
builder.Services.AddStackExchangeRedisCache((options) =>
{
    options.Configuration = builder.Configuration.GetSection("CacheSettings")
                                                 .GetValue<string>("ConnectionString");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
