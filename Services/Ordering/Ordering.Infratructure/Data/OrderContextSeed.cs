using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Ordering Database: {typeof(OrderContext).Name} seeded");
            }
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new List<Order>
            { 
                new()
                { 
                    UserName = "rahul",
                    FirstName = "Rahul",
                    LastName = "Sahay",
                    EmailAddress = "rahulsahay@ecommerce.net",
                    AddressLine = "Ranchi",
                    State = "JH",
                    Country = "India",
                    ZipCode = "834009",

                    CardName = "Visa",
                    CardNumber = "4111111111111111",
                    CreatedBy = "Rahul",
                    Expiration = "12/25",
                    Cvv = "123",
                    PaymentMethod = 1,
                    LastModifiedBy = "Rahul",
                    LastModifiedDate = DateTime.UtcNow
                }
            };
        }
    }
}
