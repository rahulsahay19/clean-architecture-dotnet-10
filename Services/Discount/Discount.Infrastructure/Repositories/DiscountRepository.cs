using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Discount.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly string _connectionString;

        public DiscountRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            _connectionString = databaseSettings.Value.ConnectionString;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var affected = await connection.ExecuteAsync(
                "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new { coupon.ProductName, coupon.Description, coupon.Amount });
            return affected > 0;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });
            return affected > 0;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
               "SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });
            return coupon ?? new Coupon
            {
                ProductName = "No Discount",
                Amount = 0,
                Description = "No Discount Available"
            };
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var affected = await connection.ExecuteAsync(
                "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                new { coupon.ProductName, coupon.Description, coupon.Amount });
            return affected > 0;
        }
    }
}
