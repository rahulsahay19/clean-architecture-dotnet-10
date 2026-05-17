using Basket.Application.GrpcService;
using Basket.Application.Handlers;
using Basket.Application.Settings;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Basket.Infrastructure.Settings;
using Common.Logging;
using Discount.Grpc.Protos;
using MassTransit;
using Microsoft.Extensions.Options;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add application services
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// Add swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MediatR
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(CreateShoppingCartHandler).Assembly
};

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(assemblies));

// Options pattern
builder.Services.Configure<CacheSettings>(
    builder.Configuration.GetSection("CacheSettings"));

builder.Services.Configure<GrpcSettings>(
    builder.Configuration.GetSection("GrpcSettings"));

// Register gRPC client using IOptions
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    (sp, cfg) =>
    {
        var grpcSetting = sp.GetRequiredService<IOptions<GrpcSettings>>().Value;
        cfg.Address = new Uri(grpcSetting.DiscountUrl);
    });

// Register gRPC service wrapper
builder.Services.AddScoped<DiscountGrpcService>();

// Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration
        .GetSection("CacheSettings")
        .GetValue<string>("ConnectionString");
});

// Add MassTransit
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ct, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

// Register Logging
builder.Host.UseSerilog(Logging.ConfigureLogger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();