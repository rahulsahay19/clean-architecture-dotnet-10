using EventBus.Messages.Common;
using MassTransit;
using Ordering.API.Extensions;
using Ordering.Application.Dispatcher;
using Ordering.Application.EventBusConsumer;
using Ordering.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Ordering services
builder.Services.AddOrderingServices(builder.Configuration);

//Register Outbox Message Dispatcher
builder.Services.AddHostedService<OutboxMessageDispatcher>();
//Mass Transit 
builder.Services.AddMassTransit(config =>
{
    //Mark as Consumer
    config.AddConsumer<BasketOrderingConsumer>();
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        //provide the queue name with consumer settings
        cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketOrderingConsumer>(ctx);
        });
    });
});
var app = builder.Build();

//Migration
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
});
//using (var scope = app.Services.CreateScope())
//{
//    var logger = scope.ServiceProvider
//        .GetRequiredService<ILogger<Program>>();

//    var configuration = scope.ServiceProvider
//        .GetRequiredService<IConfiguration>();

//    DbMigrationRunner.Run(
//        configuration.GetConnectionString("OrderingConnectionString"),
//        logger);
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
