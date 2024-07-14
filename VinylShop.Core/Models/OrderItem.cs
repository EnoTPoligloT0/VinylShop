using CSharpFunctionalExtensions;

namespace VinylShop.Core.Models;

public class OrderItem
{
    public Guid Id { get; }
    public Guid OrderId { get; }

    public Order Order { get; }
    public Guid VinylId { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }

    public List<Vinyl> Vinyls { get; }

    private OrderItem(Guid id, Guid orderId, Order order, Guid vinylId, int quantity, decimal unitPrice,
        List<Vinyl> vinyls)
    {
        Id = id;
        OrderId = orderId;
        Order = order;
        VinylId = vinylId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Vinyls = vinyls;
    }

    //todo Validation
    public static Result<OrderItem> Create(Guid id, Guid orderId, Order order, Guid vinylId,
        int quantity, decimal unitPrice, List<Vinyl> vinyls)
    {
        if (quantity <= 0)
        {
            return Result.Failure<OrderItem>("Quantity must be greater than zero.");
        }


        if (unitPrice < 0)
        {
            return Result.Failure<OrderItem>("Unit price must be zero or greater than zero.");
        }

        var orderItem = new OrderItem(id, orderId, order, vinylId, quantity, unitPrice, vinyls);

        return Result.Success(orderItem);
    }
}