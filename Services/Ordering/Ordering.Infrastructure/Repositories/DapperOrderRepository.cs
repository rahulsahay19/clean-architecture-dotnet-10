//using Dapper;
//using Ordering.Core.Entities;
//using Ordering.Core.Repositories;
//using System.Data;
//using System.Linq.Expressions;

//namespace Ordering.Infrastructure.Repositories
//{
//    public class DapperOrderRepository : IOrderRepository
//    {
//        private readonly IDbConnection _connection;

//        public DapperOrderRepository(IDbConnection connection)
//        {
//            _connection = connection;
//        }

//        // ========================
//        // DAPPER-SUPPORTED METHODS
//        // ========================

//        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
//        {
//            const string sql = """
//                SELECT *
//                FROM Orders
//                WHERE UserName = @UserName
//                ORDER BY CreatedDate DESC
//            """;

//            return await _connection.QueryAsync<Order>(
//                sql,
//                new { UserName = userName });
//        }

//        public async Task<Order?> GetByIdAsync(int id)
//        {
//            const string sql = """
//                SELECT *
//                FROM Orders
//                WHERE Id = @Id
//            """;

//            return await _connection.QuerySingleOrDefaultAsync<Order>(
//                sql,
//                new { Id = id });
//        }

//        public async Task<Order> AddAsync(Order entity)
//        {
//            const string sql = """
//                INSERT INTO Orders (
//                    UserName, FirstName, LastName, EmailAddress,
//                    AddressLine, State, Country, ZipCode,
//                    CardName, CardNumber, Expiration, Cvv,
//                    PaymentMethod, CreatedBy, CreatedDate
//                )
//                VALUES (
//                    @UserName, @FirstName, @LastName, @EmailAddress,
//                    @AddressLine, @State, @Country, @ZipCode,
//                    @CardName, @CardNumber, @Expiration, @Cvv,
//                    @PaymentMethod, @CreatedBy, SYSUTCDATETIME()
//                );

//                SELECT CAST(SCOPE_IDENTITY() AS INT);
//            """;

//            var id = await _connection.ExecuteScalarAsync<int>(sql, entity);
//            entity.Id = id;
//            return entity;
//        }

//        public async Task UpdateAsync(Order entity)
//        {
//            const string sql = """
//                UPDATE Orders
//                SET
//                    FirstName = @FirstName,
//                    LastName = @LastName,
//                    EmailAddress = @EmailAddress,
//                    AddressLine = @AddressLine,
//                    State = @State,
//                    Country = @Country,
//                    ZipCode = @ZipCode,
//                    CardName = @CardName,
//                    CardNumber = @CardNumber,
//                    Expiration = @Expiration,
//                    Cvv = @Cvv,
//                    PaymentMethod = @PaymentMethod,
//                    LastModifiedBy = @LastModifiedBy,
//                    LastModifiedDate = SYSUTCDATETIME()
//                WHERE Id = @Id
//            """;

//            await _connection.ExecuteAsync(sql, entity);
//        }

//        public async Task DeleteAsync(Order entity)
//        {
//            const string sql = """
//                DELETE FROM Orders
//                WHERE Id = @Id
//            """;

//            await _connection.ExecuteAsync(sql, new { entity.Id });
//        }

//        // ========================
//        // EF-ONLY METHODS (BLOCKED)
//        // ========================

//        public Task<IReadOnlyList<Order>> GetAllAsync()
//            => throw new NotSupportedException("EF-only method. Use explicit queries.");

//        public Task<IReadOnlyList<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate)
//            => throw new NotSupportedException("EF-only method. Use SQL queries.");
//    }
//}
