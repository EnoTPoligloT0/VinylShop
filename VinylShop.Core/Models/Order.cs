using CSharpFunctionalExtensions;
using VinylShop.Core.Enums;

namespace VinylShop.Core.Models;

public class Order
{
    private readonly List<OrderItem> _orderItems = [];
    private Order(Guid id, Guid userId,  DateTime orderDate, decimal totalAmount)
    {
        Id = id;
        UserId = userId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public User User { get; }
    public DateTime OrderDate { get; }
    public decimal TotalAmount { get; }
    public Status Status { get; private set; }

    public IReadOnlyList<OrderItem>? OrderItems => _orderItems;
    
    //todo Validation
    public static Result<Order> Create(Guid id, Guid userId, DateTime orderDate, decimal totalAmount)
    {
        var order = new Order(id, userId, orderDate, totalAmount);

        return Result.Success(order);
    }

    public void UpdateStatus(Status status)
    {
        Status = status;
    }
}