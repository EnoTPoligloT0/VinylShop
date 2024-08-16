using CSharpFunctionalExtensions;

namespace VinylShop.Core.Models;

public class OrderItem
{
    private readonly List<Vinyl> _vinyls = [];
    private OrderItem(Guid id, Guid orderId, int quantity, decimal unitPrice)
    {
        Id = id;
        OrderId = orderId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
    public Guid Id { get; }
    public Guid OrderId { get; }

    public Order Order { get; }
    public Guid VinylId { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }

    public IReadOnlyList<Vinyl>? Vinyls => _vinyls;
    public static Result<OrderItem> Create(Guid id, Guid orderId,
        int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
        {
            return Result.Failure<OrderItem>("Quantity must be greater than zero.");
        }


        if (unitPrice < 0)
        {
            return Result.Failure<OrderItem>("Unit price must be zero or greater than zero.");
        }

        var orderItem = new OrderItem(id, orderId, quantity, unitPrice);

        return Result.Success(orderItem);
    }
}