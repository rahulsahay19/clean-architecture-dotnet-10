using Ordering.Application.Abstractions;

namespace Ordering.Application.Orders.CreateOrder
{
    public record CreateOrderCommand : ICommand<int>
    {
        public string? UserName { get; init; }
        public decimal? TotalPrice { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? EmailAddress { get; init; }
        public string? AddressLine { get; init; }
        public string? Country { get; init; }
        public string? State { get; init; }
        public string? ZipCode { get; init; }
        public string? CardName { get; init; }
        public string? CardNumber { get; init; }
        public string? Expiration { get; init; }
        public string? Cvv { get; init; }
        public int? PaymentMethod { get; init; }
        public Guid CorrelationId { get; init; }
    }
}
