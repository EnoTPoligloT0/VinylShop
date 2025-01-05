using CSharpFunctionalExtensions;
using VinylShop.Core.Enums;

namespace VinylShop.Core.Models;

public class Order
{
    private readonly List<OrderItem> _orderItems = [];
    private Order(Guid id, Guid userId,  DateTime orderDate, decimal totalAmount, Status status)
    {
        Id = id;
        UserId = userId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        Status = status;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public User User { get; }
    public DateTime OrderDate { get; }
    public decimal TotalAmount { get; }
    public Status Status { get; private set; }

    public IReadOnlyList<OrderItem>? OrderItems => _orderItems;
    
    //todo Validation
    public static Result<Order> Create(Guid id, Guid userId, DateTime orderDate, decimal totalAmount, Status status)
    {
        var order = new Order(id, userId, orderDate, totalAmount, status);

        return Result.Success(order);
    }

}