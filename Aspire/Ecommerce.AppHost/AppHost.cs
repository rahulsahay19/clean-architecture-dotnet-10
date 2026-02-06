var builder = DistributedApplication.CreateBuilder(args);

// MongoDB ? Catalog
var mongo = builder.AddMongoDB("catalogdb")
                   .WithDataVolume();

// Redis ? Basket
var redis = builder.AddRedis("basketdb");

// PostgreSQL ? Discount
var postgres = builder.AddPostgres("discount-postgres")
                      .WithDataVolume();
var discountDb = postgres.AddDatabase("discountdb");

// SQL Server ? Ordering + Identity
var sqlServer = builder.AddSqlServer("sqlserver")
                       .WithDataVolume();
var orderDb = sqlServer.AddDatabase("orderdb");
var identityDb = sqlServer.AddDatabase("identitydb");

// RabbitMQ
var rabbitMq = builder.AddRabbitMQ("rabbitmq");

// Catalog
var catalog = builder.AddProject<Projects.Catalog_API>("catalog")
    .WithReference(mongo);

// Discount
var discount = builder.AddProject<Projects.Discount_API>("discount")
    .WithReference(discountDb);

// Basket
var basket = builder.AddProject<Projects.Basket_API>("basket")
    .WithReference(redis)
    .WithReference(rabbitMq)
    .WithReference(discount);

// Ordering
var ordering = builder.AddProject<Projects.Ordering_API>("ordering")
    .WithReference(orderDb)
    .WithReference(rabbitMq);

// Payment
var payment = builder.AddProject<Projects.Payment>("payment")
    .WithReference(rabbitMq);

// Identity
var identity = builder.AddProject<Projects.Identity>("identity")
    .WithReference(identityDb)
    .WithEnvironment("Jwt__Key", "super_secure_secret_key_1234567890!@#")
    .WithEnvironment("Jwt__Issuer", "ecommerce.identity")
    .WithEnvironment("Jwt__Audience", "ecommerce.clients")
    .WithEnvironment("Jwt__DurationInMinutes", "60");

builder.AddProject<Projects.ApiGateway>("apigateway")
    .WithReference(identity)
    .WithReference(catalog)
    .WithReference(basket)
    .WithReference(ordering)
    .WithReference(payment)
    .WithReference(discount);

builder.Build().Run();

