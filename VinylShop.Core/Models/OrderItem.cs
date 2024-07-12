namespace VinylShop.Core.Models;

public class OrderItem
{
    public Guid Id { get; } 
    public Guid OrderId { get; }
    public Guid VinylId { get; }
    public int Quantity { get; } 
    public decimal UnitPrice { get; } 

    private OrderItem(Guid id, Guid orderId, Guid vinylId, int quantity, decimal unitPrice)
    {
        Id = id;
        OrderId = orderId;
        VinylId = vinylId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static (OrderItem OrderItem, string Error) Create(Guid id, Guid orderId, Guid vinylId, int quantity,
        decimal unitPrice)
    {
        var error = string.Empty;

        if (quantity <= 0)
        {
            error = "Quantity must be greater than zero.";
            return (null, error);
        }

        if (unitPrice < 0)
        {
            error = "Unit price must be zero or greater than zero.";
            return (null, error);
        }

        var orderItem = new OrderItem(id, orderId, vinylId, quantity, unitPrice);

        return (orderItem, error);
    }
}