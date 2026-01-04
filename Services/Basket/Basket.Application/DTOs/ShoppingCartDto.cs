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
     (
        string ProductId,
        string ProductName,
        string ImageFile,
        decimal Price,
        int Quantity
     );
}
