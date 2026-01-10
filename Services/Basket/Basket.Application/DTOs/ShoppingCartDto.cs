namespace Basket.Application.DTOs
{
    public record ShoppingCartDto
    (
        string userName,
        List<ShoppingCartItemDto> Items,
        decimal TotalPrice
    );

    public record ShoppingCartItemDto(
        string ProductId,
        string ProductName,
        string ImageFile,
        decimal Price,
        int Quantity);

    public record CreateShoppingCartItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
